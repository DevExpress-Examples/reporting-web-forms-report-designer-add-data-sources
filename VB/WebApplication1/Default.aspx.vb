Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
Imports System
Imports System.Collections.Generic
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace WebApplication1

    Public Partial Class [Default]
        Inherits Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Dim connectionParameters As MsSqlConnectionParameters = New MsSqlConnectionParameters(".", "Northwind", Nothing, Nothing, MsSqlAuthorizationType.Windows)
            Dim ds As DevExpress.DataAccess.Sql.SqlDataSource = New DevExpress.DataAccess.Sql.SqlDataSource(connectionParameters)
            GenerateSqlDataSource(ds)
            Dim objds As DevExpress.DataAccess.ObjectBinding.ObjectDataSource = New DevExpress.DataAccess.ObjectBinding.ObjectDataSource()
            GenerateObjectDataSource(objds)
            Dim efds As DevExpress.DataAccess.EntityFramework.EFDataSource = New DevExpress.DataAccess.EntityFramework.EFDataSource()
            GenerateEFDataSource(efds)
            ASPxReportDesigner1.DataSources.Add("CustomSqlDS", ds)
            ASPxReportDesigner1.DataSources.Add("CustomObjDs", objds)
            ASPxReportDesigner1.DataSources.Add("CustomEfDs", efds)
            ASPxReportDesigner1.OpenReport(New DevExpress.XtraReports.UI.XtraReport())
        End Sub

        Private Sub GenerateEFDataSource(ByVal efds As DevExpress.DataAccess.EntityFramework.EFDataSource)
            CType(efds, System.ComponentModel.ISupportInitialize).BeginInit()
            efds.Name = "efDataSource1"
            efds.ConnectionParameters = ConfigureEfConnectionParameters()
            CType(efds, System.ComponentModel.ISupportInitialize).EndInit()
        End Sub

        Private Function ConfigureEfConnectionParameters() As DevExpress.DataAccess.EntityFramework.EFConnectionParameters
            Dim efConnParam As DevExpress.DataAccess.EntityFramework.EFConnectionParameters = New DevExpress.DataAccess.EntityFramework.EFConnectionParameters()
            efConnParam.ConnectionString = Connection
            efConnParam.ConnectionStringName = ""
            efConnParam.Source = SourceType
            Return efConnParam
        End Function

        Private Shared Sub GenerateSqlDataSource(ByVal ds As DevExpress.DataAccess.Sql.SqlDataSource)
            ' Create an SQL query to access the Products table.
            Dim query As CustomSqlQuery = New CustomSqlQuery()
            query.Name = "customQuery1"
            query.Sql = "SELECT * FROM Products"
            ds.Queries.Add(query)
            ds.RebuildResultSchema()
        End Sub

        Private Shared Sub GenerateObjectDataSource(ByVal objds As DevExpress.DataAccess.ObjectBinding.ObjectDataSource)
            objds.BeginInit()
            objds.Name = "ObjSource"
            objds.DataSourceType = GetType(ItemList)
            objds.Constructor = New DevExpress.DataAccess.ObjectBinding.ObjectConstructorInfo()
            objds.EndInit()
        End Sub
    End Class

    Public Class ItemList
        Inherits List(Of Item)

        Public Sub New()
            For i As Integer = 0 To 10 - 1
                Add(New Item() With {.Name = i.ToString()})
            Next
        End Sub
    End Class

    Public Class Item

        Public Property Name As String
    End Class

    Public Module EFConnectionParams

        Public ReadOnly Property Connection As String
            Get
                Return "Data Source=localhost;Initial Catalog=Northwind;Persist Security Info=True;User ID=sa;Password=dx;MultipleActiveResultSets=True;Application Name=EntityFramework"
            End Get
        End Property

        Public ReadOnly Property SourceType As Type
            Get
                Return GetType(NorthwindEntities)
            End Get
        End Property
    End Module
End Namespace
