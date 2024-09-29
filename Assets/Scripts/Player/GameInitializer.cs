using UnityEngine;
using Unity.Entities;

public class GameInitializer : MonoBehaviour 
{
    [SerializeField] private GameObject go_PlayerPrefab;
    [SerializeField] private Vector2 v2_SpawnPosition;

    private class GameBaker : Baker<GameInitializer>
    {
        public override void Bake(GameInitializer authoring)
        {
            Entity PlayerEntity = GetEntity(TransformUsageFlags.None);
            AddComponent(PlayerEntity, new PlayerSpawnerComponent 
            {
                PlayerPrefab = GetEntity(authoring.go_PlayerPrefab, TransformUsageFlags.Dynamic),
                PlayerSpawnPosition = authoring.v2_SpawnPosition
            });
        }
    }
}
