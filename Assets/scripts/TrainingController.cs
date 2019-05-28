using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TrainingController : MonoBehaviour
{
    public GameObject TurorialBox;
    public GameObject player;
    public float WordDelay = 0.08f;
    public string Greetings;
    public string TrainInvitation;
    public string Maneuvering;
    public string Temperature;
    public string Shooting;
    public string RocketBarrage;
    public string Drone;
    public string FinishTurorial;
    private string currentText = "";
    private int TrainingProgress;
    private bool done;
    private bool isTimerFinished = false;
    public float ReadTime = 3;

    void Start()
    {
        DisablePlayer(false);
        TrainingProgress = (int)TurorialProgress.Greetings;
        StartCoroutine(DispayTutorialText(TrainingProgress, Alignment.MiddleCenter));
        TurorialBox.GetComponentInChildren<Button>().gameObject.SetActive(true);
        TurorialBox.GetComponentInChildren<Button>().gameObject.SetActive(false);

    }
    private void Update()
    {
        if (done && ReadTime > 0) ReadTime -= Time.deltaTime;
        if (ReadTime <= 0) TurorialBox.GetComponentInChildren<Button>().gameObject.SetActive(true);

        if (done && ReadTime <= 0 && TrainingProgress == 3)
            {
                StartCoroutine(CountDown(5f));
                done = false;
                TurorialBox.SetActive(false);
                DisablePlayer(true);
            }
            else if (isTimerFinished)
            {
                done = true;
                TurorialBox.SetActive(true);
                DisablePlayer(false);
            }
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
            TrainingProgress++;
            TextController();
        }
    }
    private void DisablePlayer(bool state)
    {
        player.GetComponentInChildren<Player>().enabled = state;
    }

    private void TextController()
    { 
       StartCoroutine(DispayTutorialText(TrainingProgress, Alignment.UpperLeft));
    }
    IEnumerator DispayTutorialText(int Progress, Alignment alignment)
    {
        string fullText = "";
        switch ((int)Progress)
        {
            case 0:
                fullText = this.Greetings;
                break;
            case 1:
                fullText = this.TrainInvitation;
                break;
            case 2:
                fullText = this.Maneuvering;
                break;
            case 3:
                fullText = this.Temperature;
                break;
            case 4:
                fullText = this.Shooting;
                break;
            case 5:
                fullText = this.RocketBarrage;
                break;
            case 6:
                fullText = this.Drone;
                break;
            case 7:
                fullText = this.FinishTurorial;
                break;
        }
        switch (alignment)
        {
            case Alignment.MiddleCenter:
                TurorialBox.GetComponentInChildren<Text>().alignment = TextAnchor.MiddleCenter;
                break;
            case Alignment.UpperLeft:
                TurorialBox.GetComponentInChildren<Text>().alignment = TextAnchor.UpperLeft;
                break;
        }
        for (int i = 0; i < fullText.Length + 1; i++)
        {
            currentText = fullText.Substring(0, i);
            TurorialBox.GetComponentInChildren<Text>().text = currentText;
            yield return new WaitForSeconds(WordDelay);
        }
        done = true;
        ReadTime = 3;
    }
    public enum Alignment
    {
         UpperLeft = 0
        ,MiddleCenter = 1
    }
    public enum TurorialProgress
    {
        Greetings = 0
        ,TrainInvitation = 1
        ,Maneuvering = 2
        ,Temperature = 3
        ,Shooting = 4
        ,RocketBarrage = 5
        ,Drone = 6
        ,FinishTurorial = 7
    }
}