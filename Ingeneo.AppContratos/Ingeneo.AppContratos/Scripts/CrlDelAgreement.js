function Confirm(ctl, event) {
    event.preventDefault();
    swal({
        title: "¿Estas seguro de borrar este contrato?",
        text: "Verifiqué la información antes de proceder a borrar este registro.",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Aceptar",
        cancelButtonText: "Cancelar",
        closeOnConfirm: true,
        closeOnCancel: false
    }, function (isConfirm) {
        if (isConfirm) {
            $("#DeleteAgreement").submit();

        } else {
            window.location.href = "/Contratos/Index";
        }
    });
}