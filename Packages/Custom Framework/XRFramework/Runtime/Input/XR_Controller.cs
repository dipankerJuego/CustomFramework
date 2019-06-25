using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

namespace XRCustomFramework
{
    public class XR_Controller : MonoBehaviour
    {
        public XR_Enum.Device deviceType;
        [HideInInspector]
        public XRNode node = XRNode.TrackingReference;
        [HideInInspector]
        public InputDevice device;

        public XR_Enum.Hand HandType { get; protected set; }

        #region BOOL VARIABLES

        public bool IsPrimaryPressed { get { return primaryPress; } }
        public bool IsPrimaryTouched { get { return primaryTouch; } }
        public bool IsSecondaryPressed { get { return secondaryPress; } }
        public bool IsSecondaryTouched { get { return secondaryTouch; } }
        public bool IsGripPressed { get { return gripPressed; } }
        public bool IsTriggerPressed { get { return triggerPressed; } }
        public bool IsMenuPressed { get { return menuPressed; } }
        public bool IsPrimary2DAxisPressed { get { return primary2DAxisPress; } }
        public bool IsPrimary2DAxisTouch { get { return primary2DAxisTouch; } }
        public bool IsThumbrestPressed { get { return thumbrestPress; } }

        protected bool primaryPress, primaryTouch;
        protected bool secondaryPress, secondaryTouch;
        protected bool gripPressed;
        protected bool triggerPressed;
        protected bool menuPressed;
        protected bool primary2DAxisPress, primary2DAxisTouch;
        protected bool thumbrestPress;

        #endregion

        #region FLOAT VARIABLES

        public float TriggerValue { get { return triggerValue; } }
        public float GripValue { get { return gripValue; } }
        public float IndexTouchValue { get { return indexTouchValue; } }
        public float ThumbTouchValue { get { return thumbTouchValue; } }
        public float IndexFingerValue { get { return indexFingerValue; } }
        public float MiddleFingerValue { get { return middleFingerValue; } }
        public float RingFingerValue { get { return ringFingerValue; } }
        public float PinkyFingerValue { get { return pinkyFingerValue; } }
        public float CombinedTriggerValue { get { return combinedTriggerValue; } }

        protected float triggerValue;
        protected float gripValue;
        protected float indexTouchValue;
        protected float thumbTouchValue, indexFingerValue, 
            middleFingerValue, ringFingerValue, pinkyFingerValue;
        protected float combinedTriggerValue;

        #endregion

        #region Vector2 VARIABLES

        public Vector2 GetPrimary2DAxisValue { get { return primary2DAxisValue; } }
        public Vector2 GetSecondary2DAxisValue { get { return secondary2DAxisValue; } }

        protected Vector2 primary2DAxisValue;
        protected Vector2 secondary2DAxisValue;

        #endregion

        protected ControllerModel controllerModel;
        protected KeyMap[] keyMapped;

        protected List<InputFeatureUsage<bool>> inputFeaturesMainButtons;
        protected List<InputFeatureUsage<bool>> inputFeaturesMappedButtons;

        protected List<InputFeatureUsage<float>> inputFeaturesMainAxis;
        protected List<InputFeatureUsage<float>> inputFeaturesMappedAxis;

        protected List<InputFeatureUsage<Vector2>> inputFeaturesMain2DAxis;
        protected List<InputFeatureUsage<Vector2>> inputFeaturesMapped2DAxis;

        #region Unity MonoBehaviour Methods

