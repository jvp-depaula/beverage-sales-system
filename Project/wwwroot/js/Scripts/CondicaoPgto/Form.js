var tableFormaPgto = null;
var tableParcelas = null;
$(document).ready(function () {

    tableFormaPgto = $('#tbFormaPgto').DataTable({
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.10.24/i18n/Portuguese-Brasil.json',
        },
        columns: [
            { data: "idFormaPgto" },
            { data: "dsFormaPgto" },
            {
                data: null,
                className: "text-center",
                mRender: function (data) {
                    return '<button type="button" class="btn btn-primary text-center selectFormaPgto-btn" data-id="' + data.idFormaPgto + '" data-nm="' + data.dsFormaPgto + '">Selecionar</button>'
                }
            }
        ]
    });

    tableParcelas = $('#tbParcelas').DataTable({
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.10.24/i18n/Portuguese-Brasil.json',
        },
        data: $("#jsParcelas").val() != "" ? JSON.parse($("#jsParcelas").val()) : null,
        columns: [
            { data: "nrParcela" },
            { data: "qtDias" },
            { data: "txPercentual" },
            {
                data: null,
                mRender: function (data) {
                    if (data.idFormaPgto != null)
                        return data.idFormaPgto + ' - ' + data.dsFormaPgto;
                    else
                        return "";
                }
            },
            {
                data: null,
                className: "text-center",
                mRender: function (data) {
                    return '<a class="text-center"><i class="btn btn-danger btn-sm fa fa-trash"></i></a>';
                }
            }
        ]
    });

    $("#btnSubmit").on('click', function () {
        let percentuais = tableParcelas.column(2).data().toArray();
        let txPercentual = 0;
        for (var i = 0; i < percentuais.length; i++) {
            txPercentual += percentuais[i];
        };

        if (parseFloat(txPercentual) != parseFloat(100)) {
            alert("A quantidade das parcelas não fecham 100%");
        } else {
            var jsParcelas = JSON.stringify($("#tbParcelas").DataTable().rows().data().toArray());
            $("#jsParcelas").val(jsParcelas);

            $("#txMulta").val(parseFloat($("#txMulta").val()));
            $("#txDesconto").val(parseFloat($("#txDesconto").val()));
            $("#txJuros").val(parseFloat($("#txJuros").val()));

            $("form").submit();
        }
    });

    // --------------------- FORMA DE PGTO ------------------------
    // SELECIONAR
    $('#modalFormaPgto').on('show.bs.modal', function (e) {
        FormaPgto.mostraSelecionarFormaPgto();
        FormaPgto.CarregaLista();
    });

    $(document).on('click', '.selectFormaPgto-btn', function () {
        var id = $(this).data('id');
        $("#idFormaPgto").val(id);
        $("#idFormaPgto").change();
        FormaPgto.fechaModal();
    });

    // ADICIONAR
    $("#btnSalvarAddFormaPgto").on('click', function () {
        if (FormaPgto.validaForm()) {
            $.ajax({
                url: "/FormaPgto/JsAddFormaPgto",
                data: {
                    dsFormaPgto: $("#dsFormaPgto").val(),
                },
                success: function (result) {
                    if (result.success) {

                        var options = result.novaListaFormaPgto.map(function (el, i) {
                            return $("<option></option>").val(el.idFormaPgto).text(el.dsFormaPgto)
                        });

                        $('#idFormaPgto').html(options);
                        FormaPgto.CarregaLista($("#modalFormaPgto"));
                        FormaPgto.limpaForm();
                    }
                }
            });
        };
        FormaPgto.fechaAddFormaPgto();
    });

    $("#btnAddFormaPgto").on('click', function () {
        FormaPgto.mostraAddFormaPgto();
    });

    $("#btnEscondeAddFormaPgto").on('click', function () {
        FormaPgto.fechaAddFormaPgto();
    });

    // --------------------- PARCELAS ------------------------
    $("#btnAdcParcela").on('click', function () {
        if (Parcelas.validaForm()) {
            var parcelasAdicionadas = tableParcelas.rows().count();

            txPercentual = 0;
            let percentuais = tableParcelas.column(2).data().toArray();

            for (var i = 0; i < percentuais.length; i++) {
                txPercentual += percentuais[i];
            };

            if (txPercentual + parseFloat($("#txPercentual").val().replace(",", ".")) > parseFloat(100)) {
                alert("A soma dos percentuais das parcelas não pode ser maior do que 100%");
            } else if (txPercentual == parseFloat(100)) {
                alert("A soma dos percentuais já é 100%");
            } else {

                var parcela = {
                    nrParcela: parcelasAdicionadas + 1,
                    idFormaPgto: $("#idFormaPgto").val(),
                    dsFormaPgto: $("#idFormaPgto option:selected").text(),
                    qtDias: $("#qtDias").val(),
                    txPercentual: parseFloat($("#txPercentual").val().replace(",", "."))
                };

                tableParcelas.row.add(parcela);
                tableParcelas.draw();
            }
        }
    });

    $("#tbParcelas").on('click', '.fa-trash', function () {
        tableParcelas.row($(this).parents('tr')).remove().draw(false);
    });
});

var FormaPgto = {
    mostraSelecionarFormaPgto() {
        $(".AddFormaPgto").css("display", "none");
        $(".SelecionaFormaPgto").css("display", "");
    },

    mostraAddFormaPgto() {
        $(".AddFormaPgto").css("display", "");
        $(".SelecionaFormaPgto").css("display", "none");
    },

    fechaAddFormaPgto() {
        FormaPgto.limpaForm();
        $(".AddFormaPgto").css("display", "none");
        $(".SelecionaFormaPgto").css("display", "");
    },

    fechaModal() {
        $("#btnFechaModalFormaPgto").click();
    },

    validaForm() {
        if (!$("#dsFormaPgto").val()) {
            alert("Digite a descrição da Forma de Pgto!");
            return false;
        } else
            return true;
    },

    limpaForm() {
        $("#dsFormaPgto").val("");
    },

    CarregaLista() {
        $.ajax({
            url: "/FormaPgto/JsSearch",
            success: function (result) {
                tableFormaPgto.clear().draw();
                tableFormaPgto.rows.add(result);
                tableFormaPgto.draw();
            },
            error: function () {
                alert("Houve um erro na busca das Formas de Pgto");
            }
        });

    },
};

var Parcelas = {
    validaForm() {
        var formaPgto = $("#idFormaPgto option:selected").text().toLowerCase();

        if (!formaPgto) {
            alert("Informe a Forma de Pgto!");
        } else if (!$("#qtDias").val()) {
            alert("Informe os dias do parcelamento!");
            return false;
        } else if (!$("#txPercentual").val()) {
            alert("Informe o percentual da parcela!");
            return false;
        } else {
            return true;
        }
    },
}
