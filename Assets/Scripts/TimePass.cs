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
    public decimal moneyNeeded = 20;
    public const decimal moneyNeededAdd = 20; 
    public decimal moneyNeededMarkiplier = 1.5m;
    public TMP_Text dateText;
    public TMP_Text money;
    public TMP_Text gg;
    public bool hardMode;
    public CanvasGroup weekEnd;
    public CanvasGroup texts;
    public Slider paper;
    private bool _timeStop = false;
    private bool _stopTheStop = false;
    private float _weekendTime;
    private int loop;
    public DayOverScript textsScript;
    private decimal _lastWeekCash = 0;
    public decimal moneySpent = 0;
    public bool giveOnDay = false;
    private decimal _howMuch;
    private bool _once = true;
    private decimal _passiveGained;
    void Start()
    {
        dateText.text = gameDate.ToString("yyyy/dd/MM HH:mm:ss");
        weekEnd.blocksRaycasts = false;
    }

    void OneSecondPass()
    {
        gameDate = gameDate.AddSeconds(1 * markiplier);
        dateText.text = gameDate.ToString("yyyy/dd/MM HH:mm:ss");
    }

    public void StartGiving(decimal howMuch)
    {
        giveOnDay = true;
        _howMuch = howMuch;
    }

    public void ChangeCash(decimal how, bool isPassiveGain)
    {
        if (isPassiveGain)
        {
            _passiveGained += how;
        }
        decimal.TryParse(money.text.Replace("yuan", ""), out decimal cash);
        cash += how;
        money.text = cash.ToString("0.#") + "yuan";
    }

    public decimal CheckCash()
    {
        decimal.TryParse(money.text.Replace("yuan", ""), out decimal cash);
        return cash;
    }

    public void TOKIWOUGOKIDAS()
    {
        paper.value = 0;
        weekEnd.alpha = 0;
        weekEnd.blocksRaycasts = false;
        texts.alpha = 0;
        _timeStop = false;
        _stopTheStop = false;
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
                weekEnd.blocksRaycasts = true;
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

        DateTime current = DateTime.ParseExact(dateText.text, "yyyy/dd/MM HH:mm:ss", CultureInfo.InvariantCulture);
        if (current.Hour == 6 && current.Day != 1)
        {
            gg.text = "Day over!";
            if (giveOnDay && _once)
            {
                ChangeCash(_howMuch, true);
                _once = false;
            }
        }
        else
        {
            gg.text = "";
            _once = true;
        }
        if (current > dueRentPeriod)
        {
            dueRentPeriod = dueRentPeriod.AddDays(rentPeriod);
            decimal.TryParse(money.text.Replace("yuan", ""), out decimal cash);
            if (cash >= moneyNeeded) //change to moneyNeeded
            {
                cash -= moneyNeeded;
                moneySpent += moneyNeeded;
                money.text = cash.ToString("0.#") + "yuan";
                if (hardMode)
                {
                    moneyNeeded *= moneyNeededMarkiplier;
                }
                else
                {
                    moneyNeeded += moneyNeededAdd;
                }

                _timeStop = true; // week passeds
                decimal gained;
                if (_lastWeekCash < cash)
                {
                    gained = Math.Abs(_lastWeekCash - cash) + moneySpent;
                }
                else
                {
                    gained = cash + moneySpent - _lastWeekCash;
                }
                string to;
                if (gained - moneySpent > 0)
                {
                    to = $"+{(gained - moneySpent):0.#}";
                }
                else
                {
                    to = $"{(gained - moneySpent):0.#}";
                }
                textsScript.SetText("Week survived!", $"{(gained - _passiveGained):0.#} (+{_passiveGained:0.#}p)", $"{moneySpent:0.#}", $"{to}", $"{cash:0.#}");
                _lastWeekCash = cash;
                moneySpent = 0;
                _passiveGained = 0;
            }
            else
            {
                _timeStop = true; // lose
                decimal gained;
                if (_lastWeekCash < cash)
                {
                    gained = Math.Abs(_lastWeekCash - cash) + moneySpent;
                }
                else
                {
                    gained = cash + moneySpent - _lastWeekCash;
                }
                string to;
                if (gained - moneySpent > 0)
                {
                    to = $"+{(gained - moneySpent):0.#}";
                }
                else
                {
                    to = $"{(gained - moneySpent):0.#}";
                }
                textsScript.SetText("You starved to death...", $"{(gained - _passiveGained):0.#} (+{_passiveGained:0.#}p)", $"{moneySpent:0.#}", $"{to}", $"{cash:0.#}");
                Application.Quit();
            }
        }
    }
}
