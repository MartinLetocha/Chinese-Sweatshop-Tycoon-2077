using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimePass : MonoBehaviour
{
    public DateTime gameDate = new DateTime(1984, 1, 1, 6, 0, 0); //change to day 1 hour 6
    public float markiplier = 1; //multiplier
    private float time = 0;
    private float timePeriod = 1;
    public DateTime dueRentPeriod = new DateTime(1984, 1, 8, 6, 0, 0); //in days
    private float rentPeriod = 7;
    public float moneyNeeded = 20;
    public const float moneyNeededAdd = 20; 
    public float moneyNeededMarkiplier = 1.5f;
    public TMP_Text dateText;
    public TMP_Text money;
    public bool hardMode;
    public CanvasGroup weekEnd;
    public CanvasGroup texts;
    public Slider paper;
    private bool _timeStop = false;
    private bool _stopTheStop = false;
    private float _weekendTime;
    private int loop;
    void Start()
    {
        dateText.text = gameDate.ToString("yyyy/dd/MM HH:mm:ss");
    }

    void OneSecondPass()
    {
        gameDate = gameDate.AddSeconds(1 * markiplier);
        dateText.text = gameDate.ToString("yyyy/dd/MM HH:mm:ss");
    }

    public void TOKIWOUGOKIDAS()
    {
        paper.value = 0;
        weekEnd.alpha = 0;
        texts.alpha = 0;
        _timeStop = false;
    }
    void Update()
    {
        if (_timeStop)
        {
            if (_stopTheStop)
            {
                return;
            }
            _weekendTime += Time.deltaTime;
            if (_weekendTime > 1 && loop == 0)
            {
                _weekendTime = 0;
                weekEnd.alpha = 1;
                loop = 1;
                return;
            }
            if (_weekendTime > 1 && loop == 1)
            {
                _weekendTime = 0;
                paper.value = 1;
                loop = 2;
                return;
            }
            if (_weekendTime > 1 && loop == 2)
            {
                _weekendTime = 0;
                texts.alpha = 1;
                loop = 0;
                _stopTheStop = true;
                return;
            }

            if (loop == 0)
            {
                weekEnd.alpha = _weekendTime;
            }
            else if(loop == 1)
            {
                paper.value = _weekendTime;
            }
            else if (loop == 2)
            {
                texts.alpha = _weekendTime;
            }

            return;
        }
        if (time > timePeriod / markiplier)
        {   
            OneSecondPass();
            time = 0;
        }

        time += Time.deltaTime;

        if (DateTime.ParseExact(dateText.text, "yyyy/dd/MM HH:mm:ss", CultureInfo.InvariantCulture) > dueRentPeriod)
        {
            dueRentPeriod = dueRentPeriod.AddDays(rentPeriod);
            float.TryParse(money.text.Replace("yuan", ""), out float cash);
            if (cash >= moneyNeeded) //change to moneyNeeded
            {
                if (hardMode)
                {
                    moneyNeeded *= moneyNeededMarkiplier;
                }
                else
                {
                    moneyNeeded += moneyNeededAdd;
                }

                _timeStop = true; // week passed
            }
            else
            {
                _timeStop = true; // lose
            }
        }
    }
}
