using Assets.Scripts.Systems.Interfaces;
using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class ServerConfigProvider : IServerConfigProvider
    {
        private static readonly string _path = Application.dataPath + "/config.json";
        private ServerConfiguration _configuration;

        public ServerConfiguration Config
        {
            get
            {
                if(_configuration == null)
                {
                    _configuration = new ServerConfiguration()
                    {
                        RunnerResourcesURL = "gs://acquired-sunup-429810-u9.appspot.com/AssetsBundles/runner",
                        ClickerResourcesURL = "gs://acquired-sunup-429810-u9.appspot.com/AssetsBundles/clicker",
                        GameConfigURL = "gs://acquired-sunup-429810-u9.appspot.com/Configs/config.json"
                    };
                }

                return _configuration;
            }
        }

        private ServerConfiguration ReadfromJson()
        {
            try
            {
                var str = File.ReadAllText(_path);
                return JsonConvert.DeserializeObject<ServerConfiguration>(File.ReadAllText(_path));
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return null;
            }
        }
    }
}
