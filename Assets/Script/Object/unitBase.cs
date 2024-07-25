using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitBase : MonoBehaviour
{
    Rigidbody2D rig2d;
    camCtrl camCtrl;
    public int id = 0;
    UnitSpawner m_unitSpawner;
    protected bool active = true;
    private void Awake()
    {
        rig2d = GetComponent<Rigidbody2D>();
        camCtrl = Camera.main.GetComponent<camCtrl>();
        m_unitSpawner = FindFirstObjectByType<UnitSpawner>();
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(name+"--"+collision.name);
        if (active)
        {
            active = false;
            m_unitSpawner.SpawnerFuntion();
            rig2d.velocity = Vector2.zero;
            camCtrl.lastUnitPos = transform.position + Vector3.forward * -10;
        }
    }
    private void OnBecameInvisible()
    {
        //FindFirstObjectByType<GameSystem>().g_Data.changeGameModeTo(gameData.gameMode.dead);
        if (FindFirstObjectByType<GameSystem>().g_Data.GameMode == gameData.gameMode.playing)
        {
            if (Mathf.Acos(Vector3.Dot(Vector2.up, transform.up)) * Mathf.Rad2Deg>=5) FindFirstObjectByType<GameSystem>().g_Data.changeGameModeTo(gameData.gameMode.dead);
        }
    }
}
