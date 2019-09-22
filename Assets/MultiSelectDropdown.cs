using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MultiSelectDropdown: MonoBehaviour
{
    #region FIELDS

    public delegate void OnIdSelectedDelegate(int id);
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

    public void SetDropdownData (IEnumerable<IIDEquatable> objectsToConvert, IEnumerable<IIDEquatable> listOfSelectedObjects, string idField, string nameField)
    {
        foreach(IIDEquatable objectToConvert in objectsToConvert)
        {
            GenerateDropdownData(objectToConvert, listOfSelectedObjects, idField, nameField);
        }

        SetDropdownOption();
    }

    private void GenerateDropdownData(IIDEquatable objectsToConvert, IEnumerable<IIDEquatable> listOfSelectedObjects, string idField, string nameField)
    {
        Type type = objectsToConvert.GetType();
        PropertyInfo infoId = type.GetProperty(idField);
        PropertyInfo infoName = type.GetProperty(nameField);
        object intObj = infoId.GetValue(objectsToConvert);
        object stringObj = infoName.GetValue(objectsToConvert);

        if (intObj != null && stringObj != null)
        {
            if (listOfSelectedObjects.Contains(objectsToConvert))
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

        for (int i = 0; i < dropdownData.Count; i++)
        {
            if (dropdownData[i].CompareName(selectedValue) == true)
            {
                dropdownData[i].ToggleSelection();
                id = dropdownData[i].Id;
            }
        }
        SetDropdownOptions(Dropdown.value);
        OnIdSelected(id);
    }

    #endregion

}
