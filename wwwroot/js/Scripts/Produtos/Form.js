$(document).ready(function () {
// ------------------------ MARCAS ------------------------
    $("#btnAbreModalMarca").on('click', function () {
        Marcas.SelecionarMarcas(true);
        Marcas.AddMarcas(false);
        Marcas.CarregaLista();
    });

    // SELECIONAR   
    $(document).on('click', '.selectMarca-btn', function () {
        var id = $(this).data('value');
        $("#idMarca").val(id);
        $("#btnFecharModalMarca").click();
    });

    // ADICIONAR
    $("#btnSalvarMarca").on('click', function () {
        if (Marcas.validaForm()) {
            $.ajax({
                url: "/Marcas/JsAddMarca",
                data: {
                    nmMarca: $("#nmMarca").val(),
                },
                success: function (result) {
                    if (result.success) {

                        var options = result.novaListaMarcas.map(function (el, i) {
                            return $("<option></option>").val(el.idMarca).text(el.nmMarca)
                        });
                        $('#idMarca').html(options);

                        Marcas.limpaForm();
                        Marcas.AddMarcas(false);
                        Marcas.SelecionarMarcas(true);

                        Marcas.CarregaLista();

                    } else {
                        alert("Ocorreu um erro!");
                    }
                }
            });
        };
    });

    $("#btnMostrarAddMarca").on('click', function () {
        Marcas.SelecionarMarcas(false);
        Marcas.AddMarcas(true);
        $("#txtTituloMarca").html("Adicionar Marca");
    });

    $("#btnFecharAddMarca").on('click', function () {
        Marcas.limpaForm();
        Marcas.AddMarcas(false);
        Marcas.SelecionarMarcas(true);
        $("#txtTituloMarca").html("Lista de Marcas");
    });

    $('#modalMarca').on('hide.bs.modal', function (e) {
        Marcas.limpaForm();
        Marcas.AddMarcas(false);
        Marcas.SelecionarMarcas(false);
    });

// ------------------------ UNIDADES ------------------------
    $("#btnAbreModalUnidade").on('click', function () {
        Unidades.SelecionarUnidades(true);
        Unidades.AddUnidades(false);
        Unidades.CarregaLista();
    });

    // SELECIONAR
    $(document).on('click', '.selectUnidade-btn', function () {
        var id = $(this).data('value');
        $("#idUnidade").val(id);
        $("#btnFecharModalUnidade").click();
    });

    // ADICIONAR
    $("#btnSalvarUnidade").on('click', function () {
        if (Unidades.validaForm()) {
            $.ajax({
                url: "/Unidades/JsAddUnidade",
                data: {
                    dsUnidade: $("#dsUnidade").val(),
                    sigla: $("#sigla").val(),
                },
                success: function (result) {
                    if (result.success) {

                        var options = result.novaListaUnidades.map(function (el, i) {
                            return $("<option></option>").val(el.idUnidade).text(el.dsUnidade)
                        });
                        $('#idUnidade').html(options);

                        Unidades.limpaForm();
                        Unidades.AddUnidades(false);
                        Unidades.SelecionarUnidades(true);
                        Unidades.CarregaLista();
                    } else {
                        alert("Ocorreu um erro!");
                    }
                }
            });
        };
    });

    $("#btnMostrarAddUnidade").on('click', function () {
        Unidades.SelecionarUnidades(false);
        Unidades.AddUnidades(true);
        $("#txtTituloUnidade").html("Adicionar Unidade");
    });

    $("#btnFecharAddUnidade").on('click', function () {
        Unidades.limpaForm();
        Unidades.AddUnidades(false);
        Unidades.SelecionarUnidades(true);
        $("#txtTituloUnidade").html("Lista de Unidades");
    });

    $('#modalUnidade').on('hide.bs.modal', function (e) {
        Unidades.limpaForm();
        Unidades.AddUnidades(false);
        Unidades.SelecionarUnidades(false);
    });

// ------------------------ CATEGORIAS ------------------------
    $("#btnAbreModalCategoria").on('click', function () {
        Categorias.SelecionarCategorias(true);
        Categorias.AddCategorias(false);
        Categorias.CarregaLista();
    });

    // SELECIONAR
    $(document).on('click', '.selectCategoria-btn', function () {
        var id = $(this).data('value');
        $("#idCategoria").val(id);
        $("#btnFecharModalCategoria").click();
    });

    // ADICIONAR
    $("#btnSalvarCategoria").on('click', function () {
        if (Categorias.validaForm()) {
            $.ajax({
                url: "/Categorias/JsAddCategoria",
                data: {
                    nmCategoria: $("#nmCategoria").val(),
                },
                success: function (result) {
                    if (result.success) {

                        var options = result.novaListaCategorias.map(function (el, i) {
                            return $("<option></option>").val(el.idCategoria).text(el.nmCategoria)
                        });
                        $('#idCategoria').html(options);

                        Categorias.limpaForm();
                        Categorias.AddCategorias(false);
                        Categorias.SelecionarCategorias(true);
                        Categorias.CarregaLista();
                    } else {
                        alert("Ocorreu um erro!");
                    }
                }
            });
        };
    });

    $("#btnMostrarAddCategoria").on('click', function () {
        Categorias.SelecionarCategorias(false);
        Categorias.AddCategorias(true);
        $("#txtTituloCategoria").html("Adicionar Categoria");
    });

    $("#btnFecharAddCategoria").on('click', function () {
        Categorias.limpaForm();
        Categorias.AddCategorias(false);
        Categorias.SelecionarCategorias(true);
        $("#txtTituloCategoria").html("Lista de Categorias");
    });

    $('#modalCategoria').on('hide.bs.modal', function (e) {
        Categorias.limpaForm();
        Categorias.AddCategorias(false);
        Categorias.SelecionarCategorias(false);
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
});

var Marcas = {
    SelecionarMarcas(mostra) {
        if (mostra)
            $(".SelecionaMarca").css("display", "");
        else
            $(".SelecionaMarca").css("display", "none");
    },

    AddMarcas(mostra) {
        if (mostra)
            $(".AddMarca").css("display", "");
        else {
            $(".AddMarca").css("display", "none");
        }
    },

    validaForm() {
        if (!$("#nmMarca").val()) {
            alert("Digite o nome da Marca!");
            return false;
        } else
            return true;
    },

    limpaForm() {
        $("#nmMarca").val("");
    },

    CarregaLista() {
        let modal = $("#modalMarca");
        let url = "/Marcas/JsSearch";
        $.ajax({
            url: url,
            success: function (result) {
                var tbody = modal.find('#bodyMarcas');
                tbody.empty();
                result.forEach(function (marcas) {
                    tbody.append(
                        `
                        <tr>
                            <td scope="row">${marcas.idMarca}</td>
                            <td>${marcas.nmMarca}</td>
                            <td style="text-align: right">
                            <button type="button" class="btn btn-sm btn-primary selectMarca-btn" data-value="${marcas.idMarca}" data-name="${marcas.nmMarca}">
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

var Unidades = {
    SelecionarUnidades(mostra) {
        if (mostra)
            $(".SelecionaUnidade").css("display", "");
        else
            $(".SelecionaUnidade").css("display", "none");
    },

    AddUnidades(mostra) {
        if (mostra)
            $(".AddUnidade").css("display", "");
        else {
            $(".AddUnidade").css("display", "none");
        }
    },

    validaForm() {
        if (!$("#dsUnidade").val()) {
            alert("Digite a descrição da Unidade!");
            return false;
        } else if (!$("#sigla").val()) {
            alert("Digite a sigla da Unidade!");
            return false;
        } else
            return true;
    },

    limpaForm() {
        $("#dsUnidade").val("");
        $("#sigla").val("");
    },

    CarregaLista() {
        let modal = $("#modalUnidade");
        let url = "/Unidades/JsSearch";
        $.ajax({
            url: url,
            success: function (result) {
                var tbody = modal.find('#bodyUnidades');
                tbody.empty();
                result.forEach(function (unidades) {
                    tbody.append(
                        `
                        <tr>
                            <td scope="row">${unidades.dsUnidade}</td>
                            <td>${unidades.sigla}</td>
                            <td style="text-align: right">
                            <button type="button" class="btn btn-sm btn-primary selectUnidade-btn" data-value="${unidades.idUnidade}" data-name="${unidades.dsUnidade}">
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

var Categorias = {
    SelecionarCategorias(mostra) {
        if (mostra)
            $(".SelecionaCategoria").css("display", "");
        else
            $(".SelecionaCategoria").css("display", "none");
    },

    AddCategorias(mostra) {
        if (mostra)
            $(".AddCategoria").css("display", "");
        else {
            $(".AddCategoria").css("display", "none");
        }
    },

    validaForm() {
        if (!$("#nmCategoria").val()) {
            alert("Digite o nome da Categoria!");
            return false;
        } else
            return true;
    },

    limpaForm() {
        $("#nmCategoria").val("");
    },

    CarregaLista() {
        let modal = $("#modalCategoria");
        let url = "/Categorias/JsSearch";
        $.ajax({
            url: url,
            success: function (result) {
                var tbody = modal.find('#bodyCategorias');
                tbody.empty();
                result.forEach(function (categorias) {
                    tbody.append(
                        `
                        <tr>
                            <td scope="row">${categorias.idCategoria}</td>
                            <td scope="row">${categorias.nmCategoria}</td>
                            <td style="text-align: right">
                            <button type="button" class="btn btn-sm btn-primary selectCategoria-btn" data-value="${categorias.idCategoria}" data-name="${categorias.nmCategoria}">
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