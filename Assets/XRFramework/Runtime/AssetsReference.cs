using UnityEngine;

namespace XRCustomFramework
{
    [CreateAssetMenu(fileName = "Assets_Reference", menuName = "Create Reference Object")]
    public class AssetsReference : ScriptableObject
    {
        [Space(), Header("Testing")]
        public GameObject canvasDebugger;

        [Space()]
        public XR_Enum.SDKType SDK_Selected;

        [Space(), Header("XR")]
        public GameObject XR_Manager;

        [Space(), Header("XR Controller Prefabs")]
        [SerializeField] private ControllerModel[] controllerModels;
        [Space(), Header("WaveVR Prefabs")]
        [SerializeField] private GameObject WaveVR_SDK;
        [SerializeField] private GameObject WaveVR_ControllerLoader;
        [SerializeField] private GameObject WaveVR_Buttons;
        [SerializeField] private GameObject WaveVR_InputModule;

        public ControllerModel GetControllerMode(XR_Enum.Device device, XR_Enum.Hand hand)
        {
            if (controllerModels == null)
                return null;

            for (int i = 0; i < controllerModels.Length; i++)
            {
                if (controllerModels[i].device == device && controllerModels[i].hand == hand)
                    return controllerModels[i];
            }

            return null;
        }

        public GameObject GetWaveVRSDK { get { return WaveVR_SDK; } }
        public GameObject GetWaveVRButtons { get { return WaveVR_Buttons; } }
        public GameObject GetWaveVRControllerLoader { get { return WaveVR_ControllerLoader; } }
        public GameObject GetWaveVRInputModule { get { return WaveVR_InputModule; } }
    }
}
