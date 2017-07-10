using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brainstorm.Models
{
    public class GestaoBrainstorm
    {
        //public string Titulo { get; set; }
        public int Id { get; set; }
        public string Data { get; set; }
        public string Estado { get; set; }
        public string Descricao { get; set; }
        public string Observacoes { get; set; }
        public int Duracao { get; set; }
        public int DuracaoReal { get; set; }
        public string Local { get; set; }

        public string Data_ins { get; set; }
        public string Data_alt { get; set; }
        public string Utilizador_ins { get; set; }

        public string Utilizador_alt { get; set; }
    }
}