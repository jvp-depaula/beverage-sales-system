var tableFornecedores = null;
var tableProdutos = null;
var tableItensCompra = null;
var tableCondicaoPgto = null;
var vlTotal = null;
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

    tableItensCompra = $("#listaCompra").DataTable({
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
            {
                data: "qtdProduto",
                mRender: function (data) {
                    return parseFloat(data).toFixed(1).replace(".", ",");
                }
            },
            {
                data: "vlCompra",
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
                    return "R$ " + parseFloat(data).toFixed(2).replace(".", ",");
                }
            },
            {
                data: null,
                mRender: function() {
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
                $("#btnSubmit").prop('disabled', false);
            } else {
                $("#observacao").val("");
                $("#vlFrete").val("");
                $("#vlSeguro").val("");
                $("#vlDespesas").val("");
                tableParcelas.clear().draw();
                $(".divfinalizar").css("display", "none");
                $("#btnSubmit").prop('disabled', true);
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

    $("#idFornecedor").on('change', function () {
        if ($("#idFornecedor").val()) {
            $.ajax({
                url: "/Fornecedores/JsFornecedor",
                data: {
                    idFornecedor: $("#idFornecedor").val()
                },
                success: function (result) {
                    if (result) {
                        $("#idCondicaoPgto").val(result.idCondicaoPgto);                        
                        $("#dsCondicaoPgto").val(result.dsCondicaoPgto);                        
                    }
                }
            });
        }        
    });

    $("#idFornecedor").change();

    $("#btnValidaNF").on('click', function () {
        if (!$("#nrModelo").val()) {
            alert("Preencha o número do modelo da nota!");
        } else if (!$("#nrSerie").val()) {
            alert("Preencha o número de série da nota!");
        } else if (!$("#nrNota").val()) {
            alert("Preencha o número da nota!");
        } else if (!$("#idFornecedor").val()) {
            alert("Preencha fornecedor da nota!");
        } else {
            $.ajax({
                url: "/Compras/VerificaNota",
                data: {
                    idFornecedor: $("#idFornecedor").val(),
                    nrModelo: $("#nrModelo").val(),
                    nrSerie: $("#nrSerie").val(),
                    nrNota: $("#nrNota").val()
                },
                success: function (result) {
                    if (result.type == "success") {
                        $(".Corpo").css("display", "");
                    } else {
                        $(".Corpo").css("display", "none");
                        alert(result.msg);
                    };
                }
            });
        }
    })

    $("#dtEmissao").on('keyup change', function () {
        $(this).mask("99/99/9999");
    });

    $("#dtEntrega").on('keyup change', function () {
        $(this).mask("99/99/9999");
    });

    $("#vlTotal").on('change', function () {
        $(this).val() != "" ? $(this).val(parseFloat($(this).val().replace(",", ".").replace("R$", "")).toFixed(2)) : $(this).val(0);
        Compras.VerificaTotal();
    });

    $("#vlFrete").on('change', function () {
        $(this).val() != "" ? $(this).val(parseFloat($(this).val().replace(",", ".")).toFixed(2)) : $(this).val(0);
        Compras.VerificaTotal();
    });

    $("#vlSeguro").on('change', function () {
        $(this).val() != "" ? $(this).val(parseFloat($(this).val().replace(",", ".")).toFixed(2)) : $(this).val(0);
        Compras.VerificaTotal();
    });

    $("#vlDespesas").on('change', function () {
        $(this).val() != "" ? $(this).val(parseFloat($(this).val().replace(",", ".")).toFixed(2)) : $(this).val(0);
        Compras.VerificaTotal();
    });

    $("#vlTotal").on('change', function () {
        if (parseFloat(vlTotal) > 0) {
            $("#btnGeraParcelas").prop('disabled', false);
            // MOSTRAR DIV PARCELAS
        } else {
            $("#btnGeraParcelas").prop('disabled', true);
            // ESCONDER DIV PARCELAS
        }
    });

    $("#btnGeraParcelas").on('click', function () {
        if (!$("#dtEmissao").val()) {
            alert("Informe a data de emissão da nota!");
        } else if (!$("#listaCompra").DataTable().rows().data().toArray().length > 0) {
            alert("Informe pelo menos um produto a comprar!");
        } else if (!$("#idCondicaoPgto").val()) {
            alert("Preencha o Fornecedor para que seja carregado a condição de pagamento!");
        } else {
            $.ajax({
                url: "/Compras/MontaParcelas",
                data: {
                    dtEmissao: $("#dtEmissao").val(),
                    vlTotal: $("#vlTotal").val().replace(",", ".").replace("R$", ""),
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

                    $("#jsProdutos").val(JSON.stringify($("#listaCompra").DataTable().rows().data().toArray()));
                    $("#jsParcelas").val(JSON.stringify($("#tbParcelas").DataTable().rows().data().toArray()));
                }
            });            
        }      
    });

    $("#btnSubmit").on('click', function () {
        $("#vlFrete").val($("#vlFrete").val().replace("R$", "").replace(",", "."));
        $("#vlSeguro").val($("#vlSeguro").val().replace("R$", "").replace(",", "."));
        $("#vlDespesas").val($("#vlDespesas").val().replace("R$", "").replace(",", "."));
        $("#vlTotal").val($("#vlTotal").val().replace("R$", "").replace(",", "."));
        $('form').submit();
    })

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
                $("#idUnidade").val(result.idUnidade);
                $("#dsUnidade").val(result.dsUnidade);
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
            let idUnidade = $("#idUnidade").val();
            let dsUnidade = $("#dsUnidade").val();
            let quantidade = parseFloat($("#Produto_qtdProduto").val().replace(",", "."));
            let vlCompraUnitario = parseFloat($("#Produto_vlVenda").val().replace(",", "."));
            let txDesconto = $("#Produto_txDesconto").val() != "" ? parseFloat($("#Produto_txDesconto").val().replace(",", ".")) : 0;

            var subTotal = quantidade * vlCompraUnitario;
            var vlTotal = subTotal - subTotal * txDesconto / 100;

            let produtoCompra = {
                idProduto: idProduto,
                dsProduto: dsProduto,
                idUnidade: idUnidade,
                dsUnidade: dsUnidade,
                qtdProduto: quantidade,
                vlCompra: vlCompraUnitario,
                txDesconto: txDesconto != "" ? txDesconto : 0,
                idFornecedor: $("#idFornecedor").val(),
                nmFornecedor: $("#idFornecedor option:selected").text(),
                vlTotal: vlTotal
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

var Compras = {
    limpaForm() {
        $("#idProduto").val("");
        $("#idUnidade").val("");
        $("#Produto_qtdProduto").val("");
        $("#Produto_vlVenda").val("");
        $("#Produto_txDesconto").val("");
    },

    VerificaTotal() {
        
        vlFrete = $("#vlFrete").val() != "" ? parseFloat($("#vlFrete").val()) : 0;
        vlSeguro = $("#vlSeguro").val() != "" ? parseFloat($("#vlSeguro").val()) : 0;
        vlDespesas = $("#vlDespesas").val() != "" ? parseFloat($("#vlDespesas").val()) : 0;

        let novoTotal = vlTotal + vlFrete + vlSeguro + vlDespesas;

        $("#vlTotal").val("R$ " + parseFloat(novoTotal).toFixed(2).replace(".", ","));
    }
}
