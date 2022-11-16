using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Building : MonoBehaviour
{
    private BuildingData _assignedData;
    private BoxCollider _boxCollider;
    private GameObject _graphic;
    private Transform _colliders;
    private Renderer _renderer;
    private Material _defaultMaterial;
    private bool _isOverlaping;
    
    public bool IsOverlaping => _isOverlaping;

    public BuildingData AssignedData => _assignedData;

    private bool _flaggedForDelete; 
    public bool FlaggedForDelete => _flaggedForDelete;


    public void Init(BuildingData data)
    {
        _assignedData = data;
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.size = _assignedData.BuildingSize;
        _boxCollider.center = new Vector3(0, (_assignedData.BuildingSize.y + .1f) * 0.5f, 0);
        _boxCollider.isTrigger = true;

        var rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        
        _graphic = Instantiate(data.Prefab, transform);
        _renderer = _graphic.GetComponentInChildren<Renderer>();
        _defaultMaterial = _renderer.material;
        
        _colliders = _graphic.transform.Find("Colliders");
        if(_colliders != null) _colliders.gameObject.SetActive(false);
    }
    
    public void UpdateMaterial(Material newMaterial)
    {
        if (_renderer == null) return;
        if (_renderer.material != newMaterial) _renderer.material = newMaterial;
    }
    
    public void PlaceBuilding()
    {
        _boxCollider.enabled = false;
        if(_colliders != null) _colliders.gameObject.SetActive(true);
        UpdateMaterial(_defaultMaterial);
        gameObject.layer = 10;
        //for loop to iterate through all children in _colliders and set their layer to 10
        for (int i = 0; i < _colliders.childCount; i++){
            _colliders.GetChild(i).gameObject.layer = 10;
        }
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

    private void OnTriggerStay(Collider other)
    {
        _isOverlaping = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _isOverlaping = false;
    }
}
