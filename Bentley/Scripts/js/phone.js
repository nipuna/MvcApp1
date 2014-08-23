var step = 130
var xOffset = 260


function showResults() {

    var testInstanceId = document.getElementById("ddPhones").value;
    var conclusion;
    var commentText;
    i = 1;
    var rand_no = Math.random();
    var topFeatureResults = eval(allResults);

    $.each(topFeatureResults,
        function (entryIndex, entry) {

            conclusion = entry['Conclusion'];

            if (entry['TestInstanceId'] == testInstanceId) {
                if ((entry['TopFeatureName']).toLowerCase() != "enhanced audio quality") {
                    $('#tdFeature' + i).empty();
                    $('#tdResult' + i).empty();
                    $('#tdFeature' + i).append(entry['TopFeatureName']);
                    var html = getImage(entry['TopFeatureResult']);
                    commentText = entry['CommentText'];
                    $('#tdResult' + i).append(html);
                    i = i + 1;
                }
            }
            if (i == 6) {
                return false;
            }
        }
    );

    $('#dvCompat').empty();
    $("#dvCompat").append(conclusion);

    var commentDiv = $('#divComm').get(0);
    var instanceId = document.getElementById("ddPhones").value;
    var anch = document.getElementById('aMoreInfo');
    var url = document.location.href;
    url = url.toLowerCase();
    url = url.replace(document.location.pathname, "")
    if (conclusion == "Not Recommended") {
        anch.style.visibility = "hidden";
        anch.style.display = "none";
        commentDiv.style.visibility = "visible";
        commentDiv.style.display = "inline";
        $('#finalComment').empty();
        $('#finalComment').append(commentText);
    }
    else {
        anch.style.visibility = "visible";
        anch.style.display = "block";
        commentDiv.style.visibility = "hidden";
        commentDiv.style.display = "none";
        $('#finalComment').empty();
    }
    anch.href = "/Phones/moreinfo" + "?testInstanceId=" + instanceId + "&" + rand_no;

    var si = document.getElementById("ddPhones").selectedIndex;

}

function toDigit(n) { return (n < 10) ? '0' + n : n; }

function getImage(r) {
    var h = ''

    switch (r.toUpperCase()) {
        case 'YES': h = '<img src=\'/images/greenTick.gif\' />'; break;
        case 'NO': h = '<img src=\'/images/redCross.gif\' />'; break;
        default: h = r;
    }

    return h
}

var wndo;

function init_dw_Scroll() {
    wndo = new dw_scrollObj('wn', 'lyr1', 'phoneContent');
    wndo.setUpScrollbar("dragBar", "track", "h", 1, 1);
    wndo.setUpScrollControls('scrollbar', true, "h");
    wndo.timerId = 0;

    wndo.on_scroll = wndo.on_glidescroll = function () {
        document.getElementById('left').style.visibility = (wndo.x == 0) ? 'hidden' : 'visible';
        document.getElementById('right').style.visibility = (wndo.x == -wndo.maxX) ? 'hidden' : 'visible';
    }
    wndo.on_scroll();

    wndo.on_scrolled = function () {
        if (wndo.getTimerID() == 0) {
            wndo.shiftTo(130 * parseInt(wndo.x / 130), 0, true)
            wndo.on_scroll();

            document.getElementById("ddPhones").selectedIndex = (Math.abs(wndo.x) / step)

            if (!scrollbar.beingDragged) showResults()
        }
    }
}

function selectPhone(p) {
    document.getElementById("ddPhones").selectedIndex = p
    wndo.initScrollToVals(p * step, 0, 300);
    wndo.scroll()
}

function fillModels(sIndex) {
    var tData;
    //alert("i m here");
    document.getElementById("ddPhones").innerHTML = "";
    var dpdModel = document.getElementById("ddBrand");

    var brandId = dpdModel.options[sIndex].value;

    var rand_no = Math.random();
    $.getJSON("/Phones/getBrandModels?brandId=" + brandId + "&" + rand_no,
                        function (myData) {
                            //alert('data is:' + myData);
                            tData = myData;
                            var ddPhones = document.getElementById("ddPhones");

                            var phoneDivs = "<table id='t1' border='0' cellpadding='0' cellspacing='0' style='line-height:10px;border-collapse:collapse'>" + "<tbody><tr>";

                            $.each(myData, function (entryIndex, entry) {

                                //Filling the Drop down list with models for the brand
                                var optn = document.createElement("OPTION");
                                optn.text = entry['Name'];
                                optn.value = entry['TestInstanceId'];
                                ddPhones.options.add(optn);

                                phoneDivs = phoneDivs + "<td style='width:130px'>";
                                //Phone Photo div
                                phoneDivs = phoneDivs + "<div id='divPhone" + entryIndex + "' class='phone'>";
                                phoneDivs = phoneDivs + "<div>";

                                phoneDivs = phoneDivs + "<img style='border-width: 0px; height: 100px; width: 90px; padding-top: 4px' src='" + entry['Photo'] + "' onclick='selectPhone(" + entryIndex + ")' />"

                                phoneDivs = phoneDivs + "</div>";
                                //Phone Name div
                                phoneDivs = phoneDivs + "<div style='padding: 5px;'>";
                                phoneDivs = phoneDivs + "<span class='smallText' id='ph" + entryIndex + "_lblPhone'>";
                                phoneDivs = phoneDivs + entry['BrandName'];
                                phoneDivs = phoneDivs + "<br/>";
                                phoneDivs = phoneDivs + entry['Name'];
                                phoneDivs = phoneDivs + "</span></div>";
                                phoneDivs = phoneDivs + "</div>";
//                                Script for changing the CSS
//                                if (entryIndex == 0) {
//                                    phoneDivs = phoneDivs + "<script type='text/javascript'>"
//                                    phoneDivs = phoneDivs + "divCss = document.getElementById(currentDiv);"
//                                    phoneDivs = phoneDivs + "divCss.style.background = \"url(/images/boaderphone.gif) no-repeat\";";
//                                    phoneDivs = phoneDivs + "</script>";
//                                }
                                phoneDivs = phoneDivs + "</td>";

                            });

                            phoneDivs = phoneDivs + "<TD style='width:130px'><DIV class='phone'></DIV></TD>";
                            phoneDivs = phoneDivs + "<TD style='width:130px'><DIV class='phone'></DIV></TD>";
                            var nPhoneDivs = phoneDivs + "</tr></tbody></table>";
                            var abc = $("#phoneContent").html();
                            $("#phoneContent").empty();
                            $(nPhoneDivs).appendTo("#phoneContent");
                            
                            showResults();
                            init_dw_Scroll();
                            SelectedCarDiv = "0";
                            pSelectedCarDiv = "0";
                        });

    return false;
}

