using Assets.Scripts.Enums;
using Assets.Scripts.Systems.Interfaces;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class AssetProvider : IAssetProvider
    {
        private readonly IAssetsLoader _assetLoader;
        private readonly IUriResolver _assetsUriResolver;
        private readonly IServerConfigProvider _configProvider;

        private readonly Dictionary<GamesType, AssetBundle> _gameTypeToBundle = new ();
        
        public AssetProvider(IAssetsLoader assetLoader, IUriResolver assetsUriResolver, IServerConfigProvider configProvider)
        {
            _assetLoader = assetLoader;
            _assetsUriResolver = assetsUriResolver;
            _configProvider = configProvider;
        }

        public async UniTask PreloadAsync(GamesType gamesType)
        {
            var bundle = GetAssetBundle(gamesType);
            if (bundle != null)
            {
                return;
            }

            string url = gamesType == GamesType.Clicker
               ? _configProvider.Config.ClickerResourcesURL
               : _configProvider.Config.RunnerResourcesURL;

            Uri uri = await _assetsUriResolver.ResolveAssetsUriAsync(url);
            AssetBundle assetBundle = await _assetLoader.LoadAsync(uri);
            if (assetBundle == null)
            {
                throw new NullReferenceException();
            }
            _gameTypeToBundle.Add(gamesType, assetBundle);
        }

        public UniTask UnloadAsync(GamesType gamesType)
        {
            var bundle = GetAssetBundle(gamesType);
            if (bundle != null)
            {
                bundle.Unload(false);
                _gameTypeToBundle.Remove(gamesType);
            }
            
            return UniTask.CompletedTask;
        }
 
        public AssetBundle GetAssetBundle(GamesType gamesType)
        {
            _gameTypeToBundle.TryGetValue(gamesType, out var bundle);
            return bundle;
        }
    }
}
