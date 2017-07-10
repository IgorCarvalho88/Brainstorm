using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


    public partial class Sessoes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["t_sessao"] != null && Request.QueryString["t_sessao"].ToString() == "1")
            {
                try
                {
                    Response.Write(HttpContext.Current.Session["Utilizador_codigo"].ToString()+"||"+ HttpContext.Current.Session["Empresa_codigo"].ToString());
                }
                catch(Exception ex)
                {
                    Response.Write("ERRO");
                }
            }
            else
            {
                if (Request.QueryString["t_sessao"] != null && Request.QueryString["t_sessao"].ToString() == "2" && Request.QueryString["utilizador"] != null && Request.QueryString["utilizador"].ToString() != "")
                {
                    carregasessoes(Request.QueryString["utilizador"].ToString());
                    Response.Write("Sucesso");
                }
                else
                {
                    Response.Write("ERRO");
                }
            }

        }

        public void carregasessoes(string ut)
        {
            string sSql = "";
            string strConnString = "";

            try
            {
                sSql = "select variavel, valor from asptoaspxsession where utilizador='" + Request.QueryString["utilizador"].ToString() + "'";


                strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;


                SqlConnection con = new SqlConnection(strConnString);

                SqlCommand com = new SqlCommand(sSql, con);
                com.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader RDB = com.ExecuteReader();
                string ses = "";

                while (RDB.Read())
                {
                    HttpContext.Current.Session[RDB.GetValue(0).ToString().ToLower()] = RDB.GetValue(1).ToString();
                    ses += RDB.GetValue(0).ToString().ToLower() + " = " + RDB.GetValue(1).ToString() + ";";
                }


                RDB.Close();
                con.Close();
            }
            catch (SqlException erro)
            {
                Response.Write(erro.Message);
            }

        }
    }