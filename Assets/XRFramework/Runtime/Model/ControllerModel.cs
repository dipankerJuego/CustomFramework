using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XRCustomFramework
{
    public abstract class ControllerModel : MonoBehaviour
    {
        public XR_Enum.Device device;
        public XR_Enum.Hand hand;
        //public int controllerId;
        //public string controllerName;
        //public Transform position;

        protected virtual void OnEnable()
        {
            InputManager.OnButtonPressed += XR_InputManager_OnButtonPressed;
            InputManager.OnAxis += XR_InputManager_OnAxis1D;
            InputManager.On2DAxis += XR_InputManager_OnAxis2D;
        }

        protected virtual void OnDisable()
        {
            InputManager.OnButtonPressed -= XR_InputManager_OnButtonPressed;
            InputManager.OnAxis -= XR_InputManager_OnAxis1D;
            InputManager.On2DAxis -= XR_InputManager_OnAxis2D;
        }

        protected abstract void XR_InputManager_OnButtonPressed(bool action, XR_Enum.Hand hand, XR_Enum.FeatureUsageButton usageButton);
        protected abstract void XR_InputManager_OnAxis1D(float value, XR_Enum.Hand hand, XR_Enum.FeatureUsageAxis usageAxis);
        protected abstract void XR_InputManager_OnAxis2D(Vector2 value, XR_Enum.Hand hand, XR_Enum.FeatureUsage2DAxis usage2DAxis);
    }
}
