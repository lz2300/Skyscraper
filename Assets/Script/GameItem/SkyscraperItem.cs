using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BoxType 
{
    LinReder = -1,
    Normal = 0,
    Box = 1,
}

public class SkyscraperItem : ItemData
{
    public BoxType itemType;

    bool isOpen = true;

    private Rigidbody2D rig2d;
    private float speed = 10f;
    Vector2 pos;
    public override void SetType(BoxType _type)
    {
        itemType = _type;
        switch (itemType)
        {
            case BoxType.LinReder:
                base.LinderStarItem();
                break;
            case BoxType.Normal:
                base.NorMalItem();
                break;
            case BoxType.Box:
                base.BoxItem();
                break;
        }
    }
    private void Start()
    {
        rig2d = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isOpen)
        {
            isOpen = false;
            rig2d.mass = 100;
            rig2d.gravityScale = 1;
        }
    }
}
