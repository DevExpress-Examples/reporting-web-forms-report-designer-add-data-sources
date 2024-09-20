using System;
using System.Collections.Generic;

namespace WebApplication1 {
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource = GenerateSqlDataSource();
            DevExpress.DataAccess.ObjectBinding.ObjectDataSource objDataSource = GenerateObjectDataSource();
            DevExpress.DataAccess.EntityFramework.EFDataSource efDataSource = GenerateEFDataSource();
            DevExpress.DataAccess.Excel.ExcelDataSource excelDataSource = GenerateExcelDataSource();
            DevExpress.DataAccess.Json.JsonDataSource jsonDataSource = GenerateJsonDataSource();

            ASPxReportDesigner1.DataSources.Add(sqlDataSource.Name, sqlDataSource);
            ASPxReportDesigner1.DataSources.Add(objDataSource.Name, objDataSource);
            ASPxReportDesigner1.DataSources.Add(efDataSource.Name, efDataSource);
            ASPxReportDesigner1.DataSources.Add(excelDataSource.Name, excelDataSource);
            ASPxReportDesigner1.DataSources.Add(jsonDataSource.Name, jsonDataSource);

            ASPxReportDesigner1.OpenReport("XtraReportTest");
        }

        private DevExpress.DataAccess.Json.JsonDataSource GenerateJsonDataSource()
        {
            DevExpress.DataAccess.Json.JsonDataSource jsonDataSource = new DevExpress.DataAccess.Json.JsonDataSource();
            jsonDataSource.Name = "CustomJsonDataSource";
            var uri = new System.Uri("~/App_Data/nwind.json", System.UriKind.Relative);
            jsonDataSource.JsonSource = new DevExpress.DataAccess.Json.UriJsonSource(uri);
            jsonDataSource.Fill();
            return jsonDataSource;
        }

        private DevExpress.DataAccess.Excel.ExcelDataSource GenerateExcelDataSource()
        {
            DevExpress.DataAccess.Excel.ExcelDataSource excelDS = new DevExpress.DataAccess.Excel.ExcelDataSource();

            excelDS.FileName = Server.MapPath("App_Data/Categories.xlsx");
            excelDS.Name = "CustomExcelDataSource";

            DevExpress.DataAccess.Excel.ExcelWorksheetSettings excelWorksheetSettings1 = new DevExpress.DataAccess.Excel.ExcelWorksheetSettings() { CellRange = null, WorksheetName = "Sheet" };
            DevExpress.DataAccess.Excel.ExcelSourceOptions excelSourceOptions1 = new DevExpress.DataAccess.Excel.ExcelSourceOptions(excelWorksheetSettings1) { SkipEmptyRows = true, SkipHiddenColumns = true, SkipHiddenRows = true, UseFirstRowAsHeader = true };

            excelDS.SourceOptions = excelSourceOptions1;

            DevExpress.DataAccess.Excel.FieldInfo fieldInfo1 = new DevExpress.DataAccess.Excel.FieldInfo() { Name = "CategoryID", Type = typeof(double) };
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo2 = new DevExpress.DataAccess.Excel.FieldInfo() { Name = "CategoryName", Type = typeof(string) };
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo3 = new DevExpress.DataAccess.Excel.FieldInfo() { Name = "Description", Type = typeof(string) };

            excelDS.Schema.AddRange(new DevExpress.DataAccess.Excel.FieldInfo[] {
                        fieldInfo1,
                        fieldInfo2,
                        fieldInfo3
            });
            excelDS.RebuildResultSchema();
            return excelDS;
        }
        private DevExpress.DataAccess.EntityFramework.EFDataSource GenerateEFDataSource()
        {
            DevExpress.DataAccess.EntityFramework.EFDataSource efds = new DevExpress.DataAccess.EntityFramework.EFDataSource();
            efds.Name = "CustomEntityFrameworkDataSource";
            efds.ConnectionParameters = new DevExpress.DataAccess.EntityFramework.EFConnectionParameters();
            efds.ConnectionParameters.ConnectionStringName = "NorthwindEntitiesConnString";
            efds.ConnectionParameters.Source = typeof(Models.NorthwindEntities);
            return efds;
        }
        private DevExpress.DataAccess.Sql.SqlDataSource GenerateSqlDataSource()
        {
            DevExpress.DataAccess.Sql.SqlDataSource ds =
                new DevExpress.DataAccess.Sql.SqlDataSource("localhost_Northwind_Connection");
            ds.Name = "CustomSqlDataSource";
            // Create an SQL query to access the Products table.
            DevExpress.DataAccess.Sql.CustomSqlQuery query = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            query.Name = "customQuery1";
            query.Sql = "SELECT * FROM Products";

            ds.Queries.Add(query);
            ds.RebuildResultSchema();
            return ds;
        }
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource GenerateObjectDataSource()
        {
            DevExpress.DataAccess.ObjectBinding.ObjectDataSource objds = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource();
            objds.BeginInit();
            objds.Name = "CustomObjectDataSource";
            objds.DataSource = typeof(ItemList);
            objds.Constructor = new DevExpress.DataAccess.ObjectBinding.ObjectConstructorInfo();
            objds.EndInit();
            return objds;
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
}