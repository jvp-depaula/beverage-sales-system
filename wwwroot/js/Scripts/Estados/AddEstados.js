$(document).ready(function () {
    $("#btnSalvarAddEstado").on('click', function () {
        if (validaEstado()) {
            $.ajax({
                url: "/Estados/JsAddEstado",
                data: {
                    nmEstado: $("#nmEstado").val(),
                    flUF: $("#flUF").val(),
                    idPais: $("#idPais").val(),
                },
                success: function (result) {
                    if (result.success) {
                       limpaForm()
                       $("#btnFecharModalAddEstado").click();
                       var options = result.novaListaEstados.map(function (el, i) {
                           return $("<option></option>").val(el.idEstado).text(el.nmEstado)
                       });
                        $('#idEstado').html(options);
                       $("#btnAbreModalListaEstados").click();
                    }
                }
            });
        };
    });
    $("#btnAbrirModalAddEstado").on('click', function () {
        $("#btnFecharModalSelecionarEstado").click();
    });
});

function validaEstado() {
    if (!IsNullOrEmpty($("#nmEstado").val())) {
        alert("Digite o nome do Estado!");
        return false;
    } else if (!IsNullOrEmpty($("#flUF").val())) {
        alert("Digite o UF do Estado!");
        return false;
    } else if (!IsNullOrEmpty($("#idPais").val())) {
        alert("Digite o Pais do Estado!");
        return false;
    } else
        return true;
}

function limpaForm() {
    $("#nmEstado").val("");
    $("#flUF").val("");
    $("#idPais").val("");
}