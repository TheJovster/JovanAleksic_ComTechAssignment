using Unity.Entities;
using UnityEngine;

public class EnemySpawnAuthoring : MonoBehaviour
{
    public GameObject enemyPrefab;

    public class Baker : Baker<EnemySpawnAuthoring>
    {
        public override void Bake(EnemySpawnAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new EnemyPrefabComponent
            {
                EnemyEntity = GetEntity(authoring.enemyPrefab, TransformUsageFlags.Dynamic)
            });
        }
    }
}
