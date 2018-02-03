
$(document).ready(function () {
    $('table.display').DataTable({
        "language": {
            "url": "dataTables.Spanish.lang",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },
        },
        "pageLength": 10,
        "pagingType": "full_numbers",
        "searching": false,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        dom: 'Bfrtip',
        buttons: [
       'copy', 'excel', 'pdf', 'print'
        ]
    });

});

