using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace XRCustomFramework 
{
    public class OculusControllerModel : ControllerModel
    {
        [SerializeField] private GameObject thumbStick;

        [SerializeField] private GameObject tiggerButton;

        [SerializeField] private GameObject buttonXA;
        [SerializeField] private GameObject buttonYB;

        [SerializeField] private GameObject menuButton;

        private SkinnedMeshRenderer controllerRenderer;

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


        private void Update()
        {
        //    Vector2 axis = Vector2.zero;

        //    axis.x = Input.GetAxis("Horizontal");
        //    axis.y = Input.GetAxis("Vertical");

        //    if (Input.GetKeyDown(KeyCode.X))
        //    {
        //        controllerRenderer = buttonX.GetComponent<SkinnedMeshRenderer>();
        //        controllerRenderer.material.color = Color.red;
        //    }

        //    if (Input.GetKeyDown(KeyCode.Y))
        //    {
        //        controllerRenderer = buttonY.GetComponent<SkinnedMeshRenderer>();
        //        controllerRenderer.material.color = Color.red;
        //    }
        //    if (Input.GetKeyDown(KeyCode.A))
        //    {
        //        controllerRenderer = buttonA.GetComponent<SkinnedMeshRenderer>();
        //        controllerRenderer.material.color = Color.red;
        //    }
        //    if (Input.GetKeyDown(KeyCode.B))
        //    {
        //        controllerRenderer = buttonB.GetComponent<SkinnedMeshRenderer>();
        //        controllerRenderer.material.color = Color.red;
        //    }

        //    if (Input.GetKeyDown(KeyCode.M))
        //    {
        //        controllerRenderer = menuButton.GetComponent<SkinnedMeshRenderer>();
        //        controllerRenderer.material.color = Color.red;
        //    }

        //    if (Input.GetKeyDown(KeyCode.H))
        //    {
        //        controllerRenderer = homeButton.GetComponent<SkinnedMeshRenderer>();
        //        controllerRenderer.material.color = Color.red;
        //    }

        //    if (Input.GetKeyUp(KeyCode.X) || Input.GetKeyUp(KeyCode.Y) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.B) || Input.GetKeyUp(KeyCode.M) || Input.GetKeyUp(KeyCode.H))
        //        controllerRenderer.material.color = Color.white;
        }
    }
}
