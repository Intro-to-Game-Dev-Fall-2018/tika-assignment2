using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static float scale = 2;
    public BoardManager boardScript;
    public int level;

    int goals;
    Text winText;
    Text timeText;
    private float timeCounter;
    private int timeConvert;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        
        if (SceneManager.GetActiveScene().name.Equals("Level"))
        {
            timeCounter = 90f;
        }
        
        else  if (SceneManager.GetActiveScene().name.Equals("Level2"))
        {
            timeCounter = 75f;
        }

        else
        {
            timeCounter = 60f;
        }

        
        winText = GameObject.Find("WinText").GetComponent<Text>();
        timeText = GameObject.Find("TimeText").GetComponent<Text>();
       
        boardScript = GetComponent<BoardManager>();
        goals = boardScript.SetupBoard(level);
    }

    public void Update()
    {
        timeCounter -= Time.deltaTime;
        timeConvert = (int) timeCounter;
        timeText.text = timeConvert.ToString();
        
        if (timeCounter <= 0f)
        {
            SceneManager.LoadScene("BadEnd");
        }
    }

    public void CheckWin()
    {
        int currentGoals = 0;
        GameObject[] crates = GameObject.FindGameObjectsWithTag("Crate");
        foreach (GameObject crate in crates)
        {
            if (crate.GetComponent<CrateController>().onGoal)
            {
                currentGoals += 1;
                winText.text = currentGoals.ToString() + "/8";
            }
        }

        if (currentGoals == goals)
        {
            if (SceneManager.GetActiveScene().name.Equals("Level"))
            {
                SceneManager.LoadScene("Menu"); 
            }
            
            else if (SceneManager.GetActiveScene().name.Equals("Level2"))
            {
                SceneManager.LoadScene("Level"); 
            }

            else
            {
                SceneManager.LoadScene("Level2");
            }        
        }

        
    }
}