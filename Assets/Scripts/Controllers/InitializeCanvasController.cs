using Assets.Scripts.Managers;
using Assets.Scripts.Views;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Controllers
{
    public class InitializeCanvasController: IController
    {
        private readonly IFactory<MenuView> _menuViewFactory;
        private readonly CanvasManager _canvasManager;
        private GameObject _canvas;

        public InitializeCanvasController(IFactory<MenuView> menuViewFactory, CanvasManager canvasManager)
        {
            _menuViewFactory = menuViewFactory;
            _canvasManager = canvasManager;
        }

        public void Init()
        {
            _canvas = CreateCanvas();
            var menu = _menuViewFactory.Create();
            menu.transform.parent = _canvas.transform;
            menu.transform.localPosition = Vector3.zero;

            _canvasManager.Setup(_canvas, menu);
        }

        public void Exit() { }

        //TODO: move to factory
        private GameObject CreateCanvas()
        {
            var canvasGO = new GameObject();
            canvasGO.name = "Canvas";

            var canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();

            return canvasGO;
        }

        public UniTask Run()
        {
            return UniTask.CompletedTask;
        }
    }
}
