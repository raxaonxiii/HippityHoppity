using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUp : MenuUI
{
    public TextMeshProUGUI text;
    public string startText;

    public void SetUpText(string newText)
    {
        text.text = startText + newText;
    }
}
