using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteButton : MonoBehaviour
{
    public Factory factory;
    public BuildController buildController;
    public void OnButtonPress()
    {
        Debug.Log("Received " + factory.id + factory.type);
        buildController.Delete(factory);
    }
}
