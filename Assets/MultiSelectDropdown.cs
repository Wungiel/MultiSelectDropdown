using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MultiSelectDropdown: MonoBehaviour
{
    #region FIELDS

    public delegate void OnIdSelectedDelegate(int id, bool add);
    public OnIdSelectedDelegate OnIdSelected;

    [SerializeField]
    private Dropdown dropdown;

    [SerializeField]
    private Button toggleButton;

    #endregion

    #region PROPERTIES
    public Dropdown Dropdown
    {
        get => dropdown;
        private set => dropdown = value;
    }

    public Button ToggleButton
    {
        get => toggleButton;
        private set => toggleButton = value;
    }

    private List<MultiSelectDropdownData> dropdownData
    {
        get;
        set;
    } = new List<MultiSelectDropdownData>();

    #endregion

    #region METHODS

    public void OnEnable()
    {
        ToggleButton.onClick.AddListener(ToggleButtonClicked);
    }

    public void SetDropdownData <T> (List<T> objectsToConvert, List<T> listOfSelectedObjects, string idField, string nameField)
    {
        for (int i = 0; i < objectsToConvert.Count; i++)
        {
            GenerateDropdownData(objectsToConvert[i], listOfSelectedObjects, idField, nameField, i);
        }

        SetDropdownOption();
    }

    private void GenerateDropdownData<T>(T objectsToConvert, List<T> listOfSelectedObjects, string idField, string nameField, int index)
    {
        Type type = objectsToConvert.GetType();
        PropertyInfo infoId = type.GetProperty(idField);
        PropertyInfo infoName = type.GetProperty(nameField);
        object intObj = infoId.GetValue(objectsToConvert);
        object stringObj = infoName.GetValue(objectsToConvert);

        if (intObj != null && stringObj != null)
        {
            if (listOfSelectedObjects .Contains(objectsToConvert))
            {
                dropdownData.Add(new MultiSelectDropdownData((int)intObj, stringObj.ToString(), true));
            }
            else
            {
                dropdownData.Add(new MultiSelectDropdownData((int)intObj, stringObj.ToString(), false));
            }
        }
    }

    private void SetDropdownOptions(int value)
    {
        SetDropdownOption();
        Dropdown.SetValueWithoutNotify(value);
    }

    private void SetDropdownOption()
    {
        Dropdown.ClearOptions();
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

        for (int i = 0; i < dropdownData.Count; i++)
        {
            options.Add(new Dropdown.OptionData(dropdownData[i].GetName()));
        }

        Dropdown.AddOptions(options);
    }

    private void ToggleButtonClicked()
    {
        string selectedValue = Dropdown.options[Dropdown.value].text;
        int id = -1;
        bool add = false;

        for (int i = 0; i < dropdownData.Count; i++)
        {
            if (dropdownData[i].CompareName(selectedValue) == true)
            {
                dropdownData[i].ToggleSelection();
                id = dropdownData[i].Id;
                add = dropdownData[i].IsSelected;
            }
        }
        SetDropdownOptions(Dropdown.value);
        OnIdSelected(id, add);
    }

    #endregion

}
