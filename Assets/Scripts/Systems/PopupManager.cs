using Assets.Scripts.Managers;
using Assets.Scripts.Systems.Interfaces;
using Assets.Scripts.Views;
using Cysharp.Threading.Tasks;
using System;
using Zenject;

namespace Assets.Scripts.Systems
{
    public class PopupManager : IPopupManager
    {
        private readonly CanvasManager _canvasManager;
        private readonly IFactory<SomethingWrongPopupView> _factory;

        private bool _isClicked;
        private SomethingWrongPopupView _view;

        public PopupManager(CanvasManager canvasManager, IFactory<SomethingWrongPopupView> factory)
        {
            _canvasManager = canvasManager;
            _factory = factory;
        }

        public UniTask Hide()
        {
            _view?.Destroy();
            return UniTask.CompletedTask;
        }

        public async UniTask Show(string text, bool isOkButtonActive)
        {
            _view = _factory.Create();
            _view.gameObject.transform.SetParent(_canvasManager.Canvas.transform, false);
            _view.Init(text, isOkButtonActive);

            if(!isOkButtonActive)
            {
                return;
            }

            _isClicked = false;

            Action handler = () => isOkButtonActive = true;

            _view.OkClick += handler;
            await UniTask.WaitUntil(() => _isClicked);
            _view.OkClick -= handler;
        }
    }
}
