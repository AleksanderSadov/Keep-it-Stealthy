using UnityEngine;

namespace KeepItStealthy.Gameplay.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CharacterMovement", menuName = "ScriptableObjects/CharacterMovement")]
    public class CharacterMovementSO : ScriptableObject
    {
        [SerializeField]
        [Tooltip("Move speed of the character in m/s")]
        private float maxMoveSpeed = 2.0f;
        [SerializeField]
        [Tooltip("Acceleration and deceleration")]
        private float speedAcceleration = 10.0f;
        [SerializeField]
        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        private float rotationSmoothTime = 0.12f;

        public float MaxMoveSpeed { get => maxMoveSpeed; private set => maxMoveSpeed = value; }
        public float SpeedAcceleration { get => speedAcceleration; private set => speedAcceleration = value; }
        public float RotationSmoothTime { get => rotationSmoothTime; private set => rotationSmoothTime = value; }
    }
}

