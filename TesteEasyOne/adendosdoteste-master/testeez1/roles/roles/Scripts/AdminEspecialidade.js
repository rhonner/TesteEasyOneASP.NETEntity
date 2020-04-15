
$(document).ready(function () {
    var table = $('#de_tabela_Especialidade').DataTable({
        "processing": true,
        "serverside": true,
        "selected": true,
        "ajax": {
            "url": "/Admin/ListagemEspecialidade",
            dataSrc: ''
        },
        "columns": [{
            "data":"Id"
        }, {
                "data":"Nome"
            }],
        "columnDefs": [{
            "targets": [2],
            "data": null,
            "defaultContent":'<button class="btn btn-outline-info" type="button" value=" " id="btnMostrarEspecialidade">SEE</button >'
        },
            {
                "targets": [3],
                "data": null,
                "defaultContent": '<button class="btn btn-outline-info" type="button" value=" " id="btnAlterarEspecialidade">ALT</button >'
            }]

    });

    $(document).on('click', '#btnAlterarEspecialidade', function (e) {
        e.preventDefault();
        let table = $('#de_tabela_Especialidade').DataTable();
        let data = table.row($(this).parents('tr')).data();
        let idd = data.Id;
        debugger;
        $.ajax({
            type: "GET",
            dataType: "html",
            url: "/Admin/InitAlterarEspecialidade/" + idd,
            success: function (response) {
                debugger;
                $('#modalDialogAlterarEspecialidade').html(response);
                $('#janelaAlteraEspecialidade').modal('show');
            },
            error: function (res) {

            }
        });
    });

    $(document).on('click', '#btnMostrarEspecialidade', function (e) {
        e.preventDefault();
        let table = $('#de_tabela_Especialidade').DataTable();
        let data = table.row($(this).parents('tr')).data();
        let idd = data.Id;
        $.ajax({
            type: "GET",
            dataType: "html",
            url: "/Admin/MostrarEspecialidade/" + idd,
            success: function (response) {
                debugger;
                $('#modalDialogMostrarEspecialidade').html(response);
                $('#janelaMostraUmEspecialidade').modal('show');
            },
            error: function (res) {
            }
        });
    });

});





$(document).on('click', '#btnCadastrarNovoEspecialidade', function (e) {
    e.preventDefault();
    debugger;
    $.ajax({
        dataType: "html",
        url: "/Admin/InitCadastrarEspecialidade",
        success: function (response) {
            debugger;
            $('#modalDialogCadastrarEspecialidade').html(response);
            $('#janelaCadastraEspecialidade').modal('show');
        }
    });
});