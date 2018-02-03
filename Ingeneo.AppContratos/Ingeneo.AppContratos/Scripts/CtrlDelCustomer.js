function Confirm(ctl, event) {
    event.preventDefault();
    swal({
        title: "¿Estas seguro de borrar este cliente?",
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
            $("#DeleteCustomer").submit();

        } else {
            window.location.href = "/Clientes/Index";
        }
    });
}