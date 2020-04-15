$(document).ready(function () {
    console.log($(document).toLocaleString());
    let idPaciente = $('#IdPaciente').val();

    //$.ajax({
    //    type: "GET",
    //    url: "/Paciente/ListagemConsulta/" + idPaciente,
    //    success: function (response) {
    //        $.each(response, function (indice, consulta) {
    //            var d = new Date(Number(consulta.Data.match(/(\d)+/)[0])).toLocaleString();
    //            var datee = new Date(Number(consulta.Data.match(/(\d)+/)[0]));
    //            var agora = new Date();
    //            var valido = datee - agora;
    //            console.log(valido);
    //            if (valido > 86400000) {
    //                $("#de_tabela").append('<tr><td value="' + consulta.Id + '">' + consulta.Medico.Nome + '</td>' + ' <td value="' + consulta.Id + '">' + consulta.Medico.Especialidade.Nome + '</td >' + '<td>' + d + '</td><td>' +
    //                    '<button class="btn btn-sm btn-outline-danger" type="button" value="' + consulta.Id + '" id="btnDeletar">DEL</button > ' +
    //                    //'<td><button class="btn btn-sm btn-default"  type="button" value="' + consulta.Id + '"id="btnAlterar"><span class= "glyphicon glyphicon-pencil" ></span ></button > </td >' +
    //                    '<button class="btn btn-sm btn-outline-info"  type="button" value="' + consulta.Id + '"id="btnMostrar">SEE</button > </td ></tr > ');
    //            }
    //            else {
    //                $("#de_tabela").append('<tr><td value="' + consulta.Id + '">' + consulta.Medico.Nome + '</td>' + ' <td value="' + consulta.Id + '">' + consulta.Medico.Especialidade.Nome + '</td >' + '<td>' + d + '</td><td>' +
    //                    //'<td><button class="btn btn-sm btn-default"  type="button" value="' + consulta.Id + '"id="btnAlterar"><span class= "glyphicon glyphicon-pencil" ></span ></button > </td >' +
    //                    '<button class="btn btn-sm btn-outline-info"  type="button" value="' + consulta.Id + '"id="btnMostrar">SEE</button > </td ></tr > ');
    //            }
    //            console.log(datee);


    //        });
    //    }
    //});
    var table = $('#de_tabela').DataTable({
        "processing": true,
        "serverside": true,
        "selected": true,
        "ajax": {
            url: "/Paciente/ListagemConsulta/" + idPaciente,
            dataSrc: ''
        },
        "columns": [{
            "data": "Id"
        }, {
            "data": "Medico.Nome"
        }, {
            "data": "Medico.Especialidade.Nome"
        }, {
            "data": "Data"
        }],
        "columnDefs": [
            {
                "targets": [3],
                "visible": true,
                "render": function (data, type, full, meta) {
                    let dataa = moment(formatDate(data)).format('DD/MM/YYYY HH:mm');
                    return moment(formatDate(data)).format('DD/MM/YYYY HH:mm');
                }
            },
            {
                //            var d = new Date(Number(data.match(/(\d)+/)[0])).toLocaleString();
                //            var datee = new Date(Number(consulta.Data.match(/(\d)+/)[0]));
                //            var agora = new Date();
                //            var valido = datee - agora;
                ////            console.log(valido);
                ////            if (valido > 86400000)
                //            if(true) {

                //            }
                "targets": [4],
                "data": null,
                "render": function (data, type, full, meta) {
                    //var d = new Date(Number(data.match(/(\d)+/)[0])).toLocaleString();
                    //var datee = data.Data
                    var datee = new Date(Number(data.Data.match(/(\d)+/)[0]));
                    console.log(datee);
                    var agora = new Date();
                    //agora = moment(agora).format('DD/MM/YYYY HH:mm');
                    console.log(agora)
                    var valido = datee - agora;
                    console.log(valido)
                    if (valido > 86400000) {
                        return '<button class="btn btn-outline-danger" type="button" value=" " id="btnDeletar">DEL</button >'
                    }
                    else {
                        return '<button class="btn btn-outline-danger" type="button" disabled value=" " id="btnDeletar">DEL</button >'
                    }

                }/*'<button class="btn btn-outline-danger" type="button" value=" " id="btnDeletar">DEL</button >'*/
            },
            {
                "targets": [5],
                "data": null,
                "defaultContent": '<button class="btn btn-outline-info" type="button" value=" " id="btnMostrar">SEE</button >'
            }
        ]

    });

    $(document).on('click', '#btnMostrar', function (e) {
        let table = $('#de_tabela').DataTable();

        let data = table.row($(this).parents('tr')).data();
        debugger;
        $.ajax({
            type: "GET",
            dataType: "html",
            url: "/Paciente/MostrarConsulta/" + data.Id,
            success: function (response) {
                debugger;
                $('#modalDialogMostrarConsulta').html(response);
                $('#janelaMostraConsulta').modal('show');
            },
            error: function (res) {
            }
        });

    });

    $(document).on('click', '#btnDeletar', function (e) {
        console.log("voce clicou");
        let table = $('#de_tabela').DataTable();
        let data = table.row($(this).parents('tr')).data();
        let idd = data.Id;
        let ctx = this;
        alertify.confirm('Deletar', 'Deseja deletar a consulta com o médico ' + data.Medico.Nome, function () {
            debugger;
            console.log(data.Id)
            $.ajax({
                type: "GET",
                dataType: "json",
                url: '/Paciente/DeletarConsulta/' + idd,
                success: function (res) {
                    debugger;
                    if (res.Result) {
                        let table = $('#de_tabela').DataTable();
                        table.row($(ctx).parents('tr')).remove().draw();
                        alertify.success(res.Msg);
                    }
                    else
                        alertify.error(res.Msg);
                }
            });
        }, function () {
            alertify.error('Deleção Cancelada')
        });
    });
});


