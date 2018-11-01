// $(document).ready(function () {
    var textEmail = $("#textEmail");
    var textPassword = $("#textPassword");
    var textRepetirPassword = $("#textRepetirPassword");
    var btnAceptar = $("#btnAceptar");
    var modalRegistro = $("#registro");
    var modalLogin = $("#login");
    var loginEmail = $("#loginEmail");
    var loginPassword = $("#loginPassword");
    var btnAceptarLogin = $("#btnAceptarLogin");


    modalLogin.find(".btn-cancelar").click(function () {
        modalLogin.modal('hide');
        loginEmail.val("");
        loginPassword.val("");
        ocultarMensajeValidacionErronea(loginEmail);
        ocultarMensajeValidacionErronea(loginPassword);


    });

    modalRegistro.find(".btn-cancelar").click(function () {
        modalRegistro.modal('hide');
        textEmail.val("");
        textPassword.val("");
        ocultarMensajeValidacionErronea(textEmail);
        ocultarMensajeValidacionErronea(textPassword);
        ocultarMensajeValidacionErronea(textRepetirPassword);
    });

    btnAceptar.click(function () {
        if (!validar())
            return;

        var usuario = new Object();
        usuario.email = textEmail.val();
        usuario.password = textPassword.val();

        llamadaAjax(window.pathRegistro, JSON.stringify(usuario), false, "registroOk", "registroError");
    });

    btnAceptarLogin.click(function () {
        if (!validarCampoEmail(loginEmail)) return;
        if (!validarCampoObligatorio(loginPassword)) return;

        var usuario = new Object();
        usuario.email = loginEmail.val();
        usuario.password = loginPassword.val();

        llamadaAjax(window.pathLogin, JSON.stringify(usuario), false, "loginOk", "loginError");
    });

    function registroOk() {
        mostrarMsgExito("El registro se realizó correctamente. Ahora puede iniciar sesión");
        modalRegistro.modal("hide");

    }

    function registroError(mensaje, params) {
        mensaje = JSON.parse(mensaje);
        mostrarMsgError(mensaje.message);
        console.log(mensaje);
        console.log(params);
    }

    function loginOk() {
        window.location.href = window.pathListaPedidos;

    }

    function loginError(data) {
        var mensaje = JSON.parse(data);
        mostrarMsgError(mensaje.ErrorMessage);

    }


    function validar() {
        if (!validarCampoEmail(textEmail)) return false;
        if (!validarCampoObligatorio(textPassword)) return false;
        if (!validarTextRepetir()) return false;
        return true;
    }

    function validarTextRepetir() {
        if (textPassword.val() != textRepetirPassword.val()) {
            mostrarMensajeValidacionErronea(textRepetirPassword, "Las contraseñas deben ser iguales");
            return false;
        }
        return validarCampoObligatorio(textRepetirPassword);
    }


    $("#testbtn").click(function () {
        probandoajax();

    });

    function probandoajax() {

        var pedidoRequest = new Object();
        pedidoRequest.IdUsuario = $("#idUsuario").val();
        pedidoRequest.IdPedido = $("#idPedido").val();
        pedidoRequest.Token = $("#tokenUsuario").val();

        var GustoEmpanadasCantidad = $("#listaGustos input").map(function () {
            var GustoEmpanadasCantidad = new Object();
            //GustoEmpanadasCantidad.Nombre = $("#listTest").val();
            //GustoEmpanadasCantidad.IdGustoEmpanada = $(this).data("id");
            //GustoEmpanadasCantidad.Nombre = $(this).val();

            //GustoEmpanadasCantidad.Nombre = $(this).data("name");
            GustoEmpanadasCantidad.Cantidad = $(this).val();
            GustoEmpanadasCantidad.IdGustoEmpanada = $(this).attr('id');

            //GustoEmpanadasCantidad.IdGustoEmpanada = $("input").data("id")

            //GustoEmpanadasCantidad.IdGustoEmpanada = $("#testAle").val();
            //return JSON.stringify(GustoEmpanadasCantidad);
            return GustoEmpanadasCantidad;
        }).get();

        pedidoRequest.GustoEmpanadasCantidad = GustoEmpanadasCantidad;

        //pedidoRequest.IdUsuario = $("#IdTest").val();
        //pedidoRequest.Token = $("#TokenTest").val();
        llamadaAjax("http://localhost:56230/api/pedidos", JSON.stringify(pedidoRequest), true, "pedidoOk", "mostrarMensajeDeError");

    }

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "progressBar": true,
        "preventDuplicates": false,
        "positionClass": "toast-top-right",
        "onclick": null,
        "showDuration": "4000",
        "hideDuration": "10000",
        "timeOut": "7000",
        "extendedTimeOut": "10000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

// });