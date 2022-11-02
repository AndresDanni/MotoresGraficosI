using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] animales;

    private float spawnTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAnimals());
    }

    // Update is called once per frame
    void Update()
    {
        /*spawnTime -= Time.deltaTime;

        if (spawnTime <= 0.0f)
        {
            Instantiate(animales[Random.Range(0, animales.Length)], new Vector3(Random.Range(-21.0f, 21.0f), 0.0f, GameObject.Find("Player").transform.position.z + 80.0f), animales[0].transform.rotation);
            if (GameObject.Find("Player").transform.position.z < 289.9f)
                Instantiate(animales[Random.Range(0, animales.Length)], new Vector3(GameObject.Find("Player").transform.position.x, 0.0f, GameObject.Find("Player").transform.position.z + 80.0f), animales[0].transform.rotation);
            spawnTime = Random.Range(1.0f, 1.5f);
        }*/
    }

    private IEnumerator SpawnAnimals()
    {
        yield return new WaitForSeconds(3.0f);
        while (true)
        {
            if (GameObject.Find("Player").transform.position.z <= 320.0f && (GameObject.Find("Player").transform.position.x <= 22.0f && GameObject.Find("Player").transform.position.x >= -23.0f))
                Instantiate(animales[Random.Range(0, animales.Length)], new Vector3(GameObject.Find("Player").transform.position.x, 0.0f, GameObject.Find("Player").transform.position.z + 80.0f), animales[0].transform.rotation);
            Instantiate(animales[Random.Range(0, animales.Length)], new Vector3(Random.Range(-22.0f, 21.0f), 0.0f, GameObject.Find("Player").transform.position.z + 80.0f), animales[0].transform.rotation);
            yield return new WaitForSeconds(Random.Range(0.75f, 1.0f));
        }
    }
}
