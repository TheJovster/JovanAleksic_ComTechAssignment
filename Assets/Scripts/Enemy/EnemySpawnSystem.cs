using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial class EnemySpawnSystem : SystemBase
{
    private BeginInitializationEntityCommandBufferSystem commandBufferSystem;

    protected override void OnCreate()
    {
        commandBufferSystem = World.GetOrCreateSystemManaged<BeginInitializationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var commandBuffer = commandBufferSystem.CreateCommandBuffer();

        // Spawn an enemy every 2 seconds
        if (UnityEngine.Time.frameCount % 240 == 0)
        {
            var enemyPrefab = SystemAPI.GetSingleton<
                EnemyPrefabComponent>().EnemyEntity;
            var spawnPosition = new float3(UnityEngine.Random.Range(-8f, 8f), 6f, 0f);

            var enemy = commandBuffer.Instantiate(enemyPrefab);
            commandBuffer.SetComponent(enemy, new LocalTransform
            {
                Position = spawnPosition,
                Rotation = quaternion.identity,
                Scale = 1f
            });
        }

        commandBufferSystem.AddJobHandleForProducer(Dependency);
    }
}
