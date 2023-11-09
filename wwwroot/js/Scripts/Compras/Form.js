$(document).ready(function () {

    $('#tbProdutos').DataTable({
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.10.24/i18n/Portuguese-Brasil.json',
        }
    });

    // --------------------- CONDICAO PGTO ------------------------
    // SELECIONAR
    $('#modalCondicoes').on('show.bs.modal', function (e) {
        CondicoesPgto.mostraSelecionarCondicoes();
        CondicoesPgto.CarregaLista($(this));
    });

    $(document).on('click', '.selectCondicao-btn', function () {
        var id = $(this).data('value');
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
    $("#btnAbreModalFornecedor").on('click', function () {
        Fornecedores.SelecionarFornecedores(true);
        Fornecedores.CarregaLista();
    });

    // SELECIONAR   
    $(document).on('click', '.selectFornecedor-btn', function () {
        var id = $(this).data('value');
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

    // SELECIONAR   
    $(document).on('click', '.selectProduto-btn', function () {
        var id = $(this).data('value');
        var estoque = $(this).data('qtdestoque');
        let vlUnit = $(this).data('vlunit');
        $("#idProduto").val(id);
        $("#vlMaxEstoque").val(estoque);
        Produtos.AddProdutos(true);
        Produtos.SelecionarProdutos(false);         
    });

    $('#modalProdutos').on('hide.bs.modal', function (e) {
        Produtos.SelecionarProdutos(false);
        Produtos.AddProdutos(true);
    });

    $("#btnConfirmaProd").on('click', function () {
        if (Produtos.validaProduto()) {
            let dsProduto = "";
            let dsUnidade = "";
            let quantidade = "";
            let vlCompra = "";
            let txDesconto = "";
            let totalProduto = "";
            let totalCompras = 0;

            var tbody = modal.find('#tblProduto');
            tbody.append(
                `
                    <tr>
                        <td scope="row">${dsProduto}</td>
                        <td scope="row">${dsUnidade}</td>
                        <td scope="row">${quantidade}</td>
                        <td scope="row">${vlCompra}</td>
                        <td scope="row">${txDesconto}</td>
                        <td scope="row">${totalCompras}</td>
                        <td style="text-align: right">
                        <button type="button" class="btn btn-sm btn-danger deleteProduto-btn">
                            Remover
                        </button>
                        <button type="button" class="btn btn-sm btn-info editProduto-btn">
                            Editar
                        </button>
                        </td>
                    </tr>
                `
            );            
        }
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
        let modal = $("#modalProdutos");
        let url = "/Produtos/JsSearch";
        $.ajax({
            url: url,
            success: function (result) {
                var tbody = modal.find('#bodyProdutos');
                tbody.empty();
                result.forEach(function (produtos) {
                    tbody.append(
                        `
                        <tr>
                            <td scope="row">${produtos.idProduto}</td>
                            <td scope="row">${produtos.dsProduto}</td>
                            <td scope="row">${produtos.qtdEstoque}</td>
                            <td scope="row">${produtos.vlVenda}</td>
                            <td scope="row">${produtos.nmFornecedor}</td>
                            <td style="text-align: right">
                            <button type="button" class="btn btn-sm btn-primary selectProduto-btn" data-value="${produtos.idProduto}" data-name="${produtos.dsProduto}" data-qtdEstoque="${produtos.qtdEstoque}" data-vlUnit="${produtos.vlVenda}">
                                Selecionar
                            </button>
                            </td>
                        </tr>
                        `
                    );
                });
            }
        });
    },

    validaProduto() {
        if (IsNullOrEmpty($("#idProduto").val())) {
            alert("Informe o Produto a comprar!");
            return false;
        } else if (IsNullOrEmpty($("#Produto_qtdProduto").val())) {
            alert("Informe a quantidade a comprar!");
            return false;
        } else if (IsNullOrEmpty($("#Produto_vlVenda").val())) {
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
        let modal = $("#modalFornecedor");
        let url = "/Fornecedores/JsSearch";
        $.ajax({
            url: url,
            success: function (result) {
                var tbody = modal.find('#bodyFornecedor');
                tbody.empty();
                result.forEach(function (fornecedores) {
                    tbody.append(
                        `
                        <tr>
                            <td scope="row">${fornecedores.id}</td>
                            <td scope="row">${fornecedores.nmFornecedor}</td>
                            <td style="text-align: right">
                            <button type="button" class="btn btn-sm btn-primary selectFornecedor-btn" data-value="${fornecedores.id}" data-name="${fornecedores.nmFornecedor}">
                                Selecionar
                            </button>
                            </td>
                        </tr>
                        `
                    );
                });
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
                var tbody = modal.find('#bodyCondicaoPgto');
                tbody.empty();
                result.forEach(function (condicoes) {
                    tbody.append(
                        `
                        <tr>
                            <td scope="row">${condicoes.idCondicaoPgto}</td>
                            <td>${condicoes.dsCondicaoPgto}</td>
                            <td>${condicoes.vlMulta}</td>
                            <td>${condicoes.vlDesconto}</td>
                            <td>${condicoes.vlJuros}</td>
                            <td style="text-align: right">
                            <button type="button" class="btn btn-sm btn-primary selectCondicao-btn" data-value="${condicoes.idCondicaoPgto}" data-name="${condicoes.dsCondicaoPgto}">
                                Selecionar
                            </button>
                            </td>
                        </tr>
                        `
                    );
                });
            }
        });
    }
};
