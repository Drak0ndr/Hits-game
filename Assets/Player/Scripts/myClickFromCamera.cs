using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myClickFromCamera : MonoBehaviour
{
    public Camera camera;

    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        // рисуем луч для отладки (виден только на сцене)
        float drawRayLength = 50.0f;
        Debug.DrawRay(ray.origin, ray.direction * drawRayLength, color: Color.green);

        // при щелчке мышкой
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // получаем место попадания щелчка в 3D пространстве
                Vector3 pos = hit.point;
                // создаём сферу
                GameObject s = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                // задаём положение сферы
                s.transform.position = pos;
            }
        }
    }
}
