function consultarTarefa() {
   
    $.ajax({
        type: "POST",
        url: "/Tarefa/ConsultarTarefa",
        data: {},
        success: function (result) {

            imprimirConsultaDeTarefas(result);
        },
        error: function (e) {
            
            $("#mensagem").html(e.responseText);
        }
    });
}

function consultarTarefaPorId(idTarefa) {
    $.ajax({
        type: "POST",
        url: "/Tarefa/ConsultarTarefaPorId",
        data: { id: idTarefa },
        success: function (result) {
            
            $("#idtarefa_exclusao").html(result.IdTarefa);
            $("#titulo_exclusao").html(result.Titulo);

            $("#idtarefa_edicao").val(result.IdTarefa);
            $("#titulo_edicao").val(result.Titulo);
        },
        error: function (e) {
            $("#mensagem").html(e.responseText);
        }
    });
};

function imprimirConsultaDeTarefas(result) {

    var conteudo = "";
    
    $.each(result, function (i, obj) {

        conteudo += "<tr>";
        conteudo += "<td>" + "<input class='checkbox-inline' type='checkbox' id='concluido'>" + "</td>";
        conteudo += "<td>" + obj.Titulo + "</td>";
        conteudo += "<td>";
        conteudo += "<button onclick='consultarTarefaPorId(" + obj.IdTarefa + ")' data-target='#janelaexclusao' data-toggle='modal' class='btn btn-danger btn-sm' > Excluir</button > ";
        conteudo += "&nbsp;";
        conteudo += "<button onclick='consultarTarefaPorId(" + obj.IdTarefa + ")' data-target='#janelaedicao' data-toggle='modal' class='btn btn-primary btn-sm' >Atualizar</button > ";
        conteudo += "</td>";
        conteudo += "</tr>";
    });
    
    $("#tabela tbody").html(conteudo);
   
    $("#quantidade").html(result.length);
};

function excluirTarefa() {
    $.ajax({
        type: "POST",
        url: "/Tarefa/ExcluirTarefa",
        data: { id: $("#idtarefa_exclusao").html() },
        success: function (result) {

            $("#mensagem").html(result);
            consultarTarefa();
        },
        error: function (e) {
            $("#mensagem").html(e.responseText);
        }
    });
}

function atualizarTarefa() {    

    $.ajax({
        type: "POST",
        url: "/Tarefa/AtualizarTarefa",
        data: {
            IdTarefa: $("#idtarefa_edicao").val(),
            Titulo: $("#titulo_edicao").val()
        },
        success: function (result) {
            $("#mensagem").html(result);
            $("#erros").html("");
            $(".campo").val("");

            consultarTarefa();
        },
        error: function (e) {
            $("#mensagem").html("");
            $("#loading").modal('hide');
            if (e.status == 400) {
                var conteudo = "";
                $.each(e.responseJSON, function (i, obj) {
                    conteudo += obj + "<br/>";
                });
                $("#erros").html(conteudo + "<br/>");
            }
        }
    });
};

$(document).ready(function () {
    
    consultarTarefa();

    $("#btnexclusao").click(function () {
        excluirTarefa();
    });

    $("#btnedicao").click(function () {
        atualizarTarefa();
    });
   
});

$('input').click(function () {
    input = $(this);
    classVal = "." + input.val();
    elements = $(classVal);

    if (input.is(':checked')) {
        elements.css("background-color", "#FFFF00");
        elements.css("opacity", 1);
    } else {
        elements.css("background-color", "");
        elements.css("opacity", 0.33);
    }
});