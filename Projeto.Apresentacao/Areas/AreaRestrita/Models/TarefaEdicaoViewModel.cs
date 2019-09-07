using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto.Apresentacao.Areas.AreaRestrita.Models
{
    public class TarefaEdicaoViewModel
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public int IdTarefa { get; set; }

        [MinLength(6, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(50, ErrorMessage = "Informe no máximo {1} caracteres.")]
        public string Titulo { get; set; }
    }
}