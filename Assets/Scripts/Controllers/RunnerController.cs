using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using Assets.Scripts.Systems.Interfaces;
using Assets.Scripts.Views;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
{
    public class RunnerController : PreloadControllerBase, IFixedTickable
    {
        private readonly IAssetProvider _assetsProvider;
        private readonly IGameConfigProvider _gameConfigProvider;
        private readonly IMovementSystem _movementSystem;
        private readonly IRunnerTimeManager _timeManager;
        private readonly CanvasManager _canvasManager;
        private readonly CameraManager _cameraManager;

        private PlayerView _playerView;
        private RunnerView _runnerView;
        private RunnerUIView _runnerUIView;

        private bool _isExit;
        private bool isMovingAvailable;
        private static readonly MovementParameters[] movementParams =
        {
            new(MovementType.Straight, 1, 0),
            new(MovementType.Back, -1, 0),
            new(MovementType.Left, 0, -1),
            new(MovementType.Right, 0, 1)
        };

        private readonly struct MovementParameters
        {
            public readonly MovementType Type;
            public readonly int MovingDirection;
            public readonly int RotationDirection;

            public MovementParameters(MovementType type, int movingDirection, int rotationDirection)
            {
                Type = type;
                MovingDirection = movingDirection;
                RotationDirection = rotationDirection;
            }
        }

        public RunnerController(IAssetProvider assetProvider, IPopupManager popupManager, IGameConfigProvider gameConfigProvider, IMovementSystem movementSystem, IRunnerTimeManager timeManager, CanvasManager canvasManager, CameraManager cameraManager) :
            base(popupManager)
        {
            _assetsProvider = assetProvider;
            _gameConfigProvider = gameConfigProvider;
            _movementSystem = movementSystem;
            _timeManager = timeManager;
            _canvasManager = canvasManager;
            _cameraManager = cameraManager;

        }

        public void FixedTick()
        {
            if(!isMovingAvailable)
            {
                return;
            }

            UpdateCamera();

            var currentTime = FormatTime(_timeManager.Elapsed());
            _runnerUIView.SetCurrentTime(currentTime);

            var moveType = _movementSystem.GetCurrentMovement();
            float movingDirection = 0;
            float rotationDirection = 0; 
            var isSpeedUp = false;
            var isMoving = false;

            foreach( var movementParam in movementParams )
            {
                if (moveType.HasFlag(movementParam.Type))
                {
                    movingDirection += movementParam.MovingDirection;
                    rotationDirection += movementParam.RotationDirection;
                    isMoving = true;
                }
            }
            
            if(moveType.HasFlag(MovementType.Run))
            {
                isSpeedUp = true;
            }
            
            _playerView.PlayAnimation(ChooseAnimation(isMoving, isSpeedUp));

            var fixedDeltaTime = Time.fixedDeltaTime;
            _playerView.Rotate(rotationDirection * fixedDeltaTime);
            _playerView.Move(movingDirection * fixedDeltaTime, isSpeedUp);
        }

        public override async UniTask Exit()
        {
            isMovingAvailable = false;

            _runnerView.FinishView.FinishEnter -= OnPlayerFinish;
            await _assetsProvider.UnloadAsync(GamesType.Runner);
            _runnerView.Destroy();
            _runnerUIView.Destroy();
        }

        protected override async UniTask Run()
        {
            await UniTask.WaitUntil(() => _isExit);
        }

        protected override async UniTask Preload()
        {
            _isExit = false;

            await _assetsProvider.PreloadAsync(Enums.GamesType.Runner);
            await _gameConfigProvider.Download();

            var bundle = _assetsProvider.GetAssetBundle(Enums.GamesType.Runner);

            var enviroment = (GameObject)await bundle.LoadAssetAsync("Runner");
            var playerAssets = (GameObject)await bundle.LoadAssetAsync("Corgi.prefab");
            var runnerUI = (GameObject)await bundle.LoadAssetAsync("RunnerUI");

            var uiGo = GameObject.Instantiate(runnerUI, _canvasManager.Canvas.transform);
            _runnerUIView = uiGo.GetComponent<RunnerUIView>();

            var enviromentGO = GameObject.Instantiate(enviroment);
            _runnerView = enviromentGO.GetComponent<RunnerView>();

            GameObject playerGO = GameObject.Instantiate(playerAssets, _runnerView.SpawnPointTarget, false);
            _playerView = playerGO.GetComponent<PlayerView>();

            _runnerView.FinishView.FinishEnter += OnPlayerFinish;
            _runnerUIView.BackClicked += OnBack;

            _timeManager.Start();
            isMovingAvailable = true;
        }

        private async void OnPlayerFinish()
        {
            _runnerView.FinishView.FinishEnter -= OnPlayerFinish;
            _timeManager.Stop();
            var result = _timeManager.Result();

            var resultMs = result.TotalMilliseconds;
            var bestResultMs = _gameConfigProvider.Configuration.BestRunTimeMilliseconds;

            if (bestResultMs == 0 || bestResultMs > resultMs)
            {
                _gameConfigProvider.Configuration.BestRunTimeMilliseconds = resultMs;
                _gameConfigProvider.Upload().Forget();
            }

            if (bestResultMs == 0)
            {
                bestResultMs = resultMs;
            }

            _runnerUIView.ShowResult(FormatTime(result), FormatTime(TimeSpan.FromMilliseconds(bestResultMs)));
        }

        private PlayerAnimations ChooseAnimation(bool isMoving, bool isSpeedUp)
        {
            if(!isMoving)
            {
                return PlayerAnimations.Idle;
            }

            return isSpeedUp ? PlayerAnimations.Run : PlayerAnimations.Walking;
        }

        private string FormatTime(TimeSpan time)
        {
            return time.ToString("mm':'ss");
        }

        private void UpdateCamera()
        {
            var setting = _runnerView.CameraSettings;
            var playerPosition = _playerView.gameObject.transform.position;
            var playerLookDirection = _playerView.gameObject.transform.forward;

            var cameraPosition = playerPosition 
                - setting.Distance * playerLookDirection
                + Mathf.Tan(setting.Angle * Mathf.PI / 180) * setting.Distance * new Vector3(0, 1, 0);
            var cameraLookDirection = Vector3.Normalize(playerPosition - cameraPosition);
            
            var cameraTransform = _cameraManager.MainCamera.transform;
            cameraTransform.position = cameraPosition;
            cameraTransform.forward = cameraLookDirection;
        }

        private void OnBack()
        {
            _isExit = true;
        }
    }
}
