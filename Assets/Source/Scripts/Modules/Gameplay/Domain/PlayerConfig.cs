using UnityEngine;

namespace RedBallLike.Modules.Gameplay.Domain
{
    [CreateAssetMenu(menuName = "Configs/PlayerConfig")]
    public sealed class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float MaxSpeed { get; private set; } = 6f;
        [field: SerializeField] public float Acceleration { get; private set; } = 40f;
        [field: SerializeField] public float Deceleration { get; private set; } = 50f;
        [field: SerializeField] public float JumpForce { get; private set; } = 10f;
        [field: SerializeField] public float GroundCheckRadius { get; private set; } = 0.2f;
        [field: SerializeField] public LayerMask GroundLayers { get; private set; }
    }
}