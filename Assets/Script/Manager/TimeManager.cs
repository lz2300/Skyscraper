using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDalyData
{
    public float f_Time;
    public Action callBack;
    public TimeDalyData() { }
    public TimeDalyData(float _time,Action _action)
    {
        f_Time = _time;
        callBack = _action;
    }
}

public class TimeManager : MonoBehaviour
{
    public static List<TimeDalyData> queDalyTime = new List<TimeDalyData>();
    float temp_Time = 0;
    public static void AddDalyTimeAction(float _time,Action _callback)
    {
        TimeDalyData _data = new TimeDalyData(_time, _callback);
        queDalyTime.Add(_data);
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = queDalyTime.Count - 1; i >= 0; i--)
        {
            if (queDalyTime[i].f_Time > 0)
            {
                queDalyTime[i].f_Time -= Time.deltaTime;
                continue;
            }
            queDalyTime[i].callBack?.Invoke();
            queDalyTime.RemoveAt(i);
        }
    }
}
