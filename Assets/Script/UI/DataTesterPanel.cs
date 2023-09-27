using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataTesterPanel : AbstractDataSettingPanels
{
    public TMP_InputField RunInputField => runInputField;
    public TMP_InputField HpInputField => hpInputField;

    private const string DATA_RUN_DEFAULT = "5";
    private const string DATA_HP_DEFAULT = "3";
    private void Start()
    {
        runInputField.text = DATA_RUN_DEFAULT;
        hpInputField.text = DATA_HP_DEFAULT;
    }
}
