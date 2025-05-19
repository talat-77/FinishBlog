using Blog.DataAccess.Abstraction;
using Blog.DataAccess.Context;
using Blog.DataAccess.EntityFrameWork;
using Blog.DataAccess.Repository;
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

            service.AddScoped(typeof(IGenericDal<>), typeof(GenericRepository<>));
            service.AddScoped<IPostDal,EFPostDal>();
            service.AddScoped<ICommentDal,EFCommentDal>();
            service.AddScoped<IUserDal,EFUserDal>();
           

        }
    }
}
