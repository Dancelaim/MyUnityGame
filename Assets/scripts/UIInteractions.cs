using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIInteractions : MonoBehaviour
{
    public Button[] buttons;
    public GameObject SpaceShip;
    private List<Collider2D> Enemies;
    bool BarrageCd;
    float timeLeft;
    public float CoolDownTime;
    float delay;
    public float Delay;
    public GameObject BotPrefab;
    public Transform Mark;
    public bool GameIsOver;
    public Text second;
    public Text minutes;
    public float time;
    public float second1;
    public float minuta1;
    void Awake()
    { 
        Mark.gameObject.SetActive(false);
        HideButtons();
        Enemies = new List<Collider2D>();
        BarrageCd = false;
    }
    private void Update()
    {
        if (GameIsOver == false)
        {
            FullGameTime();
        }
        if (timeLeft > 0) timeLeft -= Time.deltaTime;
        if (timeLeft>0) timeLeft -= Time.deltaTime;

        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        else if (delay < 0.000000)
        {
            if (!GameIsOver)
            {
                GameObject.Find("Scripts").GetComponent<Spawn>().enabled = true;
            }
            GameObject.Find("Scripts").GetComponent<Spawn>().enabled = true;
        }
    }
    public void HideButtons()
    {
        foreach (var b in buttons)
        {
            b.gameObject.SetActive(false);
        }
    }
    public void ShowButtons()
    {
        foreach (var b in buttons)
        {
            b.gameObject.SetActive(true);
        }
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Level_1");
    }
    public void LaunchBarrage()
    {
        if (!BarrageCd)
        {
            BarrageCd = true;
            timeLeft += CoolDownTime;
            delay += 8f;
            Enemies.Clear();
            Vector3 pos = SpaceShip.transform.position;
            foreach (var Col in Physics2D.OverlapCircleAll(pos, 500))
            {
                if (Col.tag == "Alien") Enemies.Add(Col);
            }
            GameObject Scripts = GameObject.Find("Scripts");
            Missile rocketLaunch = Scripts.GetComponent<Missile>();

            StopSpawn();
            StartCoroutine(LaunchBarrageRoutine(pos, rocketLaunch));
        }
        if (timeLeft < 1)
        {
            BarrageCd = false;
        }
    }
    IEnumerator LaunchBarrageRoutine(Vector2 pos, Missile MissileLaunch)
    {
        foreach (Collider2D Enemy in Enemies)
        {
            MissileLaunch.AutoAim(pos, Enemy);
            yield return new WaitForSeconds(0.08f);
        }
    }
    public void StopSpawn()
    {
        GameObject Scripts = GameObject.Find("Scripts");
        Spawn SpawnScript = Scripts.GetComponent<Spawn>();
        SpawnScript.enabled = false;
    }
    public void LaunchRepairBots()
    {
        int botNumber = 8;
        StartCoroutine(LaunchRepairBotsRoutine(botNumber));
    }
    IEnumerator LaunchRepairBotsRoutine(int botNumber)
    {
        for (int i = 0; i < botNumber; i++)
        {
            Instantiate(BotPrefab);
            yield return new WaitForSeconds(0.02f);
        }
    }
    public void ShowMark()
    {
        Mark.gameObject.SetActive(true);
    }
    public void NextLvl()
    {
        SceneManager.LoadScene("Level_1");
    }
    public void StopPlayerMove()
    {
        GameObject.Find("spaceship").gameObject.GetComponent<Player>().enabled = false;
    }
    public void GameOver()
    {
        GameObject.Find("Background").gameObject.GetComponent<Scrolling>().enabled = false;
        GameIsOver = true;
        ShowButtons();
        StopSpawn();
        ShowMark();
        foreach (var enemy in Physics2D.OverlapCircleAll(new Vector3(0, 0, 0), 500))
        {
            if (enemy.tag == "Alien")
            {
                Destroy(enemy);
            }
        }
    }
    public void FullGameTime() 
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            time = 1;
            second1 = second1 + 1;
        }
        if (second1 >= 60)
        {
            minuta1 = minuta1 + 1;
            second1 = 0;
        }
        second.text = "" + second1;
        minutes.text = "" + minuta1 + "  :";
    }  
}