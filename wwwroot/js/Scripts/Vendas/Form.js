var tableFuncionarios = null;
var tableClientes = null;
var tableProdutos = null;
var tableItensVenda = null;
var vlTotal = null;
$(document).ready(function () {

    tableFuncionarios = $("#tbFuncionarios").DataTable({
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.10.24/i18n/Portuguese-Brasil.json',
        },
        columns: [
            { data: "nmFuncionario" },
            { data: "nrCPF" },
            {
                data: null,
                className: "text-center",
                mRender: function (data) {
                    return '<button type="button" class="btn btn-primary text-center selectFuncionario-btn" data-id="' + data.id + '">Selecionar</button>'
                }
            }
        ]
    });

    tableClientes = $("#tbClientes").DataTable({
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.10.24/i18n/Portuguese-Brasil.json',
        },
        columns: [
            { data: "nmCliente" },
            { data: "nrCPFCNPJ" },
            {
                data: null,
                className: "text-center",
                mRender: function (data) {
                    return '<button type="button" class="btn btn-primary text-center selectCliente-btn" data-id="' + data.id + '">Selecionar</button>'
                }
            }
        ]
    });

    tableProdutos = $('#tbProdutos').DataTable({
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.10.24/i18n/Portuguese-Brasil.json',
        },
        columns: [
            {
                data: "idProduto",
                bVisible: false
            },
            { data: "dsProduto" },
            {
                data: "idUnidade",
                bVisible: false
            },
            { data: "dsUnidade" },
            { data: "qtdEstoque" },
            { data: "vlVenda" },
            {
                data: "idFornecedor",
                bVisible: false
            },
            { data: "nmFornecedor" },
            {
                data: null,
                mRender: function (data) {
                    return '<button type="button" class="btn btn-primary text-center selectProduto-btn" data-id="' + data.idProduto + '">Selecionar</button>'
                }
            }
        ]
    });

    tableItensVenda = $("#listaVenda").DataTable({
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.10.24/i18n/Portuguese-Brasil.json',
        },
        data: $("#jsProdutos").val() != "" ? JSON.parse($("#jsProdutos").val()) : "",
        columns: [
            {
                data: "idProduto",
                bVisible: false
            },
            { data: "dsProduto" },
            {
                data: "idUnidade",
                bVisible: false
            },
            { data: "dsUnidade" },
            {
                data: "qtdProduto",
                mRender: function (data) {
                    return parseFloat(data).toFixed(1).replace(".", ",");
                }
            },
            {
                data: "vlVenda",
                mRender: function (data) {
                    return "R$ " + parseFloat(data).toFixed(2).replace(".", ",");
                }
            },
            {
                data: "txDesconto",
                mRender: function (data) {
                    return parseFloat(data).toFixed(2).replace(".", ",") + "%"
                }
            },
            {
                data: "vlTotal",
                mRender: function (data) {
                    return "R$ " + parseFloat(data).toFixed(2).replace(".", ",").trim();
                }
            },
            {
                data: null,
                mRender: function () {
                    return '<a href="#" class="btn btn-sm"><i class="btn btn-danger btn-sm fa fa-trash"></i></a>';
                }
            }
        ],
        footerCallback: function (row, data, start, end, display) {
            let api = this.api();

            // Remove the formatting to get integer data for summation
            let intVal = function (i) {
                return typeof i === 'string'
                    ? i.replace(/[\$,]/g, '') * 1
                    : typeof i === 'number'
                        ? i
                        : 0;
            };

            // Total over all pages
            total = api.column(7).data().reduce((a, b) => intVal(a) + intVal(b), 0);
            vlTotal = total;

            let novoTotal = 'R$ ' + total.toFixed(2).replace(".", ",");
            // Update footer
            api.column().footer().innerHTML = novoTotal;

            $("#vlTotal").val(novoTotal);
            $("#vlTotal").change();
            if (total > 0) {
                $(".divfinalizar").css("display", "");
            } else {
                $("#observacao").val("");
                tableParcelas.clear().draw();
                $(".divfinalizar").css("display", "none");
            }
        }
    });

    tableParcelas = $("#tbParcelas").DataTable({
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.10.24/i18n/Portuguese-Brasil.json',
        },
        data: $("#jsParcelas").val() != "" ? JSON.parse($("#jsParcelas").val()) : "",
        columns: [
            { data: "nrParcela" },
            {
                data: "vlParcela",
                mRender: function (data) {
                    return data.toFixed(2);
                }
            },
            {
                data: "dtVencimento",
                mRender: function (data) {
                    return data.substring(0, 10).split("-").reverse().join("/")
                }
            },
            {
                data: "idFormaPgto",
                bVisible: false
            },
            { data: "dsFormaPgto" }
        ]
    });

    $("#idCliente").on('change select', function () {
        if ($("#idCliente").val()) {
            $.ajax({
                url: "/Clientes/JsCliente",
                data: {
                    idCliente: $("#idCliente").val()
                },
                success: function (result) {
                    if (result) {
                        $("#idCondicaoPgto").val(result.idCondicaoPgto);
                        $("#dsCondicaoPgto").val(result.dsCondicaoPgto);
                        $("#idCondicaoPgto").change();
                    }
                }
            });

            if ($("#idFuncionario").val()) {
                $(".Corpo").css('display', "");
            }
        } else {
            tableProdutos.clear().draw();
            tableParcelas.clear().draw();
            $(".Corpo").css('display', "none");
        }
    });

    $("#idCliente").change();

    $("#idCondicaoPgto").on('change', function () {
        if ($("#idCondicaoPgto").val()) {
            $.ajax({
                url: "/CondicaoPgto/JsGetCondicao",
                data: {
                    idCondicaoPgto: $("#idCondicaoPgto").val()
                },
                success: function (result) {
                    if (result) {
                        $("#txJuros").val(result.txJuros);
                        $("#txMulta").val(result.txMulta);
                        $("#txDesconto").val(result.txDesconto);
                    }
                }
            });
        }

    });

    $("#vlTotal").on('change', function () {
        if (parseFloat(vlTotal) > 0) {
            $("#btnGeraParcelas").prop('disabled', false);
        } else {
            $("#btnGeraParcelas").prop('disabled', true);
        }

        $(this).val() != "" ? $(this).val(parseFloat($(this).val().replace(",", ".").replace("R$", "").trim()).toFixed(2)) : $(this).val(0);
        Vendas.VerificaTotal();
    });

    $("#btnGeraParcelas").on('click', function () {
        if (!$("#listaVenda").DataTable().rows().data().toArray().length > 0) {
            alert("Informe pelo menos um produto a vender!");
        } else if (!$("#idCondicaoPgto").val()) {
            alert("Preencha o Cliente para que seja carregado a condição de pagamento!");
        } else {
            $.ajax({
                url: "/Vendas/MontaParcelas",
                data: {
                    vlTotal: $("#vlTotal").val().replace(",", ".").replace("R$", "").trim(),
                    idCondicaoPgto: $("#idCondicaoPgto").val()
                },
                success: function (result) {
                    tableParcelas.clear().draw();
                    tableParcelas.rows.add(result);
                    tableParcelas.draw();

                    if ($("#tbParcelas").DataTable().rows().data().toArray().length > 0) {
                        $("#btnSubmit").prop('disabled', false);
                    } else {
                        $("#btnSubmit").prop('disabled', true);
                    }

                    $("#jsProdutos").val(JSON.stringify($("#listaVenda").DataTable().rows().data().toArray()));
                    $("#jsParcelas").val(JSON.stringify($("#tbParcelas").DataTable().rows().data().toArray()));
                }
            });
        }
    });

    $("#btnSubmit").on('click', function () {
        $("#vlTotal").val($("#vlTotal").val().replace("R$", "").replace(",", ".").trim());
        $('#formSubmit').submit();
    })

    // ------------------------ FUNCIONARIOS ------------------------
    $("#modalFuncionario").on('show.bs.modal', function () {
        Funcionarios.SelecionarFuncionarios(true);
        Funcionarios.CarregaLista();
    });

    // SELECIONAR   
    $(document).on('click', '.selectFuncionario-btn', function () {
        var id = $(this).data('id');
        $("#idFuncionario").val(id);
        $("#btnFecharModalFuncionario").click();
    });

    $('#modalFuncionario').on('hide.bs.modal', function (e) {
        Funcionarios.SelecionarFuncionarios(false);
    });

    // ------------------------ CLIENTES ------------------------
    $("#modalCliente").on('show.bs.modal', function () {
        Clientes.SelecionarCliente(true);
        Clientes.CarregaLista();
    });

    // SELECIONAR   
    $(document).on('click', '.selectCliente-btn', function () {
        var id = $(this).data('id');
        $("#idCliente").val(id);
        $("#btnFecharModalFuncionario").click();
    });

    $('#modalFuncionario').on('hide.bs.modal', function (e) {
        Clientes.SelecionarClientes(false);
    });

    // ------------------------ PRODUTOS ------------------------
    $("#btnMostraSelecionarProdutos").on('click', function () {
        Produtos.SelecionarProdutos(true);
        Produtos.AddProdutos(false);
        Produtos.CarregaLista();
    });

    $("#btnFechaSelecProd").on('click', function () {
        Produtos.SelecionarProdutos(false);
        Produtos.AddProdutos(true);
    })

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
                if (result.qtdEstoque > 0) {
                    $("#qtdEstoque").val(result.qtdEstoque);
                    $("#idUnidade").val(result.idUnidade);
                    $("#dsUnidade").val(result.dsUnidade);
                    $("#Produto_vlVenda").val("R$ " + parseFloat(result.vlVenda).toFixed(2).replace(".", ","));
                } else {
                    alert("Produto não disponível em estoque!");
                    $("#idProduto").val("");
                }
            }
        });
    });

    // SELECIONAR   
    $(document).on('click', '.selectProduto-btn', function () {
        var id = $(this).data('id');
        $("#idProduto").val(id);        
        Produtos.AddProdutos(true);
        Produtos.SelecionarProdutos(false);
    });

    $('#modalProdutos').on('hide.bs.modal', function (e) {
        Vendas.limpaForm();
    });

    $("#btnConfirmaProd").on('click', function () {
        if (Produtos.validaProduto()) {
            let idProduto = $("#idProduto").val();
            let dsProduto = $("#idProduto option:selected").text();
            let idUnidade = $("#idUnidade").val();
            let dsUnidade = $("#dsUnidade").val();
            let quantidade = parseFloat($("#Produto_qtdProduto").val().replace(",", "."));
            let vlVenda = parseFloat($("#Produto_vlVenda").val().replace(",", ".").replace("R$", "").trim());
            let txDesconto = $("#Produto_txDesconto").val() != "" ? parseFloat($("#Produto_txDesconto").val().replace(",", ".")) : 0;

            var subTotal = quantidade * vlVenda;
            var vlTotal = subTotal - subTotal * txDesconto / 100;

            let produtoVenda = {
                idProduto: idProduto,
                dsProduto: dsProduto,
                idUnidade: idUnidade,
                dsUnidade: dsUnidade,
                qtdProduto: quantidade,
                vlVenda: vlVenda,
                txDesconto: txDesconto != "" ? txDesconto : 0,
                idCliente: $("#idCliente").val(),
                nmCliente: $("#idCliente option:selected").text(),
                vlTotal: vlTotal
            }

            tableItensVenda.row.add(produtoVenda);
            tableItensVenda.draw();
        };

        $("#btnFechaProd").click();
    });

    $("#listaVenda").on('click', '.fa-trash', function () {
        tableItensVenda.row($(this).parents('tr')).remove().draw(false);
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
        $.ajax({
            url: "/Produtos/JsSearch",
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

var Funcionarios = {
    SelecionarFuncionarios(mostra) {
        if (mostra)
            $(".SelecionaFuncionario").css("display", "");
        else
            $(".SelecionaFuncionario").css("display", "none");
    },

    CarregaLista() {
        let url = "/Funcionarios/JsSearch";
        $.ajax({
            url: url,
            success: function (result) {
                tableFuncionarios.clear().draw();
                tableFuncionarios.rows.add(result);
                tableFuncionarios.draw();
            }
        });
    },
};

var Clientes = {
    SelecionarClientes(mostra) {
        if (mostra)
            $(".SelecionaCliente").css("display", "");
        else
            $(".SelecionaCliente").css("display", "none");
    },

    CarregaLista() {
        let url = "/Clientes/JsSearch";
        $.ajax({
            url: url,
            success: function (result) {
                tableClientes.clear().draw();
                tableClientes.rows.add(result);
                tableClientes.draw();
            }
        });
    },
}

var Vendas = {
    limpaForm() {
        $("#idProduto").val("");
        $("#idUnidade").val("");
        $("#Produto_qtdProduto").val("");
        $("#Produto_vlVenda").val("");
        $("#Produto_txDesconto").val("");
    },

    VerificaTotal() {
        let novoTotal = vlTotal;
        $("#vlTotal").val("R$" + parseFloat(novoTotal).toFixed(2).replace(".", ",").trim());
    }
}
