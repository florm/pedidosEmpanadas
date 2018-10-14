var selectAll = $("#selectAll");
var textValidacionGustos = $("#textValidacionGustos");
var btnIniciar = $("#btnIniciar");
var btnDuplicar = $("#btnDuplicar");
var inputInvitado = $("#inputInvitado");
var divInvitado = $("#divInvitado");


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

btnDuplicar.click(function () {
    var modeloInput = inputInvitado.clone();
    modeloInput.removeAttr('hidden');
    modeloInput.removeAttr('id');
    divInvitado.append(modeloInput);

});