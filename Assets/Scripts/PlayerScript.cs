using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public float speed = 1.0f;
    public int bananas = 0;

    private Rigidbody playerRb;

    public TextMeshProUGUI scoreButtonText;
    public Button pointButton;

    public TMP_Text W_Input;
    public TMP_Text S_Input;
    public TMP_Text A_Input;
    public TMP_Text D_Input;

    public Transform vehicle;

    public AudioClip[] picking;
    public AudioClip[] footstep;

    private bool footstepCheck;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        bananas = 0;
        footstepCheck = true;
    }

    private IEnumerator FootstepSound()
    {
        footstepCheck = false;
        GetComponent<AudioSource>().PlayOneShot(footstep[Random.Range(0, 2)]);
        yield return new WaitForSeconds(0.4f);
        footstepCheck = true;
    }

    // Update is called once per frame
    void Update()
    {
        float WS_Input = Input.GetAxis("Vertical");
        float AD_Input = Input.GetAxis("Horizontal");

        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
        {
            playerRb.AddRelativeForce((Vector3.forward * WS_Input * speed * Time.deltaTime) + (Vector3.right * AD_Input * speed * Time.deltaTime), ForceMode.Force);
            playerRb.velocity = Vector3.ClampMagnitude(playerRb.velocity, 8.0f);
            //if (GetComponent<AudioSource>().isPlaying == false)
            //    GetComponent<AudioSource>().PlayOneShot(footstep[Random.Range(0, 2)]);
            if (footstepCheck)
                StartCoroutine(FootstepSound());
        }
        else
        {
            playerRb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.W))
            W_Input.color = Color.red;
        if (Input.GetKeyUp(KeyCode.W))
            W_Input.color = Color.green;
        if (Input.GetKeyDown(KeyCode.S))
            S_Input.color = Color.red;
        if (Input.GetKeyUp(KeyCode.S))
            S_Input.color = Color.green;
        if (Input.GetKeyDown(KeyCode.A))
            A_Input.color = Color.red;
        if (Input.GetKeyUp(KeyCode.A))
            A_Input.color = Color.green;
        if (Input.GetKeyDown(KeyCode.D))
            D_Input.color = Color.red;
        if (Input.GetKeyUp(KeyCode.D))
            D_Input.color = Color.green;

        if (this.transform.position.z < 0.0f)
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0.0f);
        if (this.transform.position.z >= 320.0f)
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 320.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Animal":
                if (bananas > PlayerPrefs.GetInt("Bananas"))
                    PlayerPrefs.SetInt("Bananas", bananas);
                SceneManager.LoadScene("LoseScreen");
                break;
            case "Banana":
                other.gameObject.SetActive(false);
                GetComponent<AudioSource>().PlayOneShot(picking[Random.Range(0, picking.Length)]);
                bananas++;
                scoreButtonText.text = "Bananas encontradas: " + bananas;
                scoreButtonText.color = Color.red;
                //Destroy(other.gameObject);
                pointButton.gameObject.SetActive(true);
                StartCoroutine(HidePointButton());
                break;
            case "Look":
                this.transform.LookAt(vehicle);
                break;
            case "Finish":
                if (bananas > PlayerPrefs.GetInt("Bananas"))
                    PlayerPrefs.SetInt("Bananas", bananas);
                PlayerPrefs.SetInt("Rating", bananas);
                SceneManager.LoadScene("WinScreen");
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Look")
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    private IEnumerator HidePointButton()
    {
        yield return new WaitForSeconds(1.0f);
        scoreButtonText.color = Color.green;
        yield return new WaitForSeconds(1.0f);
        pointButton.gameObject.SetActive(false);
    }
}
