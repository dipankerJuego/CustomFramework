using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XRCustomFramework
{
    public class ControllerRaycaster : RaycastController
    {
        [SerializeField] Color c1 = Color.yellow;
        [SerializeField] Color c2 = Color.red;
        [SerializeField] int lengthOfLineRenderer = 20;

        private bool rayState;
        private GameObject controller;

        private void Start()
        {
            rayState = true;

            controller = SDKSetup.GetHand();

            LineRenderer lineRenderer = controller.AddComponent<LineRenderer>();
            lineRenderer.useWorldSpace = false;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.widthMultiplier = 0.02f;
            lineRenderer.positionCount = lengthOfLineRenderer;

            float alpha = 1.0f;
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );
            lineRenderer.colorGradient = gradient;
        }

        internal void EnableRay(bool value)
        {
            rayState = value;
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (rayState)
                {
                    rayState = false;
                }
                else
                    rayState = true;
            }
#endif
            RaycastHit hit;
            LayerMask layerMask = selectable;

            if (rayState)
            {
                Debug.DrawRay(controller.transform.position, controller.transform.forward * 2500, Color.red);

                LineRenderer lineRenderer = controller.transform.GetComponent<LineRenderer>();
                lineRenderer.enabled = true;
                var t = Time.time;
                for (int i = 0; i < lengthOfLineRenderer; i++)
                {
                    lineRenderer.SetPosition(i, Vector3.forward * i * 10);
                }

                if (Physics.Raycast(controller.transform.position, controller.transform.TransformDirection(Vector3.forward), out hit, 2500f, layerMask))
                {
                    //Hit action
                    //TODO: COntrollet Inreration
                }
                else
                {
                    //TODO: COntrollet Inreration
                }
            }
            else
            {
                LineRenderer lineRenderer = controller.transform.GetComponent<LineRenderer>();
                lineRenderer.enabled = false;
            }



        }
    }
}
