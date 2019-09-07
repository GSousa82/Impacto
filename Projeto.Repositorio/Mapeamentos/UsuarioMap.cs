using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Projeto.Entidades;

namespace Projeto.Repositorio.Mapeamentos
{
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {            
            Table("Usuario");

            Id(u => u.IdUsuario, "IdUsuario")
                .GeneratedBy.Identity();

            Map(u => u.Nome)
                .Length(50)
                .Not.Nullable();
            Map(u => u.Email)
                .Length(50)
                .Not.Nullable();
            Map(u => u.Permissao)
                .Length(50)
                .Not.Nullable();
            Map(u => u.Senha)
               .Length(50)
               .Not.Nullable();
        }
    }
}
