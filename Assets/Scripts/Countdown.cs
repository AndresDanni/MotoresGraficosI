using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    public TextMeshProUGUI timeButtonText;

    private int second = 0;
    public int countDown = 99;

    // Start is called before the first frame update
    void Start()
    {
        timeButtonText.text = "Tiempo restante: " + countDown;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        second++;

        if (second == 50)
        {
            second = 0;
            countDown--;
            timeButtonText.text = "Tiempo restante: " + countDown;
        }

        if (countDown == 0)
        {
            if (GameObject.Find("Player").GetComponent<PlayerScript>().bananas > PlayerPrefs.GetInt("Bananas"))
                PlayerPrefs.SetInt("Bananas", GameObject.Find("Player").GetComponent<PlayerScript>().bananas);
            SceneManager.LoadScene("LoseScreen");
        }
    }
}
