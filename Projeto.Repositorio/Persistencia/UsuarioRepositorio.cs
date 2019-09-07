using NHibernate.Linq;
using NHibernate;
using Projeto.Entidades;
using Projeto.Repositorio.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Repositorio.Persistencia
{
    public class TarefaRepositorio : BaseRepository<Tarefa>
    {
        public virtual List<Tarefa> ListarTodos(string titulo)
        {
            using (ISession session = NhibernateUtil.Factory.OpenSession())
            {
                return session.Query<Tarefa>()
                    .Where(p => p.Titulo.Contains(titulo))
                    .ToList();
            }
        }
    }
}
