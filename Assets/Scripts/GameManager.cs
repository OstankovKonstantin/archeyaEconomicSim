using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    Color warning = new Color(200f / 255f, 50f / 255f, 50f / 255f);
    Color normal = new Color(50f / 255f, 170f / 255f, 50f / 255f);
    private int moneyPerDay = 0;
    private int needPersonal = 0;
    private int personalNow = 0;
    private int salary = 0;
    private int taxes = 10;
    private int selectedTile = 0;


    public static GameManager Instance;
    public UIManager uIManager;
    public ResourceManager resourceManager;

    private List<Factory> factories = new List<Factory>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterFactory(Factory factory)
    {
        int index = 0;
        factories.Add(factory);
        foreach (string resourse in factory.resourcesConsumptionKeys)
        {
            supplyConsume[resourse] -= factory.resourcesConsumptionValues[index];
            index++;
        }

        index = 0;
        foreach (string product in factory.productOutputKeys)
        {
            production[product] += factory.productOutputValues[index];
            index++;
        }

        moneyPerDay -= factory.expensesPerTurn;
        needPersonal += factory.personal;

        PersonalUpdate();
        MoneyCounterUpdate();
        resourceManager.CounterAndChangeUpdate();

        Debug.Log("Money: " + moneyPerDay);

        foreach (KeyValuePair<string, int> a in supplyConsume)
        {
            Debug.Log("Consume " + a.Key + ": " + a.Value);
        }

        foreach (KeyValuePair<string, int> a in production)
        {
            Debug.Log("Produce " + a.Key + ": " + a.Value);
        }

    }

    public void DeleteFactory(Factory factory)
    {
        factories.Remove(factory);
        int index = 0;
        foreach (string resourse in factory.resourcesConsumptionKeys)
        {
            supplyConsume[resourse] += factory.resourcesConsumptionValues[index];
            index++;
        }

        index = 0;
        foreach (string product in factory.productOutputKeys)
        {
            production[product] -= factory.productOutputValues[index];
            index++;
        }

        moneyPerDay += factory.expensesPerTurn;
        needPersonal -= factory.personal;

        PersonalUpdate();
        Debug.Log("Salary: " + salary);
        MoneyCounterUpdate();
        resourceManager.CounterAndChangeUpdate();

        Debug.Log("Money: " + moneyPerDay);

        foreach (KeyValuePair<string, int> a in supplyConsume)
        {
            Debug.Log("Consume " + a.Key + ": " + a.Value);
        }

        foreach (KeyValuePair<string, int> a in production)
        {
            Debug.Log("Produce " + a.Key + ": " + a.Value);
        }
    }

    private int dayCount = 1;
    private int moneyCount = 1500;
    public Text dayPanel;
    public Text moneyPanel;
    public Text moneyChange;
    public Text personalCounter;

    internal Dictionary<string, int> supplyStock = new Dictionary<string, int>{
        { "Wood", 0 },
        { "Metal", 0 },
        { "Cloth", 0 },
        { "Nature", 0 }
    };

    internal Dictionary<string, int> supplyGet = new Dictionary<string, int>{
        { "Wood", 2 },
        { "Metal", 2 },
        { "Cloth", 0 },
        { "Nature", 1 }
    };

    internal Dictionary<string, int> supplyConsume = new Dictionary<string, int>{
        { "Wood", 0 },
        { "Metal", 0 },
        { "Cloth", 0 },
        { "Nature", 0 }
    };

    internal Dictionary<string, int> warehouseStock = new Dictionary<string, int>{
        { "Tool", 0 },
        { "Furn", 0 },
        { "Food", 0 },
        { "Med", 0 }
    }; // Количество товара на складе

    internal Dictionary<string, int> production = new Dictionary<string, int>{
        { "Tool", 0 },
        { "Furn", 0 },
        { "Food", 0 },
        { "Med", 0 }
    }; // Производство

    public void Start()
    {
        dayPanel.text = ("Day: " + dayCount);
        moneyPanel.text = ("Money: " + moneyCount);
        PersonalUpdate();
        MoneyCounterUpdate();
        resourceManager.CounterAndChangeUpdate();
    }

    public int GetMoney()
    {
        return moneyCount;
    }

    public void EndTurn()
    {
        dayCount++;
        moneyCount = moneyCount - salary + moneyPerDay - taxes;
        dayPanel.text = ("Day: " + dayCount);
        moneyPanel.text = ("Money: " + moneyCount);

        foreach (KeyValuePair<string, int> pair in supplyGet)
        {
            supplyStock[pair.Key] += pair.Value;

        }

        FactoryWork();
        resourceManager.CounterAndChangeUpdate();
    }

   

    public void PersonalUpdate()
    {

        personalCounter.text = personalNow.ToString() + "/" + needPersonal.ToString();
        if (personalNow < needPersonal)
        {
            personalCounter.color = warning;
        }
        else
        {
            personalCounter.color = normal;
        }

        int unstability = needPersonal - personalNow;
            salary = unstability * 25;
    }

    public void MoneyCounterUpdate()
    {
        moneyChange.text = (moneyPerDay - salary - taxes).ToString();
        if ((moneyPerDay - salary - taxes) < 0)
        {
            moneyChange.color = warning;
        }
        else if ((moneyPerDay - salary - taxes) > 0)
        {

            moneyChange.color = normal;

        }
        else {
            moneyChange.color = Color.black;
        }
    }

    public void BuildTileClick(int id)
    {
        if (id > 0)
        {
            selectedTile = id;
            Debug.Log("Это плитка для строительства! " + selectedTile);
            uIManager.ActivateBuilder();
            uIManager.DeactivateUpgrader();
        }
        else
        {
            selectedTile = id;
            Debug.Log("Это плитка не для строительства! " + selectedTile);
            uIManager.DeactivateBuilder();
        }

    }

    public void SpendOnBuild(int spended)
    {
        moneyCount = moneyCount - spended;
        moneyPanel.text = ("Money: " + moneyCount);
    }

    public void UpgradeTileClick(int id, Factory factory)
    {
        Debug.Log("НАЖАТО! " + selectedTile);
        if (id > 0)
        {
            selectedTile = id;
            Debug.Log("Это завод для улучшения! " + selectedTile);
            uIManager.DeactivateBuilder();
            uIManager.ActivateUpgrader();
            uIManager.UpgradeBtnSprite(factory);
            uIManager.DeleteBtnFactory(factory);
            Debug.Log("Now: " + factory.type);
            if (factory.nextFactory != null)
            {
                Debug.Log("Next: " + factory.nextFactory.type);
            }
        }
        else
        {
            uIManager.DeactivateUpgrader();
        }

    }

    public void AllDeactivate()
    {
        uIManager.DeactivateBuilder();
        uIManager.DeactivateUpgrader();

    }

    public void EarnOnSale(int earned)
    {
        moneyCount = moneyCount + earned;
        moneyPanel.text = ("Money: " + moneyCount);
    }

   public void FactoryWork()
   {
        foreach (Factory factory in factories.OrderBy(obj => obj.id))
        {
            int workedOut = 0;
           
            while (workedOut != factory.productOutputValues.First() 
                && supplyStock[factory.resourcesConsumptionKeys[0]] > 0 
                && supplyStock[factory.resourcesConsumptionKeys[1]] > 0)
            {
                Debug.Log("РАБОТА " + workedOut);
                supplyStock[factory.resourcesConsumptionKeys[0]] -= 1;
                supplyStock[factory.resourcesConsumptionKeys[1]] -= 1;
                warehouseStock[factory.productOutputKeys[0]] += 1;
                workedOut++;
            }
        }
    }

}
