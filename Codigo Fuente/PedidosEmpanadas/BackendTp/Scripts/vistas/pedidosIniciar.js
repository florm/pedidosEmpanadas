﻿var selectAll = $("#selectAll");
var textValidacionGustos = $("#textValidacionGustos");
var btnIniciar = $("#btnIniciar");
var btnEditar = $("#btnEditar");
var btnDuplicar = $("#btnDuplicar");
var inputInvitados = $("#inputInvitados");
var divInvitado = $("#divInvitado");

inicializaSelectTags(window.usuarioController, inputInvitados, "invitados");

selectAll.click(function() {
    seleccionarTodos(this);
});

function seleccionarTodos(source) {
    var checkboxes = $("form input:checkbox");
    for (var i = 0, n = checkboxes.length; i < n; i++) {
        checkboxes[i].checked = source.checked;
    }
}


btnIniciar.click(function (e) {
    event.preventDefault(e);
    var checkboxes = $("form input:checkbox");
    armarInvitados(inputInvitados);
    if (validarSeleccionDeGustos(checkboxes)) {
        $("form").submit();
    }
    else {
        textValidacionGustos.removeClass("d-none");
        textValidacionGustos.text("Debe seleccionar al menos 1 gusto");
    }
});

function validarSeleccionDeGustos(checkboxes) {
    var validacion = 0;
    for (var i = 0, n = checkboxes.length; i < n; i++) {
        if (checkboxes[i].checked === false)
            validacion++;
    }
    if (checkboxes.length === validacion)
        return false;
    else
        return true;
}

var numeroArray = 0;
function armarInvitados(input) {
    $.each(input.tagsinput("items"), function (i, item) {
        var input = $("<input>");
        input.attr('name', 'Invitados[' + numeroArray + '].Email');
        input.attr('id', 'Invitados_' + numeroArray + '__Email');
        
        input.val(item.Email);
        divInvitado.append(input).prop("hidden","true");
    numeroArray++;
    });
    //divInvitado.append(modeloInput);

}


function inicializaSelectTags(url, select, name) {
    select.tagsinput({
        itemValue: "Id",
        itemText: "Email",
        typeaheadjs: [
            {
                highlight: true,
                minLength: 1
            },
            {
                name: name,
                displayKey: "Email",
                valueKey: "Id",
                limit: 10,
                source: function (email, sync, async) {
                    return $.get(url,
                        {
                            email: email
                        },
                        function (data) {
                            return async(data);
                        });
                }
            }
        ],
        confirmKeys: 13
    });
    select.tagsinput("input").attr("maxlength", 255).addClass("w-100");
    
}

