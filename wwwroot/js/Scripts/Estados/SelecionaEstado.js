$(document).ready(function () {
    $('#modal_SelecionaEstados').on('show.bs.modal', function (e) {

        $.ajax({
            url: "/Paises/JsSearch",
            success: function (result) {
                var options = result.map(function (el, i) {
                    return $("<option></option>").val(el.idPais).text(el.nmPais)
                });
                $('#idPais').html(options);
            }
        });

        let modal = $(this);
        let url = "/Estados/JsSearch"

        $.ajax({
            url: url,
            success: function (result) {
                var tbody = modal.find('#modal-body');
                tbody.empty();
                result.forEach(function (obj) {
                    tbody.append(
                        `
                        <tr>
                            <td scope="row">${obj.idEstado}</td>
                            <td>${obj.nmEstado}</td>
                            <td>
                            <button type="button" class="btn btn-sm btn-primary selectEstado-btn" data-value="${obj.idEstado}" data-name="${obj.nmEstado}">
                                Selecionar
                            </button>
                            </td>
                        </tr>
                        `
                    );
                });      
            }
        });
    });
    $(document).on('click', '.selectEstado-btn', function () {
        var id = $(this).data('value');
        $("#idEstado").val(id);
        $("#btnFecharModalSelecionarEstado").click();
    });
});