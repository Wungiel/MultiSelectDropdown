using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExampleData
{
    #region FIELDS

    [SerializeField]
    private int id;
    [SerializeField]
    private string exampleName;
    [SerializeField]
    private string superSecretPassword;
    [SerializeField]
    private static int exampleId = 0;

    #endregion

    #region PROPERTIES

    public int Id
    {
        get => id;
        private set => id = value;
    }

    public string ExampleName
    {
        get => exampleName;
        private set => exampleName = value;
    }


    public string SuperSecretPassword
    {
        get => superSecretPassword;
        private set => superSecretPassword = value;
    }

    #endregion

    #region METHODS

    public ExampleData(string name, string password)
    {
        Id = exampleId++;
        ExampleName = name;
        SuperSecretPassword = password;
    }
    #endregion
}
