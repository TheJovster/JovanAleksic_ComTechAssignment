using Unity.Entities;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    void Start()
    {
        var world = World.DefaultGameObjectInjectionWorld;
        var systems = DefaultWorldInitialization.GetAllSystems(WorldSystemFilterFlags.Default);
        DefaultWorldInitialization.AddSystemsToRootLevelSystemGroups(world, systems);
    }
}