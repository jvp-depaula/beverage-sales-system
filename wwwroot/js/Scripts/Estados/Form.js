var tablePaises = null;
$(document).ready(function () {

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
                    return '<button type="button" class="btn btn-primary text-center selectPais-btn" data-id="'+data.idPais+'" data-nm="'+data.nmPais+'">Selecionar</button>'
                }
            }
        ]
    });

    // PAISES
    // SELECIONAR
    $('#modal').on('show.bs.modal', function (e) {
        Paises.mostraSelecionarPaises();
        Paises.MontaTabelaPaises();
    });

    $(document).on('click', '.selectPais-btn', function () {
        var id = $(this).data('id');
        $("#idPais").val(id);
        Paises.fechaModal();
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
                        Paises.MontaTabelaPaises($("#modal"));
                    }
                }
            });
        };
        Paises.fechaAddPaises();
    });
    $("#btnAddPais").on('click', function () {
        Paises.mostraAddPaises();
    });
    $("#btnFecharModalAddPais").on('click', function () {
        Paises.fechaAddPaises();
    });
});

var Paises = {
    mostraSelecionarPaises() {
        $(".AddPais").css("display", "none");
        $(".SelecionaPais").css("display", "");
    },

    mostraAddPaises() {
        $(".AddPais").css("display", "");
        $(".SelecionaPais").css("display", "none");
    },

    fechaAddPaises() {
        Paises.limpaForm();
        $(".AddPais").css("display", "none");
        $(".SelecionaPais").css("display", "");
    },

    fechaModal() {
        $("#btnFecharModal").click();
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

    MontaTabelaPaises() {        
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
    }
};