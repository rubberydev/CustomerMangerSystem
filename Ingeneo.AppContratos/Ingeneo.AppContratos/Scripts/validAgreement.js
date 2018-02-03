function validateData() {

    var y = document.getElementById("CodigoContrato").value;
    var exp_reg = /^[a-zA-Z\w\d\s\-\ñ]+$/gi;

 
    if ($("#CodigoContrato").val() == "" && $("#FechaInicioContrato").val() == "" && $("#ValorContrato").val() == "" && $("#FechaFinContrato").val() == "") {
        swal("ERROR", "No hay datos en el formulario digité en los diferentes campos la información que desea guardar", "error");
        return false;

    } else if ($("#CodigoContrato").val() == "") {

        swal("ERROR", "El Nombre del contrato y la fecha inicio del contrato son campos obligatorios en el formulario por favor verifiqué que no estén vacios", "error");
        return false;

    } else if ($("#FechaInicioContrato").val() == "") {
        swal("ERROR", "El Nombre del contrato y la fecha inicio del contrato son campos obligatorios en el formulario por favor verifiqué que no estén vacios", "error");
        return false;

    } else if (!(exp_reg.test(y)) && y != "") {

        swal("ERROR", "En el campo Nombre del contrato no se permiten caracteres especiales solo digitos alfanuméricos, espacios en blanco y el caracter '-'", "error");
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
                if (validateData()) {
                    swal("", "", "success");
                    $("#SavedFormAgreement").submit();
                    isConfirm.closeOnConfirm = true
                }
            } else {
                swal("Cancelado", "Usted no ha guardado los datos!", "error");
            }
        });
}