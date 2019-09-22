using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    #region FIELDS

    [SerializeField]
    private MultiSelectDropdown multiSelect;
    [SerializeField]
    private List<ExampleData> dataList;
    [SerializeField]
    private List<ExampleData> selectionList;


    #endregion

    #region PROPERTIES

    public MultiSelectDropdown MultiSelect {
        get => multiSelect;
        private set => multiSelect = value;
    }
    public List<ExampleData> DataList {
        get => dataList;
        private set => dataList = value;
    }

    public List<ExampleData> SelectionList {
        get => selectionList;
        private set => selectionList = value;
    }

    #endregion

    #region METHODS

    private void OnEnable()
    {
        DataList = new List<ExampleData>();
        SelectionList = new List<ExampleData>();

        SetTestData(DataList);
        SelectionList.Add(DataList[1]);

        MultiSelect.SetDropdownData(DataList, SelectionList, nameof(ExampleData.Id), nameof(ExampleData.ExampleName));
        MultiSelect.OnIdSelected += Selection;
    }

    private void Selection(int id)
    {
        if (id != -1)
        {
            ExampleData selectedObject = SelectionList.ContainsOptimized(id);
            if (selectedObject != null)
            {
                SelectionList.Remove(selectedObject);
            } else
            {
                SelectionList.Add(DataList.ContainsOptimized(id));
            }
        }
    }

    private void SetTestData(List<ExampleData> dataList)
    {
        dataList.Add(new ExampleData("Wiktor", "XYZ"));
        dataList.Add(new ExampleData("Janek", "QWERTY"));
        dataList.Add(new ExampleData("Kuba", "haslo"));
        dataList.Add(new ExampleData("Teodozja", "haslo123"));
    }

    #endregion

}
