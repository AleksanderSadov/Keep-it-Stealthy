using KeepItStealthy.Core;
using KeepItStealthy.Gameplay.Components;

namespace KeepItStealthy.Gameplay.Events
{
    public class TeamMemberCreatedEvent : ApplicationEvent
    {
        public TeamMember teamMember;

        public TeamMemberCreatedEvent(TeamMember teamMember)
        {
            this.teamMember = teamMember;
        }
    }
}

