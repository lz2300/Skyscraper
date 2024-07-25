using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public gameData g_Data;
    UnitSpawner m_unitSpawner;
    // Start is called before the first frame update
    void Awake()
    {
        m_unitSpawner = FindFirstObjectByType<UnitSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (g_Data.GameMode == gameData.gameMode.playing)
        {
            if (Input.GetMouseButtonDown(0)||Input.GetButtonDown("Jump"))
            {
                m_unitSpawner.ReliceHolding();
            }
        }
    }
    public void resetGame()
    {
        Camera.main.GetComponent<camCtrl>().lastUnitPos = new Vector3(0, -4, 0);
        unitBase[] units = FindObjectsOfType<unitBase>();
        for(int i = 0; i < units.Length; i++)
        {
            Destroy(units[i].gameObject);
        }
        g_Data.UnitsCount = 0;
        m_unitSpawner.insObj = null;
    }
}
