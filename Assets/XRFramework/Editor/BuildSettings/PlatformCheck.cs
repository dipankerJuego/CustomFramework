using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace XRCustomFramework
{
	public class PlatformCheck : EditorWindow
	{
		private const string WaveVRDefine = "CUSTOM_WAVEVR_SDK";
		private const string LeanTweenDefine = "CUSTOM_LEAN_TWEEN";
		private const string ScreenFactoryDefine = "CUSTOM_SCREEN_FACTORY";

		static PlatformCheck()
		{
			EditorApplication.update += OnCheck;
		}

        static void OnCheck()
		{
            EditorApplication.update -= OnCheck;

            //Check if WaveVR SDK plugin exist
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = assemblies.SelectMany(a => a.GetTypes()).ToList();
#if UNITY_ANDROID
            bool hasWaveVRSDK = types.Any(t => t.FullName == "WaveVR_Render");
#endif
            bool hasLeanTween = types.Any(t => t.FullName == "LeanTween");
            bool hasScreenFactory = types.Any(t => t.FullName == "ScreenBase");

#if UNITY_ANDROID
            string symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
#elif UNITY_IOS
            string symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS);
#elif UNITY_PS4
            string symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.PS4);
#elif UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
            string symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
#endif

            string newSymbols = "";

            foreach (var define in symbols.Split(';'))
            {
#if UNITY_ANDROID
                if(define == WaveVRDefine)
                {
                    if (hasWaveVRSDK == false) continue;
                    hasWaveVRSDK = false;
                }
#endif

                if(define == LeanTweenDefine)
                {
                    if (hasLeanTween == false) continue;
                    hasLeanTween = false;
                }

                if (define == ScreenFactoryDefine)
                {
                    if (hasScreenFactory == false) continue;
                    hasScreenFactory = false;
                }

                AppendDefine(ref newSymbols, define);
            }

#if UNITY_ANDROID
            if (hasWaveVRSDK)
            {
                AssetsReference assetsReference = Resources.Load<AssetsReference>(ConstantVar.ResourcesPath.ASSETS_REFERENCE);
                if (assetsReference != null && assetsReference.SDK_Selected == XRCustomFramework.XR_Enum.SDKType.WaveVR)
                {

                    AppendDefine(ref newSymbols, WaveVRDefine);
                    Debug.LogFormat("Add scripting define symbol {0} for Android platform", WaveVRDefine);
                }
            }
#endif

            if (hasLeanTween)
            {
                AppendDefine(ref newSymbols, LeanTweenDefine);
                Debug.LogFormat("Add scripting define symbol {0} for All platform", LeanTweenDefine);
            }

            if (hasScreenFactory)
            {
                AppendDefine(ref newSymbols, ScreenFactoryDefine);
                Debug.LogFormat("Add scripting define symbol {0} for All platform", ScreenFactoryDefine);
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, newSymbols);
        }

        static void AppendDefine(ref string defines, string element)
        {
            if (defines != "")
                defines += ";";
            defines += element;
        }
    }
}
