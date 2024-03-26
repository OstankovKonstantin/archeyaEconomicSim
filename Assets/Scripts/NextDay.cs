using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextDay : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gameManager;

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(EndTurn);
    }

    void EndTurn()
    {
        gameManager.EndTurn();
    }
}
