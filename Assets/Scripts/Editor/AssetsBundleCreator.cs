using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

namespace Assets.Scripts.Editor
{
    public class AssetsBundleCreator : ScriptableObject
    {
        [MenuItem("Assets/Build AssetsBundles")]
        static void BuildAssetsBundles()
        {
            string buildPath = "Assets/AssetBundles";
            if(!Directory.Exists(buildPath))
            {
                Directory.CreateDirectory(buildPath);
            }

            BuildPipeline.BuildAssetBundles(buildPath, BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.StandaloneWindows64);
        }
    }
}