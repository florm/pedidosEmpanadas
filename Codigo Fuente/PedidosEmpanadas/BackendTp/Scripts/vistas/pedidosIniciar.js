var selectAll = $("#selectAll");

selectAll.click(function() {
    seleccionarTodos(this);
});

function seleccionarTodos(source) {
    var checkboxes = $("form input:checkbox");
    for (var i = 0, n = checkboxes.length; i < n; i++) {
        checkboxes[i].checked = source.checked;
    }
}