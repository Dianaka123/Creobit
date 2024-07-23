using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using Assets.Scripts.Systems.Interfaces;
using Assets.Scripts.Views;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
{
    public class ClickerController : IController, ITickable
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IFactory<ClickerView> _factory;
        private readonly CanvasManager _canvasManager;

        public ClickerController(IAssetProvider assetProvider, IFactory<ClickerView> clickerView, CanvasManager canvasManager)
        {
            _assetProvider = assetProvider;
            _factory = clickerView;
            _canvasManager = canvasManager;
        }

        public void Exit()
        {
        }

        public async void Init()
        {
            var clickerView = _factory.Create();
            clickerView.transform.SetParent(_canvasManager.Canvas.transform);
            clickerView.transform.localPosition = Vector3.zero;

            var assetBundle = _assetProvider.GetAssetBundle(GamesType.Clicker);
            Texture2D texture = (Texture2D) await assetBundle.LoadAssetAsync("Click");

            clickerView.SetImage(texture);
        }

        public UniTask Run()
        {
            return UniTask.CompletedTask;
        }

        public void Tick()
        {
            
        }
    }
}