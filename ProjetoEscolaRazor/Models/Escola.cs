﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ProjetoEscolaRazor.Model
{
    [Table("Escolas")]
    public class Escola
    {
        public int EscolaId { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public IEnumerable<Turma> Turmas { get; set; }
        [NotMapped]
        public int? QuantidadeTurmas
        {
            get
            {
                if (Turmas != null)
                {
                    return Turmas.Count();
                }
                return null;
            }
        }

        public Escola(int escolaId, string nome, string endereco)
        {
            EscolaId = escolaId;
            Nome = nome;
            Endereco = endereco;            
        }
    }
}
