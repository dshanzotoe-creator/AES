using UnityEngine;
using System.Collections;

public class MeteorHandler : MonoBehaviour
{

    [SerializeField] GameObject meteorPrefab;

    GameObject spawnedMeteor;

    GameObject player;

    [SerializeField] GameObject target;

    bool spawningMeteor = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnedMeteor = Instantiate(meteorPrefab, transform.position, Quaternion.identity);
        spawnedMeteor.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;

        if (spawnedMeteor.activeSelf == false && !spawningMeteor)
            StartCoroutine(EnableMeteor());
    }

    IEnumerator EnableMeteor()
    {
        spawningMeteor = true;
        yield return new WaitForSeconds(0.5f);
        spawnedMeteor.SetActive(true);
        spawningMeteor = false;
        yield return null;
        
    }

    private void OnEnable()
    {
        StartCoroutine(EnableMeteor());
    }

}
