using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct PlayerSpawnerComponent : IComponentData
{
    public Entity PlayerPrefab;
    public float2 PlayerSpawnPosition;
}
