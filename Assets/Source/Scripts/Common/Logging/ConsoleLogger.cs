using UnityEngine;

namespace RedBallLike.Common.Logging
{
    public sealed class ConsoleLogger : ILogger
    {
        public void Log(string msg)
        {
            Debug.Log($"[LOG] {msg}");
        }

        public void Warn(string msg)
        {
            Debug.LogWarning($"[WARN] {msg}");
        }

        public void Error(string msg)
        {
            Debug.LogError($"[ERROR] {msg}");
        }
    }
}