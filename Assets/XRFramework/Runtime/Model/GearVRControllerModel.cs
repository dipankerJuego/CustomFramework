using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XRCustomFramework
{
    public class GearVRControllerModel : ControllerModel
    {
        public GameObject homeButton;
        public GameObject triggerButton;
        public GameObject backButton;
        public GameObject touchPadButton;

        private MeshRenderer meshRenderer;
        private Vector3 initialPos;

        public void Start()
        {
            //tempPos = triggerButton.transform.position;
        }

        protected override void XR_InputManager_OnAxis1D(float value, XR_Enum.Hand hand, XR_Enum.FeatureUsageAxis usageAxis)
        {
            int usageAxisInt = (int)usageAxis;
            if (usageAxisInt == (int)XR_Enum.GearVR_Axis.Trigger)
            {

            }
        }

        protected override void XR_InputManager_OnAxis2D(Vector2 value, XR_Enum.Hand hand, XR_Enum.FeatureUsage2DAxis usage2DAxis)
        {
            int usage2DAxisInt = (int)usage2DAxis;
            if (usage2DAxisInt == (int)XR_Enum.GearVR_2DAxis.Joystick)
            {

            }
        }

        protected override void XR_InputManager_OnButtonPressed(bool action, XR_Enum.Hand hand, XR_Enum.FeatureUsageButton usageButton)
        {
            int usageButtonInt = (int)usageButton;
            switch (usageButtonInt)
            {
                case (int)XR_Enum.GearVR_Button.TriggerPress:
                    {
                        if (action)
                        {
#if CUSTOM_LEAN_TWEEN
                            initialPos = triggerButton.transform.localPosition;
                            LeanTween.moveLocal(triggerButton, triggerButton.transform.localPosition - new Vector3(0, 0, 0.004f), 0.2f);
#endif
                        }
                        else
                        {
#if CUSTOM_LEAN_TWEEN
                            LeanTween.moveLocal(triggerButton, initialPos, 0.2f);
#endif
                        }
                    }
                    break;
                case (int)XR_Enum.GearVR_Button.TouchpadClick:
                    {
                        meshRenderer = touchPadButton.GetComponent<MeshRenderer>();
                        if (action)
                        {
                            meshRenderer.material.color = new Color32(0, 30, 255, 255);
                            Debug.Log("TouchpadClick");
                        }
                        else
                        {
                            meshRenderer.material.color = Color.white;
                        }
                    }
                    break;
                case (int)XR_Enum.GearVR_Button.TouchpadTouch:
                    {
                        meshRenderer = touchPadButton.GetComponent<MeshRenderer>();
                        if (action)
                        {
                            meshRenderer.material.color = new Color32(0, 118, 255, 255);
                            Debug.Log("TouchpadTouch");
                        }
                        else
                        {
                            meshRenderer.material.color = Color.white;
                        }
                    }
                    break;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
#if CUSTOM_LEAN_TWEEN
                initialPos = triggerButton.transform.localPosition;
                LeanTween.moveLocal(triggerButton, triggerButton.transform.localPosition - new Vector3(0, 0, 0.004f), 0.2f);
#endif
            }

            if (Input.GetKeyUp(KeyCode.T))
            {
#if CUSTOM_LEAN_TWEEN
                LeanTween.moveLocal(triggerButton, initialPos, 0.2f);
#endif
            }

        //    if (Input.GetKeyDown(KeyCode.H))
        //    {
        //        tempRenderer = homeButton.GetComponent<MeshRenderer>();
        //        tempRenderer.material.color = Color.red;
        //    }

        //    if (Input.GetKeyDown(KeyCode.B))
        //    {
        //        tempRenderer = backButton.GetComponent<MeshRenderer>();
        //        tempRenderer.material.color = Color.red;
        //    }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                meshRenderer = touchPadButton.GetComponent<MeshRenderer>();
                meshRenderer.material.color = Color.red;
            }

            if (Input.GetKeyUp(KeyCode.H) || Input.GetKeyUp(KeyCode.B) || Input.GetKeyUp(KeyCode.Q))
                meshRenderer.material.color = Color.white;
        }
    }
}
