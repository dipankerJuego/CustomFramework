using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using UnityEditor.Build.Reporting;

namespace XRCustomFramework
{
    /// <summary>
    /// Build settings use to create Editor Window.
    /// Create own custom editor window that can float free or be docked as a tab, just like the native windows in the Unity interface.
    /// </summary>
    public class BuildSettings : EditorWindow
    {
        const string EditorPrefs_Previous_Build_Path = "Previous_Build_Path";
        const string EditorPrefs_Previous_Build_Target = "Previous_Build_Target";

        public static XR_Enum.SDKType SDK_TYPE = XR_Enum.SDKType.XR;

        static BuildSettings window;
        static BuildSettingAssetsHolder assetsHolder;
        static AssetsReference assetsReference;

        [InitializeOnLoadMethod]
        static void InitDefaultValues()
        {
            Debug.Log("Check for WaveVR prefered editor settings");
            SDK_TYPE = (XR_Enum.SDKType)EditorPrefs.GetInt("SDK_TYPE", 1);
        }

        [MenuItem("XR Framework/Build Settings")]
        static void ShowWindow()
        {
            window = GetWindow<BuildSettings>("XR FRAMEWORK BUILD SETTINGS");
            window.minSize = new Vector2(320, 100);
        }

        public void OnGUI()
        {
            if(assetsHolder == null)
                assetsHolder = Resources.Load<BuildSettingAssetsHolder>(ConstantVar.ResourcesPath.BUILD_SETTINGS);
            if(assetsReference == null)
                assetsReference = Resources.Load<AssetsReference>(ConstantVar.ResourcesPath.ASSETS_REFERENCE);

            EditorGUILayout.Space();
            SDK_GUI();
            EditorGUILayout.Space();
            BuildGUI();
        }

        void SDK_GUI()
        {
            GUILayout.Label("SDK Settings", EditorStyles.boldLabel);
            SDK_TYPE = (XR_Enum.SDKType)EditorGUILayout.EnumPopup("Select SDK: ", SDK_TYPE, GUILayout.Width(250));

            if (GUI.changed)
            {
                assetsReference.SDK_Selected = SDK_TYPE;
                Debug.Log("<color=blue>" + assetsReference.SDK_Selected + "</color>");
                EditorPrefs.SetInt("SDK_TYPE", (int)SDK_TYPE);
                PlayerPrefs.SetInt("VirtualRealitySupported", (int)SDK_TYPE);

                string[] sdkTarget = new string[0];
                if (SDK_TYPE == XR_Enum.SDKType.XR)
                {
                    if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
                        sdkTarget = PlayerSettings.GetVirtualRealitySDKs(BuildTargetGroup.Android);
                    else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS)
                        sdkTarget = PlayerSettings.GetVirtualRealitySDKs(BuildTargetGroup.iOS);
                    else
                        sdkTarget = PlayerSettings.GetVirtualRealitySDKs(BuildTargetGroup.Standalone);
                }
            }
        }

        void BuildGUI()
        {
            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(250));
            if (GUILayout.Button("Build", GUILayout.MaxWidth(100), GUILayout.MaxHeight(20)))
            {
                string build_path = EditorUtility.SaveFilePanel("", "", "", "");
                if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
                    build_path += ".apk";
                if (string.IsNullOrEmpty(build_path) == false)
                    Build(build_path);
            }
            //if (GUILayout.Button("Build And Run", GUILayout.MaxWidth(100), GUILayout.MaxHeight(20)))
            //{
            //    //InitBuildPath(true);
            //}
            EditorGUILayout.EndHorizontal();
        }

        static void InitBuildPath(bool isBuildAndRun)
        {
            string previous_Build_Path = EditorPrefs.GetString(EditorPrefs_Previous_Build_Path, "");
            int previous_Build_Target = EditorPrefs.GetInt(EditorPrefs_Previous_Build_Target, -1);
            string build_path = "";

            if (string.IsNullOrEmpty(previous_Build_Path))
            {
                build_path = EditorUtility.SaveFilePanel("", "", "", "");
                if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
                {
                    build_path += ".apk";
                }
            }
            else
            {
                if (EditorUserBuildSettings.activeBuildTarget == (BuildTarget)previous_Build_Target)
                {
                    if(XR_Utilities.IsPathDirectory(previous_Build_Path))
                    {

                    }
                    else
                    {
                        string fileName = Path.GetFileNameWithoutExtension(previous_Build_Path);
                        string fileExtensions = Path.GetExtension(previous_Build_Path);
                        fileExtensions = fileExtensions.Remove(0, 1);
                        string directoryPath = previous_Build_Path.Replace( "/"+fileName, "");
                        Debug.Log(string.Format("FILENAME: {0} EXTENSIONS {1} PATH {2}", fileName, fileExtensions, directoryPath));
                        build_path = EditorUtility.SaveFilePanel("", directoryPath, fileName, fileExtensions);
                    }
                }
            }

            if (string.IsNullOrEmpty(build_path) == false)
            {
                Build(build_path);
            }
        }

        static void Build(string targetPath)
        {
            EditorPrefs.SetString(EditorPrefs_Previous_Build_Path, targetPath);
            EditorPrefs.SetInt(EditorPrefs_Previous_Build_Target, (int)EditorUserBuildSettings.activeBuildTarget);

            Debug.Log(targetPath);

            GenericBuild(FindEnabledEditorScenes(), targetPath, EditorUserBuildSettings.activeBuildTarget, BuildOptions.None);
        }

        static void GenericBuild(string[] scenes, string target_dir, BuildTarget build_target, BuildOptions build_options)
        {
            Debug.Log("Building for " + build_target);
            EditorUserBuildSettings.SwitchActiveBuildTarget(build_target);
            var res = BuildPipeline.BuildPlayer(scenes, target_dir, build_target, build_options);
            BuildSummary summary = res.summary;
            if (summary.result == BuildResult.Failed)
                throw new UnityException("BuildPlayer Failure: " + res);
            else
            {
                Debug.Log("Build Success res- " + res + "  target_dir");
            }
        }

        private static string[] FindEnabledEditorScenes()
        {
            List<string> EditorScenes = new List<string>();

            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                if (scene.enabled == false)
                    continue;

                EditorScenes.Add(scene.path);
            }

            return EditorScenes.ToArray();
        }
    }
}
