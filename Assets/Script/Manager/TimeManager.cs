using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDalyData
{
    public float remeberMoveSpeed;
    public float f_Time;
    public Action callBack;
    public TimeDalyData() { }
    public TimeDalyData(float _time,Action _action)
    {
        f_Time = _time;
        callBack = _action;
    }
    public TimeDalyData(float _time,float speed, Action _action)
    {
        remeberMoveSpeed = speed;
        f_Time = _time;
        callBack = _action;
    }
}

public class TimeManager : MonoBehaviour
{
    //时间记录
    public List<TimeDalyData> listCumulativeTime = new List<TimeDalyData>();
    //延迟执行事件
    public List<TimeDalyData> queDalyTime = new List<TimeDalyData>();
    //游戏时间
    float fGameTime = 0;
    public int iGameTime = 0;
    public void AddDalyTimeAction(float _time,Action _callback)
    {
        TimeDalyData _data = new TimeDalyData(_time, _callback);
        queDalyTime.Add(_data);
    }

    public void AddCumulativeTime(float speed,Action _callBack = null)
    {
        for (int i = listCumulativeTime.Count - 1; i >= 0; i--)
        {
            if (listCumulativeTime[i].remeberMoveSpeed == speed)
            {
                return;
            }
        }
        TimeDalyData _data = new TimeDalyData(0,speed, _callBack);
        listCumulativeTime.Add(_data);
    }
    float temp_Time = 0;
    List<SkyscraperItem> _boxlist = new List<SkyscraperItem>();

    float speedUseTime = 0;
    float speed = 0;

    // Update is called once per frame
    void Update()
    {
        //全局检测游戏是否结束
        if (GameCtroller.Ins.gameManager.gameType == GameType.游戏结束)
        {
            return;
        }

        temp_Time += Time.deltaTime;
        if (temp_Time >= 0.5f)
        {
            _boxlist = GameCtroller.Ins.gameManager.tempListBox;
            for (int i = 0; i < _boxlist.Count; i++)
            {
                if (_boxlist[i].endGamePos > _boxlist[i].transform.localPosition.y)
                {
                    GameCtroller.Ins.gameManager.GameOver();
                    return;
                }
            }
        }

        fGameTime += Time.deltaTime;
        if (fGameTime>= 1)
        {
            fGameTime = 0;
            iGameTime++;
            GameCtroller.Ins.gameManager.time.text = iGameTime.ToString();
        }

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

        //记录速度使用时间
        for (int i = 0; i < listCumulativeTime.Count; i++)
        {
            if (listCumulativeTime[i].remeberMoveSpeed == GameCtroller.Ins.gameManager.moveSpeed)
            {
                listCumulativeTime[i].f_Time += Time.deltaTime;            
            }
            if (listCumulativeTime[i].f_Time > speedUseTime)
            {
                speed = listCumulativeTime[i].remeberMoveSpeed;
                speedUseTime = listCumulativeTime[i].f_Time;
            }
        }
        GameCtroller.Ins.gameManager.MaximumUsageSpeed.text = speed.ToString();
        GameCtroller.Ins.gameManager.MaximumUsageSpeedTime.text = speedUseTime.ToString();
    }

    internal void Init()
    {
        iGameTime = 0;
        speed = 0;
        speedUseTime = 0;
        listCumulativeTime.Clear();
        queDalyTime.Clear();
    }
}
