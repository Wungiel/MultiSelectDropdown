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

        MultiSelect.SetDropdownData<ExampleData>(DataList, SelectionList, nameof(ExampleData.Id), nameof(ExampleData.ExampleName));
        MultiSelect.OnIdSelected += Selection;
    }

    private void Selection(int id, bool add)
    {
        for (int i = 0; i < DataList.Count; i++)
        {
            if (DataList[i].Id == id)
            {
                if (add == true)
                    SelectionList.Add(DataList[i]);
                else
                    SelectionList.Remove(DataList[i]);
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
