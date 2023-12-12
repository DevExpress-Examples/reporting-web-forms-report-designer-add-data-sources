<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.Default" %>

<%@ Register assembly="DevExpress.XtraReports.v22.2.Web.WebForms, Version=22.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    
        <dx:ASPxReportDesigner ID="ASPxReportDesigner1" runat="server" Height="800px">
        </dx:ASPxReportDesigner>
    
    
    </div>
    </form>
</body>
</html>
