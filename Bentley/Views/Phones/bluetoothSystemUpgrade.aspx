<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Content/CSS/Phones.css" rel="Stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="../../Content/CSS/StyleSheet.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    
    <% using (Html.BeginForm("Index", "Phones", FormMethod.Post,
         new { enctype = "multipart/form-data" }))
       { %>
    <div id="phoneHeader" style="background-color: #ffffff;">
        <div class="title">
            <div>
                <img id="imgPhoto" src="/images/VehicleHeader.jpg" />
            </div>
            <div style="float: right;">
                <span style="width: auto; float: right; font-size: 11px;">Vehicle Selected:<%= ViewData["VehicleName"] %></span>
            </div>
        </div>
    </div>
    <div id="content" style="border-top: 2px solid #000000; margin-left: auto; margin-right: auto;
        width: 726px;">
        <div id="contentmain" style="width: 70%; float: left;">
            <div style="width: 424px; margin: 15px 15px 15px 15px">
                <div style="font-size: 11px; color: #FFFFFF; background-color: #696969; border: 0px;height: 20px; -moz-border-radius: 2px; -webkit-border-radius: 4px;">
                    <span><%= ViewData["phoneName"] %>
                    </span>
                </div>
                <div style="width:60%;float:right;font-size:small">
                     Software <%= Html.DropDownList("brands", (SelectList)(ViewData["phoneSystems"]), new { style = "width:80%;border:1px solid #125294;", onchange = "fillModels(this.selectedIndex)" })%>
                </div>
                <div id="contentDeatils" style="width:100%;margin-top:30px;border:2px solid #979797;-moz-border-radius:8px;-webkit-border-radius:10px;" >
                    <table id="overallCompatibility" style="width:100%;">
                        <tr style="border-bottom:3px solid #979797">
                            <td colspan="3">
                            
                            </td>
                        </tr>
                        <tr>
                            <td>
                                
                            </td>
                            <td>
                            
                            </td>
                            <td>
                            
                            </td>
                        </tr>
                        <tr>
                            <td>
                            
                            </td>
                            <td>
                            
                            </td>
                            <td>
                            
                            </td>
                        </tr>
                        <tr>
                            <td>
                            
                            </td>
                            <td>
                            
                            </td>
                            <td>
                            
                            </td>
                        </tr>
                        <tr>
                            <td>
                            
                            </td>
                            <td>
                            
                            </td>
                            <td>
                            
                            </td>
                        </tr>
                    </table>
                    <div style="width:100%">
                        <div id="divHeading" style="padding:20px 15px 2px 15px">
                            Connection
                        </div>
                        <div>
                            <table style="width:100%"  >
                                <tr>
                                    <td style="width:5%" >
                                        
                                    </td>
                                    <td style="width:90%" >
                                    
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                
            </div>
        </div>
        <div id="nav" style="width: 30%; float: left;">
            <div style="width: auto; background-color: White; font-size: 11px; font-weight: bold">
                <div style="background-color: #DDDDDD; padding: 2px 0px 2px 15px">
                    <a href="/Phones/MoreInfo" style="text-decoration: none; color: #000000">Main Features</a>
                </div>
                <div style="height: 2px"></div>
                <div style="background-color: #DDDDDD; padding: 2px 0px 2px 15px">
                    <a href="/Phones/MoreInfo" style="text-decoration: none; color: #000000">Additional
                        Features</a>
                </div>
                <div style="height: 2px"></div>
                <div style="background-color: #DDDDDD; padding: 2px 0px 2px 15px">
                    <a href="/Phones/MoreInfo" style="text-decoration: none; color: #000000">QuicK Start
                        Guide</a>
                </div>
                <div style="height: 2px"></div>
                <div style="background-color: #DDDDDD; padding: 2px 0px 2px 15px">
                    <a href="/Phones/MoreInfo" style="text-decoration: none; color: #000000">Phone Software</a>
                </div>
                <div style="height: 2px"></div>
                <div style="background-color: #DDDDDD; padding: 2px 0px 2px 15px">
                    <a href="/Phones/MoreInfo" style="text-decoration: none; color: #000000">Bluetooth System
                        Upgrade</a>
                </div>
            </div>
            <div style="margin-top:50px;text-align:center"> 
                <img src="../../Content/Images/phones/Prada.gif" style="" />
            </div>
            <div style="margin-top:20px;text-align:center;width:126px;background-color:#000000;color:#ffffff;height:50px;"> 
                    <span style="vertical-align:middle">FULLY COMPATIBLE </span> 
            </div>
            <div style="margin-top:20px;text-align:center;">
                <a href="Index.aspx" style="font-size: 11px; text-decoration: none; color: #000000">Select Another Vehicle</a>
            </div>
        </div>
    </div>
    <% } %>

</asp:Content>

