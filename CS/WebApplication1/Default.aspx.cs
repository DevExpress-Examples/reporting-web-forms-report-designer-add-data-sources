using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
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

            MsSqlConnectionParameters connectionParameters = new MsSqlConnectionParameters(".", "Northwind", null, null, MsSqlAuthorizationType.Windows);
            DevExpress.DataAccess.Sql.SqlDataSource ds = new DevExpress.DataAccess.Sql.SqlDataSource(connectionParameters);
            GenerateSqlDataSource(ds);

            DevExpress.DataAccess.ObjectBinding.ObjectDataSource objds = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource();
            GenerateObjectDataSource(objds);

            DevExpress.DataAccess.EntityFramework.EFDataSource efds = new DevExpress.DataAccess.EntityFramework.EFDataSource();
            GenerateEFDataSource(efds);

            ASPxReportDesigner1.DataSources.Add("CustomSqlDS", ds);
            ASPxReportDesigner1.DataSources.Add("CustomObjDs", objds);
            ASPxReportDesigner1.DataSources.Add("CustomEfDs", efds);



            ASPxReportDesigner1.OpenReport(new DevExpress.XtraReports.UI.XtraReport());
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
            objds.DataSourceType = typeof(ItemList);
            objds.Constructor = new DevExpress.DataAccess.ObjectBinding.ObjectConstructorInfo();

            objds.EndInit();
        }
    }


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
}