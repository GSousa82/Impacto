using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Projeto.Apresentacao.Areas.AreaRestrita.Models;
using Projeto.Entidades;

namespace Projeto.Apresentacao
{
    
    public class AutoMapperConfig : Profile
    {
        
        public AutoMapperConfig()
        {
            CreateMap<UsuarioCadastroViewModel, Usuario>();
            CreateMap<Usuario, UsuarioConsultaViewModel>();
            CreateMap<UsuarioEdicaoViewModel, Usuario>();

            CreateMap<TarefaCadastroViewModel, Tarefa>();
            CreateMap<Tarefa, TarefaConsultaViewModel>();
            CreateMap<TarefaEdicaoViewModel, Tarefa>();
        }
    }
}