using Unity.Entities;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float shootCooldown = 0.5f;
    public GameObject projectilePrefab;

    public class Baker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Player
            {
                MoveSpeed = authoring.moveSpeed,
                ShootCooldown = authoring.shootCooldown,
                CurrentCooldown = 0f
            });
            AddComponent(entity, new PlayerPrefab
            {
                PrefabEntity = GetEntity(authoring.projectilePrefab, TransformUsageFlags.Dynamic)
            });
            AddComponent(entity, new PlayerShootInput { IsShooting = false });
        }
    }
}