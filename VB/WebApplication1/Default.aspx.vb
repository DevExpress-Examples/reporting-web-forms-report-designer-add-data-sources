Imports DevExpress.DataAccess.ConnectionParameters
Imports System
Imports System.Collections.Generic

Namespace WebApplication1
	Partial Public Class [Default]
		Inherits System.Web.UI.Page

		Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

			Dim sqlDataSource As DevExpress.DataAccess.Sql.SqlDataSource = GenerateSqlDataSource()
			Dim objDataSource As DevExpress.DataAccess.ObjectBinding.ObjectDataSource = GenerateObjectDataSource()
			Dim efDataSource As DevExpress.DataAccess.EntityFramework.EFDataSource = GenerateEFDataSource()
			Dim excelDataSource As DevExpress.DataAccess.Excel.ExcelDataSource = GenerateExcelDataSource()
			Dim jsonDataSource As DevExpress.DataAccess.Json.JsonDataSource = GenerateJsonDataSource()

			ASPxReportDesigner1.DataSources.Add(sqlDataSource.Name, sqlDataSource)
			ASPxReportDesigner1.DataSources.Add(objDataSource.Name, objDataSource)
			ASPxReportDesigner1.DataSources.Add(efDataSource.Name, efDataSource)
			ASPxReportDesigner1.DataSources.Add(excelDataSource.Name, excelDataSource)
			ASPxReportDesigner1.DataSources.Add(jsonDataSource.Name, jsonDataSource)

			ASPxReportDesigner1.OpenReport("XtraReportTest")
		End Sub

		Private Function GenerateJsonDataSource() As DevExpress.DataAccess.Json.JsonDataSource
			Dim jsonDataSource As New DevExpress.DataAccess.Json.JsonDataSource()
			jsonDataSource.Name = "CustomJsonDataSource"
			Dim sourceUri As New Uri("~/App_Data/nwind.json", System.UriKind.Relative)
			jsonDataSource.JsonSource = New DevExpress.DataAccess.Json.UriJsonSource(sourceUri)
			jsonDataSource.Fill()
			Return jsonDataSource
		End Function

		Private Function GenerateExcelDataSource() As DevExpress.DataAccess.Excel.ExcelDataSource
			Dim excelDS As New DevExpress.DataAccess.Excel.ExcelDataSource()

			excelDS.FileName = Server.MapPath("App_Data/Categories.xlsx")
			excelDS.Name = "CustomExcelDataSource"

			Dim excelWorksheetSettings1 As New DevExpress.DataAccess.Excel.ExcelWorksheetSettings() With {.CellRange = Nothing, .WorksheetName = "Sheet"}
			Dim excelSourceOptions1 As New DevExpress.DataAccess.Excel.ExcelSourceOptions(excelWorksheetSettings1) With {.SkipEmptyRows = True, .SkipHiddenColumns = True, .SkipHiddenRows = True, .UseFirstRowAsHeader = True}

			excelDS.SourceOptions = excelSourceOptions1

			Dim fieldInfo1 As New DevExpress.DataAccess.Excel.FieldInfo() With {.Name = "CategoryID", .Type = GetType(Double)}
			Dim fieldInfo2 As New DevExpress.DataAccess.Excel.FieldInfo() With {.Name = "CategoryName", .Type = GetType(String)}
			Dim fieldInfo3 As New DevExpress.DataAccess.Excel.FieldInfo() With {.Name = "Description", .Type = GetType(String)}

			excelDS.Schema.AddRange(New DevExpress.DataAccess.Excel.FieldInfo() { fieldInfo1, fieldInfo2, fieldInfo3 })
			excelDS.RebuildResultSchema()
			Return excelDS
		End Function
		Private Function GenerateEFDataSource() As DevExpress.DataAccess.EntityFramework.EFDataSource
			Dim efds As New DevExpress.DataAccess.EntityFramework.EFDataSource()
			efds.Name = "CustomEntityFrameworkDataSource"
			efds.ConnectionParameters = New DevExpress.DataAccess.EntityFramework.EFConnectionParameters()
			efds.ConnectionParameters.ConnectionStringName = "NorthwindEntitiesConnString"
			efds.ConnectionParameters.Source = GetType(Models.NorthwindEntities)
			Return efds
		End Function
		Private Function GenerateSqlDataSource() As DevExpress.DataAccess.Sql.SqlDataSource
			Dim ds As New DevExpress.DataAccess.Sql.SqlDataSource("localhost_Northwind_Connection")
			ds.Name = "CustomSqlDataSource"
			' Create an SQL query to access the Products table.
			Dim query As New DevExpress.DataAccess.Sql.CustomSqlQuery()
			query.Name = "customQuery1"
			query.Sql = "SELECT * FROM Products"

			ds.Queries.Add(query)
			ds.RebuildResultSchema()
			Return ds
		End Function
		Private Function GenerateObjectDataSource() As DevExpress.DataAccess.ObjectBinding.ObjectDataSource
			Dim objds As New DevExpress.DataAccess.ObjectBinding.ObjectDataSource()
			objds.BeginInit()
			objds.Name = "CustomObjectDataSource"
			objds.DataSource = GetType(ItemList)
			objds.Constructor = New DevExpress.DataAccess.ObjectBinding.ObjectConstructorInfo()
			objds.EndInit()
			Return objds
		End Function
	End Class
	#Region "Object Data Source"
	Public Class ItemList
		Inherits List(Of Item)

		Public Sub New()
			For i As Integer = 0 To 9
				Add(New Item() With {.Name = i.ToString()})
			Next i
		End Sub
	End Class
	Public Class Item
		Public Property Name() As String
	End Class
	#End Region
End Namespace