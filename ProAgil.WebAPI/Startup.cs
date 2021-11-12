using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProAgil.Repository;


namespace ProAgil.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
             services.AddDbContext<ProAgilContext>(x=>x.UseSqlite(Configuration.GetConnectionString("DefaultConnection"))); 
             
             //Aqui estamo injetando por dependencia a minha classe de repositorio
             //Toda vez que alguem chamar na controller o IProAgilRepository, estara utilizando a classe de repositorio pronta
             //observe na controller, que não é mais chamado o ProAgilContext.. e sim direto o repositorio
             services.AddScoped<IProAgilRepository, ProAgilRepository>();

             services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
             

            //Nao esqueça de adicionar a permissao do cors para chamadas cruzadas entre os servidores
            services.AddCors();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            //Nao se esqueça tambem de adicionar antes da chamada o item abaixo UseMVC.. por que depois nao adianta mais que ele usou o mvc.. entao é fundamentel colcocar 
            //ANTES DA CHAMADA O UseMVC()
            //** entao falo para ele que permito qualquer origem, qualquer metodo e qualquer cabeçalho
            app.UseCors(c=>c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            //Com o item abaixo voce permite que o serviço disponibilize as imagens
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
