using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public int gold;
    public int fuel;
    public int level;
    public GameData()
    {
        level = 0;
        this.gold = 1000;
        this.fuel = 30;
    }
}