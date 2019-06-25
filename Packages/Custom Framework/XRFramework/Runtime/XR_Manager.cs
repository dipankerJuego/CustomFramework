using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace XRCustomFramework
{
    /// <summary>
    /// XRManager Set the tracking Space. Created XR Input Handler.
    /// </summary>
    public class XR_Manager : MonoBehaviour
    {
        [SerializeField] private TrackingSpaceType trackingSpaceType = TrackingSpaceType.RoomScale;
        [SerializeField] private bool lockPhysicsUpdateRateToRenderFrequency = true;
        [Space(), Header("Object Reference"), SerializeField] private GameObject m_playArea;
        [SerializeField] private GameObject m_headSet;
        [SerializeField] private Camera m_headSetCamera;
        [SerializeField] private GameObject m_leftHand;
        [SerializeField] private GameObject m_rightHand;

        [Space(), Header("Input Manager"), SerializeField] private XR_InputHandler m_inputManager;

        private XR_Enum.Device loadedDevice;

        public GameObject PlayArea { get { return m_playArea; } }
        public GameObject Headset { get { return m_headSet; } }
        public Camera HeadsetCamera { get { return m_headSetCamera; } }
        public GameObject RightHand { get { return m_rightHand; } }
        public GameObject LeftHand { get { return m_leftHand; } }
        public XR_Enum.Device GetDevice { get { return loadedDevice; } }
        public XR_InputHandler InputManager { get { return m_inputManager; } }

        protected virtual void OnEnable()
        {
            UpdateTrackingSpaceType();
            LoadedDevice();
            InitializeInputHandler();
        }

        void LoadedDevice()
        {
            if (XRDevice.model.Contains("Google"))
            {
                if (XRSettings.loadedDeviceName.Contains("carboard"))
                {
                    //TODO: Cardboard
                    loadedDevice = XR_Enum.Device.None;
                }
            }
            else if (XRDevice.model.Contains("Samsung"))
            {
                loadedDevice = XR_Enum.Device.GearVR;
            }
            else if (XRDevice.model.Contains("Vive"))
            {
                loadedDevice = XR_Enum.Device.Vive;
            }
            else if (XRDevice.model.Contains("Rift"))
            {
                loadedDevice = XR_Enum.Device.Oculus;
            }
            else
            {
                loadedDevice = XR_Enum.Device.None;
            }

            Debug.Log("VR Device Model Name: " + XRDevice.model);
            Debug.Log("Loaded Device Name: " + XRSettings.loadedDeviceName);
            Debug.Log("loadedDevice: " + loadedDevice);
        }

        /// <summary>
        /// Update every Frame...
        /// </summary>
        protected virtual void Update()
        {
            UpdateFixedDeltaTime();
        }

        /// <summary>
        /// Sets the device's current TrackingSpaceType
        /// </summary>
        protected virtual void UpdateTrackingSpaceType()
        {
            XRDevice.SetTrackingSpaceType(trackingSpaceType);
        }

        /// <summary>
        /// Updates the fixed delta time to the appropriate value.
        /// </summary>
        protected virtual void UpdateFixedDeltaTime()
        {
            if(lockPhysicsUpdateRateToRenderFrequency 
                && Time.timeScale > 0.0f
                && !string.IsNullOrEmpty(XRSettings.loadedDeviceName))
            {
                Time.fixedDeltaTime = Time.timeScale / XRDevice.refreshRate;
            }
        }

        /// <summary>
        /// Initialize Input Handler
        /// </summary>
        protected virtual void InitializeInputHandler()
        {
            if(m_inputManager == null)
            {
                GameObject handler = new GameObject("XR_Input_Manager");
                m_inputManager = handler.AddComponent<XR_InputHandler>();
                handler.transform.SetParent(this.transform);
            }
        }
    }
}
