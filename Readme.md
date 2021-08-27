<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128602805/19.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T271722)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:
* [Default.aspx](./CS/WebApplication1/Default.aspx) (VB: [Default.aspx](./VB/WebApplication1/Default.aspx))
* [Default.aspx.cs](./CS/WebApplication1/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebApplication1/Default.aspx.vb))
<!-- default file list end -->
# How to Provide Data Sources for the Web End-User Report Designer


This example demonstrates how to create the data sources at runtime and add them to the list of the data sources available in the Web Report Designer.

The following data source types are included in this example:
* [SQL Data Source](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.Sql.SqlDataSource)
* [Object Data Source](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.ObjectBinding.ObjectDataSource)
* [Entity Framework Data Source](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.EntityFramework.EFDataSource)
* [Excel Data Source](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.Excel.ExcelDataSource)
* [JSON Data Source](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.Json.JsonDataSource)

The project uses the [Northwind database](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/northwind-pubs) at the local SQL server. 

The JSON data source uses the open source [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json) library to provide JSON data at runtime.

**See also:**

- [How to create Data Access Library data sources at runtime](https://supportcenter.devexpress.com/internal/ticket/details/T423404)
