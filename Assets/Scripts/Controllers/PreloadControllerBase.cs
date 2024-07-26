using Assets.Scripts.Systems.Interfaces;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Controllers
{
    public abstract class PreloadControllerBase : IController
    {
        private readonly IPopupManager popupManager;

        public PreloadControllerBase(IPopupManager popupManager)
        {
            this.popupManager = popupManager;
        }

        public abstract UniTask Exit();

        public async UniTask Init()
        {
            popupManager.Show("Loading...", false).Forget();
            await Preload();
            await popupManager.Hide();
            await Run();
        }

        protected abstract UniTask Run();


        protected abstract UniTask Preload();
    }
}
