$(document).ready(function () {
    $("#flTipo").on('change', function () {
        if ($(this).val() == "F") {
            $(".labelCPFCNPJ").text("CPF");
            $(".labelNmFantasia").text("Apelido");
            $(".labelRG_IE").text("RG");
            $(".labelDtNasc").text("Data de Nascimento");
            $("#nrCPFCNPJ").attr("placeholder", "___.___.___-__");
            $("#nrRG_IE").attr("placeholder", "__.___.___-_");
        }
        else {
            $(".labelCPFCNPJ").text("CNPJ");
            $(".labelNmFantasia").text("Nome Fantasia");
            $(".labelRG_IE").text("Inscr. Estadual");
            $(".labelDtNasc").text("Data de Fundação");
            $("#nrCPFCNPJ").attr("placeholder", "__.___.___/____-__");
        }

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
        if (("#flTipo").val() == "F")
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

    $("#flTipo").change();
    $("#nrCPFCNPJ").change();
    $("nrRG_IE").change();
    $("#nrCEP").change();
    $("#nrTelefoneCelular").change();
    $("#nrTelefoneFixo").change();
});