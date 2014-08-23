
function fillYears(sIndex) {
    var tData;
    //alert("i m here");
    document.getElementById("ModelYears").innerHTML = "";
    var dpdModel = document.getElementById("Models");

    var modelId = dpdModel.options[sIndex].value;
    var rnd = Math.random();

    $.get("/Vehicles/getModelYears?modelId=" + modelId + "&" + rnd,
            function (data) {

                //alert("Data Loaded: " + data);

                tData = data.split(',');
                var ddYears = document.getElementById("ModelYears");
                for (var i = 0; i < tData.length - 1; ++i) {
                    var optn = document.createElement("OPTION");
                    var data = tData[i];
                    if (data.length > 4) {
                        data = data.substring(0, 4)
                    }
                    optn.text = tData[i];
                    optn.value = data;
                    ddYears.options.add(optn);
                }

            }
    );
    
    return false;
}

