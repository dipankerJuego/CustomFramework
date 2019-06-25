#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace XRCustomFramework
{
    [CreateAssetMenu(menuName = "XRBuildSetting/AssetsHolder")]
    public class BuildSettingAssetsHolder : ScriptableObject
    {
        [SerializeField] private XR_SDK_Plugin[] nativePlugins;

        public XR_SDK_Plugin[] GetAllPluginFiles()
        {
            return nativePlugins;
        }
    }

    [Serializable]
    public class XR_SDK_Plugin
    {
        public UnityEngine.Object pluginFile;
        public BuildTarget buildTarget;

        public string GetFilePath { get { return AssetDatabase.GetAssetPath(pluginFile); } }
    }
}
#endif
