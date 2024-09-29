using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

public partial class PlayerMovementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = SystemAPI.Time.DeltaTime;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Entities.ForEach((ref LocalTransform transform, in Player player) =>
        {
            float3 movement = new float3(horizontal, vertical, 0) * player.MoveSpeed * deltaTime;
            transform.Position += movement;
        }).ScheduleParallel();
    }
}
