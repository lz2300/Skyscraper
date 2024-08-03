using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BoxType 
{
    LinReder = -1,
    Normal = 0,
    Box = 1,
    Elliptical= 2,
    Garden = 3,
    Rectangle = 4,
    Trapezium = 5,
    Trapezium1 = 6,
    Trapezium2 = 7,
    Lock = 8,
}

public class SkyscraperItem : ItemData
{
    public BoxType itemType;
    bool isOpen = true;
    private Rigidbody2D rig2d;
    public float endGamePos = 0;
    Vector2 pos;
    private void Start()
    {
        rig2d = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isOpen)
        {
            if (!GameCtroller.Ins.gameManager.isCoincident)
            {
                GameCtroller.Ins.audioManager.AudioPlay(AudioType.Normal);
            }
            else
            {
                GameCtroller.Ins.audioManager.AudioPlay(AudioType.Perfect);
            }
            if (itemType == BoxType.Lock)
            {
                base.LockEvent(itemType);
            }
            GameCtroller.Ins.gameManager.AddPosition();
            isOpen = false;
            rig2d.mass = 1000;
            rig2d.gravityScale = 1;
        }
    }
}
