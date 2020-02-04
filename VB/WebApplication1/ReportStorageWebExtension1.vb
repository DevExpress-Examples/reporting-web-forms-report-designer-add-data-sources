Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.ServiceModel
Imports DevExpress.XtraReports.Web.Extensions

Namespace WebApplication1
	Public Class ReportStorageWebExtension1
		Inherits DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension

		Private ReadOnly reportDirectory As String
		Private Const FileExtension As String = ".repx"
		Public Sub New(ByVal reportDirectory As String)
			If Not Directory.Exists(reportDirectory) Then
				Directory.CreateDirectory(reportDirectory)
			End If
			Me.reportDirectory = reportDirectory
		End Sub
		Public Overrides Function CanSetData(ByVal url As String) As Boolean
			Return Not url.Contains("readonly")
		End Function
		Public Overrides Function IsValidUrl(ByVal url As String) As Boolean
			Return True
		End Function

		Public Overrides Function GetData(ByVal url As String) As Byte()
			Try
                If Directory.EnumerateFiles(reportDirectory).Select(AddressOf Path.GetFileNameWithoutExtension).Contains(url) Then
                    Return File.ReadAllBytes(Path.Combine(reportDirectory, url & FileExtension))
                End If
                Throw New FaultException(New FaultReason(String.Format("Could not find report '{0}'.", url)), New FaultCode("Server"), "GetData")
			Catch e1 As Exception
				Throw New FaultException(New FaultReason(String.Format("Could not find report '{0}'.", url)), New FaultCode("Server"), "GetData")
			End Try
		End Function

		Public Overrides Function GetUrls() As Dictionary(Of String, String)
            Return Directory.GetFiles(reportDirectory, "*" & FileExtension).Select(AddressOf Path.GetFileNameWithoutExtension).ToDictionary(Function(x) x)
        End Function

		Public Overrides Sub SetData(ByVal report As DevExpress.XtraReports.UI.XtraReport, ByVal url As String)
			report.SaveLayoutToXml(Path.Combine(reportDirectory, url & FileExtension))
		End Sub

		Public Overrides Function SetNewData(ByVal report As DevExpress.XtraReports.UI.XtraReport, ByVal defaultUrl As String) As String
			SetData(report, defaultUrl)
			Return defaultUrl
		End Function
	End Class
End Namespace
