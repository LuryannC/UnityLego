using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private BuildingData _assignedData;
    private GameObject _graphic;
    private Renderer _renderer;
    private Material _defaultMaterial;
    private bool isOverlaping;

    public BuildingData AssignedData => _assignedData;

    private bool _flaggedForDelete; 
    public bool FlaggedForDelete => _flaggedForDelete;


    public void Init(BuildingData data)
    {
        _assignedData = data;
        _graphic = Instantiate(data.Prefab, transform);
        _renderer = _graphic.GetComponentInChildren<Renderer>();
        _defaultMaterial = _renderer.material;
    }
    
    // private void Start()
    // {
    //     _renderer = GetComponentInChildren<Renderer>();
    //     if (_renderer) _defaultMaterial = _renderer.material;
    // }
    
    public void UpdateMaterial(Material newMaterial)
    {
        if (_renderer.material != newMaterial) _renderer.material = newMaterial;
    }
    
    public void PlaceBuilding()
    {
        // _renderer = GetComponentInChildren<Renderer>();
        // if (_renderer) _defaultMaterial = _renderer.material;
        // else Debug.Log("Renderer not found!");
        UpdateMaterial(_defaultMaterial);
        gameObject.layer = 10;
        gameObject.name = _assignedData.DisplayName + " - " + transform.position;
    }
    
    public void FlagForDelete(Material deleteMaterial)
    {
        UpdateMaterial(deleteMaterial);
        _flaggedForDelete = true;
    }
    
    public void RemoveDeleteFlag()
    {
        UpdateMaterial(_defaultMaterial);
        _flaggedForDelete = false;
    }
}
