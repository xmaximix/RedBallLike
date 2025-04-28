using UnityEngine;

namespace RedBallLike.Common.Cam
{
    [RequireComponent(typeof(Camera))]
    public sealed class CameraController : MonoBehaviour
    {
        private Transform target;

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        private void LateUpdate()
        {
            FollowTarget();
        }

        private void FollowTarget()
        {
            if (target == null)
                return;

            var targetPos = target.position;
            transform.position = new Vector3(
                targetPos.x,
                targetPos.y,
                transform.position.z
            );
        }
    }
}