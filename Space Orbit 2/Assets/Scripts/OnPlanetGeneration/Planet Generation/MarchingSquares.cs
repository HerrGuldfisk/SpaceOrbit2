using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MarchingSquares : MonoBehaviour
{
    [SerializeField] int _sizeX = 40;
    [SerializeField] int _sizeY = 20;
    [SerializeField] int _worldOffsetX = 0;
    [SerializeField] int _worldOffsetY = 0;

    [SerializeField] float _noiseResolution = 0.1f;
    [SerializeField] float _gridResolution = 1f;
    [SerializeField] float _heightThreshold = 0.5f;

    private float[,] _heights;

    private MeshFilter _meshFilter;
    private ColliderCreator _colliderCreator;

    private List<Vector3> _vertices = new List<Vector3>();
    private List<int> _triangles = new List<int>();

    // Debug purposes
    [SerializeField] Transform _dotParent;
    [SerializeField] Transform _dotPrefab;


    void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _colliderCreator = GetComponent<ColliderCreator>();

        SetHeights();
        MarchSquares();
        CreateMesh();
        _colliderCreator.GenerateCollider();
        CreateGrid();
    }

    
    void Update()
    {
        
    }

    private void CreateMesh()
    {
        Mesh mesh = new Mesh();

        mesh.vertices = _vertices.ToArray();
        mesh.triangles = _triangles.ToArray();

        _meshFilter.mesh = mesh;

        
    }

    private void MarchSquares()
    {
        _vertices.Clear();
        _triangles.Clear();

        for(int x = 0; x < _sizeX; x++)
        {
            for (int y = 0; y < _sizeY; y++)
            {
                float a = _heights[x, y];
                float b = _heights[x + 1, y];
                float c = _heights[x + 1, y + 1];
                float d = _heights[x, y + 1];

                MarchSquare(GetHeight(a), GetHeight(b), GetHeight(c), GetHeight(d), x + _worldOffsetX, y + _worldOffsetY);
            }
        }
    }

    void MarchSquare(int a, int b, int c, int d, float offsetX, float offsetY)
    {
        int value = a * 8 + b * 4 + c * 2 + d * 1;

        Vector3[] verticesLocal = new Vector3[6];
        int[] trianglesLocal = new int[6];
        int vertexCount = _vertices.Count;

        switch (value)
        {
            case 0:
                return;

            case 1:
                verticesLocal = new Vector3[]
                { new Vector3(0, 0.5f), new Vector3(0, 1f), new Vector3(0.5f, 1f) };

                trianglesLocal = new int[]
                { 0, 1, 2};
                break;

            case 2:
                verticesLocal = new Vector3[]
                { new Vector3(1, 1), new Vector3(1, 0.5f), new Vector3(0.5f, 1) };

                trianglesLocal = new int[]
                { 0, 1, 2};
                break;

            case 3:
                verticesLocal = new Vector3[]
                { new Vector3(0, 0.5f), new Vector3(0, 1), new Vector3(1, 1), new Vector3(1, 0.5f) };

                trianglesLocal = new int[]
                { 0, 1, 2, 0, 2, 3};
                break;

            case 4:
                verticesLocal = new Vector3[]
                { new Vector3(1, 0), new Vector3(0.5f, 0), new Vector3(1, 0.5f) };

                trianglesLocal = new int[]
                { 0, 1, 2};
                break;

            case 5:
                verticesLocal = new Vector3[]
                { new Vector3(0, 0.5f), new Vector3(0, 1), new Vector3(0.5f, 1), new Vector3(1, 0), new Vector3(0.5f, 0), new Vector3(1, 0.5f) };

                trianglesLocal = new int[]
                { 0, 1, 2, 3, 4, 5};
                break;

            case 6:
                verticesLocal = new Vector3[]
                { new Vector3(0.5f, 0), new Vector3(0.5f, 1), new Vector3(1, 1), new Vector3(1, 0) };

                trianglesLocal = new int[]
                { 0, 1, 2, 0, 2, 3};
                break;

            case 7:
                verticesLocal = new Vector3[]
                { new Vector3(0, 1), new Vector3(1, 1), new Vector3(1, 0), new Vector3(0.5f, 0), new Vector3(0, 0.5f) };

                trianglesLocal = new int[]
                { 2, 3, 1, 3, 4, 1, 4, 0, 1};
                break;

            case 8:
                verticesLocal = new Vector3[]
                { new Vector3(0, 0.5f), new Vector3(0, 0), new Vector3(0.5f, 0) };

                trianglesLocal = new int[]
                { 2, 1, 0};
                break;

            case 9:
                verticesLocal = new Vector3[]
                { new Vector3(0, 0), new Vector3(0.5f, 0), new Vector3(0.5f, 1), new Vector3(0, 1) };

                trianglesLocal = new int[]
                { 1, 0, 2, 0, 3, 2};
                break;

            case 10:
                verticesLocal = new Vector3[]
                { new Vector3(0, 0), new Vector3(0, 0.5f), new Vector3(0.5f, 0), new Vector3(1, 1), new Vector3(0.5f, 1), new Vector3(1, 0.5f) };

                trianglesLocal = new int[]
                { 0, 1, 2, 5, 4, 3};
                break;

            case 11:
                verticesLocal = new Vector3[]
                { new Vector3(0, 0), new Vector3(0, 1), new Vector3(1, 1), new Vector3(1, 0.5f), new Vector3(0.5f, 0) };

                trianglesLocal = new int[]
                { 0, 1, 2, 0, 2, 3, 4, 0, 3};
                break;

            case 12:
                verticesLocal = new Vector3[]
                { new Vector3(0, 0), new Vector3(1, 0), new Vector3(1, 0.5f), new Vector3(0, 0.5f) };

                trianglesLocal = new int[]
                { 0, 3, 2, 0, 2, 1};
                break;

            case 13:
                verticesLocal = new Vector3[]
                { new Vector3(0, 0), new Vector3(0, 1), new Vector3(0.5f, 1), new Vector3(1, 0.5f), new Vector3(1, 0) };

                trianglesLocal = new int[]
                { 0, 1, 2, 0, 2, 3, 0, 3, 4};
                break;

            case 14:
                verticesLocal = new Vector3[]
                { new Vector3(1, 1), new Vector3(1, 0), new Vector3(0, 0), new Vector3(0, 0.5f), new Vector3(0.5f, 1) };

                trianglesLocal = new int[]
                { 0, 1, 4, 1, 3, 4, 1, 2, 3};
                break;

            case 15:
                verticesLocal = new Vector3[]
                { new Vector3(0, 0), new Vector3(0, 1), new Vector3(1, 1), new Vector3(1, 0) };

                trianglesLocal = new int[]
                { 0, 1, 2, 0, 2, 3};
                break;
        }

        foreach(Vector3 vert in verticesLocal)
        {
            Vector3 newVert = new Vector3((vert.x + offsetX) * _gridResolution, (vert.y + offsetY) * _gridResolution, 0);
            _vertices.Add(newVert);
        }

        foreach(int triangle in trianglesLocal)
        {
            _triangles.Add(triangle + vertexCount);
        }
    }

    private int GetHeight(float value)
    {
        return value < _heightThreshold ? 0 : 1;
    }

    void SetHeights()
    {
        _heights = new float[_sizeX + 1, _sizeY + 1];

        for (int x = 0; x < _sizeX; x++)
        {
            for (int y = 0; y < _sizeY; y++)
            {
                _heights[x, y] = Mathf.PerlinNoise((x + transform.position.x) * _noiseResolution, (y + transform.position.y) * _noiseResolution);
            }
        }
    }

    void CreateGrid()
    {
        if(_dotParent == null) { return; }

        foreach ( Transform child in _dotParent)
        {
            Destroy(child.gameObject);
        }

        for (int x= 0; x < _sizeX; x++)
        {
            for (int y = 0; y < _sizeY; y++)
            {
                Vector2 pos = transform.TransformPoint(new Vector2(x * _gridResolution, y * _gridResolution));
                Transform newDot = Instantiate(_dotPrefab, pos, new Quaternion(), _dotParent);
                newDot.localScale = Vector2.one * _gridResolution / 2;
                newDot.GetComponent<SpriteRenderer>().color = new Color(_heights[x, y], _heights[x, y], _heights[x, y], 1);
            }
        }
    }
}
