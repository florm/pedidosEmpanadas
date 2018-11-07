$(document).ready(function() {
    $('button.delete').on('click',function(e){
        var button = $(this);
        e.preventDefault();
        var form = $(this).parents('form');
        swal({
            title: "Esta seguro?",
            text: "No se podrá recuperar el pedido elminiado",
            icon: "warning",
            buttons:  ["Cancelar", "Aceptar"],
            dangerMode: true,
        })
        .then((willDelete) => {
            if (willDelete) {
                $.post( form.attr('action'))
                    .done(function(data) {
                        toastr.success("El pedido fue borrado exitosamente.");
                        tabla.row( button.closest('tr') ).remove().draw();
                    })
                    .fail(function(error) {
                        console.log(error);
                        toastr.error("Se produjo un error.");
                    });
                //form.submit();
            }
        });
    });

    $.fn.dataTable.moment( 'D/M/YYYY H:mm:ss' );//para que se pueda ordenar por fecha
    var tabla = $(".table").DataTable({
        responsive: true,
        autoWidth: false,
        paging:   false,
        ordering: true,
        info:     false,
        searching: false,
        columnDefs: [ {
            targets: -1,
            orderable: false,
            width:"50px"
        },{
            targets: 0,
            width:"180px"
        }
        ],
        order: [[ 0, "desc" ]],
        language: {
            lengthMenu: "Mostrando _MENU_ registros",
            search: "Buscar",
            zeroRecords: "No se encontraron registros",
            info: "Mostrando _START_ de _END_ sobre un total de _TOTAL_ elementos",
            infoFiltered:   "(filtrados de un total de _MAX_ total registros)",
            processing: "<i class='fa fa-refresh fa-spin'></i>",
            paginate: {
                first:      "Primera",
                last:       "Ultima",
                next:       "Siguiente",
                previous:   "Anterior"
            },
        },
    });
});


