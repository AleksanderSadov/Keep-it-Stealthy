using KeepItStealthy.Gameplay.Helpers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KeepItStealthy.Gameplay.Components
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour
    {
        [Tooltip("Move speed of the character in m/s")]
        public float maxMoveSpeed = 2.0f;
        [Tooltip("Acceleration and deceleration")]
        public float speedAcceleration = 10.0f;
        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float rotationSmoothTime = 0.12f;

        [SerializeField]
        private float currentSpeed;

        private readonly MovementCalculations movementCalculations = new();
        private Camera mainCamera;
        private MovementInputs input;
        private CharacterController controller;
        private Animator animator;

        private void Awake()
        {
            input = GetComponent<MovementInputs>();
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            if (mainCamera == null) mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

        private void Update()
        {
            Move();
            MoveAnimations();
        }

        private void Move()
        {
            currentSpeed = movementCalculations.CalculateSmoothSpeed(input.move, controller.velocity, maxMoveSpeed, speedAcceleration, Time.deltaTime);
            Quaternion targetRotation = movementCalculations.CalculateTargetRotation(input.move, transform.rotation, mainCamera);
            transform.rotation = movementCalculations.CalculateSmoothRotation(transform.rotation, targetRotation, rotationSmoothTime);
            controller.Move(movementCalculations.CalculateMotion(currentSpeed, targetRotation, Time.deltaTime));
        }

        private void MoveAnimations()
        {
            if (input.move != Vector2.zero)
            {
                animator.SetInteger("State", 1);
            }
            else
            {
                animator.SetInteger("State", 0);
            }
        }
    }
}

