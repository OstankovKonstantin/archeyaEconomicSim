using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject buildTile;
    public Factory factorySelected;
    public UpgradeButton upgradeButton;
    public UIManager uIManager;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collider = Physics2D.OverlapPoint(mousePosition);

            if (collider != null )
            {
                // Проверяем, является ли коллайдер плиткой для строительства
                if (collider.CompareTag("BuildTile"))
                {
                    if (collider.gameObject.GetComponent<BuildTile>().isEmpty)
                    {
                        buildTile = collider.gameObject;
                        gameManager.BuildTileClick(collider.gameObject.GetComponent<BuildTile>().id);
                    }
                    else
                    {
                        Debug.Log("Клетка занята!!!");
                    }
                }
                else if (collider.CompareTag("Factory"))
                {
                    factorySelected = collider.gameObject.GetComponent<Factory>();
                    Debug.Log(factorySelected.id + factorySelected.type);
                    gameManager.UpgradeTileClick(factorySelected.id, factorySelected);

                }
                else if (collider.gameObject == gameObject)
                {
                    gameManager.BuildTileClick(0);
                }
            }
        }
    }

    public void Build(Factory factory)
    {
        if (buildTile != null && buildTile.GetComponent<BuildTile>().isEmpty && gameManager.GetMoney() >= factory.cost)
        {
            Vector3 buildPoint = buildTile.transform.position;
            factory.id = buildTile.GetComponent<BuildTile>().id;
            buildTile.GetComponent<Collider2D>().enabled = false;
            buildTile.GetComponent<BuildTile>().Occupied();
            Instantiate(factory, buildPoint, Quaternion.identity);
            gameManager.SpendOnBuild(factory.cost);
            gameManager.AllDeactivate();
        }
        
    }

    public void Upgrade(Factory factory)
    {
       if (factorySelected != null && gameManager.GetMoney() >= factorySelected.upgradeCost && factorySelected.nextFactory != null)
       {
            Vector3 buildPoint = factorySelected.transform.position;
            factory.id = factorySelected.GetComponent<Factory>().id;
            Destroy(factorySelected.gameObject);
            Instantiate(factory, buildPoint, Quaternion.identity);
            gameManager.SpendOnBuild(factorySelected.upgradeCost);
            gameManager.AllDeactivate();
        }
    }

    public void Delete(Factory factory)
    {
        Debug.Log("Нажат точно");
        if (factorySelected != null )
        {
            int id = factorySelected.GetComponent<Factory>().id;
            gameManager.EarnOnSale(factorySelected.sell);
            Destroy(factorySelected.gameObject);
            GameObject[] buildTiles = GameObject.FindGameObjectsWithTag("BuildTile");
            GameObject returnTile = buildTiles.Where(e => e.GetComponent<BuildTile>().id == id).First();
            returnTile.GetComponent<BuildTile>().isEmpty = true;
            returnTile.GetComponent<Collider2D>().enabled = true;
            gameManager.AllDeactivate();
        }
    }

 /*   private IEnumerator InstantiateFactory(Factory factory, Vector3 buildPoint, int cost)
    {
        Instantiate(factory, buildPoint, Quaternion.identity);
        yield return null;

        gameManager.SpendOnBuild(factory.cost);
        gameManager.AllDeactivate();
    }
 */


}
