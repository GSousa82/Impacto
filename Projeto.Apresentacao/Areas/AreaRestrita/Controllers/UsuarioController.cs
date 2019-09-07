using AutoMapper;
using Projeto.Entidades;
using Projeto.Util;
using Projeto.Repositorio.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projeto.Apresentacao.Utils;
using Projeto.Apresentacao.Areas.AreaRestrita.Models;
using System.Web.Security;

namespace Projeto.Apresentacao.Areas.AreaRestrita.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        UsuarioRepositorio repositorio = new UsuarioRepositorio();
        // GET: Usuario
        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult Consulta()
        {
            return View();
        }

        //método para receber a requisição
        //AJAX realizada pelo JQuery..  
        public JsonResult CadastrarUsuario(UsuarioCadastroViewModel model)
        {

            Usuario result = repositorio.ObterPermissaoUsuario(EmailUsuario());

            if (result.Permissao == "Administrador")
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (repositorio.ObterPorEmail(model.Email))
                        {
                            return Json($"Este email já foi cadastrado, por favor tente outro.");
                        }
                        else
                        {
                            Usuario u = new Usuario();
                            u.Nome = model.Nome;
                            u.Email = model.Email;
                            u.Permissao = model.Permissao;
                            u.Senha = Criptografia.MD5Encrypti(model.Senha);

                            repositorio.Inserir(u);

                            return Json($"Usuário {u.Nome}, cadastrado com sucesso.");
                        }
                    }
                    catch (Exception e)
                    {
                        return Json("Ocorreu um erro: " + e.Message);
                    }
                }
                else
                {
                    Response.StatusCode = 400; //BAD REQUEST
                    return Json(ValidacaoUtil.ObterErros(ModelState));
                }
            }
            else
            {
                return Json($"Usuário não possui permissão.");
            }
        }

        public JsonResult ConsultarUsuario()
        {
            Usuario result = repositorio.ObterPermissaoUsuario(EmailUsuario());
            if (result.Permissao == "Administrador")
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var usuarios = repositorio.ListarTodos();

                        var model = Mapper.Map<List<UsuarioConsultaViewModel>>(usuarios);
                        return Json(model);

                    }
                    catch (Exception e)
                    {
                        return Json("Ocorreu um erro: " + e.Message);
                    }
                }
                else
                {
                    Response.StatusCode = 400; //BAD REQUEST
                    return Json(ValidacaoUtil.ObterErros(ModelState));
                }
            }
            else
            {
                return Json($"Usuário não possui permissão.");
            }

        }

        public JsonResult ConsultarUsuarioPorNome(string nome)
        {
            Usuario result = repositorio.ObterPermissaoUsuario(EmailUsuario());
            if (result.Permissao == "Administrador")
            {
                try
                {
                    var usuarios = repositorio.ListarTodos(nome);

                    var model = Mapper.Map<List<UsuarioConsultaViewModel>>(usuarios);
                    //retornando os dados para a página
                    return Json(model);
                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }

            }
            else
            {
                return Json($"Usuário não possui permissão.");
            }
        }

        public JsonResult ConsultarUsuarioPorId(int id)
        {
            try
            {
                var usuarios = repositorio.ObterPorId(id);

                var model = Mapper.Map<UsuarioConsultaViewModel>(usuarios);
                //retornando os dados para o javascript
                return Json(model);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        public JsonResult ExcluirUsuario(int id)
        {
            try
            {
                var usuario = repositorio.ObterPorId(id);

                repositorio.Excluir(usuario);
                return Json("Usuário excluído com sucesso.");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        public JsonResult AtualizarUsuario(UsuarioEdicaoViewModel model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var usuario = Mapper.Map<Usuario>(model);

                    repositorio.Atualizar(usuario);
                    return Json($"Usuário {usuario.Nome} atualizado com sucesso.");
                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }

            }
            else
            {
                Response.StatusCode = 400; //BAD REQUEST
                return Json(ValidacaoUtil.ObterErros(ModelState));
            }
        }

        private string EmailUsuario()
        {
            HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            var email = FormsAuthentication.Decrypt(cookie.Value);

            return email.Name;
        }
    }
}