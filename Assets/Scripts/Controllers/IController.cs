using Cysharp.Threading.Tasks;
using Zenject;

namespace Assets.Scripts.Controllers
{
    public interface IController
    {
        void Init();

        UniTask Run();

        void Exit();
    }
}
