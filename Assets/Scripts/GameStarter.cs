using Unity.Entities;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    void Start()
    {
        var world = World.DefaultGameObjectInjectionWorld;
        var systems = DefaultWorldInitialization.GetAllSystems(WorldSystemFilterFlags.Default);

        DefaultWorldInitialization.AddSystemsToRootLevelSystemGroups(world, systems);

        world.GetOrCreateSystemManaged<EnemySpawnSystem>();
        world.GetOrCreateSystemManaged<EnemyMovementSystem>();

        Debug.Log("DOTS systems initialized");
    }
}