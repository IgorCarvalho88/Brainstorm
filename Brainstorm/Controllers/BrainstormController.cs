using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Brainstorm.Models;
using Brainstorm.Repository;
using Brainstorm.Repository.Database;
using Brainstorm.ViewModel;
using Microsoft.Owin.Security.Provider;
using Brainstorm = Brainstorm.Models.ReuniaoBrainstorm;

namespace Brainstorm.Controllers
{
    public class BrainstormController : Controller
    {
        IIntervenientes repo = new IntervenientesDB();

        // GET: Brainstorm
        public ActionResult Reuniao()
        {

            //if (Session["utilizador_codigo"] != null)
            //{
            //    var ut = Session["utilizador_codigo"].ToString();
            //}

            Sessoes();

            var reuniaoBrainstorm = new ReuniaoBrainstorm();
            var intervenientes = new List<Interveniente>();
            //var temas = new List<Tema>();
            intervenientes = repo.getUT();

            //var temas = new List<Tema>
            //{
            //    new Tema {Descricao = "Inovacao", Importancia = "Alta", Comentarios = "teste",  Titulo = "titulo1", Estado = "P", GestaoInov = 0},
            //    new Tema {Descricao = "Inovacao2", Importancia = "Alta", Comentarios = "teste2",  Titulo = "titulo2", Estado = "P", GestaoInov = 0 }
            //};

             List<Tema> temas = new List<Tema>(new Tema[2]);

            var viewModel = new BrainstormViewModel
            {
                ReuniaoBrainstorm = reuniaoBrainstorm,
                Intervenientes = intervenientes,
                Temas = temas

            };

           // repo.GetUt();

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Reuniao(BrainstormViewModel model)
        {
            Sessoes();
            // ModelState.Remove(nameof(model.Temas.Id));
            // valida campos
            if (!ModelState.IsValid)
            {
                IIntervenientes repo = new IntervenientesDB();
                var intervenientes = new List<Interveniente>();
                intervenientes = repo.getUT();
                model.Intervenientes = intervenientes;

                if (model.ReuniaoBrainstorm.Estado == "Pendente")
                {
                    model.ReuniaoBrainstorm.Estado = "P";
                }
                else if (model.ReuniaoBrainstorm.Estado == "Aprovado")
                {
                    model.ReuniaoBrainstorm.Estado = "A";
                }
                else if (model.ReuniaoBrainstorm.Estado == "Encerrado")
                {
                    model.ReuniaoBrainstorm.Estado = "E";
                }

                else if (model.ReuniaoBrainstorm.Estado == "Anulada")
                {
                    model.ReuniaoBrainstorm.Estado = "X";
                }


                return View("Reuniao", model);
            }

            // nova reuniao
            if (model.ReuniaoBrainstorm.Id == 0)
            {
                BrainstormRepository brainRepo = new BrainstormDB();
                var ut = Session["utilizador_codigo"].ToString();

                // inicializa estado para pendente
                model.ReuniaoBrainstorm.Estado = "P";
                //inicializa utilizador que ins/alt e data de ins/alt.
                model.ReuniaoBrainstorm.Utilizador_ins = ut;
                model.ReuniaoBrainstorm.Data_ins = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                model.ReuniaoBrainstorm.Utilizador_alt = ut;
                model.ReuniaoBrainstorm.Data_alt = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                // split do codigo e nome
                model.Intervenientes = tratarInterv2(model.IntervenientesView);

                // guardar dados recebidos pelo utilizador aquando da criação da reuniao com os dados preenchidos na form////////////

                DataRow id = brainRepo.guardarReuniao(model);
                int idBrainstorm = int.Parse(id[0].ToString());

                // guardar intervenientes consoante o numero de intervenientes presentes no model
                for (int i = 0; i < model.Intervenientes.Count; i++)
                {
                    DataRow idIntervenienteAux = brainRepo.guardarInterveniente(model.Intervenientes[i], idBrainstorm);
                    int idInterveniente = int.Parse(idIntervenienteAux[0].ToString());
                   
                }

                // guardar temas consoante o numero de temas presentes no model
                for (int i = 0; i < model.Temas.Count; i++)
                {
                    // estado pendente como default-- mudar a logica depois de falar com rui
                    if (model.Temas[i].Comentarios != null || model.Temas[i].Descricao != null || model.Temas[i].Titulo != null /*|| model.Temas[i].Comentarios != null*/)
                    {
                        model.Temas[i].Estado = "P";
                        DataRow idTemaAux = brainRepo.guardarTema(model.Temas[i], idBrainstorm);
                        int idTema = int.Parse(idTemaAux[0].ToString());
                        model.Temas[i].Id = idTema;
                    }
                }

                // guardar estado da reuniao (falta guardar utlizador que modifica estado, usar sessions?)
                // ver hora em que é alterado o estado
                string data = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                DataRow estadoSeq = brainRepo.guardarEstado(model.ReuniaoBrainstorm.Estado, data, idBrainstorm, ut);

                TempData["additionalData"] = "Reunião criada com sucesso";
                //return RedirectToAction("Reuniao");
                return RedirectToAction("EditarReuniao", new {id = idBrainstorm});
            }
            else
            {
                // update reuniao
                             
                BrainstormRepository brainRepo = new BrainstormDB();
                IIntervenientes repo = new IntervenientesDB();
                var ut = Session["utilizador_codigo"].ToString();

                if (model.ReuniaoBrainstorm.Estado == "X")
                {
                     model.Intervenientes = tratarInterv2(model.IntervenientesView);

                    brainRepo.alterarReuniao(model, model.ReuniaoBrainstorm.Id);

                    repo.deleteIntervenientes(model.ReuniaoBrainstorm.Id);
                    for (int i = 0; i < model.Intervenientes.Count; i++)
                    {
                        DataRow idIntervenienteAux = brainRepo.guardarInterveniente(model.Intervenientes[i], model.ReuniaoBrainstorm.Id);
                        int idInterveniente = int.Parse(idIntervenienteAux[0].ToString());

                    }

                    for (int i = 0; i < model.Temas.Count; i++)
                    {
                        brainRepo.deleteTemas(model.Temas[i].Id);
                    }

                    //for (int i = 0; i < model.Temas.Count; i++)
                    //{
                    //    if (model.Temas[i].Comentarios != null || model.Temas[i].Descricao != null || model.Temas[i].Titulo != null /*|| model.Temas[i].Comentarios != null*/)
                    //    {
                    //        brainRepo.alterarTema(model.Temas[i], model.ReuniaoBrainstorm.Id);
                    //    }
                    //}

                    // guardar temas consoante o numero de temas presentes no model
                    for (int i = 0; i < model.Temas.Count; i++)
                    {
                        // estado pendente como default-- mudar a logica depois de falar com rui
                        if (model.Temas[i].Comentarios != null || model.Temas[i].Descricao != null || model.Temas[i].Titulo != null /*|| model.Temas[i].Comentarios != null*/)
                        {
                            model.Temas[i].Estado = "P";
                            DataRow idTemaAux = brainRepo.guardarTema(model.Temas[i], model.ReuniaoBrainstorm.Id);
                            int idTema = int.Parse(idTemaAux[0].ToString());
                            model.Temas[i].Id = idTema;
                        }
                    }
                    string data = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    brainRepo.guardarEstado(model.ReuniaoBrainstorm.Estado, data, model.ReuniaoBrainstorm.Id, ut);

                    return View("ReuniaoAnulada", model);

                }


                model.Intervenientes = tratarInterv2(model.IntervenientesView);

                //model.ReuniaoBrainstorm.Utilizador_ins = "IGC";
                //model.ReuniaoBrainstorm.Data_ins = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                /*** adicionar quando anulada? bloco em cima***/
                model.ReuniaoBrainstorm.Utilizador_alt = ut;
                model.ReuniaoBrainstorm.Data_alt = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                DataRow teste = brainRepo.alterarReuniao(model, model.ReuniaoBrainstorm.Id);
                // adicionar funcao de fazer delete aos intervenientes associados ao id
                repo.deleteIntervenientes(model.ReuniaoBrainstorm.Id);
                for (int i = 0; i < model.Intervenientes.Count; i++)
                {
                    DataRow idIntervenienteAux = brainRepo.guardarInterveniente(model.Intervenientes[i], model.ReuniaoBrainstorm.Id);
                    int idInterveniente = int.Parse(idIntervenienteAux[0].ToString());

                }

                for (int i = 0; i < model.Temas.Count; i++)
                {

                    if(model.Temas[i].Id != null)
                    {
                        brainRepo.deleteTemas(model.ReuniaoBrainstorm.Id);
                    }
                    
                }

                // guardar temas consoante o numero de temas presentes no model
                for (int i = 0; i < model.Temas.Count; i++)
                {
                    // estado pendente como default-- mudar a logica depois de falar com rui
                    if (model.Temas[i].Comentarios != null || model.Temas[i].Descricao != null || model.Temas[i].Titulo != null /*|| model.Temas[i].Comentarios != null*/)
                    {
                        model.Temas[i].Estado = "P";
                        DataRow idTemaAux = brainRepo.guardarTema(model.Temas[i], model.ReuniaoBrainstorm.Id);
                        int idTema = int.Parse(idTemaAux[0].ToString());
                        model.Temas[i].Id = idTema;
                    }
                }

                //for (int i = 0; i < model.Temas.Count; i++)
                //{
                //    DataRow teste2 = brainRepo.alterarTema(model.Temas[i], model.ReuniaoBrainstorm.Id);
                //}

                // se escolhi estado actualizo o estado
                if (model.ReuniaoBrainstorm.EstadoFlag)
                {
                    string data = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    brainRepo.guardarEstado(model.ReuniaoBrainstorm.Estado, data, model.ReuniaoBrainstorm.Id, ut);
                }

                return RedirectToAction("EditarReuniao", new { model.ReuniaoBrainstorm.Id });
            }

            return View();
        }

       

        public ActionResult EditarReuniao(int id)
        {
            Sessoes();
            BrainstormRepository brainRepo = new BrainstormDB();
            IIntervenientes repo = new IntervenientesDB();

            var reuniaoBrainstorm = new ReuniaoBrainstorm();
            var intervenientesSelecionados = new List<Interveniente>();
            var intervenientes = new List<Interveniente>();
            var temas = new List<Tema>();
            //List<Tema> temasAux = new List<Tema>(new Tema[10]);

            string workflow;
            //var viewModel = new BrainstormViewModel();

            reuniaoBrainstorm = brainRepo.getReuniaoBrainstorm(id);
            reuniaoBrainstorm.Id = id;

            // temas
            temas = brainRepo.getBrainstormTemas(id);
            int tamanhoLista = 10;
            tamanhoLista = tamanhoLista - temas.Count;
            List<Tema> temasAux = new List<Tema>(new Tema[tamanhoLista]);
            temas = temas.Concat(temasAux).ToList();


            intervenientesSelecionados = repo.getBrainstormIntervenientes(id);
            intervenientes = repo.getUT();

            // tratar workflow
            workflow = brainRepo.getWorkflow(reuniaoBrainstorm.Id);          
            var workflows = new List<WorkflowEstados>();
            workflows = tratarWorkflow(workflow);

            var viewModel = new BrainstormViewModel
            {
                ReuniaoBrainstorm = reuniaoBrainstorm,
                Intervenientes = intervenientes,
                Temas = temas,
                IntervenientesSelecionados = intervenientesSelecionados,
                SequenciaEstados = workflows

            };
            // Se a reuniao estiver anulada redireciona para a view reuniaoAnulada
            if (reuniaoBrainstorm.Estado == "X")
            {
                return View("ReuniaoAnulada", viewModel);
            }
            return View("Reuniao", viewModel);
        }


        /*** GESTAO DE BRAINSTORM***/
        public ActionResult GestaoBrainstorm()
        {
            Sessoes();
            BrainstormRepository brainRepo = new BrainstormDB();


            var reunioes = new List<GestaoBrainstorm>();
            reunioes = brainRepo.getReunioesBrainstorm();

            var viewModel = new GestaoBrainstormViewModel()
            {
               Reunioes = reunioes

            };
            return View(viewModel);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult GestaoBrainstorm(int id)
        //{
        //    BrainstormRepository brainRepo = new BrainstormDB();

        //    return RedirectToAction("EditarReuniao", new { id});


            
        //}      
        public ActionResult GestaoEdit(int id)
        {
            Sessoes();
            BrainstormRepository brainRepo = new BrainstormDB();

            return RedirectToAction("EditarReuniao", new { id });


        }


        
        public ActionResult GestaoDelete(int id)
        {
            BrainstormRepository brainRepo = new BrainstormDB();
            brainRepo.eliminarBrainstorm(id);

            //var reunioes = new List<GestaoBrainstorm>();
            //reunioes = brainRepo.getReunioesBrainstorm();

            //var viewModel = new GestaoBrainstormViewModel()
            //{
            //    Reunioes = reunioes

            //};
            return null;
        }


        public List<Interveniente> tratarInterv(List<Interveniente> intervs)
        {

            for (int i = 0; i < intervs.Count; i++)
            {
                string s = intervs[i].NomeAndCodigo;
                if (!(String.IsNullOrWhiteSpace(s)))
                {
                    var split = s.Split(new[] { "   " }, StringSplitOptions.None);
                    split[0] = split[0].Substring(1, split[0].Length - 2);
                    intervs[i].Nome = split[1];
                    intervs[i].Codigo = split[0];
                }
            }
            return intervs;
        }


        /*** FUNÇÔES***/
        public List<Interveniente> tratarInterv2(string[] intervsAux)
        {
            var intervs = new List<Interveniente>();
            for (int i = 0; i < intervsAux.Length; i++)
            {
                string s = intervsAux[i];
                Interveniente interveniente = new Interveniente();
                if (!(String.IsNullOrWhiteSpace(s)))
                {
                    var split = s.Split(new[] { "   " }, StringSplitOptions.None);
                    split[0] = split[0].Substring(1, split[0].Length - 2);
                    interveniente.Nome = split[1];
                    interveniente.Codigo = split[0];
                }

                intervs.Add(interveniente);
            }
            return intervs;
        }

        public List<WorkflowEstados> tratarWorkflow(string workflow)
        {
            //var intervs = new List<Interveniente>();
            Char delimiter = '|';
            Char delimiter2 = ';';
            var workflows = new List<WorkflowEstados>();
           

            String[] substrings = workflow.Split(delimiter);
            int i = 1;
            foreach (var substring in substrings)
            {
                // last element
                WorkflowEstados workflowEstado = new WorkflowEstados();
                int tamanho = substrings.Length;
                if (i!=tamanho)
                {
                    String[] subsubstrings = substring.Split(delimiter2);
                    Estado estado = (Estado)Enum.Parse(typeof(Estado), subsubstrings[0]);
                    workflowEstado.estado = estado;
                    workflowEstado.data = subsubstrings[1];
                    workflowEstado.UtilizadorDescritivo = subsubstrings[2];
                    workflows.Add(workflowEstado);

                }
               
                i++;
            }
               

            return workflows;
        }

        public ActionResult Sessoes()
        {
            if (Session["utilizador_codigo"] != null)
            {
                var ut = Session["utilizador_codigo"].ToString();
            }

            else
            {
                //return RedirectToAction("Reuniao");
                return HttpNotFound();
            }

            return null;
        }
        //for (int i = 0; i < intervsAux.Length; i++)
        //{
        //    string s = intervsAux[i];
        //    Interveniente interveniente = new Interveniente();
        //    if (!(String.IsNullOrWhiteSpace(s)))
        //    {
        //        var split = s.Split(new[] { "   " }, StringSplitOptions.None);
        //        split[0] = split[0].Substring(1, split[0].Length - 2);
        //        interveniente.Nome = split[1];
        //        interveniente.Codigo = split[0];
        //    }

        //    intervs.Add(interveniente);
        //}
        //    return null;
        //}
    }
}