$(document).ready(function () {
    $("#nrCPF").on('keyup change', function () {
        $(this).attr("placeholder", "___.___.___-__");
        $(this).mask("999.999.999-99");        
    });

    $("#nrRG").on('keyup change', function () {
        $(this).attr("palceholder", "__.___.___-_");
        $(this).mask("99.999.999-9");
    });

    $("#nrCEP").on('keyup change', function () {
        $(this).attr("placeholder", "_____-___");
        $(this).mask("99999-999");
    });

    $("#nrTelefoneCelular").on('keyup change', function () {
        $(this).attr("placeholder", "(__) _____-____");
        $(this).mask("(99) 99999-9999");
    });

    $("#nrTelefoneFixo").on('keyup change', function () {
        $(this).attr("placeholder", "(__) ____-____");
        $(this).mask("(99) 9999-9999");
    });

    $("#nrCPF").change();
    $("nrRG").change();
    $("#nrCEP").change();
    $("#nrTelefoneCelular").change();
    $("#nrTelefoneFixo").change();
});