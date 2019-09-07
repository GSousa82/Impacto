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
    public class UsuarioRepositorio : BaseRepository<Usuario>
    {
        //consultar usuário pelo nome..
        public Usuario ObterPermissaoUsuario(string email)
        {
            using (ISession session = NhibernateUtil.Factory.OpenSession())
            {
                return session.Query<Usuario>()
                .FirstOrDefault(p => p.Email.Equals(email));
            }            
        }

        public bool ObterPorEmail(string email)
        {
            using (ISession session = NhibernateUtil.Factory.OpenSession())
            {
                var result = session.Query<Usuario>()
                    .FirstOrDefault(p => p.Email.Equals(email));

                if(result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
        }

        //consultar 1 lista de usuário por tipo de permissão
        public Usuario ObterUsuario(string email, string senha)
        {
            using (ISession session = NhibernateUtil.Factory.OpenSession())
            {
                return session.Query<Usuario>()
                    .FirstOrDefault(p => p.Email.Equals(email) && p.Senha.Equals(senha) );
            }
        }

        public virtual List<Usuario> ListarTodos(string nome)
        {
            using (ISession session = NhibernateUtil.Factory.OpenSession())
            {
                return session.Query<Usuario>()
                    .Where(p => p.Nome.Contains(nome))
                    .ToList();
            }
        }
    }
}
