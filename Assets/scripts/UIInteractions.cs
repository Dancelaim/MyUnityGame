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

    void Awake()
    {
        HideButtons();
        Enemies = new List<Collider2D>();
        BarrageCd = false;
    }
    private void Update()
    {
        if (timeLeft>0) timeLeft -= Time.deltaTime;
        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        else
        {
            GameObject Scripts = GameObject.Find("Scripts");
            Spawn Spawn = Scripts.GetComponent<Spawn>();
            Spawn.enabled = true;
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
            StartCoroutine(LaunchBarrageRoutine(pos,rocketLaunch));
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
}