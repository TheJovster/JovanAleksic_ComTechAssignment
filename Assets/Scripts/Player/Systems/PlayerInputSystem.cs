using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Burst;
using Unity.Transforms;

public partial class PlayerInputSystem : SystemBase
{
    public PlayerInput input;
    public bool CanShoot;

    protected override void OnCreate() 
    {
        input = new PlayerInput();
    }

    protected override void OnStartRunning()
    {
        input.Enable();
    }

    protected override void OnUpdate()
    {
        bool IsShooting = input.Player.Shoot.IsPressed();
        float2 MoveInput = input.Player.Move.ReadValue<Vector2>();
        double ElapsedTime = SystemAPI.Time.ElapsedTime;
    }
}
[BurstCompile]
public partial struct UpdatePlayerInputJob: IJobEntity
{
    public bool b_IsShooting;
    public float3 f3_MoveInput;

    [BurstCompile]
    public void Execute(ref PlayerMovement movement, ref PlayerShooter shooter, PlayerTag tag, LocalTransform localTransform) 
    {
        movement.MoveDirection = new float3(f3_MoveInput.x, f3_MoveInput.y, 0f);
        shooter.IsShooting = b_IsShooting;
        shooter.SpawnPosition = localTransform.Position;
        shooter.TimeSinceLastShot = 0f;
        shooter.RateOfFire = .3f;
    }
}