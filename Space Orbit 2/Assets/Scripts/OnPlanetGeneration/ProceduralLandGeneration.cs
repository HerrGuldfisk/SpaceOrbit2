using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class ProceduralLandGeneration : MonoBehaviour
{
    [SerializeField] int _width, _height;
    [SerializeField] float _smoothness;
    [SerializeField] float _seed;
    [SerializeField] TileBase _groundTile;
    [SerializeField] Tilemap _groundTileMap;

    int[,] _map;

    void Start()
    {
        _map = GenerateArray(_width, _height, true);
        _map = TerrainGeneration(_map);
        RenderMap(_map, _groundTileMap, _groundTile);
    }


    void Update()
    {
        
    }

    private int[,] GenerateArray(int width, int height, bool empty)
    {
        int[,] map = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = empty ? 0 : 1;
            }
        }
        return map;
    }

    private int[,] TerrainGeneration(int[,] map)
    {
        int perlinHeight;

        for (int x = 0; x < _width; x++)
        {
            perlinHeight = Mathf.RoundToInt(Mathf.PerlinNoise(x / _smoothness, _seed) * _height / 2);
            perlinHeight += _height / 2;
            for (int y = 0; y < perlinHeight; y++)
            {
                map[x, y] = 1;
            }
        }
        return map;
    }

    private void RenderMap(int[,] map, Tilemap groundTileMap, TileBase groundTileBase)
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (map[x, y] == 1)
                {
                    groundTileMap.SetTile(new Vector3Int(x, y, 0), groundTileBase);
                }
            }
        }
    }
}
