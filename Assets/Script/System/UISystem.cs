using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UISystem : MonoBehaviour
{
    public GameObject tmpPanel;
    public TMP_Text resultCount, dynamicCount;
    public gameData g_data;
    UnitSpawner m_unitSpawner;
    GameSystem m_gameSystem;
    private void Awake()
    {
        m_unitSpawner = FindFirstObjectByType<UnitSpawner>();
        m_gameSystem = FindAnyObjectByType<GameSystem>();
    }
    private void Update()
    {
        if (g_data.GameMode == gameData.gameMode.playing)
        {
            tmpPanel.SetActive(false);
            dynamicCount.gameObject.SetActive(true);
        }
        if (g_data.GameMode == gameData.gameMode.dead|| g_data.GameMode == gameData.gameMode.noInit)
        {
            tmpPanel.SetActive(true);
            dynamicCount.gameObject.SetActive(false);
        }
        resultCount.text = g_data.UnitsCount+"";
        dynamicCount.text = g_data.UnitsCount + "";
    }
    public void StartGame()
    {
        m_gameSystem.resetGame();
        g_data.changeGameModeTo(gameData.gameMode.playing);
        m_unitSpawner.SpawnerFuntion();
    }
    public void Tryagame()
    {
        StartGame();
    }
    public void ExitGame()
    {

    }
    void drawNumber()
    {

    }
    public void changeMenuActive(bool value)
    {

    }
    public void changeMenuActive(GameObject UIObject,bool value)
    {
        UIObject.SetActive(value);
    }
}
