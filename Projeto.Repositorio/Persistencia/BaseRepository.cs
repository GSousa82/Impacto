using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;
using Projeto.Entidades;
using Projeto.Repositorio.Util;

namespace Projeto.Repositorio.Persistencia
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        public virtual void Inserir(TEntity entidade)
        {
            using (ISession session = NhibernateUtil.Factory.OpenSession())
            {
                ITransaction transaction = session.BeginTransaction();

                try
                {
                    session.Save(entidade);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public virtual void Atualizar(TEntity entidade)
        {
            using (ISession session = NhibernateUtil.Factory.OpenSession())
            {
                ITransaction transaction = session.BeginTransaction();

                try
                {
                    session.Update(entidade);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public virtual void Excluir(TEntity entidade)
        {
            using (ISession session = NhibernateUtil.Factory.OpenSession())
            {
                ITransaction transaction = session.BeginTransaction();

                try
                {
                    session.Delete(entidade);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public virtual List<TEntity> ListarTodos()
        {
            using (ISession session = NhibernateUtil.Factory.OpenSession())
            {
                return session.Query<TEntity>().ToList();
            }
        }      

        public virtual TEntity ObterPorId(int id)
        {
            using (ISession session = NhibernateUtil.Factory.OpenSession())
            {
                return session.Get<TEntity>(id);
            }
        }
    }
}
