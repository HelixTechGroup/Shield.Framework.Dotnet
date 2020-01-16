using Shin.Framework;

namespace Shield.Framework.Services.Extensibility
{
    public interface IAssemblyResolver : IDispose
    {
        void LoadAssemblyFrom(string assemblyPath);
    }
}
