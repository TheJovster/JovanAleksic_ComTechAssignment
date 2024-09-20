using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;
using UnityEditor.Build;
using Unity.Transforms;
using Unity.Burst;
using Unity.VisualScripting;

[BurstCompile]
public partial struct PlayerMovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float DeltaTime = SystemAPI.Time.DeltaTime;

        new PlayerMovementJob
        {
            DeltaTime = DeltaTime
        }.Schedule();
    }

    [BurstCompile]
    public partial struct PlayerMovementJob : IJobEntity 
    {
        public float DeltaTime;

        public void Execute(PlayerMovement movement, ref LocalTransform localTransform) 
        {
            localTransform.Position += movement.MoveDirection * movement.MoveSpeed * DeltaTime;
        }
    }
}

