using System;
using System.ComponentModel.DataAnnotations;

namespace TasksApi.Models
{
  public class Tarefa
  {
      public Guid Id {get; set;}

      public Guid UsuarioId {get; set;}

    [Required]
    public string Nome {get; set;}

      public string Data {get; set;}

      public bool Concluida {get; set;}
  }
}