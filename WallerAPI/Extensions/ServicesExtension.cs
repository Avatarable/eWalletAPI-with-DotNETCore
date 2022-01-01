using Microsoft.Extensions.DependencyInjection;
using WallerAPI.Services.Implementations;
using WallerAPI.Services.Interfaces;

namespace WallerAPI.Extensions
{
    public static class ServicesExtension
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IUtilServices, UtilServices>();
            services.AddScoped<IAuthServices, AuthServices>();

            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IRoleServices, RoleServices>();
            services.AddScoped<ITransactionServices, TransactionServices>();
            services.AddScoped<ICurrencyServices, CurrencyServices>();
            services.AddScoped<IWalletServices, WalletServices>();
            services.AddScoped<IPhotoServices, PhotoServices>();
        }
    }
}
