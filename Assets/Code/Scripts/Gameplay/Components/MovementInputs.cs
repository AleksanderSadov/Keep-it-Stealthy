using UnityEngine;
using UnityEngine.InputSystem;

namespace KeepItStealthy.Gameplay.Components
{
	public class MovementInputs : MonoBehaviour
	{
        [Header("Runtime")]
        [SerializeField]
        private Vector2 move;

        public Vector2 Move { get => move; private set => move = value; }

        public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void MoveInput(Vector2 newMoveDirection)
		{
			Move = newMoveDirection;
		}
	}
}