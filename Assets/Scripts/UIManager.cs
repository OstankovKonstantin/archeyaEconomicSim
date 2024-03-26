using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.U2D;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject UIBuilder;
    public GameObject UIUpgrader;
    public GameObject upgradeBtn;
    public GameObject deleteBtn;
    public GameObject UIResources;
    public Sprite[] buttonsSprites;

    private void Start()
    {

    }

    public void DeactivateBuilder()
    {
        UIBuilder.SetActive(false);
        UIResources.SetActive(true);
    }

    public void ActivateBuilder()
    {
        UIBuilder.SetActive(true);
        UIUpgrader.SetActive(false);
        UIResources.SetActive(false);
    }

    public void DeactivateUpgrader()
    {
        UIUpgrader.SetActive(false);
    }

    public void ActivateUpgrader()
    {
        UIUpgrader.SetActive(true);
        UIBuilder.SetActive(false);
        UIResources.SetActive(false);
    }

    public void DeactivateCounter()
    {
        UIResources.SetActive(false);
    }

    public void ActivateCounter()
    {
        UIResources.SetActive(true);
        UIBuilder.SetActive(false);
        UIUpgrader.SetActive(false);
    }

    public void UpgradeBtnSprite(Factory factory)
    {
        string factoryType;

        if (factory.nextFactory != null)
        {
            factoryType = factory.nextFactory.type;
        }
        else
        {
            factoryType = factory.type;
        }

        int spriteNumber = 0;

        switch (factoryType.Substring(0, 4))
        {
            case "tool":
                {
                    break;
                }
            case "med_":
                {
                    spriteNumber += 2;
                    break;
                }
            case "food":
                {
                    spriteNumber += 4;
                    break;
                }
            case "furn":
                {
                    spriteNumber += 6;
                    break;
                }
        }

        if (factoryType[factoryType.Length - 1].Equals('2'))
        {
            spriteNumber += 0;
        }
        else if (factoryType[factoryType.Length - 1].Equals('3'))
        {
            spriteNumber += 1;
        }

        upgradeBtn.GetComponent<Image>().sprite = buttonsSprites[spriteNumber];

        if (factory.nextFactory != null)
        {
            Factory send = factory.nextFactory;
            send.id = factory.id;
            Debug.Log("UI Answer " + send.type + " " + send.id);
            upgradeBtn.GetComponent<TooltipTrigger>().factory = factory;
            upgradeBtn.GetComponent<UpgradeButton>().factory = send;
        }
        else
        {
            Debug.Log("UI Answer Unfinined " + factory.type + " " + factory.id);
            upgradeBtn.GetComponent<TooltipTrigger>().factory = factory;
            upgradeBtn.GetComponent<UpgradeButton>().factory = null;
        }

    }



    public void DeleteBtnFactory(Factory factory)
    {
        deleteBtn.GetComponent<TooltipTrigger>().factory = factory;
        deleteBtn.GetComponent<DeleteButton>().factory = factory;
    }
}
