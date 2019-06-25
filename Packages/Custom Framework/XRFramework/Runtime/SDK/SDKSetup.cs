using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XRCustomFramework
{
    /// <summary>
    /// SDK Setup will Initalize the SDK by checking SDK type.
    /// </summary>
    public class SDKSetup : MonoBehaviour
    {
        private static SDKSetup m_instance;
        public static SDKSetup Instance { get { return m_instance; } }

        /// <summary>
        /// Create Singleton Instance of SDKSetup...
        /// </summary>
        private void CreateInstance()
        {
            if (m_instance == null)
            {
                m_instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        [SerializeField] private bool showCanvasDebug;
        [SerializeField] private GameObject m_XR_SDK_GameObject;

        public XR_Manager XRManager { get; private set; }
        private XR_Enum.SDKType currentSDK;
        private InputManager m_inputManager;

        private void Awake()
        {
            CreateInstance();

            if (m_XR_SDK_GameObject != null)
                m_XR_SDK_GameObject.SetActive(false);

            if (showCanvasDebug)
            {
                AssetsReference assetsReference = Resources.Load<AssetsReference>(ConstantVar.ResourcesPath.ASSETS_REFERENCE);
                GameObject canvasDebug = Instantiate(assetsReference.canvasDebugger);
                canvasDebug.transform.SetParent(this.transform);
            }
        }

        private void OnEnable()
        {
            AssetsReference assetsReference = Resources.Load<AssetsReference>(ConstantVar.ResourcesPath.ASSETS_REFERENCE);
            if (assetsReference != null)
            {
                currentSDK = assetsReference.SDK_Selected;
                if (m_inputManager == null) m_inputManager = new InputManager();

                switch (currentSDK)
                {
                    case XR_Enum.SDKType.None: NonVR_SetUp(); break;
                    case XR_Enum.SDKType.XR: XR_SetUp(); break;
                    case XR_Enum.SDKType.WaveVR: WaveVR_Setup(); break;
                }
            }
        }

        #region SDK Setup

        void NonVR_SetUp()
        {

        }

        void XR_SetUp()
        {
            AssetsReference assetsReference = Resources.Load<AssetsReference>(ConstantVar.ResourcesPath.ASSETS_REFERENCE);
            if (assetsReference != null)
            {
                if (m_XR_SDK_GameObject == null)
                    m_XR_SDK_GameObject = (GameObject)Instantiate(assetsReference.XR_Manager, this.transform);

                XRManager = m_XR_SDK_GameObject.GetComponent<XR_Manager>();
                m_XR_SDK_GameObject.SetActive(true);
            }
        }

        void WaveVR_Setup()
        {
#if CUSTOM_WAVEVR_SDK
            AssetsReference assetsReference = Resources.Load<AssetsReference>(ConstantVar.ResourcesPath.ASSETS_REFERENCE);
            if (assetsReference != null)
            {
                //WaveVR Root GameObject
                GameObject waveSDK = new GameObject("WaveVR_SETUP");

                //Create WaveVR SDK for Camera rendering and Head Gear.
                Instantiate(assetsReference.GetWaveVRSDK, waveSDK.transform);

                //Create WaveVR Button to receive any button event...
                Instantiate(assetsReference.GetWaveVRButtons, waveSDK.transform);

                //Create Controller Loader for Left and Right Controller.
                GameObject controllerLeft = Instantiate(assetsReference.GetWaveVRControllerLoader, waveSDK.transform);
                GameObject controllerRight = Instantiate(assetsReference.GetWaveVRControllerLoader, waveSDK.transform);
                controllerLeft.GetComponent<WaveVR_ControllerLoader>().WhichHand = WaveVR_ControllerLoader.ControllerHand.Non_Dominant;
                controllerRight.GetComponent<WaveVR_ControllerLoader>().WhichHand = WaveVR_ControllerLoader.ControllerHand.Dominant;

                //Create Input Module Manager
                Instantiate(assetsReference.GetWaveVRInputModule, waveSDK.transform);
            }
#endif
        }

        #endregion

        #region PUBLIC METHODS AND PROPETIES

        public static XR_Enum.SDKType GetSDKType { get { return m_instance.currentSDK; } }

        public static GameObject PlayArea()
        {
            switch (m_instance.currentSDK)
            {
                case XR_Enum.SDKType.XR: return m_instance.XRManager.PlayArea;
                //case XR_Enum.SDKType.WaveVR: return m_instance.m_XRManager.GetDevice;
                default: return null;
            }
        }

        public static GameObject GetHeadset()
        {
            switch (m_instance.currentSDK)
            {
                case XR_Enum.SDKType.XR: return m_instance.XRManager.PlayArea;
                //case XR_Enum.SDKType.WaveVR: return m_instance.m_XRManager.GetDevice;
                default: return null;
            }
        }

        public static Camera GetCenterCamera()
        {
            switch (m_instance.currentSDK)
            {
                case XR_Enum.SDKType.XR: return m_instance.XRManager.HeadsetCamera;
                //case XR_Enum.SDKType.WaveVR: return m_instance.m_XRManager.GetDevice;
                default: return null;
            }
        }

        public static GameObject GetHand(XR_Enum.Hand hand = XR_Enum.Hand.RIGHT)
        {
            switch (m_instance.currentSDK)
            {
                case XR_Enum.SDKType.XR:
                    {
                        if (hand == XR_Enum.Hand.RIGHT)
                            return m_instance.XRManager.RightHand;
                        else
                            return m_instance.XRManager.LeftHand;
                    }
                //case XR_Enum.SDKType.WaveVR: return m_instance.m_XRManager.GetDevice;
                default: return null;
            }
        }

        public static XR_Enum.Device GetDevice()
        {
            switch (m_instance.currentSDK)
            {
                case XR_Enum.SDKType.XR: return m_instance.XRManager.GetDevice;
                //case XR_Enum.SDKType.WaveVR: return m_instance.m_XRManager.GetDevice;
                default: return XR_Enum.Device.None;
            }
        }

        #endregion
    }
}
