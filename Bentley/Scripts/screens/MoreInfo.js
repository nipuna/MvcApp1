
function fillResults(sIndex) {
    var tData;
    //alert("i m here");
    //document.getElementById("ModelYears").innerHTML = "";
    var dpdModel = document.getElementById("SoftwareVersions");

    var testInstanceId = dpdModel.options[sIndex].value;
    $.get("/Phones/getMainFeatureResults/", { 'testInstanceId': testInstanceId },
            function (data) {
                if (data != "") {
                    $("#detailedResults").empty();
                    $(data).appendTo("#detailedResults");
                }
            }
                );

    return false;
}