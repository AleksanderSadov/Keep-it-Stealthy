using KeepItStealthy.Gameplay.Helpers;
using KeepItStealthy.Gameplay.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KeepItStealthy.Gameplay.Components
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private CharacterMovementSO characterMovementSO;

        [Header("Runtime")]
        [SerializeField]
        private float currentSpeed;
        [SerializeField]
        private bool isRunning;

        private readonly MovementCalculations movementCalculations = new();
        private Camera mainCamera;
        private MovementInputs input;
        private CharacterController controller;
        private Animator animator;
        private readonly int isRunningHash = Animator.StringToHash("IsRunning");

        private void Awake()
        {
            input = GetComponent<MovementInputs>();
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

        private void Update()
        {
            Move();
            MoveAnimations();
        }

        private void Move()
        {
            isRunning = input.Move != Vector2.zero;
            currentSpeed = movementCalculations.CalculateSmoothSpeed(input.Move, controller.velocity, characterMovementSO.MaxMoveSpeed, characterMovementSO.SpeedAcceleration, Time.deltaTime);
            Quaternion targetRotation = movementCalculations.CalculateTargetRotation(input.Move, transform.rotation, mainCamera);
            transform.rotation = movementCalculations.CalculateSmoothRotation(transform.rotation, targetRotation, characterMovementSO.RotationSmoothTime);
            controller.Move(movementCalculations.CalculateMotion(currentSpeed, targetRotation, Time.deltaTime));
        }

        private void MoveAnimations()
        {
            animator.SetBool(isRunningHash, isRunning);
        }
    }
}

