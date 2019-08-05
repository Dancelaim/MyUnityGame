using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TrainingController : MonoBehaviour
{
    public GameObject TurorialBox;
    public GameObject player;
    public float WordDelay = 0.08f;
    string currentText = "";
    public bool done = true;
    bool isTimerFinished = false;
    public float ReadTime = 3;
    public string Text1;
    public string Text2;
    public string Text3;
    public string Text4;
    public List<bool> Progress = new List<bool>();
    Enemy Alien;
    bool isDestroyed;

    public void Awake()
    {
        bool[] input = { false, false, false, false, false };
        Progress.AddRange(input);
        TurorialBox.gameObject.SetActive(false);
        GameObject.Find("Scripts").GetComponent<Spawn>().enabled = false;
        GameObject.Find("Scripts").GetComponent<GameDifficulty>().enabled = false;
        DisablePlayer(true,true);
        TurorialBox.GetComponentInChildren<Button>(true).gameObject.SetActive(false);
        Alien = FindObjectOfType<Enemy>();
        Alien.GetComponentInChildren<Move>().speed.x = 0;
        isDestroyed = false;
    }
    private void Update()
    {

        if (!Progress[0] && Input.touchCount > 0 || !Progress[0] && Input.GetMouseButton(0))
        {
            Progress[0] = true;
            TurorialBox.gameObject.SetActive(true);
            DisableAll(true);
            StartCoroutine(DispayTutorialText(Text1));
        }
        if (!Progress[1] && player && player.gameObject.GetComponent<Player>().Temp > 65)
        {
            Progress[1] = true;
            DisableAll(true);
            TurorialBox.gameObject.SetActive(true);
            StartCoroutine(DispayTutorialText(Text2));
        }
        if (!Progress[2]  && Alien && Alien.transform.position.x <= Camera.main.ViewportToWorldPoint(new Vector3(0.85f, 0f, 0f)).x)
        {
            Progress[2] = true;
            DisablePlayer(true,true);
            Alien.GetComponentInChildren<Move>().speed.x = 0;
            TurorialBox.gameObject.SetActive(true);
            StartCoroutine(DispayTutorialText(Text3));
        }
        if (!Progress[3] && player && player.gameObject.GetComponent<Health>().hp <5)
        {
            Progress[3] = true;
            DisableAll(true);
            TurorialBox.gameObject.SetActive(true);
            StartCoroutine(DispayTutorialText(Text4));
        }

        
        if (!Alien && !isDestroyed)
        {
            isDestroyed = true;
            GameObject.Find("Scripts").GetComponent<GameDifficulty>().enabled = true;
            GameObject.Find("Scripts").GetComponent<Spawn>().enabled = true;
        }

        if (ReadTime > 0) ReadTime -= Time.deltaTime;
        if (ReadTime <= 0) TurorialBox.GetComponentInChildren<Button>(true).gameObject.SetActive(true);

    }
    public IEnumerator CountDown(float Delay)
    {
        while (Delay > 0)
        {
            yield return new WaitForSeconds(1.0f);
            Delay--;
        }
        isTimerFinished = true;
    }

    public void NextClick()
    {
        if (done)
        {
            done = false;
            TurorialBox.gameObject.SetActive(false);
            TurorialBox.GetComponentInChildren<Button>(true).gameObject.SetActive(false);
            if(Progress[0] && !Progress[2])
            {
                DisablePlayer(false);
                if (Alien) Alien.GetComponentInChildren<Move>().speed.x = 10;
            }
            else if (Progress[2] && !Progress[3])
            {
                DisablePlayer(false, false);
                if (Alien) Alien.GetComponentInChildren<Collider2D>().enabled = Alien.GetComponentInChildren<Enemy>().enabled = true;
            }
            else 
            {
                DisableAll(false);
            }
        }
    }
    IEnumerator DispayTutorialText(string TextToShow)
    {
        ReadTime += 1.5f;
        string fullText= TextToShow;
        for (int i = 0; i < fullText.Length + 1; i++)
        {
            currentText = fullText.Substring(0, i);
            TurorialBox.GetComponentInChildren<Text>().text = currentText;
            yield return new WaitForSeconds(WordDelay);
        }
        done = true;
        
    }
    private void DisablePlayer(bool state, bool WeaponState = true)
    {
        player.GetComponentInChildren<Player>().enabled = !state;
        player.GetComponentInChildren<Weapon>().enabled = !WeaponState;
    }
    public void DisableAll(bool state)
    {
        DisablePlayer(state,state);
        GameObject.Find("Scripts").GetComponent<GameDifficulty>().enabled = !state;
        GameObject.Find("Scripts").GetComponent<Spawn>().enabled = !state;
        foreach (Enemy Alien in  FindObjectsOfType<Enemy>())
        {
            if (state)
            {
                Alien.enabled = !state;
                Alien.GetComponentInChildren<Move>().speed.x = 0;
            }
            else
            {
                Alien.enabled = !state;
                Alien.GetComponentInChildren<Move>().speed.x = 25;
            }
        }
        foreach (Wreckage wreckage in FindObjectsOfType<Wreckage>())
        {
            if (state)
            {
                wreckage.enabled = !state;
                wreckage.GetComponentInChildren<Move>().speed.x = 0;
            }
            else
            {
                wreckage.enabled = !state;
                wreckage.GetComponentInChildren<Move>().speed.x = 25;
            }
        }

    }
}