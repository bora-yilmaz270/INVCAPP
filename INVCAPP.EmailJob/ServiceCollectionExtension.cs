using INVCAPP.Core.Repositories;
using INVCAPP.Core.Services;
using INVCAPP.Repository;
using INVCAPP.Repository.Repositories;
using INVCAPP.Service.Services;

namespace INVCAPP.EmailJob
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ServiceBusiness(this IServiceCollection service)
        {
            service.AddDbContext<AppDbContext>(ServiceLifetime.Transient,ServiceLifetime.Transient);

            service.AddTransient<IInvoiceRepository, InvoiceRepository>();
            
            service.AddTransient<IInvoiceService, InvoiceService>();

            service.AddTransient<IEmailService, EmailService.EmailService>();

            return service;
        }

    }

}
