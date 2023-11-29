var tableFornecedores = null;
var tableProdutos = null;
var tableItensCompra = null;
var tableCondicaoPgto = null;
$(document).ready(function () {

    tableFornecedores = $("#tbFornecedores").DataTable({
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.10.24/i18n/Portuguese-Brasil.json',
        },
        columns: [
            { data: "nmFornecedor" },
            { data: "nrCNPJ" },
            {
                data: null,
                className: "text-center",
                mRender: function (data) {
                    return '<button type="button" class="btn btn-primary text-center selectFornecedor-btn" data-id="' + data.id + '">Selecionar</button>'
                }
            }
        ]
    });

    tableProdutos = $('#tbProdutos').DataTable({
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.10.24/i18n/Portuguese-Brasil.json',
        },
        columns: [
            { data: "dsProduto" },
            { data: "dsUnidade" },
            { data: "qtdEstoque" },
            { data: "vlUltCompra" },
            { data: "nmFornecedor" },
            {
                data: null,
                mRender: function (data) {
                    return '<button type="button" class="btn btn-primary text-center selectProduto-btn" data-id="' + data.idProduto + '" data-unidade="' + data.dsUnidade + '">Selecionar</button>'
                }
            },            
        ]
    });

    tableItensCompra = $("#listaCompra").DataTable({
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.10.24/i18n/Portuguese-Brasil.json',
        },
        columns: [
            {
                data: null,
                mRender: function (data) {
                    return data.idProduto + " - " + data.dsProduto;
                }
            },
            {
                data: null,
                mRender: function (data) {
                    return data.idUnidade + " - " + data.dsUnidade;
                }
            },
            { data: "qtdProduto" },
            { data: "vlCompra" },
            {
                data: "txDesconto",
                mRender: function (data) {
                    return data + "%";
                }
            },
            {
                data: null,
                mRender: function (data) {
                    return data.idFornecedor + " - " + data.nmFornecedor;
                }
            },
            {
                data: null,
                mRender: function (data) {
                    return data.totalProduto - (data.totalProduto * data.txDesconto/100);
                }
            },
            {
                data: null,
                mRender: function (data) {
                    return '<a class="text-center"><i class="btn btn-danger btn-sm fa fa-trash"></i></a>'                    
                }
            },
        ]
    });

    tableCondicaoPgto = $("#tbCondicaoPgto").DataTable({
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.10.24/i18n/Portuguese-Brasil.json',
        },
        columns: [
            { data: "idCondicaoPgto" },
            { data: "dsCondicaoPgto" },
            { data: "vlMulta" },
            { data: "vlDesconto" },
            { data: "vlJuros" },
            {
                data: null,
                className: "text-center",
                mRender: function (data) {
                    return '<button type="button" class="btn btn-primary text-center selectCondicaoPgto-btn" data-id="' + data.idCondicaoPgto + '">Selecionar</button>'
                }
            }
        ]
    });

    // --------------------- CONDICAO PGTO ------------------------
    // SELECIONAR
    $('#modalCondicoes').on('show.bs.modal', function (e) {
        CondicoesPgto.mostraSelecionarCondicoes();
        CondicoesPgto.CarregaLista($(this));
    });

    $(document).on('click', '.selectCondicaoPgto-btn', function () {
        var id = $(this).data('id');
        $("#idCondicaoPgto").val(id);
        CondicoesPgto.fechaModal();
    });

    // ADICIONAR
    $("#btnSalvarAddCondicao").on('click', function () {
        if (CondicoesPgto.validaForm()) {
            $.ajax({
                url: "/CondicaoPgto/JsAddCondicao",
                data: {
                    dsCondicaoPgto : $("#dsCondicaoPgto").val(),
                    vlMulta: $("#vlMulta").val(),
                    vlDesconto : $("#vlDesconto").val(),
                    vlJuros : $("#vlJuros").val(),
                },
                success: function (result) {
                    if (result.success) {

                        var options = result.novaListaCondicoes.map(function (el, i) {
                            return $("<option></option>").val(el.idCondicaoPgto).text(el.dsCondicaoPgto)
                        });

                        $('#idCondicaoPgto').html(options);
                        CondicoesPgto.CarregaLista($("#modalCondicoes"));
                        CondicoesPgto.limpaForm();
                    }
                }
            });
        };
        CondicoesPgto.fechaAddCondicoes();
    });
    $("#btnAddCondicao").on('click', function () {
        CondicoesPgto.mostraAddCondicoes();
    });
    $("#btnEscondeAddCondicao").on('click', function () {
        CondicoesPgto.fechaAddCondicoes();
    });

