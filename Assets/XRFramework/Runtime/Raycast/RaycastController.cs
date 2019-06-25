using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace XRCustomFramework
{
    public class RaycastController :
#if CUSTOM_SCREEN_FACTORY
        ScreenBase
#else
        MonoBehaviour
#endif
    {
        public LayerMask interactable;
        public LayerMask selectable;

        private GameObject cameraRaycaster;
        private GameObject controllerRaycaster;

        private static RaycastController _instance;

        public static RaycastController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<RaycastController>();
                }

                return _instance;

            }
        }

        public enum UiElementType
        {
            button,
            inputfield,
            checkbox,
            slider
        }

        private void Start()
        {
            cameraRaycaster = GetComponentInChildren<CameraRaycaster>().gameObject;
            controllerRaycaster = GetComponentInChildren<ControllerRaycaster>().gameObject;

            if (SDKSetup.GetHand() == null)
            {
                //Controller raycast
                controllerRaycaster.SetActive(true);
                cameraRaycaster.SetActive(false);
            }
            else
            {
                //Camera raycast
                cameraRaycaster.SetActive(true);
                controllerRaycaster.SetActive(false);
            }
        }

    }
}
