using UnityEngine;
using System.Collections;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [Header("Grid Settings")]
    public GameObject tilePrefab; 
    public int width = 8;
    public int height = 8;

    private Tile[,] grid;

    [Header("Growth Settings")]
    public float spawnInterval = 1f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GenerateGrid();
        StartCoroutine(GrowthRoutine());
        
        // seed a starting tile
        int startX = width / 2;
        int startY = height / 2;

        grid[startX, startY].Fill(Random.ColorHSV());
    }

    void GenerateGrid()
    {
        grid = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Center the grid
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
    
    void Update()
    {
        
        //does not work, idk why 
        if (Input.GetMouseButtonDown(0)) // left mouse click
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 clickPos = new Vector2(worldPoint.x, worldPoint.y);

            RaycastHit2D hit = Physics2D.Raycast(clickPos, Vector2.zero);
            if (hit.collider != null)
            {
                Tile tile = hit.collider.GetComponent<Tile>();
                if (tile != null)
                {
                    tile.Clear(); // clear the tile when clicked
                }
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
        // Pick random tile
        int x = Random.Range(0, width);
        int y = Random.Range(0, height);

        Tile tile = grid[x, y];

        // skip if already filled
        if (tile.isFilled) return;

        int neighbors = CountNeighbors(x, y);

        float growChance = neighbors > 0 ? 0.8f : 0.3f;

        if (Random.value < growChance)
        {
            //tile.Fill(); 
            
            //new colors
            Color chosenColor = ChooseGrowthColor(x, y);
            tile.Fill(chosenColor);
        }
    }

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
                    if (grid[nx, ny].isFilled)
                        count++;
                }
            }
        }

        return count;
    }
    
    Color ChooseGrowthColor(int x, int y)
    {
        // collect neighbor colors
        System.Collections.Generic.List<Color> neighborColors = new System.Collections.Generic.List<Color>();
    
        for(int dx = -1; dx <= 1; dx++)
        {
            for(int dy = -1; dy <= 1; dy++)
            {
                if(dx == 0 && dy == 0) continue;
    
                int nx = x + dx;
                int ny = y + dy;
    
                if(nx >= 0 && nx < width && ny >= 0 && ny < height)
                {
                    Tile neighbor = grid[nx, ny];
    
                    if(neighbor.isFilled)
                    {
                        neighborColors.Add(neighbor.tileColor);
                    }
                }
            }
        }
    
        // If neighbors exist=
        if(neighborColors.Count > 0)
        {
            // 70% same color
            if(Random.value < 0.7f)
            {
                return neighborColors[Random.Range(0, neighborColors.Count)];
            }
    
            // 30% mutatation 
            return Random.ColorHSV(
                0f,1f,
                0.7f,1f,
                0.7f,1f
            );
        }
    
        // No neighbors=
        return Color.white;
    }

    
}
