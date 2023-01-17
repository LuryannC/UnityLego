using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildingPanelUI : MonoBehaviour
{
    private Button _button;
    private BuildingData _assignedData;
    public BuildingData[] KnowingBuildingParts;
    
    public static UnityAction<BuildingData> onPartChosen;

    private UIManager uiManager;
    
    
    public void OnClick(BuildingData choosenData)
    {
        uiManager.OpenCloseUI();
        onPartChosen?.Invoke(choosenData);
    }

    private void Awake()
    {
        _button = GetComponentInChildren<Button>();
        _button.onClick.AddListener(() => OnClick(_assignedData));
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    public void Init(BuildingData assignedData)
    {
        _assignedData = assignedData;
        _button.GetComponent<Image>().sprite = _assignedData.Icon;
    }
    
}
