using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Projeto.Entidades;

namespace Projeto.Repositorio.Mapeamentos
{
    public class TarefaMap : ClassMap<Tarefa>
    {
        public TarefaMap()
        {
            Table("TArefa");

            Id(u => u.IdTarefa, "IdTarefa")
                .GeneratedBy.Identity();

            Map(u => u.Titulo, "Titulo")
                .Length(250)
                .Not.Nullable();
        }
    }
}
