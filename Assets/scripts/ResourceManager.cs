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
    public static bool delay = false;
    public float burnRate = 5;
    void Update()
    {
        StatCollectionSender();
        FuelController();
        if(delay)EmergencyFuelRestore();
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
            FuelBarWarning(FuelStatus.Normal);
        else if(Fuel < 65 && Fuel > 35)
            FuelBarWarning(FuelStatus.Warning);
        else if (Fuel < 35)
            FuelBarWarning(FuelStatus.High);

        FuelRemainsCounter(Fuel);
    }
    public void Thrust()
    {
        EngineController EngContrl = FindObjectOfType<EngineController>();
        if(EngContrl.isActive)Fuel -= burnRate * Time.deltaTime;
    }
    public static IEnumerator AvoidThrust(float fuelRequired)
    {
        for (int i = 0; i < fuelRequired; i++)
        {
            Fuel -= 1f;
            yield return new WaitForSeconds(0.05f);
        }
    }
    public static void EmergencyFuelRestore()
    {
        Fuel += 20f * Time.deltaTime;
        if(Fuel >=100) delay = false;
    }
    public static void StartDelay()
    {
      delay = true;
    }
    
    public enum FuelStatus
    { 
        Normal = 0
        ,Warning = 1
        ,High = 2
    }
    #endregion
}