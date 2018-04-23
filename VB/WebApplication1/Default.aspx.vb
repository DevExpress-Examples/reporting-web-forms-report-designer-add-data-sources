Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
Imports DevExpress.XtraReports.UI
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace WebApplication1
    Partial Public Class [Default]
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

            Dim connectionParameters As New MsSqlConnectionParameters(".", "NWind", Nothing, Nothing, MsSqlAuthorizationType.Windows)
            Dim ds As New DevExpress.DataAccess.Sql.SqlDataSource(connectionParameters)
            GenerateSqlDataSource(ds)

            Dim objds As New DevExpress.DataAccess.ObjectBinding.ObjectDataSource()
            GenerateObjectDataSource(objds)

            Dim efds As New DevExpress.DataAccess.EntityFramework.EFDataSource()
            GenerateEFDataSource(efds)

            Dim exds As New DevExpress.DataAccess.Excel.ExcelDataSource()
            GenerateExcelDataSource(exds)

            ASPxReportDesigner1.DataSources.Add("CustomSqlDS", ds)
            ASPxReportDesigner1.DataSources.Add("CustomObjDs", objds)
            ASPxReportDesigner1.DataSources.Add("CustomEfDs", efds)
            ASPxReportDesigner1.DataSources.Add("CustomExcelDs", exds)

            ASPxReportDesigner1.OpenReport(New DevExpress.XtraReports.UI.XtraReport())
        End Sub

        Private Sub GenerateExcelDataSource(ByVal exds As DevExpress.DataAccess.Excel.ExcelDataSource)
            exds.FileName = Server.MapPath("bin") & "\Categories.xlsx"
            exds.Name = "excelDataSource1"

            Dim excelWorksheetSettings1 As New DevExpress.DataAccess.Excel.ExcelWorksheetSettings() With {.CellRange = Nothing, .WorksheetName = "Sheet"}
            Dim excelSourceOptions1 As New DevExpress.DataAccess.Excel.ExcelSourceOptions(excelWorksheetSettings1) With {.SkipEmptyRows = True, .SkipHiddenColumns =True, .SkipHiddenRows = True, .UseFirstRowAsHeader = True}

            exds.SourceOptions = excelSourceOptions1

            Dim fieldInfo1 As New DevExpress.DataAccess.Excel.FieldInfo() With {.Name = "CategoryID", .Type = GetType(Double)}
            Dim fieldInfo2 As New DevExpress.DataAccess.Excel.FieldInfo() With {.Name = "CategoryName", .Type = GetType(String)}
            Dim fieldInfo3 As New DevExpress.DataAccess.Excel.FieldInfo() With {.Name = "Description", .Type = GetType(String)}

            exds.Schema.AddRange(New DevExpress.DataAccess.Excel.FieldInfo() { fieldInfo1, fieldInfo2, fieldInfo3 })
            exds.RebuildResultSchema()
        End Sub
        Private Sub GenerateEFDataSource(ByVal efds As DevExpress.DataAccess.EntityFramework.EFDataSource)
            DirectCast(efds, System.ComponentModel.ISupportInitialize).BeginInit()
            efds.Name = "efDataSource1"
            efds.ConnectionParameters = ConfigureEfConnectionParameters()
            DirectCast(efds, System.ComponentModel.ISupportInitialize).EndInit()
        End Sub
        Private Function ConfigureEfConnectionParameters() As DevExpress.DataAccess.EntityFramework.EFConnectionParameters
            Dim efConnParam As New DevExpress.DataAccess.EntityFramework.EFConnectionParameters()
            efConnParam.ConnectionString = EFConnectionParams.Connection
            efConnParam.ConnectionStringName = ""
            efConnParam.Source = EFConnectionParams.SourceType
            Return efConnParam
        End Function
        Private Shared Sub GenerateSqlDataSource(ByVal ds As DevExpress.DataAccess.Sql.SqlDataSource)
            ' Create an SQL query to access the Products table.
            Dim query As New CustomSqlQuery()
            query.Name = "customQuery1"
            query.Sql = "SELECT * FROM Products"

            ds.Queries.Add(query)
            ds.RebuildResultSchema()
        End Sub
        Private Shared Sub GenerateObjectDataSource(ByVal objds As DevExpress.DataAccess.ObjectBinding.ObjectDataSource)
            objds.BeginInit()

            objds.Name = "ObjSource"
            objds.DataSource = GetType(ItemList)
            objds.Constructor = New DevExpress.DataAccess.ObjectBinding.ObjectConstructorInfo()

            objds.EndInit()
        End Sub
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
    #Region "EF Data Source "
    Public NotInheritable Class EFConnectionParams

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property Connection() As String
            Get
                Return "Data Source=localhost;Initial Catalog=Northwind;Persist Security Info=True;User ID=sa;Password=dx;MultipleActiveResultSets=True;Application Name=EntityFramework"
            End Get
        End Property

        Public Shared ReadOnly Property SourceType() As Type
            Get
                Return GetType(NorthwindEntities)
            End Get
        End Property
    End Class
    #End Region
End Namespace