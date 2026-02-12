using UnityEngine;
using System.Collections;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public GameObject tilePrefab;

    public int width = 8;
    public int height = 8;

    private Tile[,] grid;

    public float spawnInterval = 2f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GenerateGrid();
        StartCoroutine(GrowthRoutine());
    }

    void GenerateGrid()
    {
        grid = new Tile[width, height];

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                // Calculate position so the grid is centered
                Vector2 position = new Vector2(
                    x - width / 2f + 0.5f,  // horizontal offset
                    y - height / 2f + 0.5f  // vertical offset
                );

                GameObject tileObj = Instantiate(tilePrefab, position, Quaternion.identity);


                Tile tile = tileObj.GetComponent<Tile>();
                grid[x,y] = tile;

                tile.SetCoordinates(x,y);
            }
        }
    }

    IEnumerator GrowthRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnInterval);

            TryGrow();
        }
    }

    void TryGrow()
    {
        int x = Random.Range(0, width);
        int y = Random.Range(0, height);

        Tile tile = grid[x,y];

        if(tile.isFilled) return;

        int neighbors = CountNeighbors(x,y);

        float growChance = neighbors > 0 ? 0.8f : 0.3f;

        if(Random.value < growChance)
        {
            tile.Fill();
        }
    }

    int CountNeighbors(int x, int y)
    {
        int count = 0;

        for(int dx = -1; dx <= 1; dx++)
        {
            for(int dy = -1; dy <= 1; dy++)
            {
                if(dx == 0 && dy == 0) continue;

                int nx = x + dx;
                int ny = y + dy;

                if(nx >= 0 && nx < width && ny >= 0 && ny < height)
                {
                    if(grid[nx,ny].isFilled)
                        count++;
                }
            }
        }

        return count;
    }
}