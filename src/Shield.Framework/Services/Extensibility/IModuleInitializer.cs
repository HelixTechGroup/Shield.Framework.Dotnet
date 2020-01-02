namespace Shield.Framework.Services.Extensibility
{
    public interface IModuleInitializer
    {
        void Initialize(ModuleInfo moduleInfo);
    }
}