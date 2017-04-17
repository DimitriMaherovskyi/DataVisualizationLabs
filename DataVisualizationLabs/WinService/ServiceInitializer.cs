using Topshelf;

namespace WinService
{
    public class ServiceInitializer : ServiceControl
    {
        public void Initialize()
        {
            HostFactory.Run(f =>
            {
                f.StartAutomatically();
                f.SetServiceName("Lab 8 service");
                f.SetDescription("Startup service, where extensions can be added");
                f.RunAsLocalSystem();

                f.EnableServiceRecovery(r =>
                {
                    // Restarts Service in 1 minute.
                    r.RestartService(1);
                });

                f.Service<ServiceInitializer>();
            });
        }

        public bool Start(HostControl hostControl)
        {
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            return true;
        }
    }
}
