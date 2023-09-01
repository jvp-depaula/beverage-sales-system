$(document).ready(function () {
    $('#modal_SelecionaPaises').on('show.bs.modal', function (e) {
        let modal = $(this);
        let url = "/Paises/JsSearch"

        $.ajax({
            url: url,
            success: function (result) {
                var tbody = modal.find('#modal-body');
                tbody.empty();
                result.forEach(function (paises) {
                    tbody.append(
                        `
                        <tr>
                            <td scope="row">${paises.idPais}</td>
                            <td>${paises.nmPais}</td>
                            <td>
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
    });

    $(document).on('click', '.selectPais-btn', function () {
        var id = $(this).data('value');
        $("#idPais").val(id);
        $("#btnFecharModalSelecionarPais").click();
    });
});