using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HiringGame.Application.Interfaces;
using HiringGame.Application.Services;
using HiringGame.DataModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HiringGame
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
            services.AddDbContext<AppDbContext>(
                opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<AppUser, IdentityRole<int>>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            services.AddSession();
            services.AddScoped<DbContext, AppDbContext>();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();


            services.AddScoped<IJobServices,JobServices>();
            services.AddScoped<IVideoServices, VideoServices>();
            services.AddScoped<IQuestionServices, QuestionServices>();
            services.AddScoped<IPlayerServices, PlayerServices>();
            services.AddScoped<IEmailServices, EmailServices>();

            services.AddSwaggerGen();

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Player API V1");
            });
            app.UseAuthentication();

            app.UseRouting();

            app.UseCors("AllowOrigin");

            app.UseStaticFiles();


            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
