$(document).ready(function () {
    // PAISES
    // SELECIONAR
    $('#modal').on('show.bs.modal', function (e) {
        Paises.mostraSelecionarPaises();
        Paises.MontaTabelaPaises($(this));
    });

    $(document).on('click', '.selectPais-btn', function () {
        var id = $(this).data('value');
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

    MontaTabelaPaises(modal) {
        $.ajax({
            url: "/Paises/JsSearch",
            success: function (result) {
                var tbody = modal.find('#bodyPaises');
                tbody.empty();
                result.forEach(function (paises) {
                    tbody.append(
                        `
                        <tr>
                            <td scope="row">${paises.idPais}</td>
                            <td>${paises.nmPais}</td>
                            <td style="text-align: right">
                            <button type="button" class="btn btn-sm btn-primary selectPais-btn" data-value="${paises.idPais}" data-name="${paises.nmPais}">
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