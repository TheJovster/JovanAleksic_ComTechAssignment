using Unity.Entities;
using Unity.Mathematics;

public struct PlayerMovement : IComponentData
{
    public float MoveSpeed;
    public float3 MoveDirection;
}
