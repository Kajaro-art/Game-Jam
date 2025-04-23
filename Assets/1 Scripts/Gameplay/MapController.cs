using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public Transform followTarget;                        // Player or camera to follow

    public List<GameObject> backgroundObjs;               // Keep continous background 
    public List<GameObject> terrainPrefabs;               // Prefabs to pick from
    public float terrainWidth = 37f;                      // Distance between terrain pieces

    private float spawnXPosition = 0;                     // Tracks the next X spawn point
    private readonly int piecesAhead = 6;                 // How far ahead to generate
    public int currentBackground = 0;

    private Queue<GameObject> spawnedTerrains = new();
    private Queue<GameObject> spawnedBackgrounds = new(); // To handle cleanup if needed
    public int maxPieces = 10;                            // Max pieces allowed

    void Start()
    {
        // Spawn initial pieces
        for (int i = 0; i < piecesAhead; i++)
        {
            SpawnNextPiece();
        }
    }

    void Update()
    {
        if (followTarget.position.x + (piecesAhead * terrainWidth) > spawnXPosition)
        {
            SpawnNextPiece();
        }
    }

    void SpawnNextPiece()
    {
        GameObject back = Instantiate(backgroundObjs[currentBackground], new Vector3(spawnXPosition, -2.4138f, 0), Quaternion.identity);
        spawnedBackgrounds.Enqueue(back);
        if (currentBackground >= backgroundObjs.Count - 1)
        {
            currentBackground = 0;
        }
        else
        {
            currentBackground++;
        }

        int index = Random.Range(0, terrainPrefabs.Count);
        GameObject piece = Instantiate(terrainPrefabs[index], new Vector3(spawnXPosition, 0, 0), Quaternion.identity);
        spawnedTerrains.Enqueue(piece);
        spawnXPosition += terrainWidth;

        if (spawnedTerrains.Count > maxPieces)
        {
            Destroy(spawnedTerrains.Dequeue());
            Destroy(spawnedBackgrounds.Dequeue());
        }
    }
}
