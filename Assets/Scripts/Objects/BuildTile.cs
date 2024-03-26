using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTile : MonoBehaviour
{
    // Start is called before the first frame update
    public int id;
    public bool isEmpty;
    public void Start()
    {
        isEmpty = true;
    }

    public void Occupied()
    {
        isEmpty = false;
    }

    public void UnOccupied()
    {
        isEmpty = true;
    }
}
