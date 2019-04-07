using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    private int Score;
    public Text ScoreText;
    public Text DiffText;
    public Text Temperature;
    public Sprite[] ShipSchematics;
    public Sprite[] FuelPanel;
    public Image HpBar;
    public Image TemperatureBar;
    private int CurrentHp;
    private int MissedKills;

    void Update()
    {
        StatCollectionSender();
    }

    public void MissedCounter()
    {
        MissedKills++;
    }

    public void DiffDisplay(string Diff)
    {
        if (DiffText)
        {
            DiffText.text = Diff;
        }
    }

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
        Temperature.text = string.Format("{0}", Mathf.Round(Temp));
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

    public void TemperatureBarWarning(bool HightTemp)
    {
        
        if (HightTemp)
        {
                float c = Random.Range(0.8f, 1f);
            TemperatureBar.color = new Color(1f, 0f, 0f, c);
            Temperature.color = new Color(1f, 0f, 0f, c);
            
        }
        else
        {
            TemperatureBar.color = new Color(0.03921569f, 0.509804f, 0.9411765f, 0.5490196f);
            Temperature.color = new Color(0.03921569f, 0.509804f, 0.9411765f, 1f);
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