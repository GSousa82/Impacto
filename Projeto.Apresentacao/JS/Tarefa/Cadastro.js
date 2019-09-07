function cadastrarTarefa() {
    //imprimindo mensagem
    $("#mensagem").html("Processando requisição...");

    //função AJAX..
    $.ajax({
        type: "POST",
        url: "/Tarefa/CadastrarTarefa",
        data: {
            Titulo: $("#titulo").val()
        },

        success: function (result) {
            $("#mensagem").html(result);
            $("#erros").html("");
            $(".campo").val("");
        },

        error: function (e) { //promisse de erro
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
}

//evento para executar a função acima
$(document).ready(function () {
    $("#btncadastro").click(function () {
        //executar a função
        cadastrarTarefa();
    });
});