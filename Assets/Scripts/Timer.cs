using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public Text timerText;
    private float startime;
    public Player_controller instance;

    void Start()
    {
        instance = Player_controller.instance;
        startime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (instance.current_health>0) {
            float t = Time.time - startime;
            string minuty = ((int)t / 60).ToString();
            string sekundy = ((int)(t % 60)).ToString();
            timerText.text = "Czas: " + minuty + ":" + sekundy;
        }
    }

}