//$(document).on('click', '#btnMostrar', function (e) {
//    e.preventDefault();
//    let idd = $(this).val();
//    debugger;
//    $.ajax({
//        type: "GET",
//        dataType: "html",
//        url: "/Paciente/MostrarConsulta/" + idd,
//        success: function (response) {
//            debugger;
//            $('#modalDialogMostrarConsulta').html(response);
//            $('#janelaMostraConsulta').modal('show');
//        },
//        error: function (res) {
//        }
//    });

//});

//$(document).on('click', '#btnDeletar', function (e) {
//    console.log("voce clicou");
//    e.preventDefault();
//    e.stopPropagation();
//    var apagar = confirm("deseja apagar este registro realmente ?");
//    debugger;
//    var idd = $(this).val();
//    if (idd != null && idd !== '0') {
//        if (apagar) {
//            location.href = '/Paciente/DeletarConsulta/' + idd;
//        }
//    }
//});

$(document).on('click', '#btnAgendarConsulta', function (e) {
    e.preventDefault();
    debugger;
    $.ajax({
        dataType: "html",
        url: "/Paciente/initCadastrarConsulta",
        //data: {
        //    sangues: $("#sangues").val();
        //},
        success: function (response) {
            debugger;
            //$("#sangues").html(sangues)
            $('#modalDialogCadastrarConsulta').html(response);
            $('#janelaCadastraConsulta').modal('show');
        }
    });
});
$(document).on('click', '#btnAgendarConsultaEspecialidade', function (e) {
    e.preventDefault();
    debugger;
    $.ajax({
        dataType: "html",
        type: "GET",
        url: "/Paciente/init_CadastrarConsultaEspecialidade/",
        //data: {
        //    sangues: $("#sangues").val();
        //},
        success: function (response) {
            debugger;
            //$("#sangues").html(sangues)
            $('#modalDialogCadastrarConsultaEspecialidade').html(response);
            $('#janelaCadastraConsultaEspecialidade').modal('show');
        }
    });
});

