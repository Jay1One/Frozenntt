using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject player;
    [SerializeField] private float distanceOfSpawn = 10f;
    private float beginPoint=-30f;
    private bool spawn = false;
    void Start()
    {
        beginPoint = player.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y > beginPoint)
        {
            beginPoint += distanceOfSpawn;
            var Enemy = Instantiate(enemy,
                new Vector3(enemy.transform.position.x+Random.Range(-4f,4f), beginPoint+Random.Range(2f,-2f), enemy.transform.position.z),
                Quaternion.identity);
            var Platform=Instantiate(platform,
                new Vector3(platform.transform.position.x+Random.Range(-2f,2f), beginPoint+distanceOfSpawn, enemy.transform.position.z),
                Quaternion.identity);
            
        }
        // if (player.transform.position.y > beginPoint && player.transform.position.y<beginPoint+distanceOfSpawn && spawn==false)
        // {
        //     
        //     Spawn();
        //     spawn = true;
        //     beginPoint = transform.position.y;
        // }
        //
        // if (player.transform.position.y > beginPoint+distanceOfSpawn)
        // {
        //     spawn = false;
        //    
        // }
    }

    private IEnumerator Spawn()
    {
        // print(beginPoint);
        // print(distanceOfSpawn);
        // var Enemy = GameObject.Instantiate(enemy,
        //     new Vector3(enemy.transform.position.x+Random.Range(-4f,4f), beginPoint+distanceOfSpawn, enemy.transform.position.z),
        //     Quaternion.identity);
         return null;
    }
}
