using KeepItStealthy.Core;
using KeepItStealthy.Gameplay.Events;
using UnityEngine;

namespace KeepItStealthy.Gameplay.Components
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Rigidbody))]
	public class ExitPoint : MonoBehaviour
	{
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController player))
            {
                EventsManager.Broadcast(new PlayerExitEvent());
            }
        }
    }
}