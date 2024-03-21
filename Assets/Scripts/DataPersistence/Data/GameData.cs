using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public int bulletAmmo;
    public int playerHealth;
    public int sceneLevelIndex;
    public bool isContinue;

    public GameData()
    {
        playerPosition = new Vector3(-8.25f, -0.32f,0);
        bulletAmmo = 5;
        playerHealth = 5;
        sceneLevelIndex = 0;
        isContinue = false;
    }
}