        protected virtual void OnEnable()
        {
            XR_InputHandler.OnUpdateEvent += OnUpdate;
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            Debug.Log("NEW OBJECT " + gameObject.name);

            inputFeaturesMainButtons = new List<InputFeatureUsage<bool>>();
            inputFeaturesMappedButtons = new List<InputFeatureUsage<bool>>();

            inputFeaturesMainAxis = new List<InputFeatureUsage<float>>();
            inputFeaturesMappedAxis = new List<InputFeatureUsage<float>>();

            inputFeaturesMain2DAxis = new List<InputFeatureUsage<Vector2>>();
            inputFeaturesMapped2DAxis = new List<InputFeatureUsage<Vector2>>();

            if (keyMapped != null)
            {
                for (int i = 0; i < keyMapped.Length; i++)
                {
                    if (keyMapped[i].type == typeof(bool))
                    {
                        string usageNameMain = ((XR_Enum.FeatureUsageButton)keyMapped[i].mainInput).ToString();
                        string usageNameMaped = ((XR_Enum.FeatureUsageButton)keyMapped[i].mapedInput).ToString();
                        inputFeaturesMainButtons.Add(new InputFeatureUsage<bool>(usageNameMain));
                        inputFeaturesMappedButtons.Add(new InputFeatureUsage<bool>(usageNameMaped));
                    }

                    if (keyMapped[i].type == typeof(float))
                    {
                        string usageNameMain = ((XR_Enum.FeatureUsageAxis)keyMapped[i].mainInput).ToString();
                        string usageNameMaped = ((XR_Enum.FeatureUsageAxis)keyMapped[i].mapedInput).ToString();
                        inputFeaturesMainAxis.Add(new InputFeatureUsage<float>(usageNameMain));
                        inputFeaturesMappedAxis.Add(new InputFeatureUsage<float>(usageNameMaped));
                    }

                    if (keyMapped[i].type == typeof(Vector2))
                    {
                        string usageNameMain = ((XR_Enum.FeatureUsage2DAxis)keyMapped[i].mainInput).ToString();
                        string usageNameMaped = ((XR_Enum.FeatureUsage2DAxis)keyMapped[i].mapedInput).ToString();
                        inputFeaturesMain2DAxis.Add(new InputFeatureUsage<Vector2>(usageNameMain));
                        inputFeaturesMapped2DAxis.Add(new InputFeatureUsage<Vector2>(usageNameMaped));
                    }
                }
            }
            else
            {
                //NO MAPPING FOUND USE UNITY XR DEFAULT INPUT MAPPING...
                var inputFeatures = new List<InputFeatureUsage>();
                if (device.TryGetFeatureUsages(inputFeatures))
                {
                    foreach (var feature in inputFeatures)
                    {
                        if (feature.type == typeof(bool))
                        {
                            inputFeaturesMainButtons.Add(new InputFeatureUsage<bool>(feature.name));
                            inputFeaturesMappedButtons.Add(new InputFeatureUsage<bool>(feature.name));
                        }

                        if (feature.type == typeof(float))
                        {
                            inputFeaturesMainAxis.Add(new InputFeatureUsage<float>(feature.name));
                            inputFeaturesMappedAxis.Add(new InputFeatureUsage<float>(feature.name));
                        }

                        if (feature.type == typeof(Vector2))
                        {
                            inputFeaturesMain2DAxis.Add(new InputFeatureUsage<Vector2>(feature.name));
                            inputFeaturesMapped2DAxis.Add(new InputFeatureUsage<Vector2>(feature.name));
                        }
                    }
                }
            }
        }

        // Update is called once per frame
        internal virtual void OnUpdate()
        {
            if (!gameObject.activeSelf) return;
            if (device == null) return;
            if (device.isValid == false) return;
            
            #region Input Bool

            for (int i = 0; i < inputFeaturesMappedButtons.Count; i++)
            {
                bool buttonPressed;
                if (device.TryGetFeatureValue(inputFeaturesMappedButtons[i], out buttonPressed))
                {
                    OnButton(inputFeaturesMainButtons[i], buttonPressed);
                }
            }

            #endregion

            #region Input Axis

            for (int i = 0; i < inputFeaturesMappedAxis.Count; i++)
            {
                float axisValue;
                if (device.TryGetFeatureValue(inputFeaturesMappedAxis[i], out axisValue))
                {
                    OnAxis(inputFeaturesMainAxis[i], axisValue);
                }
            }

            #endregion

            #region Input 2DAxis

            for (int i = 0; i < inputFeaturesMapped2DAxis.Count; i++)
            {
                Vector2 axis2DValue;
                if (device.TryGetFeatureValue(inputFeaturesMapped2DAxis[i], out axis2DValue))
                {
                    OnAxis2D(inputFeaturesMain2DAxis[i], axis2DValue);
                }
            }

            #endregion
        }

        protected virtual void OnDisable()
        {
            XR_InputHandler.OnUpdateEvent -= OnUpdate;
        }

        #endregion

        #region Controller Input Handling

        private void OnButton(InputFeatureUsage<bool> inputFeature, bool value)
        {
            if(inputFeature.Equals(CommonUsages.primaryButton))
            {
                if (primaryPress != value)
                {
                    primaryPress = value;
                    InputManager.InvokeButtonPress(primaryPress, HandType, CommonUsages.primaryButton.name.ToEnum<XR_Enum.FeatureUsageButton>());
                }
            }
            if (inputFeature.Equals(CommonUsages.primaryTouch))
            {
                if (primaryTouch != value)
                {
                    primaryTouch = value;
                    InputManager.InvokeButtonPress(primaryTouch, HandType, CommonUsages.primaryTouch.name.ToEnum<XR_Enum.FeatureUsageButton>());
                }
            }
            if (inputFeature.Equals(CommonUsages.secondaryButton))
            {
                if (secondaryPress != value)
                {
                    secondaryPress = value;
                    InputManager.InvokeButtonPress(secondaryPress, HandType, CommonUsages.secondaryButton.name.ToEnum<XR_Enum.FeatureUsageButton>());
                }
            }
            if (inputFeature.Equals(CommonUsages.secondaryTouch))
            {
                if (secondaryTouch != value)
                {
                    secondaryTouch = value;
                    InputManager.InvokeButtonPress(secondaryTouch, HandType, CommonUsages.secondaryTouch.name.ToEnum<XR_Enum.FeatureUsageButton>());
                }
            }
            if (inputFeature.Equals(CommonUsages.gripButton))
            {
                if (gripPressed != value)
                {
                    gripPressed = value;
                    InputManager.InvokeButtonPress(gripPressed, HandType, CommonUsages.gripButton.name.ToEnum<XR_Enum.FeatureUsageButton>());
                }
            }
            if (inputFeature.Equals(CommonUsages.triggerButton))
            {
                if (triggerPressed != value)
                {
                    triggerPressed = value;
                    InputManager.InvokeButtonPress(triggerPressed, HandType, CommonUsages.triggerButton.name.ToEnum<XR_Enum.FeatureUsageButton>());
                }
            }
            if (inputFeature.Equals(CommonUsages.menuButton))
            {
                if (menuPressed != value)
                {
                    menuPressed = value;
                    InputManager.InvokeButtonPress(menuPressed, HandType, CommonUsages.menuButton.name.ToEnum<XR_Enum.FeatureUsageButton>());
                }
            }
            if (inputFeature.Equals(CommonUsages.primary2DAxisClick))
            {
                if (primary2DAxisPress != value)
                {
                    primary2DAxisPress = value;
                    InputManager.InvokeButtonPress(primary2DAxisPress, HandType, CommonUsages.primary2DAxisClick.name.ToEnum<XR_Enum.FeatureUsageButton>());
                }
            }
            if (inputFeature.Equals(CommonUsages.primary2DAxisTouch))
            {
                if (primary2DAxisTouch != value)
                {
                    primary2DAxisTouch = value;
                    InputManager.InvokeButtonPress(primary2DAxisTouch, HandType, CommonUsages.primary2DAxisTouch.name.ToEnum<XR_Enum.FeatureUsageButton>());
                }
            }
            if (inputFeature.Equals(CommonUsages.thumbrest))
            {
                if (thumbrestPress != value)
                {
                    thumbrestPress = value;
                    InputManager.InvokeButtonPress(thumbrestPress, HandType, CommonUsages.thumbrest.name.ToEnum<XR_Enum.FeatureUsageButton>());
                }
            }
        }

