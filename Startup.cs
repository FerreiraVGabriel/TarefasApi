using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TasksApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace TasksApi
{
    public class Startup
    {
       public IConfiguration Configuration {get; set;}

        public Startup (IConfiguration configuration){
          Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
           services.AddControllers();

            var key = "UmTokenMuitoGrandeParaNinguemDescobrir";
            var keyByte = Encoding.ASCII.GetBytes(key);
           services.AddAuthentication(
             options=>{
                options.DefaultAuthenticateScheme =JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
             }
           ).AddJwtBearer(options=>{
             options.RequireHttpsMetadata = false;
             options.SaveToken = true;
             options.TokenValidationParameters = new TokenValidationParameters{
               ValidateIssuerSigningKey =true,
               IssuerSigningKey = new SymmetricSecurityKey(keyByte),
               ValidateIssuer = false,
               ValidateAudience = false,
             };
           });
            //services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("BDTarefas"));
            services.AddDbContext<DataContext>(options =>options.UseNpgsql(Configuration.GetConnectionString("Heroku")));
            services.AddTransient<ITarefaRepository,TarefaRepository>();
            services.AddTransient<IUsuarioRepository,UsuarioRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
