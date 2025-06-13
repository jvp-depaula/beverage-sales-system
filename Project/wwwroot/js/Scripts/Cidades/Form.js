var tableEstados = null;
var tablePaises = null;
$(document).ready(function () {
    tableEstados = $('#tbEstados').DataTable({
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.10.24/i18n/Portuguese-Brasil.json',
        },     
        columns: [
            { data: "idEstado" },
            { data: "nmEstado" },
            { data: "nmPais" },
            {
                data: null,
                className: "text-center",
                mRender: function (data) {
                    return '<button type="button" class="btn btn-primary text-center selectEstado-btn" data-id="' + data.idEstado + '" data-nm="' + data.nmEstado + '">Selecionar</button>'
                }
            }
        ]
    });

    tablePaises = $('#tbPaises').DataTable({
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.10.24/i18n/Portuguese-Brasil.json',
        },
        columns: [
            { data: "idPais" },
            { data: "nmPais" },
            {
                data: null,
                className: "text-center",
                mRender: function (data) {
                    return '<button type="button" class="btn btn-primary text-center selectPais-btn" data-id="' + data.idPais + '" data-nm="' + data.nmPais + '">Selecionar</button>'
                }
            }
        ]
    });

    // --------------------- ESTADOS ------------------------
    // SELECIONAR
    $('#modal').on('show.bs.modal', function (e) {
        Estados.SelecionarEstados(true);
        Estados.CarregaLista();
    });
    $(document).on('click', '.selectEstado-btn', function () {
        var id = $(this).data('id');
        $("#idEstado").val(id);
        $("#btnFecharModal").click();
    });

    // ADICIONAR
    $("#btnMostrarAddEstado").on('click', function () {
        $.ajax({
            url: "/Paises/JsSearch",
            success: function (result) {
                if (result.length > 0) {

                    var options = result.map(function (el, i) {
                        return $("<option></option>").val(el.idPais).text(el.nmPais)
                    });
                    $('#idPais').html(options);
                }
            }
        });
        Estados.SelecionarEstados(false);
        Estados.AddEstados(true);
    });

    $("#btnSalvarAddEstado").on('click', function () {
        if (Estados.validaForm()) {
            $.ajax({
                url: "/Estados/JsAddEstado",
                data: {
                    nmEstado: $("#nmEstado").val(),
                    UF: $("#UF").val(),
                    idPais: $("#idPais").val()
                },
                success: function (result) {
                    if (result.success) {
                        var options = result.novaListaEstados.map(function (el, i) {
                            return $("<option></option>").val(el.idEstado).text(el.nmEstado)
                        });
                        $('#idEstado').html(options);

                        Estados.limpaForm();
                        Estados.AddEstados(false);
                        Estados.SelecionarEstados(true);

                        Estados.CarregaLista();
                    } else {
                        alert("Ocorreu um erro!");
                    }
                }
            });
        };
    });

    // --------------------- PAISES ------------------------
    $("#btnMostraSelecionarPaises").on('click', function () {
        Estados.SelecionarEstados(false);
        Estados.AddEstados(false);
        Paises.AddPaises(false);
        Paises.SelecionarPaises(true);
        Paises.CarregaLista();
    });

    // SELECIONAR   
    $(document).on('click', '.selectPais-btn', function () {
        var id = $(this).data('id');
        $("#idPais").val(id);
        Estados.AddEstados(true);
        Paises.SelecionarPaises(false);
    });
    // ADICIONAR
    $("#btnSalvarAddPais").on('click', function () {
        if (Paises.validaForm()) {
            $.ajax({
                url: "/Paises/JsAddPais",
                data: {
                    nmPais: $("#nmPais").val(),
                    sigla: $("#sigla").val(),
                    DDI: $("#DDI").val(),
                },
                success: function (result) {
                    if (result.success) {

                        var options = result.novaListaPaises.map(function (el, i) {
                            return $("<option></option>").val(el.idPais).text(el.nmPais)
                        });
                        $('#idPais').html(options);

                        Paises.limpaForm();
                        Paises.AddPaises(false);
                        Paises.SelecionarPaises(true);

                        Paises.CarregaLista();

                    } else {
                        alert("Ocorreu um erro!");
                    }
                }
            });
        };
    });
    $("#btnAddPais").on('click', function () {
        Paises.SelecionarPaises(false);
        Paises.AddPaises(true);
    });
    $("#btnFecharModalAddPais").on('click', function () {
        Paises.AddPaises(false);
        Paises.SelecionarPaises(true);
    });
    $("#btnEscondeAddPais").on('click', function () {
        Paises.limpaForm();
        Paises.AddPaises(false);
        Paises.SelecionarPaises(true);
    });
    $("#bntEscondeSelecionaPais").on('click', function () {
        Paises.SelecionarPaises(false);
        Estados.AddEstados(true);
    });
    $("#btnEscondeAddEstado").on('click', function () {
        Estados.limpaForm();
        Estados.AddEstados(false);
        Estados.SelecionarEstados(true);
    });
    $('#modal').on('hide.bs.modal', function (e) {
        Paises.limpaForm();
        Paises.AddPaises(false);
        Paises.SelecionarPaises(false);
        Estados.limpaForm();
        Estados.AddEstados(false);
        Estados.SelecionarEstados(false);
    });
});

var Estados = {
    SelecionarEstados(mostra) {
        if (mostra)
            $(".SelecionaEstado").css("display", "");
        else
            $(".SelecionaEstado").css("display", "none");
    },

    AddEstados(mostra) {
        if (mostra)
            $(".AddEstados").css("display", "");
        else {
            $(".AddEstados").css("display", "none");
        }
    },

    validaForm() {
        if (!$("#nmEstado").val()) {
            alert("Digite o nome do Estado!");
            return false;
        } else if (!$("#UF").val()) {
            alert("Digite o UF do Estado!");
            return false;
        } else if (!$("#idPais").val()) {
            alert("Digite o Pais do Estado!");
            return false;
        } else
            return true;
    },

    limpaForm() {
        $("#nmEstado").val("");
        $("#UF").val("");
        $("#idPais").val("");
    },

    CarregaLista() {
        let url = "/Estados/JsSearch";
        $.ajax({
            url: url,
            success: function (result) {
                tableEstados.clear().draw();
                tableEstados.rows.add(result);
                tableEstados.draw();
            }
        });
    }
};

var Paises = {
    SelecionarPaises(mostra) {
        if (mostra)
            $(".SelecionaPais").css("display", "");
        else
            $(".SelecionaPais").css("display", "none");
    },

    AddPaises(mostra) {
        if (mostra)
            $(".AddPais").css("display", "");
        else {
            $(".AddPais").css("display", "none");
        }
    },

    validaForm() {
        if (!$("#nmPais").val()) {
            alert("Digite o nome do Pais!");
            return false;
        } else if (!$("#sigla").val()) {
            alert("Digite a sigla do Pa�s!");
            return false;
        } else if (!$("#DDI").val()) {
            alert("Digite o DDI do Pa�s!");
            return false;
        } else
            return true;
    },

    limpaForm() {
        $("#nmPais").val("");
        $("#sigla").val("");
        $("#DDI").val("");
    },

    CarregaLista() {
        let url = "/Paises/JsSearch";
        $.ajax({
            url: url,
            type: 'GET',
            dataType: 'json',
            cache: 'false',
            success: function (result) {
                tablePaises.clear().draw();
                tablePaises.rows.add(result);
                tablePaises.draw();
            },
            error: function () {
                alert("Houve um erro na busca dos Paises");
            }
        });
    },
};