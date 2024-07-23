using UnityEngine.Networking;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.Systems.Interfaces
{
    public class AssetsBundleLoader : IAssetsLoader
    {
        public async UniTask<AssetBundle> LoadAsync(Uri uri)
        {
            var request = UnityWebRequestAssetBundle.GetAssetBundle(uri);
            await request.SendWebRequest();
            return DownloadHandlerAssetBundle.GetContent(request);
        }
    }
}
