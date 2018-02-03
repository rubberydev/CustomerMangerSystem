$(document).ready(function () {
    $("#State").prop("disabled", true);
    $("#Pais").change(function () {        
        if ($("#Pais").val() != "Seleccione") {
            var CountryOptions = {};
            CountryOptions.url = "/Clientes/states";
            CountryOptions.type = "POST";
            CountryOptions.data = JSON.stringify({ Country: $("#Pais").val() });
            CountryOptions.datatype = "json";
            CountryOptions.contentType = "application/json";
            CountryOptions.success = function (StatesList) {
                $("#State").empty();
                for (var i = 0; i < StatesList.length; i++) {
                    $("#State").append("<option>" + StatesList[i] + "</option>");
                }
                $("#State").prop("disabled", false);
            };
            CountryOptions.error = function () { alert("Error al intentar traer paises!!"); };
            $.ajax(CountryOptions);
        }
        else {
            $("#State").empty();

            $("#State").prop("disabled", true);
        }

    });

});

