using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using Assets.Scripts.Systems.Interfaces;
using Assets.Scripts.Views;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
{
    public class ClickerController : IController
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IFactory<ClickerView> _factory;
        private readonly CanvasManager _canvasManager;

        private ClickerView _clickerView;
        private int _clickerCount;
        private bool _isGoBackClicked;

        public ClickerController(IAssetProvider assetProvider, IFactory<ClickerView> clickerView, CanvasManager canvasManager)
        {
            _assetProvider = assetProvider;
            _factory = clickerView;
            _canvasManager = canvasManager;
        }

        public void Exit()
        {
            _clickerView.Clicked -= OnClicked;
            _clickerView.Destroy();
        }

        public void Init()
        {
            _clickerView = _factory.Create();
            _clickerView.transform.SetParent(_canvasManager.Canvas.transform);
            _clickerView.transform.localPosition = Vector3.zero;
            
            _clickerCount = 0;
            _clickerView.SetText(_clickerCount.ToString());
        }

        //TODO: Need to fix - Image loaded not in time
        public async UniTask Run()
        {
            var assetBundle = _assetProvider.GetAssetBundle(GamesType.Clicker);
            if (assetBundle == null)
            {
                await _assetProvider.PreloadAsync(GamesType.Clicker);
            }

            assetBundle = _assetProvider.GetAssetBundle(GamesType.Clicker);

            Texture2D texture = (Texture2D)await assetBundle.LoadAssetAsync("Click");
            _clickerView.SetImage(texture);

            _clickerView.Clicked += OnClicked;
            _clickerView.GoBack += OnGoBack;

            await UniTask.WaitUntil(() => _isGoBackClicked);
        }

        private void OnGoBack()
        {
            _isGoBackClicked = true;
        }

        private void OnClicked()
        {
            //TODO: write/read it by used json file
            _clickerCount++;
            _clickerView.SetText(_clickerCount.ToString());
        }
    }
}