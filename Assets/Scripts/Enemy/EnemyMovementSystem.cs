using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial class EnemyMovementSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem commandBufferSystem;

    protected override void OnCreate()
    {
        commandBufferSystem = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        float deltaTime = SystemAPI.Time.DeltaTime;
        var commandBuffer = commandBufferSystem.CreateCommandBuffer().AsParallelWriter();

        int enemyCount = 0;

        Entities
            .WithAll<Enemy>()
            .ForEach((Entity entity, int entityInQueryIndex, ref LocalTransform transform, in Enemy enemy) =>
            {
                float3 movement = new float3(0f, -enemy.MovementSpeed * deltaTime, 0f);
                transform.Position += movement;

                enemyCount++;

                if (transform.Position.y < -5f)
                {
                    commandBuffer.DestroyEntity(entityInQueryIndex, entity);
                }
            }).Run(); // Changed from ScheduleParallel to Run for debugging

        commandBufferSystem.AddJobHandleForProducer(Dependency);
    }
}