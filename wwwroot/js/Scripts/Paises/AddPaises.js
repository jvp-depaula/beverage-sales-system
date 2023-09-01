$(document).ready(function () {

    $("#btnSalvarAddPais").on('click', function () {
        if (validaPais()) {
            $.ajax({
                url: "/Paises/JsAddPais",
                data: {
                    nmPais: $("#nmPais").val(),
                    sigla: $("#sigla").val(),
                    DDI: $("#DDI").val(),
                },
                success: function (result) {
                    if (result.success) {
                        $("#nmPais").val("");
                        $("#sigla").val("");
                        $("#DDI").val("");
                        $("#btnFecharModalAddPais").click();
                        var options = result.novaListaPaises.map(function (el, i) {
                            return $("<option></option>").val(el.idPais).text(el.nmPais)
                        });
                        $('#idPais').html(options);
                        $("#btnAbreModalListaPaises").click();
                    }
                }
            });
        }
    });
    $("#btnAbrirModalAddPais").on('click', function () {
        $("#btnFecharModalSelecionarPais").click();
    });
});
function validaPais() {
    if (!IsNullOrEmpty($("#nmPais").val())) {
        alert("Digite o nome do País!");
        return false;
    } else if (!IsNullOrEmpty($("#sigla").val())) {
        alert("Digite a sigla do País!");
        return false;
    } else if (!IsNullOrEmpty($("#DDI").val())) {
        alert("Digite o DDI do País!");
        return false;
    } else
        return true;
}