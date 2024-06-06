using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();

    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            

            float middleW = _camera.pixelWidth / 2;
            float middleH = _camera.pixelHeight / 2;
            Vector3 point = new Vector3(middleW, middleH, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // создание новой сферы
                GameObject s = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                // задание положения сферы
                s.transform.position = hit.point;
            }
        }

    }

    void OnGUI()
    {
        int n = 12;
        float xxx = _camera.pixelWidth / 2 - n / 2;
        float yyy = _camera.pixelHeight / 2 - n / 2;
        GUI.Label(new Rect(xxx, yyy, n, n), "+");
    }
}
