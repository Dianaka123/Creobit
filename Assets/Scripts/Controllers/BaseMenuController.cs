using Assets.Scripts.Managers;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Controllers
{
    public class BaseMenuController : IController
    {
        protected readonly CanvasManager _canvasManager;
        protected readonly GameManager _gameManager;

        protected bool isClicked;

        public BaseMenuController(CanvasManager canvasManager, GameManager gameManager)
        {
            _canvasManager = canvasManager;
            _gameManager = gameManager;
        }

        public virtual void Init()
        {
            isClicked = false;
        }

        public virtual UniTask Run()
        {
            return UniTask.WaitUntil(() => isClicked);
        }

        public virtual void Exit()
        {
        }
    }
}
