
function exluirUsuario(idUsuario){
    $.ajax({
        type: "POST",
        url: "/Usuario/ExcluirUsuario",
        data: { id: $("#idusuario_exclusao").html()},
        success: function (result) {
            
            $("#mensagem").html(result);
            consultarUsuario();
        },
        error: function (e) {
            $("#mensagem").html(e.responseText);
        }
    });
};

function consultarUsuarioPorId(idUsuario) {
    $.ajax({
        type: "POST",
        url: "/Usuario/ConsultarUsuarioPorId",
        data: { id: idUsuario },
        success: function (result) {
            $("#idusuario_exclusao").html(result.IdUsuario);
            $("#nome_exclusao").html(result.Nome);

            $("#idusuario_edicao").val(result.IdUsuario);
            $("#senha_edicao").val(result.Senha);
            $("#nome_edicao").val(result.Nome);            $("#email_edicao").val(result.Email);            $("#permissao_edicao").val(result.Permissao);
        },
        error: function (e) {
            $("#mensagem").html(e.responseText);
        }
    });
};

function consultarUsuario() {

    $.ajax({
        type: "POST",
        url: "/Usuario/ConsultarUsuario",
        data: {},
        success: function (result) {
            imprimirConsultaDeUsuarios(result);
        },

        error: function (e) {

            $("#mensagem").html(e.responseText);
        }
    });
}

function consultarUsuariosPorNome() {
    //requisição AJAX para o controller..
    $.ajax({
        type: "POST",
        url: "/Usuario/ConsultarUsuarioPorNome",
        data: { nome: $("#pesquisa").val().trim() },
        success: function (result) {
            imprimirConsultaDeUsuarios(result);
        },
        error: function (e) {
            //imprimir a mensagem na página
            $("#mensagem").html(e.responseText);
        }
    });
}

function imprimirConsultaDeUsuarios(result) {

    var conteudo = "";
    
    $.each(result, function (i, obj) {

        conteudo += "<tr>";
        conteudo += "<td>" + obj.Nome + "</td>";
        conteudo += "<td>" + obj.Email + "</td>";
        conteudo += "<td>" + obj.Permissao + "</td>";
        conteudo += "<td>";
        conteudo += "<button onclick='consultarUsuarioPorId(" + obj.IdUsuario +")' data-target='#janelaexclusao' data-toggle='modal' class='btn btn-danger btn-sm'>Excluir</button>";
        conteudo += "&nbsp;";
        conteudo += "<button onclick='consultarUsuarioPorId(" + obj.IdUsuario +")' data-target='#janelaedicao' data-toggle='modal' class='btn btn-primary btn-sm'>Atualizar</button>";
        conteudo += "</td>";
        conteudo += "</tr>";
    });

    $("#tabela tbody").html(conteudo);

    $("#quantidade").html(result.length);

}

function atualizarUsuario() {
    
    $("#mensagem").html("Processando requisição...");
   
    $.ajax({
        type: "POST",
        url: "/Usuario/AtualizarUsuario",
        data: {
            IdUsuario: $("#idusuario_edicao").val(),
            Senha: $("#senha_edicao").val(),
            Nome: $("#nome_edicao").val(),
            Email: $("#email_edicao").val(),
            Permissao: $("#permissao_edicao").val()
        },
        success: function (result) { 
            $("#mensagem").html(result);
            $("#erros").html("");
            $(".campo").val("");
            
            consultarUsuario();
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

    consultarUsuario();

    //criando o evento de pesquisa
    $("#pesquisa").keyup(function () {
        var qtdCaracteres = $("#pesquisa").val().trim().length;
        if (qtdCaracteres >= 3) {
            consultarUsuariosPorNome();
        }
        else if (qtdCaracteres == 0) {
            consultarUsuario();
        }
    });    //criando o evento de exclusão
    $("#btnexclusao").click(function () {
        exluirUsuario();
    });    $("#btnedicao").click(function () {
        atualizarUsuario();
    });
})