// ------------------------ FORNECEDORES ------------------------
    $("#modalFornecedor").on('show.bs.modal', function () {
        Fornecedores.SelecionarFornecedores(true);
        Fornecedores.CarregaLista();
    });

    // SELECIONAR   
    $(document).on('click', '.selectFornecedor-btn', function () {
        var id = $(this).data('id');
        $("#idFornecedor").val(id);
        $("#btnFecharModalFornecedor").click();
    });

    $('#modalFornecedor').on('hide.bs.modal', function (e) {
        Fornecedores.SelecionarFornecedores(false);
    });

    // ------------------------ PRODUTOS ------------------------
    $("#btnMostraSelecionarProdutos").on('click', function () {
        Produtos.SelecionarProdutos(true);
        Produtos.AddProdutos(false);
        Produtos.CarregaLista();
    });

    $("#modalProdutos").on('show.bs.modal', function () {
        $("#idProduto").val("");
    })

    $("#idProduto").on('change select', function () {
        let url = "/Produtos/jsGetProduto";
        $.ajax({
            url: url,
            data: {
                idProduto: $("#idProduto").val()
            },
            success: function (result) {
                $("#idUnidade").val(result.idUnidade);
            }
        });
    });

    // SELECIONAR   
    $(document).on('click', '.selectProduto-btn', function () {
        var id = $(this).data('id');
        var estoque = $(this).data('qtdestoque');
        $("#idProduto").val(id);
        $("#vlMaxEstoque").val(estoque);
        Produtos.AddProdutos(true);
        Produtos.SelecionarProdutos(false);         
    });

    $('#modalProdutos').on('hide.bs.modal', function (e) {
        Compras.limpaForm();
    });

    $("#btnConfirmaProd").on('click', function () {
        if (Produtos.validaProduto()) {
            let idProduto = $("#idProduto").val();
            let dsProduto = $("#idProduto option:selected").text();
            let idUnidade = $("#idUnidade option:selected").val();
            let dsUnidade = $("#idUnidade option:selected").text();
            let quantidade = parseFloat($("#Produto_qtdProduto").val().replace(",", "."));
            let vlCompraUnitario = parseFloat($("#Produto_vlVenda").val().replace(",", "."));
            let txDesconto = parseFloat($("#Produto_txDesconto").val().replace(",", "."));
            let totalProduto = vlCompraUnitario * quantidade;

            let produtoCompra = {
                idProduto: idProduto,
                dsProduto: dsProduto,
                idUnidade: idUnidade,
                dsUnidade: dsUnidade,
                qtdProduto: quantidade,
                vlCompra: vlCompraUnitario,
                txDesconto: txDesconto ?? 0,
                idFornecedor: $("#idFornecedor").val(),
                nmFornecedor: $("#idFornecedor option:selected").text(),
                totalProduto: totalProduto
            }

            tableItensCompra.row.add(produtoCompra);
            tableItensCompra.draw();
        };

        $("#btnFechaProd").click();
    });

    $("#listaCompra").on('click', '.fa-trash', function () {
        tableItensCompra.row($(this).parents('tr')).remove().draw(false);            
    });
});

var Produtos = {
    AddProdutos(mostra) {
        if (mostra) 
            $(".addProduto").css('display', "");
        else
            $(".addProduto").css('display', "none");
    },

    SelecionarProdutos(mostra) {
        if (mostra)
            $(".SelecionaProduto").css("display", "");
        else
            $(".SelecionaProduto").css("display", "none");
    }, 

    CarregaLista() {
        let url = "/Produtos/JsSearch";
        $.ajax({
            url: url,
            success: function (result) {
                tableProdutos.clear().draw();
                tableProdutos.rows.add(result);
                tableProdutos.draw();
            }
        });
    },

    validaProduto() {
        if (!($("#idProduto").val())) {
            alert("Informe o Produto a comprar!");
            return false;
        } else if (!($("#Produto_qtdProduto").val())) {
            alert("Informe a quantidade a comprar!");
            return false;
        } else if (!($("#Produto_vlVenda").val())) {
            alert("Informe o valor unitário!");
            return false
        } else {
            return true;
        };
    }
};

var Fornecedores = {
    SelecionarFornecedores(mostra) {
        if (mostra)
            $(".SelecionaFornecedor").css("display", "");
        else
            $(".SelecionaFornecedor").css("display", "none");
    },

    CarregaLista() {
        let url = "/Fornecedores/JsSearch";
        $.ajax({
            url: url,
            success: function (result) {
                tableFornecedores.clear().draw();
                tableFornecedores.rows.add(result);
                tableFornecedores.draw();
            }
        });
    },
};

var CondicoesPgto = {
    mostraSelecionarCondicoes() {
        $(".AddCondicoes").css("display", "none");
        $(".SelecionaCondicoes").css("display", "");
    },

    mostraAddCondicoes() {
        $(".AddCondicoes").css("display", "");
        $(".SelecionaCondicoes").css("display", "none");
    },

    fechaAddCondicoes() {
        CondicoesPgto.limpaForm();
        $(".AddCondicoes").css("display", "none");
        $(".SelecionaCondicoes").css("display", "");
    },

    fechaModal() {
        $("#btnFechaModalCondicao").click();
    },

    validaForm() {
        if (!$("#dsCondicaoPgto").val()) {
            alert("Digite a descrição da Condição de Pgto!");
            return false;
        } else if (!$("#vlMulta").val()) {
            alert("Digite o valor da multa!");
            return false;
        } else if (!$("#vlDesconto").val()) {
            alert("Digite o valor do desconto!");
            return false;
        } else if (!$("#vlJuros").val()) {
            alert("Digite o valor do juros!");
            return false;
        } else
            return true;
    },

    limpaForm() {
        $("#dsCondicaoPgto").val("");
        $("#vlMulta").val("");
        $("#vlDesconto").val("");
        $("#vlJuros").val("");
    },

    CarregaLista(modal) {
        $.ajax({
            url: "/CondicaoPgto/JsSearch",
            success: function (result) {
                tableCondicaoPgto.clear().draw();
                tableCondicaoPgto.rows.add(result);
                tableCondicaoPgto.draw();
            }
        });
    }
};

var Compras = {
    limpaForm() {
        $("#idProduto").val("");
        $("#idUnidade").val("");
        $("#Produto_qtdProduto").val("");
        $("#Produto_vlVenda").val("");
        $("#Produto_txDesconto").val("");
    }
}
