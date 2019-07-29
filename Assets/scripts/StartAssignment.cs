using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class StartAssignment : MonoBehaviour
{
    public Sprite [] Stars;
    private string DifficultySet;
    public Image Mark;
    public Image Mark_opacity;
    public bool Done;
    public void DifficultySetter()
    {
        DifficultySet = GameObject.Find("Scripts").gameObject.GetComponent<GameDifficulty>().Difficulty;
    }
    private void Update()
    {
        GameTimeController();
    }
    public void GameTimeController()
    {
        UIInteractions interactionScript = GetComponent<UIInteractions>();
        if (interactionScript.GameIsOver)
        { 
            DifficultySetter();    
            AssignStar(DifficultySet);
        } 
    }
    public void AssignStar(string Difficulty)
    {
        switch (Difficulty)
        {
            case "ACE":
                StarDisplay(8);
                break;
            case "Hard":
                StarDisplay(6);
                break;
            case "Advanced":
                StarDisplay(4);
                break;
            case "Normal":
                StarDisplay(2);
                break;
            default:
                StarDisplay(0);
                break;
        }
    }
    IEnumerator StarAssignment(float start, float end)
    {
        for (float i = start; i < end; i += 0.01f)
        {
            Mark.fillAmount = i;
            yield return new WaitForSeconds(0.009f);
        }
    }
    public void StarDisplay(int StarAmount)
    {
        Mark_opacity.sprite = Stars[StarAmount];
        StarAmount++;
        Mark.sprite = Stars[StarAmount];
        StartCoroutine(Waiter(0.7f));
        if (Done)StartCoroutine(StarAssignment(0f, 1f));
    }
    IEnumerator Waiter(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        Done = true;
    }
}