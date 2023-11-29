var tableFormaPgto = null;
var tableParcelas = null;
var nrParcelas = null;
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
        data: $("#jsItens").val(),
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

    $("#dsCondicaoPgto").on('change', function () {
        tableParcelas.clear().draw();
        let condicaoPgto = $("#dsCondicaoPgto").val();
        if (condicaoPgto) {
            let parcelas = condicaoPgto.split(",");
            if (parcelas.length > 1) { 
                $("#idFormaPgto").val($('#idFormaPgto option:contains(prazo)').val());
                $("#idFormaPgto").prop('disabled', true);
                $("#btnModalFormaPgto").prop('disabled', true);
                nrParcelas = parcelas.length;
                $("#qtDias").prop("disabled", false);
                $("#qtDias").val(parcelas[0]);
                $("#txPercentual").prop("disabled", false);
                $("#txPercentual").val("");
            } else {
                $("#idFormaPgto").val($('#idFormaPgto option:contains(vista)').val());
                $("#idFormaPgto").prop('disabled', false);
                $("#btnModalFormaPgto").prop('disabled', false);
                $("#qtDias").val($("#dsCondicaoPgto").val());
                $("#qtDias").prop("disabled", true);
                $("#txPercentual").val("100");
                $("#txPercentual").prop("disabled", true);
                var parcela = {
                    nrParcela: 1,
                    idFormaPgto: $("#idFormaPgto").val(),
                    dsFormaPgto: $("#idFormaPgto option:selected").text(),
                    qtDias: 1,
                    txPercentual: 100
                };
                tableParcelas.clear().draw();
                tableParcelas.row.add(parcela);
                tableParcelas.draw();
            }
        }
    });

    $("#btnSubmit").on('click', function () {
        var jsItens = JSON.stringify($("#tbParcelas").DataTable().rows().data().toArray());
        $("#jsItens").val(jsItens);
        $("form").submit();
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

    $("#idFormaPgto").on('change', function () {
        var text = $("#idFormaPgto option:selected").text();

        if (text) {
            if (text.toLowerCase().includes("prazo")) {
                $("#qtDias").prop("disabled", false);
                $("#txPercentual").prop("disabled", false);
            } else {
                $("#qtDias").prop("disabled", true);
                $("#txPercentual").prop("disabled", true);
            }
        }
    });

    $("#idFormaPgto").change();    

    // --------------------- PARCELAS ------------------------
    $("#btnAdcParcela").on('click', function () {
        if (Parcelas.validaForm()) {
            var formaPgto = $("#idFormaPgto option:selected").text().toLowerCase();
            if (formaPgto.includes("vista")) {
                
            } else {
                var parcelasAdicionadas = tableParcelas.rows().count();

                if (parcelasAdicionadas <= nrParcelas) {
                    var parcela = {
                        nrParcela: tableParcelas.rows().count() + 1,
                        idFormaPgto: $("#idFormaPgto").val(),
                        dsFormaPgto: $("#idFormaPgto option:selected").text(),
                        qtDias: $("#qtDias").val(),
                        txPercentual: $("#txPercentual").val()
                    };                
                    tableParcelas.row.add(parcela);
                    tableParcelas.draw();
                }
            }

            Parcelas.VerificarParcelas();
        }
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
        } else {
            if (formaPgto.includes("prazo")) {
                if (!$("#qtDias").val()) {
                    alert("Informe a referencia do dia da parcela!");
                    return false;
                } else if (!$("#txPercentual").val()) {
                    alert("Informe o percentual da parcela!");
                    return false;
                } else {
                    return true;
                }
            } else if (formaPgto.includes("vista")) {
                return true;
            }
        }        
    },

    VerificarParcelas() {
        if (tableParcelas.rows().count() > 0) {
            $("#dsCondicaoPgto").attr('readonly', 'readonly');
            $("#dsCondicaoPgto").css('background-color', '#dee2e6');
            $("#vlMulta").attr('readonly', 'readonly');
            $("#vlMulta").css('background-color', '#dee2e6');
            $("#vlDesconto").attr('readonly', 'readonly');
            $("#vlDesconto").css('background-color', '#dee2e6');
            $("#vlJuros").attr('readonly', 'readonly');
            $("#vlJuros").css('background-color', '#dee2e6');
        } else {
            $("#dsCondicaoPgto").attr('readonly', '');
            $("#vlMulta").attr('readonly', '');
            $("#vlDesconto").attr('readonly', '');
            $("#vlJuros").attr('readonly', '');
        }
    }
}
