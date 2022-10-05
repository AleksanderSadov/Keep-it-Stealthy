using KeepItStealthy.Core;
using KeepItStealthy.Gameplay.Events;
using KeepItStealthy.Gameplay.Managers;
using System.Collections;
using UnityEngine;

namespace KeepItStealthy.Gameplay.Components
{
    public class GameManager : MonoBehaviour
    {
        private LevelGenerator levelGenerator;

        private void Awake()
        {
            levelGenerator = GetComponent<LevelGenerator>();
        }

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

            levelGenerator.GenerateLevel();
        }

        private void OnPlayerExit(PlayerExitEvent evt)
        {
            Debug.Log("Player enter exit point");
        }
    }
}
