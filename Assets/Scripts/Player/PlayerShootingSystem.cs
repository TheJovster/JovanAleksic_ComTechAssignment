using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class PlayerShootingSystem : SystemBase
{
    private BeginInitializationEntityCommandBufferSystem commandBufferSystem;
    private EntityQuery playerQuery;

    protected override void OnCreate()
    {
        commandBufferSystem = World.GetOrCreateSystemManaged<BeginInitializationEntityCommandBufferSystem>();
        playerQuery = GetEntityQuery(typeof(Player), typeof(LocalTransform), typeof(PlayerPrefab), typeof(PlayerShootInput));
    }

    protected override void OnUpdate()
    {
        bool isShooting = Input.GetButton("Fire");
        if (isShooting) 
        {
            Debug.Log("Pew");
        }
        float deltaTime = SystemAPI.Time.DeltaTime;
        var commandBuffer = commandBufferSystem.CreateCommandBuffer();

        Entities
            .WithStoreEntityQueryInField(ref playerQuery)
            .ForEach((Entity entity, ref Player player, ref LocalTransform transform, ref PlayerPrefab playerPrefab, ref PlayerShootInput inputState) =>
            {
                inputState.IsShooting = isShooting;

                player.CurrentCooldown -= deltaTime;

                if (isShooting && player.CurrentCooldown <= 0)
                {
                    player.CurrentCooldown = player.ShootCooldown;

                    var projectile = commandBuffer.Instantiate(playerPrefab.PrefabEntity);
                    commandBuffer.SetComponent(projectile, new LocalTransform
                    {
                        Position = transform.Position,
                        Rotation = quaternion.identity,
                        Scale = 1f
                    });
                    commandBuffer.AddComponent(projectile, new PlayerProjectile
                    {
                        Speed = 10f,
                        Lifetime = 5f,
                        
                    });
                }
            }).Run();

        commandBufferSystem.AddJobHandleForProducer(Dependency);
    }
}