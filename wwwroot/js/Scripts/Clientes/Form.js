var tableCidades = null;
var tableEstados = null;
var tablePaises = null;
var tableCondicaoPgto = null;

$(document).ready(function () {
    tableCidades = $("#tbCidades").DataTable({
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.10.24/i18n/Portuguese-Brasil.json',
        },
        columns: [
            { data: "idCidade" },
            { data: "nmCidade" },
            { data: "nmEstado" },
            {
                data: null,
                className: "text-center",
                mRender: function (data) {
                    return '<button type="button" class="btn btn-primary text-center selectCidade-btn" data-id="' + data.idCidade + '" data-nm="' + data.nmCidade + '">Selecionar</button>'
                }
            }
        ]
    })

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

    tableCondicaoPgto = $('#tbCondicaoPgto').DataTable({
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

    $("#flTipo").on('change', function () {
        if ($(this).val() == "") {
            $("#formDiv").css("display", "none");
        } else if ($(this).val() == "F") {
            $("#formDiv").css("display", "");
            $(".labelCPFCNPJ").text("CPF");
            $(".labelNmFantasia").text("Apelido");
            $(".labelRG_IE").text("RG");
            $(".labelDtNasc").text("Data de Nascimento");
            $("#nrCPFCNPJ").attr("placeholder", "___.___.___-__");
            $("#nrRG_IE").attr("placeholder", "__.___.___-_");
        } else {
            $("#formDiv").css("display", "");
            $(".labelCPFCNPJ").text("CNPJ");
            $(".labelNmFantasia").text("Nome Fantasia");
            $(".labelRG_IE").text("Inscr. Estadual");
            $(".labelDtNasc").text("Data de Fundação");
            $("#nrCPFCNPJ").attr("placeholder", "__.___.___/____-__");
        };
    });

    $("#nrCPFCNPJ").on('keyup change', function () {
        if ($("#flTipo").val() == "F") {
            $("#nrCPFCNPJ").mask("999.999.999-99");
            $("#nrRG_IE").mask("99.999.999-9");
        }
        else {
            $("#nrCPFCNPJ").mask("99.999.999/9999-99");
        }
    });

    $("#nrRG_IE").on('keyup change', function () {
        if ($("#flTipo").val() == "F")
            $("#nrRG_IE").mask("99.999.999-9");
        else
            $("#nrRG_IE").mask("");
    });

    $("#nrCEP").on('keyup change', function () {
        $(this).mask("99999-999");
    });

    $("#nrTelefoneCelular").on('keyup change', function () {
        $(this).mask("(99) 99999-9999");
    });

    $("#nrTelefoneFixo").on('keyup change', function () {
        $(this).mask("(99) 9999-9999");
    });

    $("#dtNasc").on('keyup change', function () {
        $(this).mask("99/99/9999");
    });

    $("#flTipo").change();
    $("#nrCPFCNPJ").change();
    $("nrRG_IE").change();
    $("#nrCEP").change();
    $("#nrTelefoneCelular").change();
    $("#nrTelefoneFixo").change();

    $("#nrCEP").on('change', function () {
        if ($(this).val()) {
            Endereco.consultaCep($(this).val());
        }
    });

    // --------------------- CIDADES ------------------------
    // SELECIONAR
    $("#modal").on('show.bs.modal', function (e) {
        $("#txtTituloModal").html("Lista de Cidades");
        Cidades.SelecionarCidades(true);
        Cidades.CarregaLista();
    });

    $(document).on('click', '.selectCidade-btn', function () {
        var id = $(this).data('id');
        $("#idCidade").val(id);
        $("#btnFecharModal").click();
    });
    // ADICIONAR
    $("#btnMostrarAddCidade").on('click', function () {
        $.ajax({
            url: "/Estados/JsSearch",
            success: function (result) {
                if (result.length > 0) {

                    var options = result.map(function (el, i) {
                        return $("<option></option>").val(el.idEstado).text(el.nmEstado)
                    });
                    $('#idEstado').html(options);
                }
            }
        });
        Cidades.SelecionarCidades(false);
        Cidades.AddCidades(true);
        $("#txtTituloModal").html("Adicionar Cidade");
    });

    $("#btnSalvarAddCidade").on('click', function () {
        if (Cidades.validaForm()) {
            $.ajax({
                url: "/Cidades/JsAddCidade",
                data: {
                    nmCidade: $("#nmCidade").val(),
                    idEstado: $("#idEstado").val(),
                    DDD: $("#DDD").val()
                },
                success: function (result) {
                    if (result.success) {
                        var options = result.novaListaCidades.map(function (el, i) {
                            return $("<option></option>").val(el.idCidade).text(el.nmCidade)
                        });
                        $('#idCidade').html(options);

                        Cidades.limpaForm();
                        Cidades.AddCidades(false);
                        Cidades.SelecionarCidades(true);
                        Cidades.CarregaLista();
                    } else {
                        alert("Ocorreu um erro!");
                    }
                }
            });
        };
    });

    $("#btnEscondeAddCidade").on('click', function () {
        $("#txtTituloModal").html("Lista de Cidades");
        Cidades.limpaForm();
        Cidades.AddCidades(false);
        Cidades.SelecionarCidades(true);
    })

    // --------------------- ESTADOS ------------------------
    // SELECIONAR
    $("#btnMostraSelecionarEstados").on('click', function () {
        $("#txtTituloModal").html("Lista de Estados")
        Cidades.SelecionarCidades(false);
        Cidades.AddCidades(false);
        Estados.SelecionarEstados(true);
        Estados.CarregaLista();
    });

    $(document).on('click', '.selectEstado-btn', function () {
        var id = $(this).data('id');
        $("#idEstado").val(id);
        Estados.SelecionarEstados(false);
        Cidades.AddCidades(true);
    });

    // ADICIONAR
    $("#btnMostrarAddEstado").on('click', function () {
        $("#txtTituloModal").html("Adicionar Estado");
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

    $("#bntEscondeSelecionaEstado").on('click', function () {
        $("#txtTituloModal").html("Adicionar Cidade");
        Estados.SelecionarEstados(false);
        Cidades.AddCidades(true);
    });

    // --------------------- PAISES ------------------------
    $("#btnMostraSelecionarPaises").on('click', function () {
        $("#txtTituloModal").html("Lista de Países");
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
        $("#txtTituloModal").html("Adicionar País");
        Paises.SelecionarPaises(false);
        Paises.AddPaises(true);
    });
    $("#btnFecharModalAddPais").on('click', function () {
        Paises.AddPaises(false);
        Paises.SelecionarPaises(true);
    });
    $("#btnEscondeAddPais").on('click', function () {
        $("#txtTituloModal").html("Lista de Países")
        Paises.limpaForm();
        Paises.AddPaises(false);
        Paises.SelecionarPaises(true);
    });
    $("#bntEscondeSelecionaPais").on('click', function () {
        $("#txtTituloModal").html("Adicionar Estado");
        Paises.SelecionarPaises(false);
        Estados.AddEstados(true);
    });
    $("#btnEscondeAddEstado").on('click', function () {
        $("#txtTituloModal").html("Lista de Estados");
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

    // --------------------- CONDICAO PGTO ------------------------
    // SELECIONAR
    $('#modalCondicoes').on('show.bs.modal', function (e) {
        CondicoesPgto.mostraSelecionarCondicoes();
        CondicoesPgto.CarregaLista();
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
                    dsCondicaoPgto: $("#dsCondicaoPgto").val(),
                    vlMulta: $("#vlMulta").val(),
                    vlDesconto: $("#vlDesconto").val(),
                    vlJuros: $("#vlJuros").val(),
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
});

var Cidades = {
    SelecionarCidades(mostra) {
        if (mostra)
            $(".SelecionaCidade").css("display", "");
        else
            $(".SelecionaCidade").css("display", "none");
    },

    AddCidades(mostra) {
        if (mostra)
            $(".AddCidades").css("display", "");
        else
            $(".AddCidades").css("display", "none");
    },

    validaForm() {
        if (!$("#nmCidade").val()) {
            alert("Digite o nome da Cidade!");
            return false;
        } else if (!$("#idEstado").val()) {
            alert("Digite o Estado da Cidade!");
            return false;
        } else if (!$("#DDD").val()) {
            alert("Digite o DDD da Cidade!");
            return false;
        } else
            return true;
    },

    limpaForm() {
        $("#nmCidade").val("");
        $("#idEstado").val("");
        $("#DDD").val("");
    },


    CarregaLista() {
        let url = "/Cidades/JsSearch";
        $.ajax({
            url: url,
            type: 'GET',
            dataType: 'json',
            cache: 'false',
            success: function (result) {
                tableCidades.clear().draw();
                tableCidades.rows.add(result);
                tableCidades.draw();
            },
            error: function () {
                alert("Houve um erro na busca das Cidades");
            }
        });
    },
};

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
            },
            error: function () {
                alert("Houve um erro na busca dos Estados!");
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
            alert("Digite a sigla do País!");
            return false;
        } else if (!$("#DDI").val()) {
            alert("Digite o DDI do País!");
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

var Endereco = {
    limpaForm() {
        $("#dsLogradouro").val("");
        $("#dsBairro").val("");
        $("#dsComplemento").val("");
    },

    consultaCep(cep) {
        if (cep != "") {
            //Expressão regular para validar o CEP.
            var validacep = /^[0-9]{8}$/;

            //Valida o formato do CEP.
            if (validacep.test(cep.replace("-", ""))) {

                //Preenche os campos com "..." enquanto consulta webservice.
                $("#dsLogradouro").val("...");
                $("#dsBairro").val("...");
                $("#dsComplemento").val("...");

                $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {
                    if (!("erro" in dados)) {
                        //Atualiza os campos com os valores da consulta.
                        $("#dsLogradouro").val(dados.logradouro);
                        $("#dsBairro").val(dados.bairro);
                        $("#dsComplemento").val(dados.complemento);
                        let cidade = dados.localidade ?? "";
                        Endereco.buscaCidade(null, cidade);
                    }
                    else {
                        //CEP pesquisado não foi encontrado.
                        Endereco.limpaForm();
                        alert("CEP não encontrado.");
                    }
                });
            } else {
                //cep é inválido.
                Endereco.limpaForm();
                alert("Formato de CEP inválido.");
            }
        } else {
            //cep sem valor, limpa formulário.
            Endereco.limpaForm();
        };
    },

    buscaCidade(id, cidade) {
        $.ajax({
            url: "/Cidades/JsConsultaCidade",
            data: {
                idCidade: id ?? null,
                nmCidade: cidade
            },
            success: function (result) {
                if (result.idCidade != null) {
                    $("#idCidade").val(result.idCidade);
                } else {
                    alert("Cidade não encontrada no banco de dados do Sistema!");
                };
            }
        });
    }
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

    CarregaLista() {
        $.ajax({
            url: "/CondicaoPgto/JsSearch",
            success: function (result) {
                tableCondicaoPgto.clear().draw();
                tableCondicaoPgto.rows.add(result);
                tableCondicaoPgto.draw();
            },
            error: function () {
                alert("Houve um erro na busca das Condições de Pgto");
            }
        });

    },
};
