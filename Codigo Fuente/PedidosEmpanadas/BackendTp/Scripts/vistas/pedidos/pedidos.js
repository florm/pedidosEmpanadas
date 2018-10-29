$(document).ready(function () {
    var textValidacionGustos = $("#textValidacionGustos");
    //var btnIniciar = $("#btnIniciar");
    var inputInvitados = $("#inputInvitados");
    var divInvitado = $("#divInvitado");
});


function seleccionarTodos(source) {
    var checkboxes = $("form input:checkbox");
    for (var i = 0, n = checkboxes.length; i < n; i++) {
        checkboxes[i].checked = source.checked;
    }
}

function validacionYEnvio(e, arrayViejos)
{
    event.preventDefault(e);
    var checkboxes = $("form input:checkbox");
    if (validarSeleccionDeGustos(checkboxes)) {
        armarInvitados(inputInvitados, arrayViejos);
        $("form").submit();
    }
    else {
        textValidacionGustos.removeClass("d-none");
        textValidacionGustos.text("Debe seleccionar al menos 1 gusto");
    }
}

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
function armarInvitados(nuevosInput, arrayViejos) {
    $.each(nuevosInput.tagsinput("items"), function (i, item) {
        var nuevoInput = $("<input>");
        nuevoInput.attr('name', 'Invitados[' + numeroArray + '].Email');
        nuevoInput.attr('id', 'Invitados_' + numeroArray + '__Email');
        nuevoInput.addClass("d-none invitadosEditar");
        nuevoInput.val(item.Email);
        divInvitado.append(nuevoInput);
    numeroArray++;
    });
    if (arrayViejos !== null || arrayViejos !== undefined) {
        $.each(arrayViejos, function (i, item) {
            var viejosEmail = $("<input>");
            var viejosCompletoPedido = $("<input>");

            viejosEmail.attr('name', 'Invitados[' + numeroArray + '].Email');
            viejosEmail.attr('id', 'Invitados_' + numeroArray + '__Email');
            viejosEmail.addClass("d-none invitadosEditar");
            viejosEmail.val(item.id);

            viejosCompletoPedido.attr('name', 'Invitados[' + numeroArray + '].CompletoPedido');
            viejosCompletoPedido.attr('id', 'Invitados_' + numeroArray + '__CompletoPedido');
            viejosCompletoPedido.addClass("d-none invitadosEditar");
            viejosCompletoPedido.val(item.completoPedido);
            divInvitado.append(viejosEmail);
            divInvitado.append(viejosCompletoPedido);
        numeroArray++;
        });
    }
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

function agregarTag(select, array) {
    $.each(array,
        function (i, item) {
            select.tagsinput("add", { Id: item.id, Email: item.defaultValue });
        });
}




