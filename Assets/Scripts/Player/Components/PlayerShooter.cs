using Unity.Entities;
using Unity.Mathematics;

public struct PlayerShooter : IComponentData
{
    public Entity ProjectilePrefab;
    public bool IsShooting;
    public float3 SpawnPosition;
    public float RateOfFire;
    public double TimeSinceLastShot; 
}
