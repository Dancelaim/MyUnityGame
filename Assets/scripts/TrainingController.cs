using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TrainingController : MonoBehaviour
{
    public GameObject TurorialBox;
    public GameObject player;
    public float WordDelay = 0.08f;
    private string currentText = "";
    private bool done = true;
    private bool isTimerFinished = false;
    public float ReadTime = 3;
    public int TrainingFinished = 0;
    public string Text1;
    public string Text2;
    public string Text3;
    public string Text4;
    bool FirstStep = false;
    bool SecondStep = false;
    bool ThirdStep = false;
    bool ForthStep = false;

    public void Start()
    {
        TurorialBox.gameObject.SetActive(false);
        GameObject.Find("Scripts").GetComponent<Spawn>().enabled = false;
        DisablePlayer(true);
        TurorialBox.GetComponentInChildren<Button>(true).gameObject.SetActive(false);
    }
    private void Update()
    {
        if (TrainingFinished < 20 & Input.touchCount > 0 || TrainingFinished < 20 & Input.GetMouseButton(0))
        {
            TrainingFinished = 21;
            TurorialBox.gameObject.SetActive(true);
            StartCoroutine(DispayTutorialText(Text1));
            FirstStep = true;
        }
        if (FirstStep && TrainingFinished < 40 & player.gameObject.GetComponent<Player>().Temp > 65)
        {
            DisablePlayer(true);
            TrainingFinished = 41;
            TurorialBox.gameObject.SetActive(true);
            StartCoroutine(DispayTutorialText(Text2));
            SecondStep = true;
            GameObject.Find("Scripts").GetComponent<Spawn>().maxEnemies = 1;
            GameObject.Find("Scripts").GetComponent<Spawn>().enabled = true;
        }
        if (SecondStep && TrainingFinished <60)
        {

        }

        if (done && ReadTime > 0) ReadTime -= Time.deltaTime;
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
            DisablePlayer(false);
        }
    }
    private void DisablePlayer(bool state)
    {
        player.GetComponentInChildren<Player>().enabled = !state;
    }

    IEnumerator DispayTutorialText(string TextToShow)
    {
        ReadTime += 3;
        string fullText= TextToShow;
        for (int i = 0; i < fullText.Length + 1; i++)
        {
            currentText = fullText.Substring(0, i);
            TurorialBox.GetComponentInChildren<Text>().text = currentText;
            yield return new WaitForSeconds(WordDelay);
        }
        done = true;
        
    }
}