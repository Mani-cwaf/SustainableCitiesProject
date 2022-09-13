using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Unity.VisualScripting;

public class GenerateTiles : MonoBehaviour
{
    public int length = 25;
    public int height = 16;
    public int[,] tilesPos;

    public Tile gridPrefab;
    public GameObject parent;
    public GameManager gameManager;

    public Dictionary<Tile, Vector3Int> TilePoses = new Dictionary<Tile, Vector3Int>();
    public Dictionary<Vector3Int, Tile> TilePosesInverse = new Dictionary<Vector3Int, Tile>();

    void Start()
    {
        tilesPos = new int[length, height];
        for(float x = 0; x < length; x++)
        {
            for(float y = 0; y < height; y++)
            {
                SpawnTile(x - 10.51f, y - 8f);
            }
        }
    }
    private void SpawnTile(float x, float y)
    {
        Tile gridGO = Instantiate(gridPrefab, new Vector3(x, y), Quaternion.identity, parent.transform);
        gameManager.tiles = gameManager.tiles.Append(gridGO).ToArray();
        TilePoses.Add(gridGO, new Vector3Int(x.ConvertTo<int>(), y.ConvertTo<int>()));
        TilePosesInverse.Add(new Vector3Int(x.ConvertTo<int>(), y.ConvertTo<int>()), gridGO);
    }
}
