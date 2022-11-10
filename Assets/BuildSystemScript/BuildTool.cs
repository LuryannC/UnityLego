using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildTool : MonoBehaviour
{

    [SerializeField] private float _rayDistance;
    [SerializeField] private LayerMask _buildModeLayerMask;
    [SerializeField] private LayerMask _deleteModeLayerMask;
    [SerializeField] private int _defaultLayerInt = 8;
    [SerializeField] private Transform _rayOrigin;
    [SerializeField] private Material _buildingMatPositive;
    [SerializeField] private Material _buildingMatNegative;

    private bool _deleteModeEnabled;
    
    private Camera _mainCamera;
    
    public GameObject _gameObjectToPosition;

    private void Start()
    {
        _mainCamera = Camera.main;
    }


    private void Update()
    {
        if (_gameObjectToPosition is null || !IsRayHitting(_buildModeLayerMask, out RaycastHit hitInfo)) return;

        _gameObjectToPosition.transform.position = hitInfo.point;

        if (Mouse.current.leftButton.wasPressedThisFrame) Instantiate(_gameObjectToPosition, hitInfo.point, Quaternion.identity);
    }


    private bool IsRayHitting(LayerMask layerMask, out RaycastHit hitInfo)
    {
        var ray = new Ray(_rayOrigin.position, _mainCamera.transform.forward * _rayDistance);
        return Physics.Raycast(ray, out hitInfo, _rayDistance, layerMask);
    }
}
