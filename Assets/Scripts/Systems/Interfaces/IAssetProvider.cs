using Assets.Scripts.Enums;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Systems.Interfaces
{
    public interface IAssetProvider
    {
        UniTask PreloadAsync(GamesType gamesType);
        UniTask UnloadAsync(GamesType gamesType);
        AssetBundle GetAssetBundle(GamesType gamesType);
    }
}
