using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
public partial class PlayerProjectileSystem : SystemBase
{
    private BeginInitializationEntityCommandBufferSystem commandBufferSystem;

    protected override void OnCreate()
    {
        commandBufferSystem = World.GetOrCreateSystemManaged<BeginInitializationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        float deltaTime = SystemAPI.Time.DeltaTime;
        var commandBuffer = commandBufferSystem.CreateCommandBuffer().AsParallelWriter();
        float3 screenBounds = new float3(Screen.width, Screen.height, 0) / 2f;

        Entities.ForEach((Entity entity, int entityInQueryIndex, ref LocalTransform transform, ref PlayerProjectile projectile) =>
        {
            transform.Position += new float3(0, projectile.Speed * deltaTime, 0);
            projectile.Lifetime -= deltaTime;

            if (projectile.Lifetime <= 0 || math.abs(transform.Position.x) > screenBounds.x || math.abs(transform.Position.y) > screenBounds.y)
            {
                commandBuffer.DestroyEntity(entityInQueryIndex, entity);
            }
        }).ScheduleParallel();

        commandBufferSystem.AddJobHandleForProducer(Dependency);
    }
}
