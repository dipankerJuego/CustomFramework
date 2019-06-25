using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XRCustomFramework
{
    public class InputManager
    {
        private static InputManager m_instance;

        public delegate void ButtonPressed(bool action, XR_Enum.Hand hand, XR_Enum.FeatureUsageButton usageButton);
        public delegate void Axis1DValue(float value, XR_Enum.Hand hand, XR_Enum.FeatureUsageAxis usageAxis);
        public delegate void Axis2DValue(Vector2 value, XR_Enum.Hand hand, XR_Enum.FeatureUsage2DAxis usage2DAxis);

        public static event ButtonPressed OnButtonPressed;
        public static event Axis1DValue OnAxis;
        public static event Axis2DValue On2DAxis;

        public static void InvokeButtonPress(bool action, XR_Enum.Hand hand, XR_Enum.FeatureUsageButton usageButton)
        {
            //Debug.Log(hand.ToString() + " InvokeButtonPress " + action + " UsageType: " + usageButton);
            OnButtonPressed?.Invoke(action, hand, usageButton);
        }

        public static void InvokeAxisValue(float value, XR_Enum.Hand hand, XR_Enum.FeatureUsageAxis usageAxis)
        {
           // Debug.Log(hand.ToString() + " InvokeAxisValue " + value + " UsageType: " + usageAxis);
            OnAxis?.Invoke(value, hand, usageAxis);
        }

        public static void InvokeAxis2DValue(Vector2 value, XR_Enum.Hand hand, XR_Enum.FeatureUsage2DAxis usage2DAxis)
        {
            //Debug.Log(hand.ToString() + " Invoke2DAxisValue " + value + " UsageType: " + usage2DAxis);
            On2DAxis?.Invoke(value, hand, usage2DAxis);
        }

        public static Object GetHandController(XR_Enum.Hand hand)
        {
            switch(SDKSetup.GetSDKType)
            {
                case XR_Enum.SDKType.XR:
                    return SDKSetup.Instance.XRManager.InputManager.GetHandController(hand) as Object;
                //case XR_Enum.SDKType.WaveVR:
                default:
                    return null;
            }
        }

        public static bool IsHanded()
        {
            if (GetHandController(XR_Enum.Hand.RIGHT) != null || GetHandController(XR_Enum.Hand.LEFT) != null)
                return true;

            return false;
        }

        public static XR_Enum.Hand GetPreferedHand()
        {
            switch (SDKSetup.GetSDKType)
            {
                case XR_Enum.SDKType.XR:
                    return SDKSetup.Instance.XRManager.InputManager.GetPreferedHand();
                //case XR_Enum.SDKType.WaveVR:
                default:
                    return XR_Enum.Hand.RIGHT;
            }
        }

        public static bool GetButon(XR_Enum.FeatureUsageButton featureUsage, XR_Enum.Hand hand)
        {
            switch (SDKSetup.GetSDKType)
            {
                case XR_Enum.SDKType.XR: return SDKSetup.Instance.XRManager.InputManager.GetButon(featureUsage, hand);
                //case XR_Enum.SDKType.WaveVR:
                default: return false;
            }
        }

        public static float GetAxis(XR_Enum.FeatureUsageAxis featureUsage, XR_Enum.Hand hand)
        {
            switch (SDKSetup.GetSDKType)
            {
                case XR_Enum.SDKType.XR: return SDKSetup.Instance.XRManager.InputManager.GetAxis(featureUsage, hand);
                //case XR_Enum.SDKType.WaveVR:
                default: return 0;
            }
        }

        public static Vector2 GetAxis2D(XR_Enum.FeatureUsage2DAxis featureUsage, XR_Enum.Hand hand)
        {
            switch (SDKSetup.GetSDKType)
            {
                case XR_Enum.SDKType.XR: return SDKSetup.Instance.XRManager.InputManager.GetAxis2D(featureUsage, hand);
                //case XR_Enum.SDKType.WaveVR:
                default: return Vector2.zero;
            }
        }
    }
}
