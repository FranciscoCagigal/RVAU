using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationModel{
    public static string gameType, gameMode = "Normal";
    public static float AIdificulty = 0.1f;
    public static int goals = 10;

    //saved state
    public static string lastGameType, lastGameMode;
    public static float lastAIdificulty;
    public static int lastGoals, score1, score2;
    public static bool resumedGame=false;
}
