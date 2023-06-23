$(document).ready(function () {
    $("#nrCNPJ").on('keyup change', function () {
        $(this).attr("placeholder", "__.___.___/____-__");
        $(this).mask("99.999.999/9999-99");
    });

    $("#nrTelefoneCelular").on('keyup change', function () {
        $(this).attr("placeholder", "(__) _____-____");
        $(this).mask("(99) 99999-9999");
    });

    $("#nrTelefoneFixo").on('keyup change', function () {
        $(this).attr("placeholder", "(__) ____-____");
        $(this).mask("(99) 9999-9999");
    });

    $("#nrCEP").on('keyup change', function () {
        $(this).attr("placeholder", "_____-___");
        $(this).mask("99999-999");
    });

    $("#nrCNPJ").change();
    $("#nrTelefoneCelular").change();
    $("#nrTelefoneFixo").change();
    $("#nrCEP").change();
});