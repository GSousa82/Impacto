using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web;
using System.Web.Mvc;

namespace Projeto.Apresentacao.Utils
{
    public class ValidacaoUtil
    {
        public static List<string> ObterErros(ModelStateDictionary ModelState)
        {
            //criando uma lista para armazenar as mensagens de erro
            var erros = new List<string>();
            //percorrer o ModelState
            foreach (var item in ModelState)
            {
                //verificar se o item possui erros de validação
                if (item.Value.Errors.Count > 0)
                {
                    //capturando o nome do campo referendo ao item
                    var nome = item.Key;
                    //capturando a mensagem de erro
                    var mensagem = item.Value.Errors
                    .Select(s => s.ErrorMessage).FirstOrDefault();
                    //adicionar na lista..
                    erros.Add($"{nome}: {mensagem}");
                }
            }
            return erros;
        }
    }
}