using KeepItStealthy.Core;
using KeepItStealthy.Gameplay.Events;
using System.Collections;
using UnityEngine;

namespace KeepItStealthy.Gameplay.Components
{
    public class GameManager : MonoBehaviour
    {
        private void OnEnable()
        {
            EventsManager.AddListener<PlayerExitEvent>(OnPlayerExit);
        }

        private void Start()
        {
            StartCoroutine(StartGame());
        }

        private void OnDisable()
        {
            EventsManager.RemoveListener<PlayerExitEvent>(OnPlayerExit);
        }

        private IEnumerator StartGame()
        {
            yield return null;
            EventsManager.Broadcast(new GameStartedEvent());
        }

        private void OnPlayerExit(PlayerExitEvent evt)
        {
            Debug.Log("Player enter exit point");
        }
    }
}
