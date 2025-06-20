using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField] private int gridWidth, gridHeight;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform cameraPosition;

    private Dictionary<Vector2, Tile> gridTiles;

    void Awake()
    {
        Instance = this;
    }

    internal void GenerateGrid()
    {
        gridTiles = new Dictionary<Vector2, Tile>();

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                var spawnTile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                spawnTile.name = $"Tile_{x}_{y}";

                var isOffset = (x + y) % 2 == 1;
                spawnTile.Initialize(isOffset);

                gridTiles[new Vector2(x, y)] = spawnTile;
            }
        }

        cameraPosition.transform.position = new Vector3(gridWidth / 2f - 0.5f, gridHeight / 2f - 0.5f, -10f);
    }

    public Tile GetTileAtPosition(Vector2 position)
    {
        if (gridTiles.TryGetValue(position, out Tile tile))
        {
            return tile;
        }
        return null;
    }
}
