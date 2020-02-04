<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="WebApplication1.Default" %>

<%@ Register assembly="DevExpress.XtraReports.v19.2.Web.WebForms, Version=19.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
	<div>


		<dx:ASPxReportDesigner ID="ASPxReportDesigner1" runat="server">
		</dx:ASPxReportDesigner>


	</div>
	</form>
</body>
</html>