using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    private int Score;
    public Text ScoreText;
    public Image FuelBar;
    public Image HpBar;
    public Sprite[] ShipSchematics;
    private int CurrentHp;
    public static float Fuel = 100;
    public static float burnRate = 15;
    public static bool delay = false;

    void Update()
    {
        StatCollectionSender();
        FuelController();
    }
    public void ScoreCounter(int reward)
    {
        if (ScoreText)
        {
            Score += reward;
            ScoreText.text = Score.ToString();
        }
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
    public void StatCollectionSender()
    {
        var Stats = FindObjectOfType<GameDifficulty>();
        if (Stats)
        {
            Stats.StatCollection(CurrentHp, Score);
        }
    }
    #region Fuel Controller
    public void FuelRemainsCounter(float Fuel)
    {
        FuelBar.fillAmount = Fuel / 100;
    }
    public void FuelBarWarning(FuelStatus Fuel)
    {
        switch(Fuel)
            {
            case FuelStatus.High:
                FuelBar.color = new Color(174, 0f, 0f, Random.Range(0.6f, 0.9f));
                break;
            case FuelStatus.Warning:
                FuelBar.color = new Color(1, 1, 0f, 0.6f);
                break;
            default:
                FuelBar.color = new Color(1f, 0.5615012f, 0f, 0.6f);
                break;
            }
    }

    public void FuelController()
    {
        if (Fuel >65)
            FuelBarWarning(ResourceManager.FuelStatus.Normal);
        else if(Fuel < 65 && Fuel > 35)
            FuelBarWarning(ResourceManager.FuelStatus.Warning);
        else if (Fuel < 35)
            FuelBarWarning(ResourceManager.FuelStatus.High);

        FuelRemainsCounter(Fuel);
    }
    public static void Thrust()
    {
        Fuel -= burnRate * Time.deltaTime;
    }
    private static void EmergencyFuelRestore()
    {
        Fuel += 25f * Time.deltaTime;
    }

    public static void StartDelay()
    {
        if (Fuel < 100)
        {
            delay = true;
            EmergencyFuelRestore();
        }
        else delay = false;
    }
    public enum FuelStatus
    {
        Normal = 0
        ,Warning = 1
        ,High = 2
    }
    #endregion
}