﻿using System.Collections;
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
    public float PublicDelay;

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
            SpawnScript SpawnScript = Scripts.GetComponent<SpawnScript>();
            SpawnScript.enabled = true;
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
        // Reload the level
        SceneManager.LoadScene("Menu");
    }

    public void RestartGame()
    {
        // Reload the level
        SceneManager.LoadScene("Stage1");
    }

    public void LaunchBarrage()
    {
        if (!BarrageCd)
        {
            BarrageCd = true;
            timeLeft += CoolDownTime;
            delay += 10f;
            Enemies.Clear();
            Vector3 pos = SpaceShip.transform.position;
            foreach (var Col in Physics2D.OverlapCircleAll(pos, 50))
            {
                if (Col.tag == "Alien") Enemies.Add(Col);
            }
            GameObject Scripts = GameObject.Find("Scripts");
            RocketScript rocketLaunch = Scripts.GetComponent<RocketScript>();

            StopSpawnScript();
            StartCoroutine(LaunchBarrageRoutine(pos,rocketLaunch));
        }
        if (timeLeft < 1)
        {
            BarrageCd = false;
        }
    }

    IEnumerator LaunchBarrageRoutine(Vector2 pos, RocketScript rocketLaunch)
    {

            foreach (Collider2D Enemy in Enemies)
            {
                rocketLaunch.AutoAim(pos, Enemy);
                yield return new WaitForSeconds(0.07f);
            }
    }

    public void StopSpawnScript()
    {
        GameObject Scripts = GameObject.Find("Scripts");
        SpawnScript SpawnScript = Scripts.GetComponent<SpawnScript>();
        SpawnScript.enabled = false;
    }
}