using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : new()
{
    public static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }
            return _instance;
        }
    }
}

public enum UIType 
{
    UIGameOverPanel,
}


public class Load : Singleton<Load> 
{
    public SkyscraperItem ItemBox(BoxType type)
    {
        SkyscraperItem box = Resources.Load<SkyscraperItem>("ItemBoxType/" + type.ToString());
        return box;
    }
    public Sprite ItemBoxSprite(BoxType type)
    {
        Sprite box = Resources.Load<Sprite>("Sprite/" + type.ToString());
        return box;
    }
    public GameObject UILoad(UIType type)
    {
        GameObject box = Resources.Load<GameObject>("UIPrefab/" + type.ToString());
        return box;
    }
}
