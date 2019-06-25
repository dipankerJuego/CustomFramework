using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XRCustomFramework
{
    public class OculusGoControllerModel : ControllerModel
    {
        protected override void XR_InputManager_OnAxis1D(float value, XR_Enum.Hand hand, XR_Enum.FeatureUsageAxis usageAxis)
        {
            int usageAxisInt = (int)usageAxis;
            switch (usageAxisInt)
            {
                case (int)XR_Enum.Oculus_Axis.Trigger:
                    {
                    }
                    break;
                case (int)XR_Enum.Oculus_Axis.Grip:
                    {
                    }
                    break;
                case (int)XR_Enum.Oculus_Axis.IndexNearTouch:
                    {
                    }
                    break;
                case (int)XR_Enum.Oculus_Axis.ThumbNearTouch:
                    {
                    }
                    break;
                case (int)XR_Enum.Oculus_Axis.CombinedTrigger:
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
                case (int)XR_Enum.Oculus_2DAxis.Joystick:
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
                case (int)XR_Enum.Oculus_Button.XA_Press:
                    {
                    }
                    break;
                case (int)XR_Enum.Oculus_Button.XA_Touch:
                    {
                    }
                    break;
                case (int)XR_Enum.Oculus_Button.YB_Press:
                    {
                    }
                    break;
                case (int)XR_Enum.Oculus_Button.YB_Touch:
                    {
                    }
                    break;
                case (int)XR_Enum.Oculus_Button.GripPress:
                    {
                    }
                    break;
                case (int)XR_Enum.Oculus_Button.IndexTouch:
                    {
                    }
                    break;
                case (int)XR_Enum.Oculus_Button.Start:
                    {
                    }
                    break;
                case (int)XR_Enum.Oculus_Button.ThumbRestTouch:
                    {
                    }
                    break;
                case (int)XR_Enum.Oculus_Button.ThumbstickClick:
                    {
                    }
                    break;
                case (int)XR_Enum.Oculus_Button.ThumbstickTouch:
                    {
                    }
                    break;
            }
        }

    }
}
