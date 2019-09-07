using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Projeto.Apresentacao.Models;
using Projeto.Entidades;
using Projeto.Repositorio.Persistencia;
using Projeto.Util;

namespace Projeto.Apresentacao.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home/Index
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AutenticarUsuario(AutenticarUsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UsuarioRepositorio repositorio = new UsuarioRepositorio();
                    //buscar o usuario no banco de dados pelo email e senha
                    Usuario u = repositorio.ObterUsuario(model.Email, Criptografia.MD5Encrypti(model.Senha));

                    //verifica se o usuario não é null
                    if (u != null)
                    {
                      
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(u.Email, false, 10);
                        //gravar o ticket em cookie

                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                        Response.Cookies.Add(cookie);

                        //redirecionar para a área restrita
                        return RedirectToAction("Index", "Principal",
                        new { area = "AreaRestrita" });

                    }
                    else
                    {
                        ViewBag.Mensagem = "Acesso Negado. Usuário não encontrado.";
                    }

                }
                catch (Exception e)
                {
                    //exibir mensagem de erro..
                    ViewBag.Mensagem =  "Ocorreu um erro: " + e.Message;

                }
            }
            return View("Index");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return View("Index");
        }
    }

}
