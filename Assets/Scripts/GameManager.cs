using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Button startButton;
    public TextMeshProUGUI startButtonText;
    public TextMeshProUGUI scoreButtonText;

    public AudioClip startSound;
    public AudioClip resetSound;
    public AudioClip buttonSound;

    // Start is called before the first frame update
    void Start()
    {
        PlayMusic();

        if (SceneManager.GetActiveScene().name == "Level1")
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }

        if (SceneManager.GetActiveScene().name == "StartScreen")
            SetHighScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ResetHighScore()
    {
        scoreButtonText.text = "HScore: 0";
        PlayerPrefs.SetInt("Bananas", 0);
    }

    public void StartButtonCount()
    {
        GetComponent<AudioSource>().PlayOneShot(startSound);
        Cursor.visible = false;
        startButton.GetComponent<Image>().color = Color.black;
        startButtonText.color = Color.green;
        startButtonText.text = "5";
        StartCoroutine("WaitStart");
    }

    private IEnumerator WaitStart()
    {
        yield return new WaitForSeconds(1.0f);
        startButtonText.text = "4";
        yield return new WaitForSeconds(1.0f);
        startButtonText.text = "3";
        yield return new WaitForSeconds(1.0f);
        startButtonText.text = "2";
        yield return new WaitForSeconds(1.0f);
        startButtonText.text = "1";
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayMusic()
    {
        GetComponent<AudioSource>().Play();
    }

    public void SetHighScoreText()
    {
        if (PlayerPrefs.GetInt("Bananas") > 0)
            scoreButtonText.text = "HScore: " + PlayerPrefs.GetInt("Bananas");
    }

    public void ResetButtonSound()
    {
        GetComponent<AudioSource>().PlayOneShot(resetSound);
    }

    public void ButtonSound()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
    }
}