function makePhoneDivs(brandId) {
    var rand_no = Math.random();
    
    $.getJSON("/Phones/getBrandModels?brandId=" + brandId + "&" + rand_no ,
            function (myData) {
            
//            <div class="phone">
//                <div>
//                    <img style="border-width: 0px; height: 100px; width: 100px;" src="images/phones/Apple/iPhone3bv5.gif"
//                        onclick="selectPhone(0)" class="hand" id="ctl00_cphContent_rptPhones_ctl01_imgPhone"></div>
//                <div style="padding: 5px;">
//                    <span class="smallText" id="ctl00_cphContent_rptPhones_ctl01_lblPhone">Apple<br>
//                        iPhone</span></div>
//            </div>

                var phoneDivs = "<table id='t1' border='0' cellpadding='0' cellspacing='0' style='line-height:10px;border-collapse:collapse'>" + "<tbody><tr>";
                $.each(myData, function (entryIndex, entry) {
                    phoneDivs = phoneDivs + "<td >";
                    //Phone Photo div
                    phoneDivs = phoneDivs + "<div id='divPhone" + entryIndex + "' class='phone'>";
                    phoneDivs = phoneDivs + "<div>";

                    phoneDivs = phoneDivs + "<img style='border-width: 0px; height: 100px; width: 100px;' src='" + entry['Photo'] + "' onclick='selectPhone(" + entryIndex + ")' />"

                    phoneDivs = phoneDivs + "</div>";
                    //Phone Name div
                    phoneDivs = phoneDivs + "<div style='padding: 5px;'>";
                    phoneDivs = phoneDivs + "<span class='smallText' id='ph" + entryIndex + "_lblPhone'>";
                    phoneDivs = phoneDivs + entry['BrandName'];
                    phoneDivs = phoneDivs + "<br/>";
                    phoneDivs = phoneDivs + entry['Name'];
                    phoneDivs = phoneDivs + "</span></div>";
                    phoneDivs = phoneDivs + "</div>";
                    //Script for changing the CSS
//                    if (entryIndex == 0) {
//                        phoneDivs = phoneDivs + "<script type='text/javascript'>"
//                        phoneDivs = phoneDivs + "divCss = document.getElementById(currentDiv);"
//                        phoneDivs = phoneDivs + "divCss.style.background = \"url(/images/boaderphone.gif) no-repeat\";";
//                        phoneDivs = phoneDivs + "</script>";
//                    }
                    phoneDivs = phoneDivs + "</td>";

                });
                phoneDivs = phoneDivs + "<TD style='width:130px'><DIV class='phone'></DIV></TD>";
                phoneDivs = phoneDivs + "<TD style='width:130px'><DIV class='phone'></DIV></TD>";
                phoneDivs = phoneDivs + "</tr></tbody></table>";
                var abc = $("#phoneContent").html();
                $("#phoneContent").empty();
                //$("#t1").append(phoneDivs);
                $(phoneDivs).appendTo("#phoneContent");
                abc = $("#phoneContent").html();
                init_dw_Scroll();
//                var cssOnPhone = $('#divCssonPhone').get(0);
//                cssOnPhone.style.visibility = "visible";
//                cssOnPhone.style.display = "inline";
                
            });


}


function getTopFeatureResults(testInstanceId) {
    var rand_no = Math.random();
    var returnedData;
    $.getJSON("/Phones/getTopFeatureResults?testInstanceId=" + testInstanceId + "&" + rand_no,
        function (myData) {

            alert("mydata is:" + myData);
            returnedData = myData;

        });
    return returnedData;
}


//if (dw_scrollObj.isSupported()) {
//    dw_Event.add(window, 'load', init_dw_Scroll);
//}
