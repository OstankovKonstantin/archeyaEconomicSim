using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class ResourceManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text[] resourceCounters;
    public Text[] resourceChanges;

    public Text[] productCounters;
    public Text[] productChanges;

    Color warning = new Color(200f / 255f, 50f / 255f, 50f / 255f);
    Color normal = new Color(50f / 255f, 100f / 255f, 50f / 255f);

    public void CounterAndChangeUpdate()
    {
        //Ресурсы
        foreach (KeyValuePair<string, int> a in GameManager.Instance.supplyStock)
        {
            var counter = resourceCounters.Where(e => e.gameObject.name == a.Key + "Count").First();
            counter.text = a.Value.ToString();

            if (a.Value < 0)
            {
                counter.color = warning;
            }
            else if (a.Value > 0)
            {
                counter.color = normal;
            }
            else
            {
                counter.color = Color.black;
            }
        }

        foreach (KeyValuePair<string, int> a in GameManager.Instance.supplyConsume)
        {
            var change = resourceChanges.Where(e => e.gameObject.name == a.Key + "Change").First();
            change.text = (GameManager.Instance.supplyGet[a.Key] + a.Value).ToString();

            if ((GameManager.Instance.supplyGet[a.Key] + a.Value) < 0)
            {
                change.color = warning;
            }
            else if ((GameManager.Instance.supplyGet[a.Key] + a.Value) > 0)
            {
                change.color = normal;
            }
            else
            {
                change.color = Color.black;
            }
        }

        //Продукция
        foreach (KeyValuePair<string, int> a in GameManager.Instance.warehouseStock)
        {
            var counter = productCounters.Where(e => e.gameObject.name == a.Key + "Count").First();
            counter.text = a.Value.ToString();

            if (a.Value < 0)
            {
                counter.color = warning;
            }
            else if (a.Value > 0)
            {
                counter.color = normal;
            }
            else
            {
                counter.color = Color.black;
            }
        }

        foreach (KeyValuePair<string, int> a in GameManager.Instance.production)
        {
            var change = productChanges.Where(e => e.gameObject.name == a.Key + "Change").First();
            change.text = a.Value.ToString();

            if (a.Value < 0)
            {
                change.color = warning;
            }
            else if (a.Value > 0)
            {
                change.color = normal;
            }
            else
            {
                change.color = Color.black;
            }
        }
    }
}
