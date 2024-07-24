using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Views
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField]
        private ChooseGameView _chooseGameView;

        [SerializeField]
        private DataManipulatorView _clikerDataManipulatorView;

        [SerializeField]
        private DataManipulatorView _runnerDataManipulatorView;

        public ChooseGameView ChooseGameView => _chooseGameView;
        public DataManipulatorView ClickerDataManipulator => _clikerDataManipulatorView;
        public DataManipulatorView RunnerDataManipulator => _runnerDataManipulatorView;

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
