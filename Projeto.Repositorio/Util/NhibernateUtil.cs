using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Projeto.Repositorio.Mapeamentos;


namespace Projeto.Repositorio.Util
{
    public static class NhibernateUtil
    {
        private static ISessionFactory factory;

        public static ISessionFactory Factory
        {
            get
            {
                if(factory == null)
                {
                    factory = Fluently.Configure().Database(MsSqlConfiguration.MsSql2012.ConnectionString(ConfigurationManager.ConnectionStrings["BDImpacto"].ConnectionString))
                        .Mappings(m => m.FluentMappings.Add<UsuarioMap>())
                        .Mappings(m => m.FluentMappings.Add<TarefaMap>())
                        .BuildSessionFactory();
                }

                return factory;
            }
        }
    }
}
