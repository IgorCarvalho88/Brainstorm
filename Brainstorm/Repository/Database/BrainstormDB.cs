using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using Brainstorm.Models;
using Brainstorm.ViewModel;

namespace Brainstorm.Repository.Database
{
    public class BrainstormDB : BrainstormRepository
    {
        public DataRow guardarReuniao(BrainstormViewModel model)
        {
         
            SqlConnection con = null;
         
            try
            {
                string strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                con = new SqlConnection(strConnString);
            }
            catch (Exception ex)
            {
                throw new Exception("Impossível de momento ligar a base de dados, tente mais tarde", ex.InnerException);
            }
            try
            {
                SqlCommand sqlComm = new SqlCommand("[dbo].[guardarBrainstorm]", con);
                //(intervenientes valor como default ???????)
                sqlComm.Parameters.AddWithValue("@brainstorm_data", model.ReuniaoBrainstorm.Data);              
                sqlComm.Parameters.AddWithValue("@brainstorm_est_codigo", model.ReuniaoBrainstorm.Estado);
                sqlComm.Parameters.AddWithValue("@brainstorm_duracaoPrev", model.ReuniaoBrainstorm.Duracao);
                sqlComm.Parameters.AddWithValue("@brainstorm_duracaoReal", model.ReuniaoBrainstorm.DuracaoReal);
                sqlComm.Parameters.AddWithValue("@brainstorm_observacoes", ((object)model.ReuniaoBrainstorm.Observacoes) ?? DBNull.Value);
                sqlComm.Parameters.AddWithValue("@brainstorm_ut_ins", ((object)model.ReuniaoBrainstorm.Utilizador_ins) ?? DBNull.Value);
                sqlComm.Parameters.AddWithValue("@brainstorm_ut_alt", ((object)model.ReuniaoBrainstorm.Utilizador_alt) ?? DBNull.Value);
                sqlComm.Parameters.AddWithValue("@brainstorm_data_ins", ((object)model.ReuniaoBrainstorm.Data_ins) ?? DBNull.Value);
                sqlComm.Parameters.AddWithValue("@brainstorm_data_alt", ((object)model.ReuniaoBrainstorm.Data_alt) ?? DBNull.Value);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv1_codigo", model.Intervenientes[0].Codigo);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv2_codigo",((object)model.Intervenientes[1].Codigo) ?? DBNull.Value);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv3_codigo", ((object)model.Intervenientes[2].Codigo) ?? DBNull.Value);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv4_codigo", ((object)model.Intervenientes[3].Codigo) ?? DBNull.Value);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv5_codigo", ((object)model.Intervenientes[4].Codigo) ?? DBNull.Value);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv1_descritivo", model.Intervenientes[0].Nome);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv2_descritivo", ((object)model.Intervenientes[1].Nome) ?? DBNull.Value);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv3_descritivo", ((object)model.Intervenientes[2].Nome) ?? DBNull.Value);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv4_descritivo", ((object)model.Intervenientes[3].Nome) ?? DBNull.Value);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv5_descritivo", ((object)model.Intervenientes[4].Nome) ?? DBNull.Value);
                sqlComm.Parameters.AddWithValue("@brainstorm_local", ((object)model.ReuniaoBrainstorm.Local) ?? DBNull.Value);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                DataTable dt = new DataTable();
                da.Fill(dt);

               


                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                else
                {
                    
                    return dt.Rows[0];
                }

            }
            catch (SqlException ex)
            {
                System.Console.WriteLine("EXCEPÇÃO  get periodos ultimas 8 horas: " + ex.Message);
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return null;

        }



        public DataRow guardarTema(Tema tema, int id)
        {
            // tratar model



            SqlConnection con = null;
            //day = new DateTime(2017, 3, 6);
            try
            {
                string strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                con = new SqlConnection(strConnString);
            }
            catch (Exception ex)
            {
                throw new Exception("Impossível de momento ligar a base de dados, tente mais tarde", ex.InnerException);
            }
            try
            {

                SqlCommand sqlComm = new SqlCommand("[dbo].[guardarBrainstormTema]", con);
                
                sqlComm.Parameters.AddWithValue("@brainstorm_tema_brainstorm_id", id);
                sqlComm.Parameters.AddWithValue("@brainstorm_tema_titulo", ((object)tema.Titulo)?? DBNull.Value);
                sqlComm.Parameters.AddWithValue("@brainstorm_tema_descricao", ((object)tema.Descricao) ?? DBNull.Value);
                //sqlComm.Parameters.AddWithValue("@brainstorm_tema_importancia", ((object)tema.Importancia) ?? DBNull.Value);
                sqlComm.Parameters.AddWithValue("@brainstorm_tema_comentarios", ((object)tema.Comentarios) ?? DBNull.Value);
                sqlComm.Parameters.AddWithValue("@brainstorm_tema_estado", ((object)tema.Estado) ?? DBNull.Value);
                //sqlComm.Parameters.AddWithValue("@brainstorm_tema_estado", tema.Estado);
                sqlComm.Parameters.AddWithValue("@brainstorm_tarefa_gestInov", ((object)tema.GestaoInov) ?? DBNull.Value);

                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                DataTable dt = new DataTable();
                da.Fill(dt);




                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                else
                {

                    return dt.Rows[0];
                }

            }
            catch (SqlException ex)
            {
                System.Console.WriteLine("EXCEPÇÃO  get periodos ultimas 8 horas: " + ex.Message);
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return null;

        }

        public DataRow guardarInterveniente(Interveniente interveniente, int idBrainstorm)
        {
            
            SqlConnection con = null;
            
            try
            {
                string strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                con = new SqlConnection(strConnString);
            }
            catch (Exception ex)
            {
                throw new Exception("Impossível de momento ligar a base de dados, tente mais tarde", ex.InnerException);
            }
            try
            {
                
                SqlCommand sqlComm = new SqlCommand("[dbo].[guardarBrainstormInterveniente]", con);

                sqlComm.Parameters.AddWithValue("@brainstorm_interveniente_brainstorm_id", idBrainstorm);
                sqlComm.Parameters.AddWithValue("@brainstorm_interveniente_codigo", ((object)interveniente.Codigo) ?? DBNull.Value);
                sqlComm.Parameters.AddWithValue("@brainstorm_interveniente_descritivo", ((object)interveniente.Nome) ?? DBNull.Value);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                DataTable dt = new DataTable();
                da.Fill(dt);




                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                else
                {

                    return dt.Rows[0];
                }

            }
            catch (SqlException ex)
            {
                System.Console.WriteLine("EXCEPÇÃO  get periodos ultimas 8 horas: " + ex.Message);
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return null;

        }
        public ReuniaoBrainstorm getReuniaoBrainstorm(int id)
        {
            
            SqlConnection con = null;         
            try
            {
                string strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                con = new SqlConnection(strConnString);
            }
            catch (Exception ex)
            {
                throw new Exception("Impossível de momento ligar a base de dados, tente mais tarde", ex.InnerException);
            }
            try
            {
              
                SqlCommand sqlComm = new SqlCommand("[dbo].[getReuniaoBrainstorm]", con);

                sqlComm.Parameters.AddWithValue("@brainstorm_id", id);
               

                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                DataTable dt = new DataTable();
                da.Fill(dt);


                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                else
                {
                    ReuniaoBrainstorm reuniaoBrainstorm = new ReuniaoBrainstorm();
                    //dt.Rows[0]["brainstorm_data"].Convert(val => DateTime.Parse(val.ToString()).ToString("dd/MMM/yyyy"));
                    reuniaoBrainstorm.Data = ((DateTime)dt.Rows[0]["brainstorm_data"]).ToString("dd/MM/yyyy");
                    reuniaoBrainstorm.Estado = dt.Rows[0]["brainstorm_est_codigo"].ToString();
                    reuniaoBrainstorm.Duracao = Convert.ToInt32(dt.Rows[0]["brainstorm_duracaoPrev"].ToString());
                    reuniaoBrainstorm.DuracaoReal = Convert.ToInt32(dt.Rows[0]["brainstorm_duracaoReal"].ToString());
                    reuniaoBrainstorm.Observacoes = dt.Rows[0]["brainstorm_observacoes"].ToString();
                    reuniaoBrainstorm.Local = dt.Rows[0]["brainstorm_local"].ToString();
                    reuniaoBrainstorm.Utilizador_ins = dt.Rows[0]["brainstorm_ut_ins"].ToString();
                    reuniaoBrainstorm.Utilizador_alt = dt.Rows[0]["brainstorm_ut_alt"].ToString();
                    reuniaoBrainstorm.Data_ins = ((DateTime)dt.Rows[0]["brainstorm_data_ins"]).ToString("dd/MM/yyyy");
                    reuniaoBrainstorm.Data_alt = ((DateTime)dt.Rows[0]["brainstorm_data_alt"]).ToString("dd/MM/yyyy");


                    return reuniaoBrainstorm;
                }

            }
            catch (SqlException ex)
            {
                System.Console.WriteLine("EXCEPÇÃO  get periodos ultimas 8 horas: " + ex.Message);
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return null;

        }

        public List<Tema> getBrainstormTemas(int id)
        {
            SqlConnection con = null;
            try
            {
                string strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                con = new SqlConnection(strConnString);
            }
            catch (Exception ex)
            {
                throw new Exception("Impossível de momento ligar a base de dados, tente mais tarde", ex.InnerException);
            }
            try
            {

                SqlCommand sqlComm = new SqlCommand("[dbo].[getBrainstormTemas]", con);

                sqlComm.Parameters.AddWithValue("@brainstorm_tema_brainstorm_id", id);


                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                DataTable dt = new DataTable();
                da.Fill(dt);
               // var intervenientes = new List<Interveniente>();
                var temas = new List<Tema>();
                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Tema tema = new Tema();
                        tema.Id = Convert.ToInt32(row["brainstorm_tema_id"].ToString());
                        tema.Titulo = row["brainstorm_tema_titulo"].ToString();
                        tema.Descricao = row["brainstorm_tema_descricao"].ToString();
                        //tema.Importancia = row["brainstorm_tema_importancia"].ToString();
                        tema.Comentarios = row["brainstorm_tema_comentarios"].ToString();
                        tema.Estado = row["brainstorm_tema_estado"].ToString();
                        tema.GestaoInov = Convert.ToInt32(dt.Rows[0]["brainstorm_tarefa_gestInov"].ToString());
                        temas.Add(tema);
                    }


                    return temas;
                }

            }
            catch (SqlException ex)
            {
                System.Console.WriteLine("EXCEPÇÃO  get periodos ultimas 8 horas: " + ex.Message);
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return null;

        }

        public DataRow alterarReuniao(BrainstormViewModel model, int id)
        {
          

           SqlConnection con = null;
          
            try
            {
                string strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                con = new SqlConnection(strConnString);
            }
            catch (Exception ex)
            {
                throw new Exception("Impossível de momento ligar a base de dados, tente mais tarde", ex.InnerException);
            }
            try
            {
                SqlCommand sqlComm = new SqlCommand("[dbo].[alterarBrainstorm]", con);
                //(intervenientes valor como default ???????)
                sqlComm.Parameters.AddWithValue("@brainstorm_id", id);
                sqlComm.Parameters.AddWithValue("@brainstorm_data", model.ReuniaoBrainstorm.Data);
                sqlComm.Parameters.AddWithValue("@brainstorm_est_codigo", model.ReuniaoBrainstorm.Estado);
                sqlComm.Parameters.AddWithValue("@brainstorm_duracaoPrev", model.ReuniaoBrainstorm.Duracao);
                sqlComm.Parameters.AddWithValue("@brainstorm_duracaoReal", model.ReuniaoBrainstorm.DuracaoReal);
                sqlComm.Parameters.AddWithValue("@brainstorm_observacoes", ((object)model.ReuniaoBrainstorm.Observacoes) ?? DBNull.Value);
                sqlComm.Parameters.AddWithValue("@brainstorm_local", ((object)model.ReuniaoBrainstorm.Local) ?? DBNull.Value);
                //sqlComm.Parameters.AddWithValue("@brainstorm_ut_ins", ((object)model.ReuniaoBrainstorm.Utilizador_ins) ?? DBNull.Value);
                sqlComm.Parameters.AddWithValue("@brainstorm_ut_alt", ((object)model.ReuniaoBrainstorm.Utilizador_alt) ?? DBNull.Value);
                //sqlComm.Parameters.AddWithValue("@brainstorm_data_ins", ((object)model.ReuniaoBrainstorm.Data_ins) ?? DBNull.Value);
                sqlComm.Parameters.AddWithValue("@brainstorm_data_alt", ((object)model.ReuniaoBrainstorm.Data_alt) ?? DBNull.Value);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv1_codigo", model.Intervenientes[0].Codigo);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv2_codigo", ((object)model.Intervenientes[1].Codigo) ?? DBNull.Value);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv3_codigo", ((object)model.Intervenientes[2].Codigo) ?? DBNull.Value);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv4_codigo", ((object)model.Intervenientes[3].Codigo) ?? DBNull.Value);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv5_codigo", ((object)model.Intervenientes[4].Codigo) ?? DBNull.Value);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv1_descritivo", model.Intervenientes[0].Nome);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv2_descritivo", ((object)model.Intervenientes[1].Nome) ?? DBNull.Value);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv3_descritivo", ((object)model.Intervenientes[2].Nome) ?? DBNull.Value);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv4_descritivo", ((object)model.Intervenientes[3].Nome) ?? DBNull.Value);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv5_descritivo", ((object)model.Intervenientes[4].Nome) ?? DBNull.Value);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                DataTable dt = new DataTable();
                da.Fill(dt);




                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                else
                {

                    return dt.Rows[0];
                }

            }
            catch (SqlException ex)
            {
                System.Console.WriteLine("EXCEPÇÃO  get periodos ultimas 8 horas: " + ex.Message);
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return null;

        }

        public DataRow alterarTema(Tema tema, int id)
        {
           

            SqlConnection con = null;
         
            try
            {
                string strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                con = new SqlConnection(strConnString);
            }
            catch (Exception ex)
            {
                throw new Exception("Impossível de momento ligar a base de dados, tente mais tarde", ex.InnerException);
            }
            try
            {

                //@brainstorm_tema_brainstorm_id int,
                //@brainstorm_tema_titulo nvarchar(4000),
                //@brainstorm_tema_descricao nvarchar(4000),
                //@brainstorm_tema_importancia nvarchar(4000),
                //@brainstorm_tema_comentarios nvarchar(4000),
                //@brainstorm_tema_estado codigo_pequeno,
                //    @brainstorm_tarefa_gestInov int
                SqlCommand sqlComm = new SqlCommand("[dbo].[alterarBrainstormTema]", con);

                sqlComm.Parameters.AddWithValue("@brainstorm_tema_id", tema.Id);
                sqlComm.Parameters.AddWithValue("@brainstorm_tema_brainstorm_id", id);
                sqlComm.Parameters.AddWithValue("@brainstorm_tema_titulo", tema.Titulo);
                sqlComm.Parameters.AddWithValue("@brainstorm_tema_descricao", tema.Descricao);
                //sqlComm.Parameters.AddWithValue("@brainstorm_tema_importancia", tema.Importancia);
                sqlComm.Parameters.AddWithValue("@brainstorm_tema_comentarios", tema.Comentarios);
                sqlComm.Parameters.AddWithValue("@brainstorm_tema_estado", tema.Estado);
                sqlComm.Parameters.AddWithValue("@brainstorm_tarefa_gestInov", tema.GestaoInov);

                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                DataTable dt = new DataTable();
                da.Fill(dt);




                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                else
                {

                    return dt.Rows[0];
                }

            }
            catch (SqlException ex)
            {
                System.Console.WriteLine("EXCEPÇÃO  get periodos ultimas 8 horas: " + ex.Message);
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return null;

        }

        public DataRow guardarEstado(string estado, string data, int id, string utilizador)
        {

            SqlConnection con = null;

            try
            {
                string strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                con = new SqlConnection(strConnString);
            }
            catch (Exception ex)
            {
                throw new Exception("Impossível de momento ligar a base de dados, tente mais tarde", ex.InnerException);
            }
            try
            {
                SqlCommand sqlComm = new SqlCommand("[dbo].[guardarBrainstormEstado]", con);
             
                sqlComm.Parameters.AddWithValue("@brainstorm_wf_estado_brainstorm_id", id);
                sqlComm.Parameters.AddWithValue("@brainstorm_estado_ut_codigo", utilizador);
                sqlComm.Parameters.AddWithValue("@brainstorm_estado_data", data);
                sqlComm.Parameters.AddWithValue("@brainstorm_estado_est_codigo", estado);              
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                DataTable dt = new DataTable();
                da.Fill(dt);




                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                else
                {

                    return dt.Rows[0];
                }

            }
            catch (SqlException ex)
            {
                System.Console.WriteLine("EXCEPÇÃO  get periodos ultimas 8 horas: " + ex.Message);
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return null;

        }

        public List<GestaoBrainstorm> getReunioesBrainstorm()
        {

            SqlConnection con = null;
            try
            {
                string strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                con = new SqlConnection(strConnString);
            }
            catch (Exception ex)
            {
                throw new Exception("Impossível de momento ligar a base de dados, tente mais tarde", ex.InnerException);
            }
            try
            {

                SqlCommand sqlComm = new SqlCommand("select *, (select top 1 brainstorm_tema_descricao from brainstorm_tema where brainstorm_tema_brainstorm_id = brainstorm_id order by brainstorm_tema_id desc) as descricao from brainstorm", con);

                //sqlComm.Parameters.AddWithValue("@brainstorm_id", id);


                //sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                DataTable dt = new DataTable();
                da.Fill(dt);

                var reunioes = new List<GestaoBrainstorm>();


                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        GestaoBrainstorm reuniaoBrainstorm = new GestaoBrainstorm();

                        reuniaoBrainstorm.Id = Convert.ToInt32(row["brainstorm_id"].ToString());
                        // reuniaoBrainstorm.Titulo = row["brainstorm_est_codigo"].ToString();
                        reuniaoBrainstorm.Data = ((DateTime)row["brainstorm_data"]).ToString("dd/MM/yyyy");
                        reuniaoBrainstorm.Estado = row["brainstorm_est_codigo"].ToString();
                        reuniaoBrainstorm.Duracao = Convert.ToInt32(row["brainstorm_duracaoPrev"].ToString());
                        reuniaoBrainstorm.DuracaoReal = Convert.ToInt32(row["brainstorm_duracaoReal"].ToString());
                        reuniaoBrainstorm.Observacoes = row["brainstorm_observacoes"].ToString();
                        reuniaoBrainstorm.Local = row["brainstorm_local"].ToString();
                        reuniaoBrainstorm.Descricao = row["descricao"].ToString();
                        //reuniaoBrainstorm.Utilizador_ins = row["brainstorm_ut_ins"].ToString();
                        //reuniaoBrainstorm.Utilizador_alt = row["brainstorm_ut_alt"].ToString();
                        //reuniaoBrainstorm.Data_ins = ((DateTime)row["brainstorm_data_ins"]).ToString("dd/MM/yyyy");
                        //reuniaoBrainstorm.Data_alt = ((DateTime)row["brainstorm_data_alt"]).ToString("dd/MM/yyyy");
                        reunioes.Add(reuniaoBrainstorm);
                    }
                    return reunioes;
                }
           

            }
            catch (SqlException ex)
            {
                System.Console.WriteLine("EXCEPÇÃO  get periodos ultimas 8 horas: " + ex.Message);
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return null;

        }

        public void eliminarBrainstorm(int id)
        {

            SqlConnection con = null;
            try
            {
                string strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                con = new SqlConnection(strConnString);
            }
            catch (Exception ex)
            {
                throw new Exception("Impossível de momento ligar a base de dados, tente mais tarde", ex.InnerException);
            }
            try
            {

                SqlCommand sqlComm = new SqlCommand("[dbo].[eliminarBrainstorm]", con);

                sqlComm.Parameters.AddWithValue("@brainstorm_id", id);


                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                DataTable dt = new DataTable();
                da.Fill(dt);
               
            }
            catch (SqlException ex)
            {
                System.Console.WriteLine("EXCEPÇÃO  get periodos ultimas 8 horas: " + ex.Message);
                
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }           

        }


        public string getWorkflow(int id)
        {

            SqlConnection con = null;
            try
            {
                string strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                con = new SqlConnection(strConnString);
            }
            catch (Exception ex)
            {
                throw new Exception("Impossível de momento ligar a base de dados, tente mais tarde", ex.InnerException);
            }
            try
            {

                SqlCommand sqlComm = new SqlCommand("SELECT dbo.brainstormWorkflow(@brainstorm_id) AS Workflow", con);

                sqlComm.Parameters.AddWithValue("@brainstorm_id", id);

                //SqlParameter brainstorm_id = new SqlParameter("@brainstorm_id", SqlDbType.Int);
                //brainstorm_id.Value = id;

                // sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                DataTable dt = new DataTable();
                da.Fill(dt);
                //string workflow;
                //workflow = sqlComm.ExecuteScalar();
                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                else
                {
                    string workflow;
                    workflow = dt.Rows[0]["Workflow"].ToString();

                    return workflow;
                }

            }
            catch (SqlException ex)
            {
                System.Console.WriteLine("EXCEPÇÃO  get periodos ultimas 8 horas: " + ex.Message);

            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return null;

        }

        public void deleteTemas(int? id)
        {
            SqlConnection con = null;
            try
            {
                string strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                con = new SqlConnection(strConnString);
            }
            catch (Exception ex)
            {
                throw new Exception("Impossível de momento ligar a base de dados, tente mais tarde", ex.InnerException);
            }
            try
            {
                SqlCommand sqlComm = new SqlCommand("Delete FROM brainstorm_tema WHERE brainstorm_tema_brainstorm_id =" + id, con);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                DataTable dt = new DataTable();
                da.Fill(dt);

                var intervenientes = new List<Interveniente>();

            }
            catch (SqlException ex)
            {
                System.Console.WriteLine("EXCEPÇÃO  get periodos ultimas 8 horas: " + ex.Message);

            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }


        }
    }
}