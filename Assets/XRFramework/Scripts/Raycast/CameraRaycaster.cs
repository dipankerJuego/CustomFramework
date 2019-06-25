using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR;

namespace XRCustomFramework
{
    public class CameraRaycaster : RaycastController
    {
        [SerializeField] private Camera mainCamera;

        [SerializeField] Transform VRCamera;
        [SerializeField] GameObject gazeCursor;
        [SerializeField] Image loadingCursor;

        private GameObject hitObject;

        //public delegate void PointerEnterDelegate(string uiElement);
        //public PointerEnterDelegate OnPointerEnter;

        private void Start()
        {
            //Attach camera component
            mainCamera = Camera.main;
            VRCamera = mainCamera.transform;

            gazeCursor = transform.GetChild(0).transform.GetComponent<Canvas>().gameObject;
            loadingCursor = transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>();
        }

        private void Update()
        {
            LayerMask layerMask = interactable;

            RaycastHit hit;

            Debug.DrawRay(VRCamera.position, VRCamera.forward * 2500, Color.green);

            if (Physics.Raycast(VRCamera.position, VRCamera.TransformDirection(Vector3.forward), out hit, 2500f, layerMask))
            {
                //Debug.Log(hit.collider);
                if (!gazeCursor.activeSelf)
                    gazeCursor.SetActive(true);

                layerMask = selectable;
                if (Physics.Raycast(VRCamera.position, VRCamera.TransformDirection(Vector3.forward), out hit, 2500f, layerMask))
                {
                    //Debug.Log("<color=green> called start coroutin</color>");

                    hitObject = hit.collider.gameObject;
                    StartCoroutine(ObjectSelector());

                    //OnCursorIn();

                    layerMask = interactable;
                }
                else
                {
                    OnCursorOut();
                }
            }
            else
            {   
                if (gazeCursor.activeSelf)
                    gazeCursor.SetActive(false);
            }
        }

        IEnumerator ObjectSelector()
        {
            while ((int)loadingCursor.fillAmount != 1)
            {
                loadingCursor.fillAmount += 0.0002f;
                if ((int)loadingCursor.fillAmount == 1)
                {
#if CUSTOM_SCREEN_FACTORY
                    // Hit action
                    if (PopupFactory.Instance.CurrentPopup != null)
                        PopupFactory.Instance.OnRayEnter(hitObject);
                    else
                        ScreenFactory.Instance.OnRayEnter(hitObject);
#endif
                    //Debug.Log("<color=red> Hit UI tag </color>" + hitObject.tag);

                }
                yield return new WaitForEndOfFrame();
            }
        }

        private void OnCursorOut()
        {
            //Debug.Log("<color=red> called stop coroutin</color>");
            StopCoroutine("ObjectSelector");
            //StopAllCoroutines();
            loadingCursor.fillAmount = 0;
        }

    }
}
