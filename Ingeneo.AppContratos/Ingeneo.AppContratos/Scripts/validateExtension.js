function validateData() {
    if ($("#FechaInicioProrroga").val() == "" && $("#FechaFinProrroga").val() == "") {
        swal("ERROR", "No hay datos en el formulario", "error");

        return false;

    } else if ($("#FechaInicioProrroga").val() == "") {
        swal("ERROR", "La fecha inicio de la prórroga es un campo obligatorio en el formulario por favor verifiqué que no este vacio", "error");
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
                    $("#SaveExtensionForm").submit();
                    isConfirm.closeOnConfirm = true
                }
            } else {
                swal("Cancelado", "Usted no ha guardado los datos!", "error");
            }
        });
}