using KeepItStealthy.Core;
using KeepItStealthy.Gameplay.Components;
using KeepItStealthy.Gameplay.Events;
using KeepItStealthy.Gameplay.ScriptableObjects;
using UnityEngine;

namespace KeepItStealthy.Gameplay.Managers
{
    public class TeamsManager : MonoBehaviour
    {
        [SerializeField]
        private TeamsListSO teamsListSO;

        private void OnEnable()
        {
            EventsManager.AddListener<TeamMemberCreatedEvent>(OnTeamMemberCreated);
            EventsManager.AddListener<TeamMemberDestroyedEvent>(OnTeamMemberDestroyed);
        }

        private void OnDisable()
        {
            EventsManager.RemoveListener<TeamMemberCreatedEvent>(OnTeamMemberCreated);
            EventsManager.RemoveListener<TeamMemberDestroyedEvent>(OnTeamMemberDestroyed);
        }

        private void OnTeamMemberCreated(TeamMemberCreatedEvent evt)
        {
            switch (evt.teamMember.TeamAffiliation)
            {
                case Constants.TeamAffiliation.Player:
                    Debug.Log("Test: " + evt.teamMember.GetComponent<PlayerController>());
                    teamsListSO.Players.Add(evt.teamMember.GetComponent<PlayerController>());
                    break;
                case Constants.TeamAffiliation.Enemy:
                    teamsListSO.Enemies.Add(evt.teamMember.GetComponent<EnemyController>());
                    break;
            }
        }

        private void OnTeamMemberDestroyed(TeamMemberDestroyedEvent evt)
        {
            switch (evt.teamMember.TeamAffiliation)
            {
                case Constants.TeamAffiliation.Player:
                    teamsListSO.Players.Remove(evt.teamMember.GetComponent<PlayerController>());
                    break;
                case Constants.TeamAffiliation.Enemy:
                    teamsListSO.Enemies.Remove(evt.teamMember.GetComponent<EnemyController>());
                    break;
            }
        }
    }
}
