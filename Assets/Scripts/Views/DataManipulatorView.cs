using Assets.Scripts.Enums;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Views
{
    public class DataManipulatorView : MonoBehaviour, IDisposable
    {
        [SerializeField]
        private Button _load;

        [SerializeField]
        private Button _unload;

        [SerializeField]
        private GamesType _gameType;

        public event Action<GamesType> LoadClick;
        public event Action<GamesType> UnloadClick;

        private void Awake()
        {
            _load.onClick.AddListener(() => LoadClick?.Invoke(_gameType));
            _unload.onClick.AddListener(() => UnloadClick?.Invoke(_gameType));
        }

        public void Dispose()
        {
            _load.onClick.RemoveAllListeners();
            _unload.onClick.RemoveAllListeners();
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}