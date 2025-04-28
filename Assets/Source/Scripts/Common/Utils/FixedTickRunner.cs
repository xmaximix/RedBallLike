using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace RedBallLike.Common.Utils
{
    public sealed class FixedTickRunner : MonoBehaviour
    {
        private static FixedTickRunner instance;
        private readonly List<IFixedTickable> items = new();

        public static FixedTickRunner Instance
        {
            get
            {
                if (instance == null)
                    instance = CreateRunner();
                return instance;
            }
        }

        private static FixedTickRunner CreateRunner()
        {
            var runnerObject = new GameObject(nameof(FixedTickRunner));
            var runner = runnerObject.AddComponent<FixedTickRunner>();
            DontDestroyOnLoad(runnerObject);
            return runner;
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void FixedUpdate()
        {
            ProcessTicks();
        }

        private void ProcessTicks()
        {
            for (var i = 0; i < items.Count; i++)
            {
                items[i].FixedTick();
            }
        }

        public void Add(IFixedTickable tickable)
        {
            if (!items.Contains(tickable))
                items.Add(tickable);
        }

        public void Remove(IFixedTickable tickable)
        {
            items.Remove(tickable);
        }
    }
}