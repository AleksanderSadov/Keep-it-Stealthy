using KeepItStealthy.Core;
using KeepItStealthy.Gameplay.Constants;
using KeepItStealthy.Gameplay.Events;
using UnityEngine;

namespace KeepItStealthy.Gameplay.Components
{
    public class TeamMember : MonoBehaviour
    {
        [SerializeField]
        private TeamAffiliation teamAffiliation;

        public TeamAffiliation TeamAffiliation { get => teamAffiliation; private set => teamAffiliation = value; }

        private void Start()
        {
            EventsManager.Broadcast(new TeamMemberCreatedEvent(this));
        }

        private void OnDestroy()
        {
            EventsManager.Broadcast(new TeamMemberDestroyedEvent(this));
        }
    }
}

