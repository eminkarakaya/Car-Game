using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Leveldata")]
public class LevelData : ScriptableObject, IDataPersistence
{
    public int level;
    public void LoadData(GameData data)
    {
        level = data.level;
    }

    public void SaveData(GameData data)
    {
        data.level = level;
    }
}
