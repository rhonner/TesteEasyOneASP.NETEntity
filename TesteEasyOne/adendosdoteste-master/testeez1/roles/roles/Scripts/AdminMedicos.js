$(document).ready(function () {
    $('#de_tabela_Medicos').DataTable({
        "processing": true,
        "serverside": true,
        "ajax": {
            "url": "/Admin/ListagemMedicos",
            dataSrc: ''
        },
        "columns": [
            {
                "data": "Id"
            },
            {
                "data": "Nome"
            },
            {
                "data": "Crm"
            },
            {
                "data": "Especialidade.Nome"
            }],
        "columnDefs": [{
            "targets": [4],
            "data": null,
            "defaultContent": '<button class="btn btn-outline-danger" type="button" value=" " id="btnDeletarMedico">DEL</button >'
        }, {
            "targets": [5],
            "data": null,
            "defaultContent": '<button class="btn btn-outline-info" type="button" value=" " id="btnMostrarMedico">SEE</button >'
        },
        {
            "targets": [6],
            "data": null,
            "defaultContent": '<button class="btn btn-outline-info" type="button" value=" " id="btnAlterarMedico">ALT</button >'
        }]
    });

    $(document).on('click', '#btnDeletarMedico', function (e) {
        console.log("voce clicou");
        e.preventDefault();
        e.stopPropagation();
        ctx = this;
        debugger;
        let table = $('#de_tabela_Medicos').DataTable();
        let data = table.row($(this).parents('tr')).data();
        idd = data.Id;
        alertify.confirm('Deletar', 'Deseja deletar o registro do médico ' + data.Nome, function () {
            debugger;
            $.ajax({
                type: "GET",
                dataType: "json",
                url: "/Admin/DeletarMedico/" + idd,
                success: function (res) {
                    if (res.Result) {
                        let table = $('#de_tabela_Medicos').DataTable();
                        table.row($(ctx).parents('tr')).remove().draw();
                        alertify.success(res.Msg);
                    }
                    else {
                        alertify.error(res.Msg);
                    }
                }
            });
        }
            , function () {
                alertify.error('Deleção Cancelada');

            });
    });
    $(document).on('click', '#btnMostrarMedico', function (e) {
        e.preventDefault();
        let table = $('#de_tabela_Medicos').DataTable();
        let data = table.row($(this).parents('tr')).data();
        idd = data.Id;
        debugger;
        $.ajax({
            type: "GET",
            dataType: "html",
            url: "/Admin/MostrarMedico/" + idd,
            success: function (response) {
                debugger;
                $('#modalDialogMostrarMedico').html(response);
                $('#janelaMostraUmMedico').modal('show');
            },
            error: function (res) {

            }
        });
    });

    $(document).on('click', '#btnAlterarMedico', function (e) {
        e.preventDefault();
        let table = $('#de_tabela_Medicos').DataTable();
        let data = table.row($(this).parents('tr')).data();
        idd = data.Id;
        $.ajax({
            type: "GET",
            dataType: "html",
            url: "/Admin/InitAlterarEspecialidadeMedico/" + idd,
            success: function (response) {
                debugger;
                $('#modalDialogAlterarMedico').html(response);
                $('#janelaAlteraMedico').modal('show');
            },
            error: function (res) {

            }
        });
    });
});








$(document).on('click', '#btnCadastrarNovoMedico', function (e) {
    e.preventDefault();
    debugger;
    $.ajax({
        dataType: "html",
        url: "/Admin/CadastrarNovoMedico",
        //data: {
        //    sangues: $("#sangues").val();
        //},
        success: function (response) {
            debugger;
            //$("#sangues").html(sangues)
            $('#modalDialogCadastrarMedico').html(response);
            $('#janelaCadastraMedico').modal('show');
        }
    });
});

