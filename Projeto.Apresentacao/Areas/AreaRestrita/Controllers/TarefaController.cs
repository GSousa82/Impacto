using AutoMapper;
using Projeto.Apresentacao.Areas.AreaRestrita.Models;
using Projeto.Apresentacao.Utils;
using Projeto.Entidades;
using Projeto.Repositorio.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.Apresentacao.Areas.AreaRestrita.Controllers
{
    public class TarefaController : Controller
    {
        // GET: AreaRestrita/Tarefa
        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult Consulta()
        {
            return View();
        }

        TarefaRepositorio repositorio = new TarefaRepositorio();

        public JsonResult CadastrarTarefa(TarefaCadastroViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var tarefa = Mapper.Map<Tarefa>(model);

                    repositorio.Inserir(tarefa);

                    return Json($"Tarefa cadastrada com sucesso.");

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

        public JsonResult ConsultarTarefa()
        {
            try
            {
                var tarefas = repositorio.ListarTodos();

                var model = Mapper.Map<List<TarefaConsultaViewModel>>(tarefas);

                return Json(model);
            }
            catch(Exception e)
            {
                return Json(e.Message);
            }
        }

        public JsonResult ConsultarTarefaPorId(int id)
        {
            try
            {
                
                var tarefa = repositorio.ObterPorId(id);
                
                var model = Mapper.Map<TarefaConsultaViewModel>(tarefa);
                //retornando os dados para o javascript
                return Json(model);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        public JsonResult ExcluirTarefa(int id)
        {
            try
            {
                var tarefa = repositorio.ObterPorId(id);

                repositorio.Excluir(tarefa);
                return Json("Tarefa excluída com sucesso.");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        public JsonResult AtualizarTarefa(TarefaEdicaoViewModel model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var tarefa = Mapper.Map<Tarefa>(model);

                    repositorio.Atualizar(tarefa);
                    return Json($"Tarefa atualizada com sucesso.");
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

    }
}
