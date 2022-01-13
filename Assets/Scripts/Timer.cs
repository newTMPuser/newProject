using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public int time;
    Text timer;
    public GameObject lose;

    void Start()
    {
        timer = GetComponent<Text>();
        changeTime();
        StartCoroutine(timer_());
    }

    void changeTime()
    {
        timer.text = (time / 60).ToString() + ":";
        if (time % 60 >= 10)
            timer.text += (time % 60).ToString();
        else
            timer.text += "0" + (time % 60).ToString();
    }

    IEnumerator timer_()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1f);
            time--;
            changeTime();
        }

        Time.timeScale = 0;
        lose.SetActive(true);
    }
}
