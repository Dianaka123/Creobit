using Cysharp.Threading.Tasks;
using Zenject;

namespace Assets.Scripts.Controllers
{
    public interface IController
    {
        UniTask Init();

        UniTask Run();

        UniTask Exit();
    }
}
