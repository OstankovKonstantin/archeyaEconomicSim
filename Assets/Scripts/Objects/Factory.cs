using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Factory: MonoBehaviour
{
    public int id;
    public string type;
    public string factoryName;
    public int cost;
    public int upgradeCost=0;
    public Factory nextFactory;

    public List<string> resourcesConsumptionKeys;
    public List<int> resourcesConsumptionValues;

    public List<string> productOutputKeys;
    public List<int> productOutputValues;

    public int personal;
    public int expensesPerTurn;
    public int sell;

    public void Start()
    {
        GameManager.Instance.RegisterFactory(this);
    }

    public void OnDestroy()
    {
        GameManager.Instance.DeleteFactory(this);
    }

}


