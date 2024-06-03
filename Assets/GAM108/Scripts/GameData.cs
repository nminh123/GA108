using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class GameData
{
    public int score;
    public string timePlayed;
}
[Serializable]
public class GameDataPlayed
{
    public List<GameData> plays;
}
    
