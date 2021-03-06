using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System.IO;

public class endGameCheck : MonoBehaviour
{
    [SerializeField] private Text score;
    private string tempNum;
    private Stopwatch timer;
    private string path;

    void Start()
    {
        path = Application.persistentDataPath + "Highscores.txt"; // Path leads to %userprofile%\AppData\LocalLow\<companyname>\<productname>
        timer = new Stopwatch();
        timer.Start();        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject tempObj = GameObject.FindGameObjectWithTag("PlayerName");
        Text playerName = tempObj.GetComponent<Text>();

        StreamWriter writer = new StreamWriter(path, true);
        
        if (score.text.Substring(0, 1) == "9")
        {
            string time = timer.Elapsed.ToString();
            timer.Stop();

            tempNum = score.text.Substring(0, 1);

            score.text = "Congratulations " + playerName.text + ", you WON!\nPress q to quit.\n" + time;
            
            writer.WriteLine("\n"  + playerName.text +  ": Completed in " + time);
            writer.Close();
        }
        else
        {
            tempNum = score.text.Substring(0, 1);
            score.text = "Not enough exams collected \nyet, get some more first!";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey("q") && score.text.Substring(0, 1) == "C")
        {
            Application.Quit();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        score.text = tempNum + "/9 Exams Collected";
    }
}
