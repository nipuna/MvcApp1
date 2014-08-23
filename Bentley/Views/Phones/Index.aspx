<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Content/CSS/form.css" type="text/css" media="all" rel="stylesheet" />
    <link href="../../Content/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" href="../../images/favicon.ico" />
    <script type="text/javascript" src="../../Scripts/js/ui.js"></script>
    <script src="../../Scripts/js/cufon.js" type="text/javascript"></script>
    <script src="../../Scripts/js/dw_event.js" type="text/javascript"></script>
    <script src="../../Scripts/js/dw_scroll.js" type="text/javascript"></script>
    <script src="../../Scripts/js/dw_scrollbar.js" type="text/javascript"></script>
    <script src="../../Scripts/js/scroll_controls.js" type="text/javascript"></script>
    <script src="../../Scripts/js/phone.js" type="text/javascript"></script>

    <script type="text/javascript">
        var currentDiv = "divPhone0";
        function scrollPhone(p) {
            wndo.applycssOnDropDown(p * step, 0, 300);
            wndo.scroll()
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div id="pnlCar" class="headerCar" >
            <img src='<%= ViewData["VehiclePhoto"] %>' />
        </div>
    </div>
    <div>
        <div class="hilite">
            <img src="../../images/hilite.gif" alt="" /></div>
        <div style="padding-left: 10px; height: 40px; min-height: 40px; display: block">
            <a id="hypHome" href="http://www.bentleymotors.com">Bentley Home</a>
            <img src="../../images/greyArrow.gif" alt="" />
            <a id="hypSelectVehicle" href="/vehicles/index"><%= ViewData["VehicleName"] %></a>
            <img src="../../images/greyArrow.gif" alt="" />
            <span id="lblBreadcrumb" class="smallText">Select phone</span>
        </div>
        <div style="clear: both; float: left; width: 255px; min-width: 255px;">
            <div style="padding-left: 10px">
                <img src="../../images/doubleArrow.gif" alt="" />
                <%= Html.ActionLink("Select Another Vehicle", "Index", new { controller = "Vehicles" }, new { @class = "anch", id="hypVehicle"  })%>
            </div>
        </div>
        <div style="float: left; width: 700px;">
            <div style="padding-bottom: 35px">
                <div style="float: left;">
                    <img id="imgTitle" src="../../images/english/titles/selectdevice.gif" style="border-width: 0px;" /></div>
                <div style="float: right; padding-top: 4px">
                    <%--<a id="hypPrint" href="#"
                        target="_blank">Print Details</a>--%></div>
            </div>
            <span id="lblCarNameYear" class="text9">Model Selected: <b><%= ViewData["VehicleName"] %></b></span><br />
            <span id="lblSoftwareVersion" class="text9">Vehicle Software: <b>0020</b></span>
            <hr />
            <div style="width: 700px; height: 56px; min-height: 56px; display: block; clear: both;">
                <div style="display: block; clear: both;">
                    <div style="clear: both; float: left">
                        <label id="labBrand" for="ddBrand" class="dd">
                            Brand</label><br />
                            <%= Html.DropDownList("brands", (SelectList)(ViewData["brands"]), new { @class = "dropdown", onchange = "fillModels(this.selectedIndex)", id = "ddBrand" })%>
                    </div>
                    <div style="float: left; padding-left: 50px">
                        <label id="labModel" for="ddModel" class="dd">
                            Model</label><br />
                            <%= Html.DropDownList("ddPhones", (SelectList)(ViewData["models"]), new { @class = "dropdown", onchange = "selectPhone(this.selectedIndex)", id = "ddPhones" })%>
                    </div>
                </div>
            </div>
            <div id="wn" style="clear: both; margin-top: 24px;">
                <div id="lyr1">
                    <div id="phoneContent">
                        <table id='t1' border='0' cellpadding='0' cellspacing='0' style='border-collapse: collapse'>
                            <tbody>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <script type="text/javascript">
                    makePhoneDivs(0);
                </script>
            </div>
            <div id="scrollbar">
                <div id="left">
                    <a class="click_left_by_130_300" href="#">
                        <img src="../../images/left.gif" alt="" border="0" /></a></div>
                <div id="track">
                    <div id="dragBar">
                    </div>
                </div>
                <div id="right">
                    <a class="click_right_by_130_300" href="#">
                        <img src="../../images/right.gif" alt="" border="0" /></a></div>
            </div>
            <div style="clear: both">
                <hr />
                <div class="roundedBox">
                    <div id="dvCompat">
                    </div>
                    <table cellpadding="5" cellspacing="1" width="710" class="rTable" style="font-size:14px;text-align:center">
                        <tr>
                            <td id="tdFeature1" style="font-size:14px;text-align:center" >
                            </td>
                            <td id="tdFeature2" style="font-size:14px;text-align:center" >
                            </td>
                            <td id="tdFeature3" style="font-size:14px;text-align:center" >
                            </td>
                            <%--<td align="center" id="tdFeature4">
                            </td>
                            <td align="center" id="tdFeature5">
                            </td>--%>
                        </tr>
                        <tr>
                            <td id="tdResult1" style="font-size:14px;text-align:center" >
                            </td>
                            <td id="tdResult2" style="font-size:14px;text-align:center" >
                            </td>
                            <td id="tdResult3" style="font-size:14px;text-align:center" >
                            </td>
                            <%--<td align="center" id="tdResult4">
                            </td>
                            <td align="center" id="tdResult5">
                            </td>--%>
                        </tr>
                    </table>
                    <div style="text-align: center; padding-top: 7px">
                        Bluetooth Profile:&nbsp;<span id="lblBluetoothProfile" style="font-weight: bold;"></span></div>
                </div>
                <hr />
                <div style="clear: both; display: block; height: 80px; padding-top: 10px">
                    <div style="float: right;">
                        <a id="aMoreInfo" class="moreinfo" href="javascript:__doPostBack('lbMoreInfo','')"><img src="../../images/english/buttons/moreInfo.gif" /></a>
                    </div>
                    <div id="divComm">
                        <div id="finalComment">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <input name="phoneScrollPos" type="hidden" id="phoneScrollPos" value="0" />
    </div>
    <div style="clear: both; padding-top: 20px; padding-bottom: 20px; width: 960px; height: 30px;">
        <div id="pDisSpacing" class="disSpacing130">
            &nbsp;
        </div>
        <div id="pDisclaimer" class="disclaimer">
            Results shown are for the electronic control module as supplied from the factory
            for this Model Year.<br />
            Please check with your <a style="font-size: 1em" href="http://www.bentleymotors.com/ownership/locate_a_dealer/">
                local dealer</a> if electronic control module updates are available.
        </div>
        <div style="float: right">
            <img src="../../images/poweredbynextgen.gif" alt="" /></div>
    </div>
    
    <script type="text/javascript">
        var allResults = '<%=ViewData["topFeatureResults"] %>';
        showResults()
    </script>
</asp:Content>
