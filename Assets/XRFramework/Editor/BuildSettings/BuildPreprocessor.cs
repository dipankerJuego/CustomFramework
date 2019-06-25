using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace XRCustomFramework
{
    public class BuildPreprocessor :
#if UNITY_2018_1_OR_NEWER
        IPreprocessBuildWithReport
#else
        IPreprocessBuild
#endif
    {
        private static string WaveVR_AndroidManifest_File_Path = Application.dataPath + "/WaveVR/AndroidManifest.xml";
        private static string Original_AndroidManifest_File = Application.dataPath + "/Plugins/Android/AndroidManifest.xml";

        public int callbackOrder { get { return 0; } }

        void AssetManagment(BuildTarget target, string path)
        {
            if(BuildSettings.SDK_TYPE == XR_Enum.SDKType.None)
            {
                PlayerSettings.virtualRealitySupported = false;
            }
            else
            {
                PlayerSettings.virtualRealitySupported = true;
            }

            if (BuildSettings.SDK_TYPE == XR_Enum.SDKType.WaveVR)
            {
                InitNativePlugins(XR_Enum.SDKType.WaveVR, target, true);

                if (target == BuildTarget.Android)
                {
                    //Copy Android Manifest files....
                    if(File.Exists(WaveVR_AndroidManifest_File_Path))
                    {
                        //Check and delete if already file exists....
                        DeleteOriginalAndroidManifestFile();
                        File.Copy(WaveVR_AndroidManifest_File_Path, Original_AndroidManifest_File);
                    }
                }
            }

            if (BuildSettings.SDK_TYPE == XR_Enum.SDKType.XR)
            {
                //AndroidManifest not require...
                DeleteOriginalAndroidManifestFile();
                //Deactivae Wave VR Native Plugin Files...
                InitNativePlugins(XR_Enum.SDKType.WaveVR, target, false);

                if (target == BuildTarget.Android)
                    PlayerSettings.SetVirtualRealitySDKs(BuildTargetGroup.Android, PlayerSettings.GetVirtualRealitySDKs(BuildTargetGroup.Android));
                else if (target == BuildTarget.iOS)
                    PlayerSettings.SetVirtualRealitySDKs(BuildTargetGroup.iOS, PlayerSettings.GetVirtualRealitySDKs(BuildTargetGroup.iOS));
                else
                    PlayerSettings.SetVirtualRealitySDKs(BuildTargetGroup.Standalone, PlayerSettings.GetVirtualRealitySDKs(BuildTargetGroup.Standalone));
            }
        }

        void InitNativePlugins(XR_Enum.SDKType type, BuildTarget target, bool enable)
        {
            //string resourceName = type.ToString() + "_Assets";
            BuildSettingAssetsHolder waveVRBuildFiles = Resources.Load<BuildSettingAssetsHolder>(ConstantVar.ResourcesPath.BUILD_SETTINGS);
            XR_SDK_Plugin[] allNativePluginFiles = null;
            if (waveVRBuildFiles != null)
                allNativePluginFiles = waveVRBuildFiles.GetAllPluginFiles();

            if (allNativePluginFiles != null)
            {
                foreach (XR_SDK_Plugin pluginFile in allNativePluginFiles)
                {
                    PluginImporter pluginImporter = AssetImporter.GetAtPath(pluginFile.GetFilePath) as PluginImporter;
                    if (pluginImporter != null && pluginFile.buildTarget == target && pluginImporter.GetCompatibleWithPlatform(pluginFile.buildTarget) == (!enable))
                        pluginImporter.SetCompatibleWithPlatform(pluginFile.buildTarget, enable);
                }
            }
        }

        public void DeleteOriginalAndroidManifestFile()
        {
            if(File.Exists(Original_AndroidManifest_File))
            {
                File.Delete(Original_AndroidManifest_File);
            }
        }

        public void OnPreprocessBuild(BuildReport report)
        {
            Debug.Log("MAIN " + report.summary.platform.ToString());
            AssetManagment(report.summary.platform, report.summary.outputPath);
        }
    }
}
