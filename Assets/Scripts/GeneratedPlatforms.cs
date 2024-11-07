using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedPlatforms : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab; // Jeden prefabrykat dla platform
    const int PLATFORMS_NUM = 9;
    private GameObject[] platforms;
    private Vector2[] positions;
    private int[] targetIndices; // Indeksy docelowych pozycji
    [SerializeField] private float radius = 3.0f; // Promieñ okrêgu
    [SerializeField] private float speed = 2.0f;  // Prêdkoœæ ruchu platform

    private void Awake()
    {
        platforms = new GameObject[PLATFORMS_NUM];
        positions = new Vector2[PLATFORMS_NUM];
        targetIndices = new int[PLATFORMS_NUM];
    }

    void Start()
    {
        for (int i = 0; i < PLATFORMS_NUM; i++)
        {
            float angle = i * Mathf.PI * 2 / PLATFORMS_NUM;
            float x = Mathf.Cos(angle) * radius + transform.position.x;
            float y = Mathf.Sin(angle) * radius + transform.position.y;
            positions[i] = new Vector2(x, y);

            platforms[i] = Instantiate(platformPrefab, new Vector3(x, y, transform.position.z), Quaternion.identity);
            targetIndices[i] = (i + 1) % PLATFORMS_NUM; // Ka¿da platforma pocz¹tkowo celuje w nastêpn¹
        }
    }

    void Update()
    {
        for (int i = 0; i < PLATFORMS_NUM; i++)
        {
            platforms[i].transform.position = Vector3.MoveTowards(
                platforms[i].transform.position,
                positions[targetIndices[i]],
                speed * Time.deltaTime
            );

            // Jeœli platforma osi¹gnie swoj¹ docelow¹ pozycjê, zaktualizuj jej docelowy indeks
            if (platforms[i].transform.position == (Vector3)positions[targetIndices[i]])
            {
                targetIndices[i] = (targetIndices[i] + 1) % PLATFORMS_NUM;
            }
        }
    }
}
