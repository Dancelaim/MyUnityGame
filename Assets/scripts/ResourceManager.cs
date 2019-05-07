using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    private int Score;
    public Text ScoreText;
    public Image TemperatureBar;
    public Image HpBar;
    public Sprite[] ShipSchematics;
    private int CurrentHp;
    private int MissedKills;

    void Update()
    {
        StatCollectionSender();
    }
    private void Awake()
    {
       var list = this.GetComponentInChildren<Image>();
    }

    public void MissedCounter()
    {
        MissedKills++;
    }

    //public void DifficultyDisplay(string Difficulty)
    //{
    //    if (DiffText)
    //    {
    //        DiffText.text = Difficulty;
    //    }
    //}

    public void ScoreCounter(int reward)
    {
        if (ScoreText)
        {
            Score += reward;
            ScoreText.text = Score.ToString();
        }
    }

    public void TempRemainsCounter(float Temp)
    {
        TemperatureBar.fillAmount = Temp/100;
    }

    public void HpBarSchema(int CurrentHp)
    {
        this.CurrentHp = CurrentHp;
        switch (CurrentHp)
        {
            case 5:
                HpBar.sprite = ShipSchematics[0];
                break;
            case 4:
                HpBar.sprite = ShipSchematics[1];
                break;
            case 3:
                HpBar.sprite = ShipSchematics[2];
                break;
            case 2:
                HpBar.sprite = ShipSchematics[3];
                break;
            case 1:
                HpBar.sprite = ShipSchematics[4];
                break;
        } 
    }

    public void TemperatureBarWarning(bool HightTemp= false,bool WarningTemp = false)
    {
        if (HightTemp)
        {
                float c = Random.Range(0.3f, 0.6f);
            TemperatureBar.color = new Color(174, 0f, 0f, c);
            
        }
        else if (WarningTemp)
        {
            TemperatureBar.color = new Color(1, 1, 0f, 0.7f);
        }
        else
        {
            TemperatureBar.color = new Color(0.03921569f, 0.509804f, 0.9411765f, 0.5490196f);
        }
    }
    public void StatCollectionSender()
    {
        var Stats = FindObjectOfType<GameDifficulty>();
        if (Stats)
        {
            Stats.StatCollection(CurrentHp, Score, MissedKills);
        }
    }
}