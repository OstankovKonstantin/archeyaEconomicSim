using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraOnClick : MonoBehaviour
{
    public GameObject point;
    public UIManager uIManager;
    public bool toBase; //  оординаты, к которым должна переместитьс€ камера

    void OnMouseDown()
    {
        if (toBase)
        {
            point.transform.position = new Vector3(20, (float)0.5, -1);
        }
        else
        {
            point.transform.position = new Vector3(0, (float)0.5, -1);
            uIManager.ActivateCounter();
        }
        Vector3 cameraPosition = point.transform.position; // ѕолучаем текущее положение камеры
        Debug.Log("Camera position: " + cameraPosition);
    }

}
