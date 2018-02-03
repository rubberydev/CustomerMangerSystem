function Mistake() {
    swal("Error", "No hay información en el formulario o recuerde que la descripción, fecha inicio y fecha fin de la cobertura son obligatorios por favor digité en los diferentes campos la información que desea guardar", "error");
}
function validateData() {   

    var w = document.getElementById("DescripcionCobertura").value;
    var exp_reg = /^[a-zA-Z\w\d\s\-\ñ]+$/gi;

    if ($("#DescripcionCobertura").val() == "") {
        Mistake();
        return false;
    }else if ($("#FechaInicioProteccion").val() == "") {
        Mistake();
        return false;

    } else if ($("#FechaFinProteccion").val() == "") {
        Mistake();
        return false;
    }
    else if ($("#DescripcionCobertura").val() == "" && $("#FechaInicioProteccion").val() == "" && $("#FechaFinProteccion").val() == "" ) {
        Mistake();
        return false;
    }
    else if(!exp_reg.test(w) && w != ""){
        
        swal("ERROR", "En el campo descripción de la cobertura solo se permiten digitos alfanuméricos espacios en blanco y el caracter '-' ", "error");
        
        }else {
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
                    $("#SaveFormPolicyDetails").submit();
                    isConfirm.closeOnConfirm = true
                }
            } else {
                swal("Cancelado", "Usted no ha guardado la póliza!", "error");
            }
        });
}