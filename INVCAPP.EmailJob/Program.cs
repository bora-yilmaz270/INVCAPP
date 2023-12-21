using INVCAPP.Service.Mapping;

namespace INVCAPP.EmailJob
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices(services =>
                {

                    services.AddHostedService<Worker>();
                    services.ServiceBusiness();
                    services.AddAutoMapper(typeof(MapProfile));

                })
                .Build();
            host.Run();
        }
    }
}


