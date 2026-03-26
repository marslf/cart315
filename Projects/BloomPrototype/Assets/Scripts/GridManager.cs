using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [Header("Grid Settings")] public GameObject tilePrefab;
    public int width = 8;
    public int height = 8;
    
    public int CountTilesOfColor(Color targetColor)
    {
        int count = 0;
    
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Tile tile = grid[x, y];
    
                if (tile.isFilled && ColorsAreEqual(tile.tileColor, targetColor))
                {
                    count++;
                }
            }
        }
    
        return count;
    }
    
    bool ColorsAreEqual(Color a, Color b)
    {
        return Mathf.Approximately(a.r, b.r) &&
               Mathf.Approximately(a.g, b.g) &&
               Mathf.Approximately(a.b, b.b);
    }

	public bool IsGridFull()
	{
    	for (int x = 0; x < width; x++)
    	{
        	for (int y = 0; y < height; y++)
            {
            	if (!grid[x, y].isFilled)
            	{
                	return false;
            	}
        	}
    	}

    	return true;
	}
    
    //get a tile at a coordinate (level completion mechanic)
    public Tile GetTileAt(int x, int y)
    {
        if (grid == null)
        {
            Debug.LogWarning("Grid not initialized yet");
            return null;
        }

        if (x >= 0 && x < width && y >= 0 && y < height)
            return grid[x, y];

        return null;
    }

    private Tile[,] grid;

    [Header("Growth Settings")] public float spawnInterval = 0.2f; // 1f = slower / 0.2f = faster growth
    
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

    public void ClearGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y].Clear();
            }
        }
    }

    public void SeedCenter()
    {
        int startX = width / 2;
        int startY = height / 2;
        grid[startX, startY].Fill(Color.white);
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

    // void TryGrowRandomTile()
    // {
    //     // Pick a random tile
    //     int x = Random.Range(0, width);
    //     int y = Random.Range(0, height);
    //
    //     Tile tile = grid[x, y];
    //
    //     // Skip if already filled
    //     if (tile.isFilled) return;
    //
    //     List<Color> neighbors = tileColorNeighbors(x, y);
    //
    //     float growChance = neighbors.Count > 0 ? 0.9f : 0.05f; // faster + less random white spawning
    //
    //     if (Random.value < growChance)
    //     {
    //         Color newColor;
    //
    //         if (neighbors.Count == 0)
    //         {
    //             // VERY rare white spawn 
    //             newColor = Color.white;
    //         }
    //         else
    //         {
    //             // Pick neighbor color
    //             Color baseColor = neighbors[Random.Range(0, neighbors.Count)];
    //
    //             // Use mutation logic based on that color
    //             tile.tileColor = baseColor;
    //             newColor = tile.GetMutationColor();
    //
    //             // LIMIT to only white + primaries (Level 0 + 1) (for first level)
    //             if (!(newColor == Color.white || newColor == Color.red || newColor == Color.blue || newColor == Color.yellow))
    //             {
    //                 newColor = baseColor;
    //             }
    //         }
    //
    //         tile.Fill(newColor);
    //     }
    // }
    
    // TryGrowRandomTile VERSION 1
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