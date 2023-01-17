using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public BuildingPanelUI BuildingPanelUI;

    private void Start()
    {
        BuildingPanelUI.gameObject.SetActive(false);
        SetMouseCursorState(BuildingPanelUI.gameObject.activeInHierarchy);
    }

    private void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            OpenCloseUI();
        }
    }
    
    private void SetMouseCursorState(bool state)
    {
        Cursor.visible = state;
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public bool OpenCloseUI()
    {
        BuildingPanelUI.gameObject.SetActive(!BuildingPanelUI.gameObject.activeInHierarchy);
        SetMouseCursorState(BuildingPanelUI.gameObject.activeInHierarchy);
        return BuildingPanelUI.gameObject.activeInHierarchy;
    }
}