        private void OnAxis(InputFeatureUsage<float> inputFeature, float value)
        {
            if(inputFeature.Equals(CommonUsages.grip))
                gripValue = value;
            if (inputFeature.Equals(CommonUsages.indexTouch))
                indexTouchValue = value;
            if (inputFeature.Equals(CommonUsages.indexFinger))
                indexFingerValue = value;
            if (inputFeature.Equals(CommonUsages.ringFinger))
                ringFingerValue = value;
            if (inputFeature.Equals(CommonUsages.pinkyFinger))
                pinkyFingerValue = value;
            if (inputFeature.Equals(CommonUsages.middleFinger))
                middleFingerValue = value;
            if (inputFeature.Equals(CommonUsages.thumbTouch))
                thumbTouchValue = value;
            if (inputFeature.Equals(CommonUsages.trigger))
                triggerValue = value;

            InputManager.InvokeAxisValue(value, HandType, inputFeature.name.ToEnum<XR_Enum.FeatureUsageAxis>());
        }

        private void OnAxis2D(InputFeatureUsage<Vector2> inputFeature, Vector2 value)
        {
            if (inputFeature.Equals(CommonUsages.primary2DAxis))
                primary2DAxisValue = value;
            if (inputFeature.Equals(CommonUsages.secondary2DAxis))
                secondary2DAxisValue = value;

            InputManager.InvokeAxis2DValue(value, HandType, inputFeature.name.ToEnum<XR_Enum.FeatureUsage2DAxis>());
        }

        #endregion

        #region Initialize

        internal virtual void InitInputDevice(InputDevice inputDevice)
        {
            device = inputDevice;

            gameObject.name = device.name;

            if (device.role == InputDeviceRole.LeftHanded)
            {
                HandType = XR_Enum.Hand.LEFT;
                transform.SetParent(SDKSetup.GetHand(XR_Enum.Hand.LEFT).transform);
            }
            else if (device.role == InputDeviceRole.RightHanded)
            {
                HandType = XR_Enum.Hand.RIGHT;
                transform.SetParent(SDKSetup.GetHand(XR_Enum.Hand.RIGHT).transform);
            }
            transform.ResetTransform();

            deviceType = SDKSetup.GetDevice();

            InitKeyBindingData();
            SpawnControllerModel();
        }

        private void SpawnControllerModel()
        {
            AssetsReference assetsReference = Resources.Load<AssetsReference>(ConstantVar.ResourcesPath.ASSETS_REFERENCE);
            ControllerModel _controllerModel = assetsReference.GetControllerMode(deviceType, HandType);
            if (_controllerModel != null)
            {
                controllerModel = (ControllerModel)Instantiate(_controllerModel);
                controllerModel.transform.SetParent(this.transform);
                controllerModel.transform.ResetTransform();
            }
        }

        private void InitKeyBindingData()
        {
            KeyBindingData keyBindingData = Resources.Load<KeyBindingData>(ConstantVar.ResourcesPath.XR_CONTROLLER_DATA);
            if (keyBindingData != null)
            {
                try
                {
                    keyMapped = keyBindingData.controllerList.Find((obj) => (obj.deviceType == (int)deviceType && (obj.hand == HandType))).keyMappingList.ToArray();
                }
                catch(NullReferenceException e)
                {
                    Debug.LogError("KEY MAPPING NOT FOUND IN DATA FOR \nDevice: " + deviceType + " InputDevice: " + device.name);
                }
            }
        }

        #endregion

    }
}
