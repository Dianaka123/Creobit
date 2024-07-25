using Assets.Scripts.ConfigData;
using Assets.Scripts.Systems.Interfaces;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Text;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Systems
{
    public class GameConfigurationProvider : IGameConfigProvider, IInitializable
    {
        private readonly IServerConfigProvider _serverConfigProvider;
        private readonly IServerFileProvider _serverFileLoader;
        private readonly IApplicationOnDestroy _appDestroyNotifier;

        private string _url;

        public GameConfiguration Configuration { get; private set; }

        public GameConfigurationProvider(IServerConfigProvider configProvider, IServerFileProvider serverFileLoader, IApplicationOnDestroy appDestroyNotifier)
        {
            _serverConfigProvider = configProvider;
            _serverFileLoader = serverFileLoader;
            _appDestroyNotifier = appDestroyNotifier;
        }

        public void Initialize()
        {
            _appDestroyNotifier.Destroing += async () => await Upload();
        }

        public async UniTask Download()
        {
            if(Configuration == null)
            {
                try
                {
                    _url = _serverConfigProvider.Config.GameConfigURL;
                    byte[] configBytes = await _serverFileLoader.LoadFile(_url);
                    string jsonStr = Encoding.UTF8.GetString(configBytes);
                    Configuration = JsonConvert.DeserializeObject<GameConfiguration>(jsonStr);
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex.Message);
                }
            }
        }

        public void Unload()
        {
            Configuration = null;
        }

        public async UniTask Upload()
        {
            string jsonStr = JsonConvert.SerializeObject(Configuration);
            var bytes = Encoding.UTF8.GetBytes(jsonStr);
            await _serverFileLoader.UpdateFile(bytes, _url);
        }
    }
}
