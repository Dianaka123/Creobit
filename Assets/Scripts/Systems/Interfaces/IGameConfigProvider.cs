using Assets.Scripts.ConfigData;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Systems.Interfaces
{
    public interface IGameConfigProvider
    {
        GameConfiguration Configuration { get; }

        UniTask Download();

        void Unload();
    }
}
