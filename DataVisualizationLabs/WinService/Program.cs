namespace WinService
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceInitializer si = new ServiceInitializer();
            si.Initialize();
        }
    }
}
