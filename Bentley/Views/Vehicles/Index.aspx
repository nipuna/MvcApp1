<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bentley Music Compatibility Center</title>
    <link href="../../Content/CSS/form.css" type="text/css" media="all" rel="stylesheet" />
    <link href="../../Content/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/js/ui.js"></script>
    <script src="../../Scripts/js/cufon.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../Scripts/screens/Vehicles.js"></script>
    <script type="text/javascript" >
        function formSubmit() {
            document.forms[0].submit();
        }
    </script>
</head>
<body>
    <% using (Html.BeginForm("Index", "Vehicles", FormMethod.Post,
     new { enctype = "multipart/form-data" }))
       { %>
            <div>
                <div id="pnlCar" class="header" style="background-image: url(../../images/header.jpg);">
                </div>
            </div>
            <div>
                <div style="height: 40px; min-height: 40px; display: block">
                    <div style="padding-left: 10px">
                        <a id="hypHome" href="http://www.bentleymotors.com">Bentley Home</a>
                        <img src="../../images/greyArrow.gif" alt="" />
                        <span id="lblBreadcrumb" class="smallText">Select vehicle model</span>
                    </div>
                </div>
                <div class="greyBox">
                    <div style="display: block; height: 50px;">
                        <img id="imgTitle" src="../../images/english/titles/music-player-compatiility-t.gif"
                            style="border-width: 0px;" />
                    </div>
                    <div style="display: block; height: 50px; width: 650px">
                        <div style="float: left; width: 270px; display: block">
                            <label id="labModel" for="ddVehicle" style="font-weight: bold">
                                Select Model</label>
                            <%= Html.DropDownList("Models", (SelectList)(ViewData["models"]), new { @class = "dropdown", onchange = "fillYears(this.selectedIndex)" })%>
                        </div>
                        <div style="float: left; width: 270px; display: block">
                            <label id="labYear" for="ddYear" style="font-weight: bold">
                                Select Model Year</label>
                            <%= Html.DropDownList("ModelYears", (SelectList)(ViewData["modelYears"]), new { @class = "dropdown" })%><br />
                        </div>
                    </div>
                    <div style="clear: both;">
                        <hr />
                    </div>
                    <div style="width: 650px; visibility: hidden; display: none;">
                        <div style="float: left; width: 270px;">
                            <label id="labVIN" for="tbVin" style="font-weight: bold">
                                Or Enter VIN (17 characters)</label>
                            <div style="clear: both">
                                <input name="tbVIN" type="text" maxlength="17" id="tbVIN" class="textBox" style="text-transform: uppercase;" /></div>
                            <div style="padding-top: 5px">
                                <a id="hypFindVIN" href="#" style="text-decoration: underline;">How do I find my VIN?</a></div>
                            <div id="errMsg" class="errMsg">
                            </div>
                        </div>
                        <div style="float: left; width: 310px;">
                            <label id="labSelectTelephone" for="rblSoftware" style="font-weight: bold">
                                Select telephone system software installed in vehicle</label><br />
                            <div>
                                <div style="float: left">
                                    <input value="Supplied" name="rblSoftware" type="radio" id="rbSupplied" class="val-radio"
                                        checked="checked" /></div>
                                <div style="float: left; padding-left: 5px">
                                    <label id="labSupplied" for="rbSupplied" class="babel">
                                        As Supplied from factory *</label></div>
                            </div>
                            <div style="clear: both">
                                <div style="float: left">
                                    <input value="Latest" name="rblSoftware" type="radio" id="rbLatest" class="val-radio" /></div>
                                <div style="float: left; padding-left: 5px">
                                    <label id="labLatest" for="rbLatest" class="babel">
                                        Latest dealer upgrade **</label></div>
                            </div>
                        </div>
                        <div style="float: right; height: 40px; min-height: 40px">
                            <a id="lbSearch" class="search" href="javascript:__doPostBack('lbSearch','')"><span
                                class="displace">Search</span></a>
                        </div>
                    </div>
                    <div style="float: right; height: 40px; min-height: 40px">
                        <img src="../../images/search.gif" onclick="formSubmit()" alt="Search" /> 
                    </div>
                    <div style="clear: both;">
                        <hr />
                    </div>
                </div>
            </div>
            <div style="clear: both; padding-top: 20px; padding-bottom: 20px; width: 960px; height: 30px;">
                <div id="pDisSpacing">
                    &nbsp;
                </div>
                <div style="float: right">
                    <img src="../../images/poweredbynextgen.gif" alt="" /></div>
            </div>
    <% } %>
</body>
</html>
