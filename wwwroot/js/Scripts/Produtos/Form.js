var tableMarcas = null;
var tableUnidades = null;
var tableCategorias = null;
var tableFornecedores = null;
$(document).ready(function () {

    tableMarcas = $("#tbMarcas").DataTable({
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.10.24/i18n/Portuguese-Brasil.json',
        },
        columns: [
            { data: "idMarca" },
            { data: "nmMarca" },
            {
                data: null,
                className: "text-center",
                mRender: function (data) {
                    return '<button type="button" class="btn btn-primary text-center selectMarca-btn" data-id="' + data.idMarca + '">Selecionar</button>'
                }
            }
        ]
    });

    tableUnidades = $("#tbUnidades").DataTable({
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.10.24/i18n/Portuguese-Brasil.json',
        },
        columns: [
            { data: "dsUnidade" },
            { data: "sigla" },
            {
                data: null,
                className: "text-center",
                mRender: function (data) {
                    return '<button type="button" class="btn btn-primary text-center selectUnidade-btn" data-id="' + data.idUnidade + '">Selecionar</button>'
                }
            }
        ]
    });

    tableCategorias = $("#tbCategorias").DataTable({
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.10.24/i18n/Portuguese-Brasil.json',
        },
        columns: [
            { data: "idCategoria" },
            { data: "nmCategoria" },
            {
                data: null,
                className: "text-center",
                mRender: function (data) {
                    return '<button type="button" class="btn btn-primary text-center selectCategoria-btn" data-id="' + data.idCategoria + '">Selecionar</button>'
                }
            }
        ]
    });

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

// ------------------------ MARCAS ------------------------
    $("#btnAbreModalMarca").on('click', function () {
        Marcas.SelecionarMarcas(true);
        Marcas.AddMarcas(false);
        Marcas.CarregaLista();
    });

    // SELECIONAR   
    $(document).on('click', '.selectMarca-btn', function () {
        var id = $(this).data('id');
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
        var id = $(this).data('id');
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
        var id = $(this).data('id');
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
        var id = $(this).data('id');
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
        let url = "/Marcas/JsSearch";
        $.ajax({
            url: url,
            success: function (result) {
                tableMarcas.clear().draw();
                tableMarcas.rows.add(result);
                tableMarcas.draw();
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
        let url = "/Unidades/JsSearch";
        $.ajax({
            url: url,
            success: function (result) {
                tableUnidades.clear().draw();
                tableUnidades.rows.add(result);
                tableUnidades.draw();
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
        let url = "/Categorias/JsSearch";
        $.ajax({
            url: url,
            success: function (result) {
                tableCategorias.clear().draw();
                tableCategorias.rows.add(result);
                tableCategorias.draw();
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