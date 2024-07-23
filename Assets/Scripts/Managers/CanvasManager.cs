using Assets.Scripts.Views;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class CanvasManager
    {
        private GameObject _canvas;
        private MenuView _menuView;
        
        public GameObject Canvas => _canvas;
        public MenuView Menu => _menuView;

        public void Setup(GameObject canvas, MenuView view)
        {
            _canvas = canvas;
            _menuView = view;
        }
    }
}