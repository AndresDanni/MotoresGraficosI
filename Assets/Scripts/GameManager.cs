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
    public TextMeshProUGUI ratingButtonText;
    public TextMeshProUGUI startLevelText;

    public AudioClip startSound;
    public AudioClip resetSound;
    public AudioClip buttonSound;
    public AudioClip playerSpawnSound;

    // Start is called before the first frame update
    void Start()
    {
        PlayMusic();

        if (SceneManager.GetActiveScene().name == "Level1")
        {
            Cursor.visible = false;
            Time.timeScale = 0.0f;
        }
        else
        {
            Cursor.visible = true;
        }

        if (SceneManager.GetActiveScene().name == "StartScreen")
            SetHighScoreText();
        if (SceneManager.GetActiveScene().name == "WinScreen")
            ratingButtonText.text = SetRatingText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if (Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().name == "Level1" && startLevelText.enabled)
        {
            Time.timeScale = 1.0f;
            GetComponent<AudioSource>().PlayOneShot(playerSpawnSound);
            startLevelText.text = "¡El juego ha comenzado!";
            StartCoroutine(StartLevelTextVanish());
        }
    }

    private IEnumerator StartLevelTextVanish()
    {
        yield return new WaitForSeconds(2.0f);
        startLevelText.enabled = false;
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
        yield return new WaitForSeconds(1.0f);
        startButtonText.text = "cargando...";
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

    public string SetRatingText()
    {
        int rating = PlayerPrefs.GetInt("Rating");

        if (rating <= 3)
        {
            return "Calificación: F";
        }
        else if (rating <= 6)
        {
            return "Calificación: E";
        }
        else if (rating <= 9)
        {
            return "Calificación: D";
        }
        else if (rating <= 12)
        {
            return "Calificación: C";
        }
        else if (rating <= 15)
        {
            return "Calificación: B";
        }
        else if (rating <= 18)
        {
            return "Calificación: A";
        }
        else if (rating >= 19)
        {
            return "Calificación: S";
        }

        return "";
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
