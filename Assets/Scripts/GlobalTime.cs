using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTime : MonoBehaviour
{
    public float _deltaTime;
    public float _time;

    public int day, week, seazon, year;

    void Start()
    {
        _deltaTime = 20f;
        day = 1; 
        week = 1;
        seazon = 1;
        year = 1;
    }


    void Update()
    {
        _time += Time.deltaTime;

        if (_time >= _deltaTime)
        {
            _time = 0;
            day++;
        }
        if (day > 7)
        {
            day = 1;
            week++;
        }
        if (week > 10)
        {
            week = 1;
            seazon++;
        }
        if (seazon > 4)
        {
            seazon = 1;
            year++;
        }
    }
}
