using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XRCustomFramework
{
    public class DaydreamControllerModel : ControllerModel
    {
        protected override void XR_InputManager_OnAxis1D(float value, XR_Enum.Hand hand, XR_Enum.FeatureUsageAxis usageAxis)
        {
            int usageAxisInt = (int)usageAxis;
            switch (usageAxisInt)
            {
                case (int)XR_Enum.Daydream_Axis.Trigger:
                    {
                    }
                    break;
                case (int)XR_Enum.Daydream_Axis.Grip:
                    {
                    }
                    break;
            }
        }

        protected override void XR_InputManager_OnAxis2D(Vector2 value, XR_Enum.Hand hand, XR_Enum.FeatureUsage2DAxis usage2DAxis)
        {
            int usage2DAxisInt = (int)usage2DAxis;
            switch (usage2DAxisInt)
            {
                case (int)XR_Enum.Daydream_2DAxis.Touchpad:
                    {
                    }
                    break;
            }
        }

        protected override void XR_InputManager_OnButtonPressed(bool action, XR_Enum.Hand hand, XR_Enum.FeatureUsageButton usageButton)
        {
            int usageButtonInt = (int)usageButton;
            switch (usageButtonInt)
            {
                case (int)XR_Enum.Daydream_Button.App:
                    {
                    }
                    break;
                case (int)XR_Enum.Daydream_Button.GripPress:
                    {
                    }
                    break;
                case (int)XR_Enum.Daydream_Button.TriggerPress:
                    {
                    }
                    break;
                case (int)XR_Enum.Daydream_Button.TouchpadClick:
                    {
                    }
                    break;
                case (int)XR_Enum.Daydream_Button.TouchpadTouch:
                    {
                    }
                    break;
            }
        }
    }
}
