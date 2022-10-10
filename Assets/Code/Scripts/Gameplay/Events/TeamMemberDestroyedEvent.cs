using KeepItStealthy.Core;
using KeepItStealthy.Gameplay.Components;

namespace KeepItStealthy.Gameplay.Events
{
    public class TeamMemberDestroyedEvent : ApplicationEvent
    {
        public TeamMember teamMember;

        public TeamMemberDestroyedEvent(TeamMember teamMember)
        {
            this.teamMember = teamMember;
        }
    }
}

