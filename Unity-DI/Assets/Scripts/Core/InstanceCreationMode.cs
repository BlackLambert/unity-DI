
namespace SBaier.DI
{
    public enum InstanceCreationMode
    {
        Undefined = 0,
        FromNew = 1,
        FromInstance = 2,
        FromMethod = 4,
        FromFactory = 8,
        FromPrefabInstance = 16,
        FromRessourcePrefabInstance = 32,
    }
}