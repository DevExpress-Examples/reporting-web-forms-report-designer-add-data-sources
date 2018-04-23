using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            MsSqlConnectionParameters connectionParameters = new MsSqlConnectionParameters(".", "NWind", null, null, MsSqlAuthorizationType.Windows);
            DevExpress.DataAccess.Sql.SqlDataSource ds = new DevExpress.DataAccess.Sql.SqlDataSource(connectionParameters);
            GenerateSqlDataSource(ds);

            DevExpress.DataAccess.ObjectBinding.ObjectDataSource objds = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource();
            GenerateObjectDataSource(objds);

            DevExpress.DataAccess.EntityFramework.EFDataSource efds = new DevExpress.DataAccess.EntityFramework.EFDataSource();
            GenerateEFDataSource(efds);

            DevExpress.DataAccess.Excel.ExcelDataSource exds = new DevExpress.DataAccess.Excel.ExcelDataSource();
            GenerateExcelDataSource(exds);
                
            ASPxReportDesigner1.DataSources.Add("CustomSqlDS", ds);
            ASPxReportDesigner1.DataSources.Add("CustomObjDs", objds);
            ASPxReportDesigner1.DataSources.Add("CustomEfDs", efds);
            ASPxReportDesigner1.DataSources.Add("CustomExcelDs", exds);

            ASPxReportDesigner1.OpenReport(new DevExpress.XtraReports.UI.XtraReport());
        }

        private void GenerateExcelDataSource(DevExpress.DataAccess.Excel.ExcelDataSource exds)
        {
            exds.FileName = Server.MapPath("bin") + "\\Categories.xlsx";
            exds.Name = "excelDataSource1";

            DevExpress.DataAccess.Excel.ExcelWorksheetSettings excelWorksheetSettings1 = new DevExpress.DataAccess.Excel.ExcelWorksheetSettings() { CellRange = null, WorksheetName = "Sheet" };
            DevExpress.DataAccess.Excel.ExcelSourceOptions excelSourceOptions1 = new DevExpress.DataAccess.Excel.ExcelSourceOptions(excelWorksheetSettings1) { SkipEmptyRows = true, SkipHiddenColumns =true, SkipHiddenRows = true, UseFirstRowAsHeader = true };

            exds.SourceOptions = excelSourceOptions1;

            DevExpress.DataAccess.Excel.FieldInfo fieldInfo1 = new DevExpress.DataAccess.Excel.FieldInfo() { Name = "CategoryID", Type = typeof(double) };
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo2 = new DevExpress.DataAccess.Excel.FieldInfo() { Name = "CategoryName", Type = typeof(string)};
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo3 = new DevExpress.DataAccess.Excel.FieldInfo() { Name = "Description", Type = typeof(string)};

            exds.Schema.AddRange(new DevExpress.DataAccess.Excel.FieldInfo[] {
                        fieldInfo1,
                        fieldInfo2,
                        fieldInfo3
            });
            exds.RebuildResultSchema();
        }
        private void GenerateEFDataSource(DevExpress.DataAccess.EntityFramework.EFDataSource efds)
        {
            ((System.ComponentModel.ISupportInitialize)(efds)).BeginInit();
            efds.Name = "efDataSource1";
            efds.ConnectionParameters = ConfigureEfConnectionParameters();
            ((System.ComponentModel.ISupportInitialize)(efds)).EndInit();
        }
        private DevExpress.DataAccess.EntityFramework.EFConnectionParameters ConfigureEfConnectionParameters()
        {
            DevExpress.DataAccess.EntityFramework.EFConnectionParameters efConnParam = new DevExpress.DataAccess.EntityFramework.EFConnectionParameters();
            efConnParam.ConnectionString = EFConnectionParams.Connection;
            efConnParam.ConnectionStringName = "";
            efConnParam.Source = EFConnectionParams.SourceType;
            return efConnParam;
        }
        private static void GenerateSqlDataSource(DevExpress.DataAccess.Sql.SqlDataSource ds)
        {
            // Create an SQL query to access the Products table.
            CustomSqlQuery query = new CustomSqlQuery();
            query.Name = "customQuery1";
            query.Sql = "SELECT * FROM Products";

            ds.Queries.Add(query);
            ds.RebuildResultSchema();
        }
        private static void GenerateObjectDataSource(DevExpress.DataAccess.ObjectBinding.ObjectDataSource objds)
        {
            objds.BeginInit();

            objds.Name = "ObjSource";
            objds.DataSource = typeof(ItemList);
            objds.Constructor = new DevExpress.DataAccess.ObjectBinding.ObjectConstructorInfo();

            objds.EndInit();
        }
    }
    #region Object Data Source
    public class ItemList : List<Item>
    {
        public ItemList()
        {
            for (int i = 0; i < 10; i++)
            {
                Add(new Item() { Name = i.ToString() });
            }
        }
    }
    public class Item
    {
        public string Name { get; set; }
    }
    #endregion
    #region EF Data Source 
    public static class EFConnectionParams
    {
        public static string Connection
        {
            get
            {
                return "Data Source=localhost;Initial Catalog=Northwind;Persist Security Info=True;User ID=sa;Password=dx;MultipleActiveResultSets=True;Application Name=EntityFramework";
            }
        }

        public static Type SourceType { get { return typeof(NorthwindEntities); } }
    }
    #endregion
}