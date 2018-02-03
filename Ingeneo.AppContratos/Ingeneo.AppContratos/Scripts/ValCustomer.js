function validateData() {

    var z = document.getElementById("email").value;
    var expregEmail = /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/gi;   

    //var x = document.getElementById("url").value;    
    //var ExpReg = gi;

    var y = document.getElementById("Nit").value;    
    var exp_reg = /^[a-z\d\u00C0\-\u00ff]+$/i;

    var t = document.getElementById("Telefono").value;
    var xR = /^[0123456789]+$/i;

    var nam = document.getElementById("NombreCliente").value;
    var exR = /^[a-zA-Z\w\d\s\-\ñ]+$/gi;

   
    
    if ($("#Nit").val() == "" && $("#NombreCliente").val() == "" && $("#Telefono").val() == "" && $("#Direccion").val() == "" && $("#Email").val() == "" && $("#URL").val() == "") {

        swal("ERROR", "No hay información en el formulario por favor verifiqué la información que desea guardar.", "error");
        return false;

    } else if ($("#Nit").val() == "") {

        swal("ERROR", "El Nit del cliente es un campo obligatorio en el formulario por favor verifiqué la información que desea guardar.", "error");
        return false;

    } else if ($("#NombreCliente").val() == "") {

        swal("ERROR", "El Nombre del cliente es un campo obligatorio en el formulario por favor verifiqué la información que desea guardar.", "error");
        return false;

    //} else if (!(ExpReg.test(x)) && x != "") {

    //        swal("ERROR", "El campo URL no es una dirección URL http, https o ftp completa.", "error");
    //        return false;

    } else if (!(expregEmail.test(z)) && z != "") {

        swal("ERROR", "El campo Email no es una dirección de correo electrónico válida.", "error");
        return false;

    } else if (!exp_reg.test(y) && y != "") {

        swal("ERROR", "En el campo Nit no se permiten caracteres especiales solo digitos alfanuméricos y el caracter '-'", "error");
        return false;

    } else if (!exR.test(nam) && nam != "") {

         swal("ERROR", "En el campo nombre del cliente no se permiten caracteres especiales solo digitos alfanuméricos, espacios en blanco y el caracter '-'", "error");
         return false;

    } else if (!xR.test(t) && t != "") {
         swal("ERROR", "El campo telefono no contiene un número de teléfono correcto solo se permiten números.", "error");
         return false;
    } else{
        swal("", "", "success");
        return true;
    }
}

$("#btnCreate").click(function (e) {
    e.preventDefault();
    var btn = "button";
    var style = "btn btn-danger";
    swal({
        title: "¿Quieres guardar estos datos? <br>" +
                "<small>Verifiqué la información antes de guardar</small>!",
        text: '<button type="' + btn + '" id="confirm" >Aceptar</button> ' +
              '<button  type="' + btn + '" id="cancel" >Cancelar</button> ',
        type: 'warning',
        html: true,
        cancelButtonColor: '#d33',
        showConfirmButton: false,
        showCancelButton: false,
        closeOnCancel: false
    }, function (dismiss) {
        // dismiss can be 'cancel', 'overlay',
        // 'close', and 'timer'
        if (dismiss === 'cancel') {
            swal(
              'Cancelado',
              'Usted no ha guardado los datos del cliente!',
              'error'
            );
        }
    });
});
$(document).on('click', "#confirm", function () {
    if (validateData() == true) {
        if ($("#Pais").val() == "Seleccione") {
            $("#Pais").empty();
        }
        $("#SavedFormCustomer").submit();
    }
});

$(document).on('click', "#cancel", function () {
    swal("Cancelado", "Usted no ha guardado los datos del cliente!", "error");
});



