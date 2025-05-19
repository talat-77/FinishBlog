using Blog.DataAccess.Repository;
using Blogger.Buisiness.Abstract;
using Blogger.Buisiness.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger.Buisiness
{
    public static class ServiceExtension
    {
        public static void AddServiceBuisiness(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));
            services.AddScoped<IPostService,PostManager>();
            services.AddScoped<IPostService, PostManager>();
            services.AddScoped<ICommentService, CommentManager>();
            services.AddScoped<IUserService, UserManager>();
        }
    }
}
