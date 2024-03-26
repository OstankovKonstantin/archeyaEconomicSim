using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButton : MonoBehaviour
{
    public Factory factory;
    public BuildController buildController;
    public void OnButtonPress()
    {
        
        buildController.Build(factory);

    }

}
