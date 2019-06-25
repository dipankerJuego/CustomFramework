using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazePointer : MonoBehaviour
{
    private Transform VRCamera;

    [SerializeField] private Canvas canvas;

    private void Start()
    {
        VRCamera = Camera.main.transform;
        canvas.worldCamera = Camera.main;
    }

    private void Update()
    {
        this.gameObject.transform.position = VRCamera.position;
    }
}
