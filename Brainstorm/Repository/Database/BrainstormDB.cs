﻿using System;
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
        public DataRow GuardarReuniao(BrainstormViewModel model)
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
                SqlCommand sqlComm = new SqlCommand("[dbo].[guardarBrainstorm]", con);
                //sqlComm.Parameters.AddWithValue("@dataNaoConformidade", day);
                
               // DateTime date1 = DateTime.ParseExact(model.ReuniaoBrainstorm.Data, "dd/MM/yyyy", CultureInfo.CurrentCulture); ;
                //DateTime date2 = ;
                //string date = "20/02/2017";
                //sqlComm.Parameters.AddWithValue("@brainstorm_data", date);
                sqlComm.Parameters.AddWithValue("@brainstorm_data", model.ReuniaoBrainstorm.Data);
                // sqlComm.Parameters.AddWithValue("@of_proj_rtp_proj_id", SqlDbType.Date).Value = new DateTime(model.ReuniaoBrainstorm.Data);
                sqlComm.Parameters.AddWithValue("@brainstorm_est_codigo", model.ReuniaoBrainstorm.Estado);
                sqlComm.Parameters.AddWithValue("@brainstorm_duracaoPrev", model.ReuniaoBrainstorm.Duracao);
                sqlComm.Parameters.AddWithValue("@brainstorm_duracaoReal", model.ReuniaoBrainstorm.DuracaoReal);
                sqlComm.Parameters.AddWithValue("@brainstorm_interv1_codigo", model.Intervenientes[0].Codigo);
                sqlComm.Parameters.AddWithValue("@brainstorm_interv2_codigo",((object)model.Intervenientes[1].Codigo) ?? DBNull.Value);
                sqlComm.Parameters.AddWithValue("@brainstorm_interv3_codigo", ((object)model.Intervenientes[2].Codigo) ?? DBNull.Value);
                sqlComm.Parameters.AddWithValue("@brainstorm_interv4_codigo", ((object)model.Intervenientes[3].Codigo) ?? DBNull.Value);
                sqlComm.Parameters.AddWithValue("@brainstorm_interv5_codigo", ((object)model.Intervenientes[4].Codigo) ?? DBNull.Value);
                sqlComm.Parameters.AddWithValue("@brainstorm_interv1_descritivo", model.Intervenientes[0].Nome);
                sqlComm.Parameters.AddWithValue("@brainstorm_interv2_descritivo", ((object)model.Intervenientes[1].Nome) ?? DBNull.Value);
                sqlComm.Parameters.AddWithValue("@brainstorm_interv3_descritivo", ((object)model.Intervenientes[2].Nome) ?? DBNull.Value);
                sqlComm.Parameters.AddWithValue("@brainstorm_interv4_descritivo", ((object)model.Intervenientes[3].Nome) ?? DBNull.Value);
                sqlComm.Parameters.AddWithValue("@brainstorm_interv5_descritivo", ((object)model.Intervenientes[4].Nome) ?? DBNull.Value);               
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv2_codigo", model.Intervenientes[1].Codigo);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv3_codigo", model.Intervenientes[2].Codigo);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv4_codigo", model.Intervenientes[3].Codigo);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv5_codigo", model.Intervenientes[4].Codigo);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv1_descritivo", model.Intervenientes[0].Nome);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv2_descritivo", model.Intervenientes[1].Nome);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv3_descritivo", model.Intervenientes[2].Nome);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv4_descritivo", model.Intervenientes[3].Nome);
                //sqlComm.Parameters.AddWithValue("@brainstorm_interv5_descritivo", model.Intervenientes[4].Nome);
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



        public DataRow GuardarTema(Tema tema, int id)
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

                //@brainstorm_tema_brainstorm_id int,
                //@brainstorm_tema_titulo nvarchar(4000),
                //@brainstorm_tema_descricao nvarchar(4000),
                //@brainstorm_tema_importancia nvarchar(4000),
                //@brainstorm_tema_comentarios nvarchar(4000),
                //@brainstorm_tema_estado codigo_pequeno,
                //    @brainstorm_tarefa_gestInov int
                SqlCommand sqlComm = new SqlCommand("[dbo].[guardarBrainstormTema]", con);
                
                sqlComm.Parameters.AddWithValue("@brainstorm_tema_brainstorm_id", id);
                sqlComm.Parameters.AddWithValue("@brainstorm_tema_titulo", tema.Titulo);
                sqlComm.Parameters.AddWithValue("@brainstorm_tema_descricao", tema.Descricao);
                sqlComm.Parameters.AddWithValue("@brainstorm_tema_importancia", tema.Importancia);
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
    }
}