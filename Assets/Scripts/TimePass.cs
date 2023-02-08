using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class TimePass : MonoBehaviour
{
    public DateTime gameDate = new DateTime(1984, 1, 1, 6, 0, 0);
    public float markiplier = 1; //mutliplier
    private float time = 0;
    private float timePeriod = 1;
    public DateTime dueRentPeriod = new DateTime(1984, 1, 7, 6, 0, 0); //in days
    private float rentPeriod;
    public float moneyNeeded = 20;
    public float moneyNeededMarkiplier = 2;
    public TMP_Text dateText;
    public TMP_Text money;
    void Start()
    {
        rentPeriod = dueRentPeriod.Day;
        dateText.text = gameDate.ToString("yyyy/dd/MM HH:mm:ss");
    }

    void OneSecondPass()
    {
        gameDate = gameDate.AddSeconds(1 * markiplier);
        dateText.text = gameDate.ToString("yyyy/dd/MM HH:mm:ss");
    }

    // Update is called once per frame
    void Update()
    {
        if (time > timePeriod / markiplier)
        {   
            OneSecondPass();
            time = 0;
        }

        time += Time.deltaTime;

        if (DateTime.ParseExact(dateText.text, "yyyy/dd/MM HH:mm:ss", CultureInfo.InvariantCulture) > dueRentPeriod)
        {
            dueRentPeriod = dueRentPeriod.AddDays(rentPeriod);
            int moneyAmount = Convert.ToInt32(money.text.Replace("yuan", ""));
            if (moneyAmount >= moneyNeeded)
            {
                moneyNeeded *= moneyNeededMarkiplier;
                Debug.Log("Week Passed");
            }
            else
            {
                //TODO: game over thing
                Debug.Log("You fucking starved to death and some chinese child with a name like Xiang Qiu ate your corpse.");
            }
        }
    }
}
