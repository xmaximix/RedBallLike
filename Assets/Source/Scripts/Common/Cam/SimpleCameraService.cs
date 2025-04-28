using System;
using UnityEngine;

namespace RedBallLike.Common.Cam
{
    public sealed class SimpleCameraService : ICameraService
    {
        private readonly CameraController controller;

        public SimpleCameraService(CameraController controller) =>
            this.controller = controller ?? throw new ArgumentNullException(nameof(controller));

        public void SetFollowTarget(Transform target)
        {
            controller.SetTarget(target);
        }
    }
}