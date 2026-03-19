using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [Header("Grid Settings")] public GameObject tilePrefab;
    public int width = 8;
    public int height = 8;
    
    //get a tile at a coordinate (level completion mechanic)
    public Tile GetTileAt(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
            return grid[x, y];
        return null;
    }

    private Tile[,] grid;

    [Header("Growth Settings")] public float spawnInterval = 1f;
    
    int CountNeighbors(int x, int y)
    {
        int count = 0;

        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;

                int nx = x + dx;
                int ny = y + dy;

                if (nx >= 0 && nx < width && ny >= 0 && ny < height)
                {
                    if (grid[nx, ny].isFilled) count++;
                }
            }
        }

        return count;
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GenerateGrid();
        StartCoroutine(GrowthRoutine());

        // Seed a starting tile in the center
        int startX = width / 2;
        int startY = height / 2;
        grid[startX, startY].Fill(Color.white); // start with white
    }

    void GenerateGrid()
    {
        grid = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 position = new Vector2(
                    x - width / 2f + 0.5f,
                    y - height / 2f + 0.5f
                );

                GameObject tileObj = Instantiate(tilePrefab, position, Quaternion.identity);
                Tile tile = tileObj.GetComponent<Tile>();
                grid[x, y] = tile;
                tile.SetCoordinates(x, y);
            }
        }
    }

    IEnumerator GrowthRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            TryGrowRandomTile();
        }
    }

    void TryGrowRandomTile()
    {
        // Pick a random tile
        int x = Random.Range(0, width);
        int y = Random.Range(0, height);

        Tile tile = grid[x, y];

        // Skip if already filled
        if (tile.isFilled) return;

        int neighbors = CountNeighbors(x, y);
        float growChance = neighbors > 0 ? 0.8f : 0.3f;

        if (Random.value < growChance)
        {
            Color newColor;

            // Tiered mutation logic
            if (tileColorNeighbors(x, y).Count == 0)
            {
                // No neighbors = only white
                newColor = Color.white;
            }
            else
            {
                // If neighbors = pick a neighbor's color and apply mutation
                List<Color> neighborColors = tileColorNeighbors(x, y);
                Color baseColor = neighborColors[Random.Range(0, neighborColors.Count)];

                // Temporarily set tile color to neighbor => tiered mutation
                tile.tileColor = baseColor;
                newColor = tile.GetMutationColor();
            }

            tile.Fill(newColor);
        }
    }

    // Helper = list of neighbor colors that are filled
    List<Color> tileColorNeighbors(int x, int y)
    {
        List<Color> neighborColors = new List<Color>();

        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;

                int nx = x + dx;
                int ny = y + dy;

                if (nx >= 0 && nx < width && ny >= 0 && ny < height)
                {
                    Tile neighbor = grid[nx, ny];
                    if (neighbor.isFilled) neighborColors.Add(neighbor.tileColor);
                }
            }
        }

        return neighborColors;
    }
    
}