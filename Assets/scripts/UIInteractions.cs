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
    bool BarrageOnCd;
    float timeLeft;
    public float CoolDownTime;
    float delay;
    public float Delay;
    public GameObject BotPrefab;
    public Transform MarkObject;
    public static Transform Mark;
    public Text second;
    public Text minutes;
    public float time;
    public float second1;
    public float minuta1;
    public static bool GameIsOver;
    public Canvas Description;
    public Button LaunchButton;
   

    void Awake()
    {
        Mark = MarkObject;
        Enemies = new List<Collider2D>();
        BarrageOnCd = false;
    }
    private void Update()
    {
        if (!GameIsOver) FullGameTime();


        if (timeLeft > 0) timeLeft -= Time.deltaTime;

        if (timeLeft < 0)
        {
            BarrageOnCd = false;
            LaunchButton.interactable = true;
            LaunchButton.GetComponentInChildren<Text>().color = LaunchButton.colors.normalColor;
        }

        if (delay > 0) delay -= Time.deltaTime;
        else if (delay < 0.000000 && !GameIsOver)
        {
            GameObject.Find("Scripts").GetComponent<Spawn>().enabled = true;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButton(0))
        {
            Description.enabled = false;
            GameObject.Find("Scripts").GetComponent<Spawn>().enabled = true;
            GameObject.Find("Scripts").GetComponent<GameDifficulty>().enabled = true;
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
        if (!BarrageOnCd)
        {
            LaunchButton.GetComponentInChildren<Text>().color = LaunchButton.colors.disabledColor;
            LaunchButton.interactable = false;
            BarrageOnCd = true;
            timeLeft += CoolDownTime;
            delay += 8f;
            Enemies.Clear();
            Vector3 pos = SpaceShip.transform.position;
            foreach (var Col in Physics2D.OverlapCircleAll(pos, 500))
            {
                if (Col.tag == "Alien") Enemies.Add(Col);
            }
            StopSpawn();
            StartCoroutine(LaunchBarrageRoutine(pos));
        }
    }
    IEnumerator LaunchBarrageRoutine(Vector2 pos)
    {
        foreach (Collider2D Enemy in Enemies)
        {
            GameObject.Find("Scripts").GetComponent<Missile>().AutoAim(pos, Enemy);
            yield return new WaitForSeconds(0.08f);
        }
    }
    public static void StopSpawn()
    {
        GameObject.Find("Scripts").GetComponent<Spawn>().enabled = false;
    }
    public  static void ShowMark()
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
    public static void GameOver()
    {
            GameIsOver = true;
            //ShowButtons();
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