using System;
using UnityEngine;

namespace RedBallLike.Modules.Gameplay.Domain
{
    public sealed class PlayerMovement : IMovement
    {
        private const float SpeedThreshold = 0.01f;
        private const float GroundCheckOffset = 0.1f;

        private readonly Rigidbody2D rb;
        private readonly PlayerConfig cfg;
        private readonly Transform trans;

        private float targetAxis;
        private bool jumpRequested;

        public PlayerMovement(Rigidbody2D rb, PlayerConfig cfg)
        {
            this.rb = rb ?? throw new ArgumentNullException(nameof(rb));
            this.cfg = cfg ?? throw new ArgumentNullException(nameof(cfg));
            trans = rb.transform;
        }

        public void SetMoveAxis(float axis)
        {
            targetAxis = Mathf.Clamp(axis, -1f, 1f);
        }

        public void RequestJump()
        {
            jumpRequested = true;
        }

        public void Tick(float dt)
        {
            var desiredSpeed = GetDesiredSpeed();
            var accelRate = GetAccelerationRate(desiredSpeed);

            UpdateVelocity(desiredSpeed, accelRate, dt);
            ProcessJump();
            jumpRequested = false;
        }

        private float GetDesiredSpeed()
        {
            return targetAxis * cfg.MaxSpeed;
        }

        private float GetAccelerationRate(float desiredSpeed)
        {
            return Mathf.Abs(desiredSpeed) > SpeedThreshold
                ? cfg.Acceleration
                : cfg.Deceleration;
        }

        private void UpdateVelocity(float desiredSpeed, float accelRate, float dt)
        {
            var newVelX = Mathf.MoveTowards(
                rb.velocity.x,
                desiredSpeed,
                accelRate * dt
            );
            var velocity = rb.velocity;
            velocity.x = newVelX;
            rb.velocity = velocity;
        }

        private void ProcessJump()
        {
            if (jumpRequested && IsGrounded())
            {
                rb.AddForce(
                    Vector2.up * cfg.JumpForce,
                    ForceMode2D.Impulse
                );
            }
        }

        private bool IsGrounded()
        {
            return Physics2D.OverlapCircle(
                trans.position + Vector3.down * GroundCheckOffset,
                cfg.GroundCheckRadius,
                cfg.GroundLayers
            );
        }
    }
}