<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.String>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Content/CSS/System.css" rel="Stylesheet" type="text/css" />
    <link href="../../Content/CSS/Phones.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" src="../../Scripts/Site/Refereshpage.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div id="vehicleHeader">
            <img id="imgPhoto" src="/images/VehicleHeader.jpg" />
        </div>
        <div id="content" style="float: left;">
            <div id="" style="padding: 5px 5px 15px 5px">
                <div style="font-size: large;float: left;width: 150px"> </div>
                <div style="float: right;">
                    <span style="width: 150px; float: right; font-size: 11px;"><a href="/Vehicles/Index" id="" class="anch">
                        Select Region </a></span>
                </div>
            </div>
            <div id="contentsActual" style="float: left;margin-top: 10px">
                <div style="float: left;height: 200px; width: 700px">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 40%;font-size:medium;">
                                    <%= ViewData["error"] %>
                                    Please wait a few moments site is being updated to the latest version. This page will automatically refresh.
                            </td>
                    </table>
                </div>
            </div>
        </div>
        <div id="phoneFooter"  style="background-color:#fff; float:left;width:100%;padding-top:30px">
            <div style="float: left; font-size: 11px;padding:0px 0px 2px 5px">
            </div>
            <div style="float: right;padding:0px 5px 2px 2px">
                <img src="/images/poweredbynextgen.gif" alt="" />
            </div>
        </div>
</asp:Content>
