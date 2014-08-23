<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
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
<div style="margin-top: 50px;text-align: center">
    <img src='<%= ViewData["phoneImageSrc"] %>' style="" />
</div>
<div style="margin-top: 20px; text-align: center; width: 126px; background-color: #000000;color: #ffffff; height: 50px;">
    <span style="vertical-align: middle"><%= ViewData["overallCompatibitiySts"] %> </span>
</div>
<div style="margin-top: 20px; text-align: center;">
    <a href="Index.aspx" style="font-size: 11px; color: #5C4864">
        Select Another Vehicle</a>
</div>
