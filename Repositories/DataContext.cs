using Microsoft.EntityFrameworkCore;
using TasksApi.Models;

namespace TasksApi.Repositories{

  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions options):base(options){}
    public DbSet<Tarefa> Tarefas{get; set;}
    public DbSet<Usuario> Usuarios{get; set;}
  }
}