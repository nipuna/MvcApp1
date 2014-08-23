﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Content/CSS/Phones.css" rel="Stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="../../Content/CSS/StyleSheet.css" />
    <script type="text/javascript" src="../../Scripts/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../Scripts/screens/moreinfo.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div id="pnlCar" class="headerCar" >
            <img src='<%= ViewData["VehiclePhoto"] %>' />
        </div>
    </div>
    <div>
        <div style="padding-left: 10px; height: 40px; min-height: 40px; display: block">
            <a id="hypHome" href="http://www.bentleymotors.com">Bentley Home</a>
            <img src="../../images/greyArrow.gif" alt="" />
            <a id="hypSelectVehicle" href="/vehicles/index"><%= ViewData["VehicleName"] %></a>
            <img src="../../images/greyArrow.gif" alt="" />
            <a id="hypSelectPhone" href="/phones/index"><%= ViewData["phoneName"] %></a>
            <img src="../../images/greyArrow.gif" alt="" />
            <span id="lblBreadcrumb" class="smallText">Phone Details</span>
        </div>
        <div>
            <div style="float: left; width: 260px; min-width: 260px; display: block;">
                <div style="padding-left: 10px">
                    <img src="../../images/doubleArrow.gif" alt="" />
                    <a id="hypSelectAnother" class="grey" href="/phones/index">Select another phone</a>
                </div>
                <ul>
                    <li><a id="aMainFeatures" href="" >Main Features</a></li>
                    <li><a id="aAddFeatures" href="" >Additional Features</a></li>
                    <li><a id="aQuicKStartGuide" href="" >Quick Start Guide</a></li>
                    <li><a id="aPhoneSoftware" href="#" class="selected" >Phone Software</a></li>
                    <li><a id="hypUpgrade" href="#">Bluetooth System Upgrade</a></li>
                    <li><a id="hypPrint" href="#" target="_blank">Print Phone Details</a>--%></li>
                </ul>
                <div style="padding-top: 0px;">
                    <hr />
                </div>
                <div style="padding-top: 20px; padding-left: 24px">
                    <div style="border: solid 1px black; width: 139px">
                        <div style="text-align: center; padding-top: 20px">
                            <img id="imgPhone" src='<%= ViewData["phonephoto"] %>' style="height: 100px; width: 100px;
                                border-width: 0px;" /></div>
                        <div style="padding: 5px; text-align: center">
                            <span id="lblPhone" class="smallText">
                                <%= ViewData["phoneName"] %></span></div>
                        <div style="padding-top: 15px; background-color: #000000; color: #ffffff; font-size: 14px;">
                            <div style="padding-top:0px; text-align:center;padding-bottom: 5px;">
                                <%=ViewData["Conclusion"] %>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div style="float: left; display: block; width: 700px;">
                <div style="padding-bottom: 35px; padding-left: 20px;">
                    <div style="float: left">
                        <img id="imgTitle" src="../../images/english/titles/phoneDetails.gif" style="border-width: 0px;" /></div>
                    <div style="float: right">
                        <a id="hypPrint2" href="generatePDF.aspx" target="_blank">Print Phone Details</a></div>
                </div>
                <div style="width: 424px; margin-left: 20px; margin-top: 10px">
                    <div style="height: 22px">
                        <span id="litPhone" class="text9">Phone Selected:&nbsp;&nbsp;&nbsp;<b><%= ViewData["phoneName"] %></b></span>
                    </div>
                    <div style="height: 22px">
                        <label id="labSoftware" for="ddSoftware" class="text9">
                            Phone Software
                            <%= Html.DropDownList("SoftwareVersions", (SelectList)(ViewData["PhoneSoftwareVersions"]), new { style = "width:75%;border:1px solid #125294;letter-spacing:0px;font-size:11px;", onchange = "fillResults(this.selectedIndex)" })%>
                    </div>
                </div>
                <span id="spnTip" class="toolTip" onmouseout="hideTip()"></span>
                <div class="pInfo" id="contentDeatils" style="font-weight: normal; line-height: 1;
                    letter-spacing: 1px; float: left; width: 430px; padding: 10px 0px 0px 0px; color: #444444;
                    font-size: 11px">
                    The test data refers to the specific version of operating software installed on
                    the phone.<br />
                    <br />
                    You may be able to upgrade the version of the software on your phone as recommended
                    by your phone manufacturer.<br />
                    <br />
                    In order to find which version you have follow this procedure<br />
                    <br />
                    <br />
                    <div id="detailedResults" style="float: left; width: 100%;">
                        <div id="divHeading" style="padding: 20px 15px 2px 15px">
                            NO Quick Guides
                        </div>
                        <div>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 5%">
                                    </td>
                                    <td style="width: 90%">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--<script language="javascript">
            function showTip(oSel) {
                var theTip = document.getElementById("spnTip");
                theTip.style.top = window.event.clientY + 20;
                theTip.style.left = window.event.clientX;
                theTip.innerText = oSel.options[oSel.selectedIndex].text;
                theTip.style.visibility = "visible";
            }

            function hideTip() {
                document.getElementById("spnTip").style.visibility = "hidden";
            }
        </script>-->
    </div>
    <div style="clear: both; padding-top: 20px; padding-bottom: 20px; width: 960px; height: 30px;">
        <div id="pDisSpacing" class="disSpacing15">
            &nbsp;
        </div>
        <div id="pDisclaimer" class="disclaimer">
            Results shown are for the latest electronic control module available for this Model
            Year.<br />
            Please check with your <a style="font-size: 1em" href="http://www.bentleymotors.com/ownership/locate_a_dealer/">
                local dealer</a> if electronic control module updates are available.
        </div>
        <div style="float: right">
            <img src="../../images/poweredbynextgen.gif" alt="" /></div>
    </div>
    <script type="text/javascript">
        var quickGuidesResults = '<%= ViewData["quickGuidesResults"] %>';
        if (quickGuidesResults != "") {
            $("#detailedResults").empty();
            $(quickGuidesResults).appendTo("#detailedResults");
        }
        $("#aMainFeatures").attr("href", "");
        $("#aMainFeatures").attr("href", "/Phones/MoreInfo?testInstanceId=" + '<%= ViewData["testInstanceId"] %>');
        $("#aAddFeatures").attr("href", "");
        $("#aAddFeatures").attr("href", "/Phones/additionalFeautres?testInstanceId=" + '<%= ViewData["testInstanceId"] %>');
        $("#aQuicKStartGuide").attr("href", "");
        $("#aQuicKStartGuide").attr("href", "/Phones/quickStartGuide?testInstanceId=" + '<%= ViewData["testInstanceId"] %>');
        $("#aPhoneSoftware").attr("href", "");
        $("#aPhoneSoftware").attr("href", "/Phones/phoneSoftware?testInstanceId=" + '<%= ViewData["testInstanceId"] %>');
    </script>
</asp:Content>
