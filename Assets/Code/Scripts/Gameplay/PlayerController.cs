using UnityEngine;
using UnityEngine.InputSystem;

namespace KeepItStealthy.Gameplay
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour
    {
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 2.0f;
        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;
        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        private float speed;
        private float targetRotation;
        private float rotationVelocity;
        private GameObject mainCamera;

        private MovementInputs input;
        private CharacterController controller;
        private Animator animator;

        private void Awake()
        {
            input = GetComponent<MovementInputs>();
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            if (mainCamera == null)
            {
                mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            float targetSpeed = MoveSpeed;

            if (input.move == Vector2.zero) targetSpeed = 0.0f;

            float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0f, controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = 1f;

            if (currentHorizontalSpeed < targetSpeed - speedOffset
                || currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);
            }
            else
            {
                speed = targetSpeed;
            }

            Vector3 inputDirection = new Vector3(input.move.x, 0f, input.move.y).normalized;

            if (input.move != Vector2.zero)
            {
                targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, RotationSmoothTime);
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0f) * Vector3.forward;
            controller.Move(targetDirection.normalized * (speed * Time.deltaTime));

            if (input.move != Vector2.zero)
            {
                animator.SetInteger("State", 1);
            }
            else
            {
                animator.SetInteger("State", 0);
            }
            //animator.SetInteger("State", 1);
        }
    }
}

