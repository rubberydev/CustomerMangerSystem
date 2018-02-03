function Mistake() {
    swal("Error", "No hay información en el formulario o recuerde que el código, nombre de la aseguradora, fecha inicio y fecha fin de la póliza son obligatorios  por favor digité en los diferentes campos la información que desea guardar.", "error");
}

function validateData() {

    var y = document.getElementById("CodigoPoliza").value;
    var expreg = /^[a-zA-Z\w\d\s\-]+$/gi;

    var w = document.getElementById("NombreAseguradora").value;
    var exp_reg = /^[a-zA-Z\w\d\s\-\ñ]+$/gi;



    if ($("#CodigoPoliza").val() == "") {
        Mistake();
        return false;
    } else if ($("#FechaInicioPoliza").val() == "") {
        Mistake();
        return false;
    } else if ($("#FechaFinpoliza").val() == "") {
        Mistake();
        return false;
    }
    else if ($("#NombreAseguradora").val() == "") {
        Mistake();
        return false;
    } else if ($("#CodigoPoliza").val() == "" && $("#NombreAseguradora").val() == "" && $("#FechaInicioPoliza").val() == "" && $("#FechaFinpoliza").val() == "") {
        Mistake();
        return false;
    }
    else if (!(exp_reg.test(w)) && w != "") {

        swal("ERROR", "En el campo Nombre de la aseguradora no se permiten caracteres especiales solo digitos alfanuméricos, espacios en blanco y el caracter '-'", "error");
        return false;

    } else if (!(expreg.test(y)) && y != "") {

        swal("ERROR", "En el campo código de la póliza no se permiten caracteres especiales solo digitos alfanuméricos, espacios en blanco y el caracter '-'", "error");
        return false;

    } else {
        swal("", "", "success");
        return true;
    }
}
function Validate(ctl, event) {
    event.preventDefault();
    swal({
        title: "¿Quieres guardar estos datos?",
        text: "Verifiqué la información antes de guardar!",
        type: "info",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Guardar",
        cancelButtonText: "Cancelar",
        closeOnConfirm: false,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {
                if (validateData() == true) {
                    $("#SavedFormPolicy").submit();
                    isConfirm.closeOnConfirm = true
                }
            } else {
                swal("Cancelado", "Usted no ha guardado la póliza", "error");
            }
        });
}