//função para realizar o cadastro de Usuário
function cadastrarUsuario() {
    //imprimindo mensagem
    $("#mensagem").html("Processando requisição...");

    //função AJAX..
    $.ajax({
        type: "POST",
        url: "/Usuario/CadastrarUsuario",
        data: {
            Nome: $("#nome").val(),
            Email: $("#email").val(),
            Permissao: $("#dropList").val(),
            Senha: $("#senha").val(),
            SenhaConfirm: $("#conf_senha").val()
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
        cadastrarUsuario();
    });
});