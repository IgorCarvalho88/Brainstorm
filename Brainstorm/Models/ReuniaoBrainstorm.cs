﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace Brainstorm.Models
{
    public class ReuniaoBrainstorm
    {
        //[Display(Name = "Data")]
        public int Id { get; set; }

        public ReuniaoBrainstorm()
        {
            Id = 0;
            EstadoFlag = false;
        }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Data é obrigatório")]
        public string Data { get; set; }

        [Required(ErrorMessage = "Duração é obrigatório")]
        public int? Duracao { get; set; }

        [Required(ErrorMessage = "Duração real é obrigatório")]
        public int? DuracaoReal { get; set; }

        public string Estado { get; set; }
        public DateTime DataEstado { get; set; }
        public string Comentarios { get; set; }
        public bool EstadoFlag { get; set; }
        public string Local { get; set; }

    }
}