//$(document).ready(function () {
//    $.ajax({
//        type: "GET",
//        url: "/Admin/ListagemPaciente",
//        success: function (response) {
//            $.each(response, function (indice, paciente) {
//                //var d = new Date(Number(paciente.Data.match(/(\d)+/)[0])).toLocaleString();
//                $("#de_tabela_AdminPacientes").append('<tr><td value="' + paciente.Id + '">' + paciente.Id + '</td>' + '<td id="liPaciente" value = "' + paciente.Nome + '">' + paciente.Nome + '</td> <td value="' + paciente.Cpf + '">' + paciente.Cpf + '</td >' + '<td>' + paciente.Email + '</td>' +
//                    '<td>' + paciente.Sangue.Tipo + '</td>' +
//                    '<td>' + paciente.Sexo + '</td>' +
//                    //'<td><button class="btn btn"  type="button" value="' + paciente.Id + '"id="btnAlterar"><span class= "glyphicon glyphicon-pencil" ></span ></button > </td >' +
//                    '<td><button class="btn btn-outline-danger"  type="button" value="' + paciente.Id + '"id="btnDeletar">DEL</button > </td >' +
//                    '<td> <button class="btn btn-outline-info"  type="button" value="' + paciente.Id + '" id="btnMostrarPaciente">SEE</button > </td ></tr > ');
//            });
//        }
//    });
//});

$(document).ready(function () {

    var table = $('#de_tabela_AdminPacientes').DataTable({
        "processing": true,
        "serverside": true,
        "selected": true,
        "ajax": {
            "url": "/Admin/ListagemPaciente",
            dataSrc: ''
        },
        "columns": [{
            "data": "Id"
        }, {
            "data": "Nome"
        }, {
            "data": "Cpf"
        }, {
            "data": "Email"
        },
        {
            "data": "Sangue.Tipo"
        },
        {
            "data": "Sexo"
        }],
        "columnDefs": [
            {
                "targets": [6],
                "data": null,
                "defaultContent": '<button class="btn btn-outline-danger" type="button" value=" " id="btnDeletarPaciente">DEL</button >'
            },
            {
                "targets": [7],
                "data": null,
                "defaultContent": '<button class="btn btn-outline-info" type="button" value=" " id="btnMostrarPaciente">SEE</button >'
            }
        ]

    });

    $(document).on('click', '#btnDeletarPaciente', function (e) {
        debugger;
        let ctx = this;
        let table = $('#de_tabela_AdminPacientes').DataTable();
        let data = table.row($(this).parents('tr')).data();
        let idd = data.Id;
        alertify.confirm('Deletar', 'Deseja deletar o registro do paciente ' + data.Nome, function () {
                $.ajax({
                    type: "GET",
                    dataType: "json",
                    url: "/Admin/DeletarPaciente/" + idd,
                    success: function (res) {
                        let table = $('#de_tabela_AdminPacientes').DataTable();
                        table.row($(ctx).parents('tr')).remove().draw();
                        alertify.success(res.Msg);
                    },
                    error: function (res) {
                        debugger;
                    }
                });

                        
        }
            , function () {
                alertify.error('Deleção Cancelada');

            });
        
    });


    $(document).on('click', '#btnMostrarPaciente', function (e) {
        e.preventDefault();
        debugger;
        let table = $('#de_tabela_AdminPacientes').DataTable();
        let data = table.row($(this).parents('tr')).data();
        let idd = data.Id;
        debugger;
        $.ajax({
            type: "GET",
            dataType: "html",
            url: "/Admin/MostrarPaciente/" + idd,
            success: function (response) {
                debugger;
                $('#modalDialogMostrar').html(response);
                $('#janelaMostraUmPaciente').modal('show');
            },
            error: function (res) {

            }
        });
    });


});














$(document).on('click', '#btnCadastrarPaciente', function (e) {
    debugger;
    console.log("voce clicou em cadastrar");

});




$(document).on('click', '#btnCadastrarNovoPaciente', function (e) {
    e.preventDefault();
    debugger;
    $.ajax({
        dataType: "html",
        url: "/Admin/CadastrarNovoPaciente",
        //data: {
        //    sangues: $("#sangues").val();
        //},
        success: function (response) {
            debugger;
            //$("#sangues").html(sangues)
            $('#modalDialogCadastrar').html(response);
            $('#janelaCadastraPaciente').modal('show');
        }
    });
});

//$("#sangues").change(function () {
//    $.ajax({
//        url: 'getCities/Trips',
//        type: 'post',
//        data: {
//            provinceId: $("#province_dll").val()
//        }
//    }).done(function (response) {
//        $("cities_dll").html(response);
//    });
//});