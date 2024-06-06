using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myClickFromCamera : MonoBehaviour
{
    public Camera camera;

    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        // ������ ��� ��� ������� (����� ������ �� �����)
        float drawRayLength = 50.0f;
        Debug.DrawRay(ray.origin, ray.direction * drawRayLength, color: Color.green);

        // ��� ������ ������
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // �������� ����� ��������� ������ � 3D ������������
                Vector3 pos = hit.point;
                // ������ �����
                GameObject s = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                // ����� ��������� �����
                s.transform.position = pos;
            }
        }
    }
}
