using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Background_Manager : MonoBehaviour
{
    [Header("Attributes")]
    private GameObject player;
    [SerializeField] private GameObject backgroundPrefab;
    private GameObject background;
    [SerializeField] Transform spawnPoint;
    private Tilemap tileMap;
    private int tileW;
    private int tileH;
    private float camHalfH;
    private float camHalfW;

    void Start()
    {
        CalculateTileMapSize();
        CalcCameraBoundary();
    }

    public void Initialize(GameObject _player)
    {
        player = _player;
    }

    public void Awake()
    {
        background = Instantiate(backgroundPrefab, spawnPoint);
        tileMap = background.GetComponentInChildren<Tilemap>();
    }

    void HandleInfMap()
    {
        Vector3 offset = Vector3.zero;

        // 가로 방향
        float deltaX = player.transform.position.x - background.transform.position.x;
        Debug.Log("deltaX " + deltaX);
        if (deltaX > tileW / 2f)
        {
            offset.x = tileW*0.5f;
        }
        else if (deltaX < -tileW / 2f)
        {
            offset.x = -tileW*0.5f;
        }

        // 세로 방향
        float deltaY = player.transform.position.y - background.transform.position.y;

        if (deltaY > tileH / 2f)
        {
            offset.y = tileH*0.5f;
        }
        else if (deltaY < -tileH / 2f)
        {
            offset.y = -tileH*0.5f;
        }

        background.transform.position += offset;
    }

    void CalculateTileMapSize()
    {
        if (tileMap == null)
            return;

        Vector3Int min = new Vector3Int(int.MaxValue, int.MaxValue, 0);
        Vector3Int max = new Vector3Int(int.MinValue, int.MinValue, 0);

        foreach (var pos in tileMap.cellBounds.allPositionsWithin)
        {
            if (tileMap.HasTile(pos))
            {
                min = Vector3Int.Min(min, pos);
                max = Vector3Int.Max(max, pos);
            }
        }

        tileW = max.x - min.x + 1;
        tileH = max.y - min.y + 1;

        tileW /= 2;
        tileH /= 2;
    }

    void Update()
    {
        HandleInfMap();
    }

    private void CalcCameraBoundary()
    {
        Camera cam = Camera.main;

        camHalfH = cam.orthographicSize;

        camHalfW = camHalfH * cam.aspect;
    }
}
