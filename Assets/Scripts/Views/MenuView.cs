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
        private DataManipulatorView _manipulatorView;

        public ChooseGameView ChooseGameView => _chooseGameView;
        public DataManipulatorView ManipulatorView => _manipulatorView;
    }
}
