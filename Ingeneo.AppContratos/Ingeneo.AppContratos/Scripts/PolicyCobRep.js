$(document).ready(function () {
    $('#Test').DataTable({
        "language": {
            "url": "dataTables.Spanish.lang",
            "sEmptyTable": "Ningún dato disponible en esta tabla",            
            //"sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
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
        "ordering": false,
        "info": false,
        "autoWidth": false,
        dom: 'Bfrtip',
        buttons: [
       'copy', 'excel', 'pdf', 'print'
        ]
    });

});