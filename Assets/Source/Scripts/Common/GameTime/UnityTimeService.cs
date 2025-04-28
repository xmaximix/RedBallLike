using UnityEngine;

namespace RedBallLike.Common.GameTime
{
    public sealed class UnityTimeService : ITimeService
    {
        private const float PausedScale = 0f;
        private const float DefaultScale = 1f;

        public float Scale => Time.timeScale;

        public void Pause()
        {
            SetTimeScale(PausedScale);
        }

        public void Resume()
        {
            SetTimeScale(DefaultScale);
        }

        private void SetTimeScale(float scale)
        {
            Time.timeScale = scale;
        }
    }
}