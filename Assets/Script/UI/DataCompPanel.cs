using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataCompPanel : AbstractDataSettingPanels
{
    public TMP_InputField RunInputField => runInputField;
    public TMP_InputField HpInputField => hpInputField;

    private const string DATA_RUN_DEFAULT = "3";
    private const string DATA_HP_DEFAULT = "0";
    void Start()
    {
        runInputField.text = DATA_RUN_DEFAULT;
        hpInputField.text = DATA_HP_DEFAULT;
    }
}
