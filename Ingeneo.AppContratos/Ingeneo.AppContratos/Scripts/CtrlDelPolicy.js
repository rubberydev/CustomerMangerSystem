﻿function Confirm(ctl, event) {
    event.preventDefault();
    swal({
        title: "¿Estas seguro de borrar esta póliza?",
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
            $("#DeletePolicy").submit();

        } else {
            swal("Accion cancelada!!", "Usted ha cancelado borrar este registro", "success");
        }
    });
}