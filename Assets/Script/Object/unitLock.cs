using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitLock : unitBase
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (active)
        {
            unitBase[] units = FindObjectsOfType<unitBase>();
            for (int i = 0; i < units.Length; i++)
            {
                Destroy(units[i].GetComponent<Rigidbody2D>());
                units[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            }
        }
        base.OnTriggerEnter2D(collision);
    }
}
