using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : MonoBehaviour
{
   public virtual void LockEvent(BoxType _type)
    {
        GameCtroller.Ins.gameManager.LockEvent();
    }
}
