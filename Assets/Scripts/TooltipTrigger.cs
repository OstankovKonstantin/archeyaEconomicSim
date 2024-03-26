using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Factory factory;
    public Text buildName;
    public Text buildCost;
    public Text buildPersonal;
    public Text buildConsumeA;
    public Text buildConsumeB;
    public Text buildConsumeMoney;
    public Text buildProduce;
    public List<Image> icons;
    public List<Text> elements;
    public List<Sprite> iconsSprites;

    private void Start()
    {
        icons.ForEach(icon => icon.preserveAspect= true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // При наведении мышкой на кнопку показываем текст подсказки
        
        Debug.Log(factory.type);
        Debug.Log(factory.cost);
        Debug.Log(factory.personal);
        buildName.gameObject.SetActive(true);
        buildCost.gameObject.SetActive(true);
        if (gameObject.GetComponent<DeleteButton>() == null)
        {
            buildPersonal.gameObject.SetActive(true);
            buildConsumeA.gameObject.SetActive(true);
            buildConsumeB.gameObject.SetActive(true);
            buildConsumeMoney.gameObject.SetActive(true);
            buildProduce.gameObject.SetActive(true);
            elements.ForEach(e => e.gameObject.SetActive(true));
            icons.ForEach(e => e.gameObject.SetActive(true));
        }

        buildName.text = factory.factoryName;

        if (gameObject.GetComponent<UpgradeButton>() == null && gameObject.GetComponent<DeleteButton>() == null) 
        {
            Debug.Log("!!!BUILD!!!");
            buildCost.text = "Cost: " + factory.cost.ToString();
            buildPersonal.text = "Personal: " + factory.personal.ToString();
            buildConsumeA.text = factory.resourcesConsumptionValues.First().ToString();
            buildConsumeB.text = factory.resourcesConsumptionValues.ElementAt(1).ToString();
            buildConsumeMoney.text = factory.expensesPerTurn.ToString();
            buildProduce.text = factory.productOutputValues.First().ToString();
        }
        else if (gameObject.GetComponent<DeleteButton>() == null)
        {
            if (gameObject.GetComponent<UpgradeButton>().factory != null)
            {
                Debug.Log("!!!UPGRADE!!!");
                buildCost.text = "Cost: " + factory.upgradeCost.ToString();
                buildPersonal.text = "Personal: " + factory.nextFactory.personal.ToString();
                buildConsumeA.text = factory.nextFactory.resourcesConsumptionValues.First().ToString();
                buildConsumeB.text = factory.nextFactory.resourcesConsumptionValues.ElementAt(1).ToString();
                buildConsumeMoney.text = factory.nextFactory.expensesPerTurn.ToString();
                buildProduce.text = factory.nextFactory.productOutputValues.First().ToString();
            }
            else
            {
                Debug.Log("!!!MAX!!!");
                buildCost.text = "Max Lvl";
            }
        }
        else
        {
            buildCost.text = "Sell: " + factory.sell.ToString();
        }

        switch (factory.resourcesConsumptionKeys.First())
        {
            case "Wood":
                {
                    icons.First().sprite = iconsSprites.Where(e => e.name == "Wood").First();
                    break;
                }
            case "Metal":
                {
                    icons.First().sprite = iconsSprites.Where(e => e.name == "Metal").First();
                    break;
                }
            case "Cloth":
                {
                    icons.First().sprite = iconsSprites.Where(e => e.name == "Cloth").First();
                    break;
                }
            case "Nature":
                {
                    icons.First().sprite = iconsSprites.Where(e => e.name == "Nature").First();
                    break;
                }
        }

        switch (factory.resourcesConsumptionKeys.ElementAt(1))
        {
            case "Wood":
                {
                    icons.ElementAt(1).sprite = iconsSprites.Where(e => e.name == "Wood").First();
                    break;
                }
            case "Metal":
                {
                    icons.ElementAt(1).sprite = iconsSprites.Where(e => e.name == "Metal").First();
                    break;
                }
            case "Cloth":
                {
                    icons.ElementAt(1).sprite = iconsSprites.Where(e => e.name == "Cloth").First();
                    break;
                }
            case "Nature":
                {
                    icons.ElementAt(1).sprite = iconsSprites.Where(e => e.name == "Nature").First();
                    break;
                }
        }

        switch (factory.productOutputKeys.First())
        {
            case "Tool":
                {
                    icons.ElementAt(3).sprite = iconsSprites.Where(e => e.name == "Tool").First();
                    break;
                }
            case "Med":
                {
                    icons.ElementAt(3).sprite = iconsSprites.Where(e => e.name == "Med").First();
                    break;
                }
            case "Furn":
                {
                    icons.ElementAt(3).sprite = iconsSprites.Where(e => e.name == "Furn").First();
                    break;
                }
            case "Food":
                {
                    icons.ElementAt(3).sprite = iconsSprites.Where(e => e.name == "Food").First();
                    break;
                }
        }


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // При уходе мышки с кнопки скрываем текст подсказки
        buildName.gameObject.SetActive(false);
        buildCost.gameObject.SetActive(false);
        buildPersonal.gameObject.SetActive(false);
        buildConsumeA.gameObject.SetActive(false);
        buildConsumeB.gameObject.SetActive(false);
        buildConsumeMoney.gameObject.SetActive(false);
        buildProduce.gameObject.SetActive(false);
        elements.ForEach(e => e.gameObject.SetActive(false));
        icons.ForEach(e => e.gameObject.SetActive(false));
    }
}