$(document).on('change', '#especialidades', function (e) {
    var mec = $('#especialidades').val();
    $.ajax({
        type: "GET",
        url: "/Paciente/ObterMedicos/" + mec,
        success: function (response) {
            debugger;
            $('#medicosDrop').empty();
            $.each(response, function (indice, medicos) {
                $('#medicosDrop').append('<option value="' + medicos.Id + '"">' + medicos.Nome + '</option>');
            });
        }
    });
});






//$(document).on('click', '#btnBuscarMedicosEspecialidade', function (e) {
//    e.preventDefault();
//    debugger;
//    let ide = $('#especialidades').val();
//    $.ajax({
//        dataType: "html",
//        type:"GET",
//        url: "/Paciente/init_CadastrarConsultaEspecialidade/" + ide,
//        //data: {
//        //    sangues: $("#sangues").val();
//        //},
//        success: function (response) {
//            debugger;
//            //$("#sangues").html(sangues)
//            $('#modalDialogCadastrarConsultaEspecialidade').html(response);
//            $('#janelaCadastraConsultaEspecialidade').modal('show');
//        }
//    });
//});
$(document).on('click', '#btnAgendarConsultaData', function (e) {


    $.ajax({
        dataType: "html",
        url: "/Paciente/init_CadastrarConsultaData",
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

$(document).on('focusout', '#dataConsulta', function (e) {
    e.preventDefault();
    let dataa = $('#dataConsulta').val();
    debugger;
    $.ajax({
        type: "GET",
        url: "/Paciente/DropMedico",
        data: { data: dataa },
        //data: {
        //    sangues: $("#sangues").val();
        //},
        success: function (response) {
            debugger;
            $('#medicosDropData').empty();

            $.each(response, function (indice, medicos) {
                $('#medicosDropData').append('<option value="' + medicos.Id + '"">' + medicos.Nome + '</option>');
            });
        }
    });
});

//$(document).on('click', '#btnDeletar', function (e) {
//    let idd = $(this).val();
//    var id = $('#IdPaciente').val();
//    debugger;
//    $.ajax({
//        url: '/Paciente/DeletarConsulta/?' + idd,
//        type: 'POST',
//        data: { "idd": idd},
//        success: function (data) {
//            location.href = '/Paciente/Index/' + id
//        }
//    });
//});

//$(document).ready(function () {
//    $('#Especialidades').change(function () {
//        let idd = $('#especialidades').val();
//        $.ajax({
//            url: '/Paciente/ObterMedicos' + idd,
//            type: 'POST',
//            data: { Id: $(this).val() },
//            datatype: 'json',
//            success: function (data) {
//                var options = '';
//                $.each(data, function () {
//                    options += '<option value="' + this.Id + '">' + this.Nome + '</option>';
//                });
//                $('#Medicos').prop('disabled', false).html(options);
//            }
//        });
//    });
//});

//$('#especialidades').change(function () {
//    debugger;
//    $.ajax({
//        url: "/Paciente/ObterMedico/" + id,
//        success: function (data) {
//            debugger;
//            $("#medicos").empty();
//            $("#medicos").append('<option value>Selecione...</option>');
//            $.each(data, function (index, element) {
//                $("#medicos").append('<option value="' + element.ProjetoId + '">' + element.Text + '</option>');
//            });
//        }
//    });
//});

function clickLink() {
    $(document).on('click', '#LinkConsultas', function (e) {
        debugger;
        let idd = $('#IdPaciente').val();
        location.href = '/Paciente/InitIndex/';
    });
}

function formatDate(data) {
    if (data != undefined && data.match('^/Date')) {
        var d = /\/Date\((\d*)\)\//.exec(data);
        return (d) ? new Date(+d[1]) : data;
    }
    else
        return data;
};
