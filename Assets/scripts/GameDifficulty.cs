using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDifficulty : MonoBehaviour
{
    bool DifficultyCheck;
    float timeLeft = 10f;
    private int hpValue;
    private int killsValue;
    private int missedKillsValue;
    private string Difficulty;

    private void Awake()
    {
        Difficulty = "Starter";
    }

    private void Update()
    {
        DifficultyCheckTimer();

        if (DifficultyCheck)
        {
            if (hpValue >= 5 && killsValue > 3 && missedKillsValue < 5 && Difficulty == "Starter")
            {
                DifficultySetter("Normal");
                timeLeft += 10f;
                DifficultyCheck = false;
            }
            else if (hpValue >= 5 && killsValue > 10 && missedKillsValue < 10 && Difficulty == "Normal")
            {
                DifficultySetter("Advanced");
                timeLeft += 10f;
                DifficultyCheck = false;
            }
            else if (hpValue >= 5 && killsValue > 20 && missedKillsValue < 20 && Difficulty == "Advanced")
            {
                DifficultySetter("Hard");
                timeLeft += 10f;
                DifficultyCheck = false;
            }
            else if (hpValue >= 5 && killsValue > 40 && missedKillsValue < 30 && Difficulty == "Hard")
            {
                DifficultySetter("ACE");
                timeLeft += 10f;
                DifficultyCheck = false;
            }
            else
                DifficultyCheck = false;
        }
        
    }


    private void DifficultyCheckTimer()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            DifficultyCheck = true;
        }
    }

    public void StatCollection(int hpValue, int killsValue, int missedKillsValue)
    {
        this.hpValue = hpValue;
        this.killsValue = killsValue /= 100;
        this.missedKillsValue = missedKillsValue;
    }

    private void DifficultySetter(string Mode)
    {
       // var counter = FindObjectOfType<ResourceManager>();
        
        var Spawner = FindObjectOfType<Spawn>();
        if (Spawner && Spawner.enabled)
        {
            switch (Mode)
            {
                case "ACE":
                    Spawner.maxEnemies = 15;
                    Spawner.maxWreckages = 5;
                    Difficulty = "ACE";
                    break;
                case "Hard":
                    Spawner.maxEnemies = 10;
                    Spawner.maxWreckages = 4;
                    Difficulty = "Hard";
                    break;
                case "Advanced":
                    Spawner.maxEnemies = 5;
                    Spawner.maxWreckages = 3;
                    Difficulty = "Advanced";
                    break;
                case "Normal":
                    Spawner.maxEnemies = 3;
                    Spawner.maxWreckages =2;
                    Difficulty = "Normal";
                    break;
            }
        }
    }
}
