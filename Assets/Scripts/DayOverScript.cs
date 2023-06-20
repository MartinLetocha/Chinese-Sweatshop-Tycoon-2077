using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayOverScript : MonoBehaviour
{
    public TMP_Text finishedText;
    public TMP_Text moneyGained;
    public TMP_Text moneyUsed;
    public TMP_Text total;
    public TMP_Text now;
    
    public void SetText(string f, string mg, string mu, string t, string n)
    {
        finishedText.text = f;
        moneyGained.text = mg;
        moneyUsed.text = mu;
        total.text = t;
        now.text = n;
    }
}
