using UnityEngine;
using UnityEngine.AI;

namespace KeepItStealthy.Gameplay.Components
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : MonoBehaviour
    {
        private NavMeshAgent agent;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        public bool CanReachPoint(Vector3 targetPoint)
        {
            NavMeshPath navMeshPath = new();
            return agent.CalculatePath(targetPoint, navMeshPath)
                && navMeshPath.status == NavMeshPathStatus.PathComplete;
        }
    }
}

