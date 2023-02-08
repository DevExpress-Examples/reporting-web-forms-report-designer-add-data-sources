## Reporting for Web Forms - How to Register Data Sources for Use in the Web Report Designer


This example demonstrates how to create data sources at runtime and add them to the list of the data sources available in the Web Report Designer.

The following data source types are included in this example:
* [SQL Data Source](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.Sql.SqlDataSource)
* [Object Data Source](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.ObjectBinding.ObjectDataSource)
* [Entity Framework Data Source](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.EntityFramework.EFDataSource)
* [Excel Data Source](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.Excel.ExcelDataSource)
* [JSON Data Source](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.Json.JsonDataSource)

The project uses the [Northwind database](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/northwind-pubs) at the local SQL server. 

The JSON data source uses the open source [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json) library to retrieve JSON data at runtime.

## Files to Review
* [Default.aspx](./CS/WebApplication1/Default.aspx) (VB: [Default.aspx](./VB/WebApplication1/Default.aspx))
* [Default.aspx.cs](./CS/WebApplication1/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebApplication1/Default.aspx.vb))

## Documentation

- [Data Sources](https://docs.devexpress.com/CoreLibraries/403632/devexpress-data-library/data-sources)

## More Examples

- [How to Create the Data Access Library Data Sources at Runtime](https://github.com/DevExpress-Examples/how-to-create-data-access-library-data-sources-at-runtime-t424210)