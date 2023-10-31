$(document).ready(function () {
    $("#nrModelo, #nrSerie, #nrNota, #idFornecedor").on('change', function () {
        if (!IsNullOrEmpty($("#nrModelo").val()) && !IsNullOrEmpty($("#nrSerie").val()) &&
            !IsNullOrEmpty($("#nrNota").val()) && !IsNullOrEmpty(("#idFornecedor").val())) {
            Cabecalho.VerificaNota();
        };
    });
});

var Cabecalho = {
    VerificaNota(nrModelo, nrNota, nrSerie, idFornecedor) {
        $.ajax({
            url: "/NotaFiscalEntrada/JsVerificaNota",
            method: "GET",
            data: {
                nrModelo: nrModelo,
                nrNota: nrNota,
                nrSerie: nrSerie,
                idFornecedor: idFornecedor
            },
            success: function (result) {
                if (result.success) {
                    $('.CabecalhoNota').children().attr("disabled", "disabled");
                    $(".Corpo").css('display', 'auto');
                } else {
                    alert("Ocorreu um erro na busca da nota!");
                }
            }
        });
    },
}