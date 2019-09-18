using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MultiSelectDropdownData
{
    #region FIELDS

    private string name = string.Empty;

    private int id;

    private bool isSelected = false;

    #endregion

    #region PROPERTIES

    public string Name {
        get => name;
        private set => name = value;
    }

    public int Id {
        get => id;
        private set => id = value;
    }

    public bool IsSelected {
        get => isSelected;
        private set => isSelected = value;
    }

    #endregion

    #region METHODS

    public MultiSelectDropdownData(int id, string name, bool isSelected)
    {
        Name = name;
        Id = id;
        IsSelected = isSelected;
    }

    public void SetSelection(bool isSelected)
    {
        IsSelected = isSelected;
    }


     public void ToggleSelection()
     {
            IsSelected = !IsSelected;
     }

    public string GetName()
    {
        if (IsSelected == true)
        {
            return Name.ToUpper();
        } 
        else
        {
            return Name;
        }
    }

    public bool CompareName(string nameToCompare)
    {
        if (Name.Equals(nameToCompare, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
        return false;
    }

    #endregion
}
