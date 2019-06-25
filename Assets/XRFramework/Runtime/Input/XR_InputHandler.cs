using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace XRCustomFramework
{
    public class XR_InputHandler : MonoBehaviour
    {
        /// <summary>
        /// Get All connected Input device..
        /// </summary>
        public List<InputDevice> GetAllConnectedDevices { get { return inputDeviceList; } }

        public delegate void OnUpdateDelegate();
        public static event OnUpdateDelegate OnUpdateEvent;

        private List<InputDevice> inputDeviceList = new List<InputDevice>();
        private List<XR_Controller> handControllers = new List<XR_Controller>();

        #region Unity MonoBehaviour Methods

        private void Awake()
        {
            InitInputDevices();
        }

        private void OnEnable()
        {
            InputTracking.nodeAdded += InputTracking_NodeAdded;
            InputTracking.nodeRemoved += InputTracking_NodeRemoved;
            InputTracking.trackingAcquired += InputTracking_TrackingAcquired;
            InputTracking.trackingLost += InputTracking_TrackingLost;
        }

        // Update is called once per frame
        void Update()
        {
            OnUpdateEvent?.Invoke();
        }

        private void OnDisable()
        {
            InputTracking.nodeAdded -= InputTracking_NodeAdded;
            InputTracking.nodeRemoved -= InputTracking_NodeRemoved;
            InputTracking.trackingAcquired -= InputTracking_TrackingAcquired;
            InputTracking.trackingLost -= InputTracking_TrackingLost;
        }

        #endregion

        #region Initialize InputManager

        private void Initialize()
        {
            InitInputDevices();
        }

        /// <summary>
        /// Initialize all input devices attached with gear.
        /// And add in the least
        /// </summary>
        private void InitInputDevices()
        {
            List<InputDevice> inputDevices = new List<InputDevice>();
            InputDevices.GetDevices(inputDevices);

            for (int i = 0; i < inputDevices.Count; i++)
            {
                Debug.Log(string.Format("Device Name: {0} \nDeviece Role: {1}", inputDevices[i].name, inputDevices[i].role.ToString()));
                if (inputDeviceList.Contains(inputDevices[i]) == false)
                {
                    inputDeviceList.Add(inputDevices[i]);
                    if (inputDevices[i].role == InputDeviceRole.LeftHanded || inputDevices[i].role == InputDeviceRole.RightHanded)
                    {
                        AddNewInputDevice(inputDevices[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Removes device from list...
        /// </summary>
        protected virtual void DeviceRemoved()
        {
            List<InputDevice> inputDevices = new List<InputDevice>();
            InputDevices.GetDevices(inputDevices);

            for (int i = 0; i < inputDeviceList.Count; i++)
            {
                if (inputDevices.Contains(inputDeviceList[i]) == false)
                {
                    RemoveHandController(inputDeviceList[i]);
                    inputDeviceList.Remove(inputDevices[i]);
                    break;
                }
            }
        }

        void RemoveHandController(InputDevice device)
        {
            for (int i = 0; i < handControllers.Count; i++)
            {
                if(handControllers[i].device.Equals(device))
                {
                    Destroy(handControllers[i].gameObject);
                    handControllers.RemoveAt(i);
                    break;
                }
            }
        }

        #endregion

        #region Input Tracking Events

        /// <summary>
        /// Called when a tracked node is added to the underlying XR system.
        /// </summary>
        /// <param name="nodeState"></param>
        private void InputTracking_NodeAdded(XRNodeState nodeState)
        {
            InitInputDevices();
        }

        /// <summary>
        /// Called when a tracked node is removed from the underlying XR system.
        /// </summary>
        /// <param name="nodeState"></param>
        private void InputTracking_NodeRemoved(XRNodeState nodeState)
        {
            DeviceRemoved();
        }

        private void InputTracking_TrackingAcquired(XRNodeState nodeState)
        {
            Debug.Log("InputTracking_TrackingAcquired " + nodeState.nodeType);
        }

        private void InputTracking_TrackingLost(XRNodeState nodeState)
        {
            Debug.Log("InputTracking_TrackingLost " + nodeState.nodeType);
        }

        #endregion

        internal void AddNewInputDevice(InputDevice device)
        {
            GameObject node = new GameObject();
            XR_Controller controller = node.AddComponent<XR_Controller>();
            controller.InitInputDevice(device);
            handControllers.Add(controller);
        }

        #region Unity Input.Get method to get input controller

        public bool GetButon(XR_Enum.FeatureUsageButton featureUsage, XR_Enum.Hand hand)
        {
            XR_Controller controller = GetHandController(hand);
            if (controller == null) return false;

            switch (featureUsage)
            {
                case XR_Enum.FeatureUsageButton.PrimaryButton: return controller.IsPrimaryPressed;
                case XR_Enum.FeatureUsageButton.PrimaryTouch: return controller.IsPrimaryTouched;
                case XR_Enum.FeatureUsageButton.SecondaryButton: return controller.IsSecondaryPressed;
                case XR_Enum.FeatureUsageButton.SecondaryTouch: return controller.IsSecondaryTouched;
                case XR_Enum.FeatureUsageButton.GripButton: return controller.IsGripPressed;
                case XR_Enum.FeatureUsageButton.TriggerButton: return controller.IsTriggerPressed;
                case XR_Enum.FeatureUsageButton.MenuButton: return controller.IsMenuPressed;
                case XR_Enum.FeatureUsageButton.Primary2DAxisClick: return controller.IsPrimary2DAxisPressed;
                case XR_Enum.FeatureUsageButton.Primary2DAxisTouch: return controller.IsPrimary2DAxisTouch;
                case XR_Enum.FeatureUsageButton.Thumbrest: return controller.IsThumbrestPressed;
                default: return false;
            }
        }
                
        public float GetAxis(XR_Enum.FeatureUsageAxis featureUsage, XR_Enum.Hand hand)
        {
            XR_Controller controller = GetHandController(hand);
            if (controller == null) return 0;

            switch (featureUsage)
            {
                case XR_Enum.FeatureUsageAxis.Trigger: return controller.TriggerValue;
                case XR_Enum.FeatureUsageAxis.Grip: return controller.GripValue;
                case XR_Enum.FeatureUsageAxis.IndexTouch: return controller.IndexTouchValue;
                case XR_Enum.FeatureUsageAxis.ThumbTouch: return controller.ThumbTouchValue;
                case XR_Enum.FeatureUsageAxis.IndexFinger: return controller.IndexFingerValue;
                case XR_Enum.FeatureUsageAxis.MiddleFinger: return controller.MiddleFingerValue;
                case XR_Enum.FeatureUsageAxis.RingFinger: return controller.RingFingerValue;
                case XR_Enum.FeatureUsageAxis.PinkyFinger: return controller.PinkyFingerValue;
                case XR_Enum.FeatureUsageAxis.CombinedTrigger: return controller.CombinedTriggerValue;
                default: return 0;
            }
        }

        public Vector2 GetAxis2D(XR_Enum.FeatureUsage2DAxis featureUsage, XR_Enum.Hand hand)
        {
            XR_Controller controller = GetHandController(hand);
            if (controller == null) return Vector2.zero;

            switch (featureUsage)
            {
                case XR_Enum.FeatureUsage2DAxis.Primary2DAxis: return controller.GetPrimary2DAxisValue;
                case XR_Enum.FeatureUsage2DAxis.Secondary2DAxis: return controller.GetSecondary2DAxisValue;
                default:return Vector2.zero;
            }
        }

        #endregion

        public XR_Controller GetHandController(XR_Enum.Hand hand)
        {
            XR_Controller controller = handControllers.Find(obj => obj.HandType == hand);
            return controller;
        }

        /// <summary>
        /// Check any Hand Controller is connected.
        /// </summary>
        /// <returns><c>true</c>, if Controller is connected, <c>false</c> otherwise.</returns>
        public bool IsHanded()
        {
            if(GetHandController(XR_Enum.Hand.RIGHT) != null || GetHandController(XR_Enum.Hand.LEFT) != null)
                return true;

            return false;
        }

        /// <summary>
        /// Gets Any the prefered Left/Right hand.
        /// If both are connected returns Right.
        /// </summary>
        /// <returns>The prefered hand.</returns>
        public XR_Enum.Hand GetPreferedHand()
        {
            List<XRNodeState> nodeStates = new List<XRNodeState>();
            InputTracking.GetNodeStates(nodeStates);

            bool hasLeft = false;
            bool hasRight = false;
            foreach (XRNodeState nodeState in nodeStates)
            {
                if (nodeState.nodeType == XRNode.LeftHand)
                    hasLeft = true;
                if (nodeState.nodeType == XRNode.RightHand)
                    hasRight = true;
            }

            if (hasLeft && !hasRight)
                return XR_Enum.Hand.LEFT;
            else if (!hasLeft && hasRight)
                return XR_Enum.Hand.RIGHT;
            else
                return XR_Enum.Hand.RIGHT;
        }
    }
}
