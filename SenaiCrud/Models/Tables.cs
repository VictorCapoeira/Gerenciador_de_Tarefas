using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SenaiCrud.Models.Tables
{
    [Table("Tarefas")]
    public class Tarefas
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public bool Concluida { get; set; }
    }
}