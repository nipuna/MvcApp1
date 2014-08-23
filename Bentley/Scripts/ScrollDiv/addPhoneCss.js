var pSelectedCarDiv = "0";
var SelectedCarDiv = "0";

function applyCss() {
    var divCssId = "";
    var divCss;
    if (SelectedCarDiv < 0)
        SelectedCarDiv = 0;

    divCssId = "divPhone" + SelectedCarDiv;
    divCss = document.getElementById(divCssId);
    divCss.style.backgroundImage = "";

    divM = divCss.getElementsByTagName("div")[0];
    var e = document.getElementById("achWithLink");
    if (e != null) {
        if (e.parentNode == divM) {
            var imgAdd = divCss.getElementsByTagName("img");
            imgAdd[0].className = "";
            divM.appendChild(imgAdd[0]);
            divM.removeChild(e);
        }
    }
    
    divCssId = "divPhone" + pSelectedCarDiv;
    divCss = document.getElementById(divCssId);
    divCss.style.backgroundImage = "";
        
    divM = divCss.getElementsByTagName("div")[0];
    var e = document.getElementById("achWithLink");
    if (e != null) {
        if (e.parentNode == divM) {
            var imgAdd = divCss.getElementsByTagName("img");
            imgAdd[0].className = "";
            divM.appendChild(imgAdd[0]);
            divM.removeChild(e);
        }
    }
    
    var newIndex = document.getElementById("ddPhones").selectedIndex;
    if (newIndex < 0)
        newIndex = 0;
    divCssId = "divPhone" + newIndex;
    divCss = document.getElementById(divCssId);
    divCss.style.background = "url(/images/boaderphone.gif) no-repeat";
    SelectedCarDiv = document.getElementById("ddPhones").selectedIndex;

//    var imgRmv = divCss.getElementsByTagName("img");

//    var anchLink = document.createElement('a');
//    anchLink.id = "achWithLink";
//    anchLink.href = "3page.html?val=" + document.getElementById("ddPhones").options[newIndex].value;
//    anchLink.style.outline = "none";

//    divCss.getElementsByTagName("div")[0].appendChild(anchLink);
//    imgRmv[0].className = "hand";
//    anchLink.appendChild(imgRmv[0]);
    
}


function applyCssOnDP() {
    var divCssId = "";
    var divCss;
    if (SelectedCarDiv < 0)
        SelectedCarDiv = 0;

    divCssId = "divPhone" + pSelectedCarDiv;
    divCss = document.getElementById(divCssId);
    divCss.style.backgroundImage = "";

    divM = divCss.getElementsByTagName("div")[0];
    var e = document.getElementById("achWithLink");
    if (e != null) {
        if (e.parentNode == divM) {
            var imgAdd = divCss.getElementsByTagName("img");
            imgAdd[0].className = "";
            divM.appendChild(imgAdd[0]);
            divM.removeChild(e);
        }
    }
    
    divCssId = "divPhone" + SelectedCarDiv;
    divCss = document.getElementById(divCssId);
    divCss.style.backgroundImage = "";

    divM = divCss.getElementsByTagName("div")[0];
    var e = document.getElementById("achWithLink");
    if (e != null) {
        if (e.parentNode == divM) {
            var imgAdd = divCss.getElementsByTagName("img");
            imgAdd[0].className = "";
            divM.appendChild(imgAdd[0]);
            divM.removeChild(e);
        }
    }
    
    var newCarDiv = document.getElementById("ddPhones").selectedIndex;
    divCssId = "divPhone" + newCarDiv;
    divCss = document.getElementById(divCssId);
    //divCss.style.border = "2px solid #666";
    divCss.style.background = "url(/images/boaderphone.gif) no-repeat";
    pSelectedCarDiv = document.getElementById("ddPhones").selectedIndex;

    var imgRmv = divCss.getElementsByTagName("img");

//    var anchLink = document.createElement('a');
//    anchLink.id = "achWithLink";
//    anchLink.href = "3page.html?val=" + document.getElementById("ddPhones").options[newCarDiv].value;
//    anchLink.style.outline = "none";

//    divCss.getElementsByTagName("div")[0].appendChild(anchLink);
//    imgRmv[0].className = "hand";
//    anchLink.appendChild(imgRmv[0]);
    
}