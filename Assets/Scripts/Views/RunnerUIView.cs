using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Views
{
    public class RunnerUIView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _bestTimeText;

        [SerializeField]
        private TMP_Text _resultTimeText;

        [SerializeField] 
        private TMP_Text _currentTime;

        [SerializeField]
        private Button _backButton;

        public event Action BackClicked;

        private void Awake()
        {
            Reset();
            _backButton.onClick.AddListener(() => BackClicked?.Invoke());
        }

        private void OnDestroy()
        {
            _backButton.onClick.RemoveAllListeners();
        }

        public void ShowResult(string yourTime, string bestTime)
        {
            _currentTime.gameObject.SetActive(false);

            SetBestTime(bestTime);
            SetYourTime(yourTime);

            _bestTimeText.gameObject.SetActive(true);
            _resultTimeText.gameObject.SetActive(true);
        }

        //TODO optimise
        public void SetBestTime(string time)
        {
            _bestTimeText.text = $"Best time: {time}";
        }

        public void SetYourTime(string time)
        {
            _resultTimeText.text = $"Your time: {time}";
        }

        public void SetCurrentTime(string time)
        {
            _currentTime.text = time;
        }

        private void Reset()
        {
            _currentTime.text = string.Empty;

            _bestTimeText.gameObject.SetActive(false);
            _resultTimeText.gameObject.SetActive(false);
            _currentTime.gameObject.SetActive(true);
        }
    }
}