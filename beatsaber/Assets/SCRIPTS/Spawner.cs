using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] cubes;
    public Transform[] points;
    public float beat = (60 / 105f) * 2;
    public float timer;

    void Update()
    {
        if (timer > beat)
        {
            // Vyberme nahodny spawn point
            Transform spawnPoint = points[Random.Range(0, points.Length)];

            // Vytvorme krychli v nahodnem bode
            GameObject cube = Instantiate(cubes[Random.Range(0, cubes.Length)], spawnPoint.position, transform.rotation);

            // Pridani nahodneho otaceni kolem osi Z
            float randomRotation = 90f * Random.Range(0, 4); // 0, 90, 180 nebo 270 stupnu
            cube.transform.Rotate(Vector3.forward, randomRotation);

            timer -= beat;
        }

        timer += Time.deltaTime;
    }
}
