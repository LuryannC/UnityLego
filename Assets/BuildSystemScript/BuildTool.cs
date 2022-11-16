using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildTool : MonoBehaviour
{

    
    [SerializeField] private LayerMask _buildModeLayerMask;
    [SerializeField] private LayerMask _deleteModeLayerMask;
    [SerializeField] private int _defaultLayerInt = 8;
    [SerializeField] private float _rotateSnapFloat;
    
    [SerializeField] private Material _buildingMatPositive;
    [SerializeField] private Material _buildingMatNegative;

    private bool _deleteModeEnabled;
    
    // Ray related
    [SerializeField] private float _rayDistance;
    [SerializeField] private Transform _rayOrigin;
    [SerializeField][Range(0, 10)] private float originRotationSensibility;
    [SerializeField]private Camera _mainCamera;
    private Vector3 screenPosition; 
    private Vector3 worldPosition;
    private float clampToForward = 1000f;
    
    
    [SerializeField]private Building _spawnedBuilding;
    
    private Quaternion _lastRotation;
    
    private Building _targetBuilding;
    public BuildingData Data;

    private void Start()
    {
        
        ChoosePart(Data);
    }

    private void ChoosePart(BuildingData data)
    {
        if (_deleteModeEnabled)
        {
            if (_targetBuilding != null && _targetBuilding.FlaggedForDelete) _targetBuilding.RemoveDeleteFlag();
            _targetBuilding = null;
            _deleteModeEnabled = false;
        }

        if (_spawnedBuilding != null)
        {
            Destroy(_spawnedBuilding.gameObject);
            _spawnedBuilding = null;
        }

        var go = new GameObject
        {
            layer = _defaultLayerInt,
            name = "Building name"
        };
        
        _spawnedBuilding = go.AddComponent<Building>();
        _spawnedBuilding.Init(data);
        _spawnedBuilding.transform.rotation = _lastRotation;
    }

    private void Update()
    {
        if (Keyboard.current.jKey.wasPressedThisFrame) _deleteModeEnabled = !_deleteModeEnabled;
        if (_deleteModeEnabled) DeleteModeLogic();
        else BuildModeLogic();
    }


    private bool IsRayHitting(LayerMask layerMask, out RaycastHit hitInfo)
    {
        var ray = new Ray(_rayOrigin.position, _mainCamera.transform.forward * _rayDistance);
        Debug.DrawRay(_rayOrigin.position, _mainCamera.transform.forward * _rayDistance, Color.green);
        return Physics.Raycast(ray, out hitInfo, _rayDistance, layerMask);
    }
    
    private void BuildModeLogic()
    {
        if (_targetBuilding != null && _targetBuilding.FlaggedForDelete)
        {
            _targetBuilding.RemoveDeleteFlag();
            _targetBuilding = null;
        }
        if (_spawnedBuilding == null) return;

        PositionBuildingPreview();


    }

    private void PositionBuildingPreview()
    {
        _spawnedBuilding.UpdateMaterial(_buildingMatNegative);
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            _spawnedBuilding.transform.Rotate(0, _rotateSnapFloat,0);
            _lastRotation = _spawnedBuilding.transform.rotation;
        }

        if (IsRayHitting(_buildModeLayerMask, out RaycastHit hitInfo))
        {
            _spawnedBuilding.UpdateMaterial(_buildingMatPositive);
            var gridPositon = WorldGrid.GridPositionFromWorldPosition(hitInfo.point, 1f);
            _spawnedBuilding.transform.position = gridPositon;
            //var obj = hitInfo.collider.gameObject;
            //Debug.Log($"Looking at {obj.name}", this);
            //_spawnedBuilding.transform.position = hitInfo.point;
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                _spawnedBuilding.PlaceBuilding();
                var dataCopy = _spawnedBuilding.AssignedData;
                _spawnedBuilding = null;
                ChoosePart(dataCopy);
            }
        }
    }

    private void DeleteModeLogic()
    {
        if (!IsRayHitting(_deleteModeLayerMask, out RaycastHit hitInfo)) return;
        var obj = hitInfo.collider.gameObject;
        
        
        var detectedBuilding = hitInfo.collider.gameObject.GetComponentInParent<Building>();
        if (detectedBuilding == null) return;

        if (_targetBuilding == null) _targetBuilding = detectedBuilding;

        if (detectedBuilding != _targetBuilding && _targetBuilding.FlaggedForDelete)
        {
            _targetBuilding.RemoveDeleteFlag();
            _targetBuilding = detectedBuilding;
        }

        if (detectedBuilding == _targetBuilding && !_targetBuilding.FlaggedForDelete)
        {
            _targetBuilding.FlagForDelete(_buildingMatNegative);
        }
        
        Debug.Log($"Looking at {obj.name}", this);
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Destroy(_targetBuilding.gameObject);
            _targetBuilding = null;
        }
    }
}
