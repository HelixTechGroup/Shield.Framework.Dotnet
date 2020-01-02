namespace Shield.Sandbox.Windows
{
    internal class Program
    {
        #region Methods
        private static void Main(string[] args)
        {
            using var app = new SandboxApplication();
            app.Run();
        }
        #endregion
    }
}