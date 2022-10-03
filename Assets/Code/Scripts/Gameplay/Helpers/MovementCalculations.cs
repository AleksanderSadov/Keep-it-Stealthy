using UnityEngine;

namespace KeepItStealthy.Gameplay.Helpers
{
    public class MovementCalculations
    {
        private float rotationVelocity;

        public Vector3 CalculateMotion(float speed, Quaternion targetRotation, float deltaTime)
        {
            Vector3 targetDirection = targetRotation * Vector3.forward;
            Vector3 motion = targetDirection.normalized * (speed * deltaTime);

            return motion;
        }

        public float CalculateSmoothSpeed(Vector2 movementInputs, Vector3 currentVelocity, float maxMoveSpeed, float speedAcceleration, float deltaTime)
        {
            float requiredSpeed = movementInputs == Vector2.zero ? 0.0f : maxMoveSpeed;
            float smoothSpeed = requiredSpeed;
            float currentHorizontalSpeed = new Vector3(currentVelocity.x, 0f, currentVelocity.z).magnitude;
            float speedOffset = 0.1f;

            if (currentHorizontalSpeed < requiredSpeed - speedOffset
                || currentHorizontalSpeed > requiredSpeed + speedOffset)
            {
                smoothSpeed = Mathf.Lerp(currentHorizontalSpeed, requiredSpeed, deltaTime * speedAcceleration); ;
            }

            return smoothSpeed;
        }

        public Quaternion CalculateTargetRotation(Vector2 movementInputs, Quaternion currentRotation, Camera camera)
        {
            if (movementInputs == Vector2.zero) return currentRotation;

            Vector3 inputDirection = new Vector3(movementInputs.x, 0f, movementInputs.y).normalized;
            float targetRotationAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + camera.transform.eulerAngles.y;
            Quaternion targetRotation = Quaternion.Euler(0.0f, targetRotationAngle, 0.0f);
            return targetRotation;
        }

        public Quaternion CalculateSmoothRotation(Quaternion currentRotation, Quaternion targetRotation, float rotationSmoothTime)
        {
            float smoothRotationAngle = Mathf.SmoothDampAngle(currentRotation.eulerAngles.y, targetRotation.eulerAngles.y, ref rotationVelocity, rotationSmoothTime);
            Quaternion smoothRotation = Quaternion.Euler(0.0f, smoothRotationAngle, 0.0f);
            return smoothRotation;
        }
    }
}

