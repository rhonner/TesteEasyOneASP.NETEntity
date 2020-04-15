$(document).on('click', '#btnBuscarMedicosData', function (e) {
    e.preventDefault();
    let dataa = $('#dataConsulta').val();
    debugger;
    $.ajax({
        dataType: "html",
        type: "GET",
        url: "/Paciente/init_CadastrarConsultaData",
        data: { data: dataa},
        //data: {
        //    sangues: $("#sangues").val();
        //},
        success: function (response) {
            debugger;
            //$("#sangues").html(sangues)
            $('#modalDialogCadastrarConsultaData').html(response);
            $('#janelaCadastraConsultaData').modal('show');
        }
    });
});

