using Blog.DataAccess.Context;
using Blog.DataAccess.Repository.Abstract;
using Blog.DataAccess.Repository.Concrete;
using Blog.DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddDataAccesService(this IServiceCollection service)
        {
            service.AddDbContext<BlogDbContext>(options => options.UseSqlServer(Configurations.DbConfiguration.GetConnectionString));
            service.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            service.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
