using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/gameData", order = 1)]
public class gameData : ScriptableObject
{
    public enum gameMode { noInit, playing, dead,stop }
    public gameMode GameMode = gameMode.noInit;
    public int UnitsCount = 0;
    public void changeGameModeTo(gameMode state)
    {
        GameMode = state;
    }
}