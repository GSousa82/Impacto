using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.Apresentacao.Areas.AreaRestrita.Models
{
    public class UsuarioConsultaViewModel
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Permissao { get; set; }
        public string Senha { get; set; }
    }
}