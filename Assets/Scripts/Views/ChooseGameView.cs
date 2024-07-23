using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Views
{
    public class ChooseGameView : MonoBehaviour, IDisposable
    {
        [SerializeField]
        private Button _clicker;

        [SerializeField]
        private Button _runner;

        public event Action ClickerClicked;
        public event Action RunnerClicked;

        private void Awake()
        {
            _clicker.onClick.AddListener(() => ClickerClicked?.Invoke());
            _runner.onClick.AddListener(() => RunnerClicked?.Invoke());
        }

        public void Dispose()
        {
            _clicker.onClick.RemoveAllListeners();
            _runner.onClick.RemoveAllListeners();
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}