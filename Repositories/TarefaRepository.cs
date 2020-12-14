using System.Collections.Generic;
using TasksApi.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TasksApi.Repositories
{
  public interface ITarefaRepository
  { 
    List<Tarefa> Read(Guid id);
    void Create (Tarefa tarefa);

    void Delete(Guid id);
    void Update (Guid id,Tarefa tarefa);
  }
  public class TarefaRepository: ITarefaRepository{
    private readonly DataContext _context;
    public TarefaRepository(DataContext context)
    {
      _context = context;
    }
    public void Create(Tarefa tarefa){
      
      tarefa.Id = Guid.NewGuid();
      _context.Tarefas.Add(tarefa);
      _context.SaveChanges();
    }
     public void Delete(Guid id){
      var tarefa = _context.Tarefas.Find(id);
      _context.Entry(tarefa).State =EntityState.Deleted;
      _context.SaveChanges();
    }
     public List<Tarefa> Read(Guid id){
      return _context.Tarefas.Where(x=>x.Id == id).ToList();
    }
     public void Update (Guid id,Tarefa tarefa){
       var _tarefa = _context.Tarefas.Find(id);
       
       _tarefa.Nome = tarefa.Nome;
        _tarefa.Concluida = tarefa.Concluida;

      _context.Entry(tarefa).State =EntityState.Deleted;
      _context.SaveChanges();
    }
  }
}