using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Å¥ºê")]
    [SerializeField] public GameObject cubePrefab;
    [SerializeField] public Transform cubeTransform;
    [SerializeField] public List<GameObject> cubeTiles;
    

    void Start()
    {
        CreateCubes();
    }

    void CreateCubes()
    {
        for (int x = -9; x <= 9; x += 2)
        {
            for (int z = 9; z >= -9; z -= 2)
            {
                Vector3 position = new Vector3(x, 0, z);
                cubeTiles.Add(Instantiate(cubePrefab, position, Quaternion.identity, cubeTransform));
            }
        }
    }

    void Update()
    {
        
    }
}
