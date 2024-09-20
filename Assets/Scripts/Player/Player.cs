using Unity.Entities;
using Unity.Mathematics;

public struct Player : IComponentData 
{
    public Entity PlayerPrefab;
    public float MoveSpeed;
    public float2 Position;
}

    
