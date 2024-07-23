using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Assets.Scripts.Systems.Interfaces
{
    public interface IAssetsLoader
    {
        UniTask<AssetBundle> LoadAsync(Uri uri);
    }
}
