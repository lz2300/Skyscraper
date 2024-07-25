using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheck : MonoBehaviour
{
    public gameData g_data;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<unitBase>(out var a)&&a.id>1)
        {
            g_data.changeGameModeTo(gameData.gameMode.dead);
        }
    }
}
