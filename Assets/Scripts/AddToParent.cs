using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AddToParent : MonoBehaviour
{
    public GameObject[] objToFind;
    public GameObject ParentPart;
    private bool engineFound = false;
    private void Start()
    {
    }

    private void Update()
    {
        if (!engineFound)
        {
            if (GameObject.Find("Engine").transform.GetChild(0).gameObject is null)return;
            ParentPart = GameObject.Find("Engine").transform.GetChild(0).gameObject;
            engineFound = true;
         
        }
        if(GameObject.FindGameObjectsWithTag("Parts")is null)return;
        objToFind = GameObject.FindGameObjectsWithTag("Parts");
        foreach (var part in objToFind)
        {
            part.transform.root.parent = ParentPart.transform;
        }
    }

    public void AddTolist(GameObject gameObject)
    {
        objToFind.Append(gameObject);
        gameObject.transform.parent = ParentPart.transform;
        if (objToFind[0]is null) return;
        //objToFind[0].transform.parent = ParentPart.transform;
        objToFind[0].transform.root.parent = ParentPart.transform;
    }
}
