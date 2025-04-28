using UnityEngine;

namespace RedBallLike.Common.Cam
{
    public interface ICameraService
    {
        void SetFollowTarget(Transform target);
    }
}