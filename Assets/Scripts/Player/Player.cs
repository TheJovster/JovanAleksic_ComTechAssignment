using Unity.Entities;

public struct Player : IComponentData
{
    public float MoveSpeed;
    public float ShootCooldown;
    public float CurrentCooldown;
}
