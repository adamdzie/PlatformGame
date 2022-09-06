using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score_Script : MonoBehaviour
{
    public  int TankCount ;
    public  int PenguinCount ;
    public int PenguinValue;
    public int TankValue;
    //public int score;
    public Text ScoreText;
    public Text TankText;
    public Text PenguinText;
    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = "0";
        TankText.text = "0";
        PenguinText.text = "0";
        TankCount = 0;
        PenguinCount = 0;
      //  score = 0;

        
    }

    // Update is called once per frame
    void Update()
    {
        TankCount= int.Parse(TankText.text);
        PenguinCount = int.Parse(PenguinText.text);
        string score = (TankCount * TankValue + PenguinCount * PenguinValue).ToString();
        ScoreText.text = score;
    }
    
}
