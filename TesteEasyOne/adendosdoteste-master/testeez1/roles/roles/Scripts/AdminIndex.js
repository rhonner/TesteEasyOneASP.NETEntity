//$(document).ready(function () {
//    $.ajax({
//        type: "GET",
//        url: "/Admin/ListagemConsulta",
//        success: function (response) {
//            $.each(response, function (indice, consulta) {
//                var d = new Date(Number(consulta.Data.match(/(\d)+/)[0])).toLocaleString();
//                $("#de_tabela").append('<tr><td value="' + consulta.Id + '">' + consulta.Medico.Nome + '</td>' + '<td id="liPaciente" value = "' + consulta.IdPaciente + '">' + consulta.Paciente.Nome + '</td> <td value="' + consulta.Id + '">' + consulta.Descricao + '</td >' + '<td>' + d + '</td><td>' +
//                    '<button class="btn btn-sm btn-default" type="button" value="' + consulta.Id + '" id="btnDeletar"><span class= "glyphicon glyphicon-remove" ></span ></button > </td >' +
//                    //'<td><button class="btn btn-sm btn-default"  type="button" value="' + consulta.Id + '"id="btnAlterar"><span class= "glyphicon glyphicon-pencil" ></span ></button > </td >' +
//                    '<td><button class="btn btn-sm btn-default" data-target="#janelaMostraUm" data-toggle="modal" type="button" value="' + consulta.Id + '"id="btnMostrar"><span class= "glyphicon glyphicon-th-list" ></span ></button > </td ></tr > ');
//            });
//        }
//    });
//});
$(document).ready(function () {

    var table = $('#de_tabela').DataTable({
        "processing": true,
        "serverside": true,
        "selected": true,
        "ajax": {
            "url": "/Admin/ListagemConsulta",
            dataSrc: ''
        },
        "columns": [{
            "data": "Id"
        }, {
            "data": "Medico.Nome"
        }, {
            "data": "Paciente.Nome"
        }, {
            "data": "Descricao"
        },
        {
            "data": "Data"
        }],
        "columnDefs": [
            {
                "targets": [4],
                "visible": true,
                "render": function (data, type, full, meta) {

                    return moment(formatDate(data)).format('DD/MM/YYYY');
                }
            },
            {
                "targets": [5],
                "data": null,
                "defaultContent": '<button class="btn btn-outline-danger" type="button" value=" " id="btnDeletar">DEL</button >'
            },
            {
                "targets": [6],
                "data": null,
                "defaultContent": '<button class="btn btn-outline-info" type="button" value=" " id="btnMostrar">SEE</button >'
            }
        ]

    });

    $(document).on('click', '#btnDeletar', function (e) {
        debugger;
        let ctx = this;
        let table = $('#de_tabela').DataTable();
        let data = table.row($(this).parents('tr')).data();
        let idd = data.Id;
        console.log(data.Id);
        alertify.confirm('Deletar', 'Deseja deletar o registro da consulta do médico ' + data.Medico.Nome + ' com o(a) paciente ' + data.Paciente.Nome, function () {
            debugger;
            $.ajax({
                type: "GET",
                dataType: "json",
                url: "/Admin/DeletarConsulta/"+idd,
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
            //location.href = '/Admin/DeletarConsulta/' + idd;
        }
            , function () {
                alertify.error('Deleção Cancelada')
            });
    });

    $(document).on('click', '#btnMostrar', function (e) {
        e.preventDefault();
        debugger;
        let table = $('#de_tabela').DataTable();
        let data = table.row($(this).parents('tr')).data();
        let idd = data.Id;
        $.ajax({
            type: "GET",
            dataType: "html",
            url: "/Admin/MostrarConsulta/" + idd,
            success: function (response) {
                debugger;
                $('#modalDialogMostrarConsulta').html(response);
                $('#janelaMostraUmaConsulta').modal('show');
            },
            error: function (res) {

            }
        });

    });
});



//$(document).on('click', '#btnDeletar', function (e) {
//    console.log("voce clicou");
//    e.preventDefault();
//    e.stopPropagation();
//    debugger;
//    var idd = $(this).val();

//    var apagar = confirm("deseja apagar este registro ?")
//    if (idd != null && idd !== '0') {
//        if (apagar) {
//            location.href = '/Admin/DeletarConsulta/' + idd;
//        }
//    }
//});

//$(document).on('click', '#btnDeletar', function (e) {
//    let idd = $(this).val();
//    debugger;
//    $.ajax({
//        url: '/Admin/DeletarConsulta/' + idd,
//        type: 'DELETE',
//        success: function (data) {
//        }
//    });
//});



$(document).on('click', '#btnGerarAgendaDiaria', function (e) {
    debugger;
    e.preventDefault();
    console.log("clicou em agendar consulta");
    location.href = "/Admin/GerarAgendaDiaria";
    location.href();
});


//Global function to format Microsoft Json Date('/Date/55412545') to normal Date format
function formatDate(data) {
    if (data != undefined && data.match('^/Date')) {
        var d = /\/Date\((\d*)\)\//.exec(data);
        return (d) ? new Date(+d[1]) : data;
    }
    else
        return data;
};