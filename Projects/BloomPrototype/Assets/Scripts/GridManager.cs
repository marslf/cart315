using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [Header("Selection")] //NEW SELECTION MODE / CONTROLS 
    public GameObject selectionIndicatorPrefab;
    private GameObject selectionIndicatorInstance;
    private int selectedX = 0;
    private int selectedY = 0;
    
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

    public int currentPhase = 1; // define phase

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

    [Header("Growth Settings")] public float spawnInterval = 0.05f; // bigger number = slower

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
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (sceneName == "Phase1") currentPhase = 1;
        else if (sceneName == "Phase2") currentPhase = 2;
        else if (sceneName == "Phase3") currentPhase = 3;
        else if (sceneName == "Phase4") currentPhase = 4;

        GenerateGrid();
        StartCoroutine(GrowthRoutine());

        SeedCenter();
        
        //NEW SELECTION MODE / CONTROLS 
        selectedX = width / 2;
        selectedY = height / 2;

        CreateSelectionIndicator();
        UpdateSelectionVisual();
    }
    
    void Update()
    {
        HandleSelectionInput();
        HandleActionInput(); 
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
    
    
    // ---- NEW SELECTION MODE / CONTROLS ----
    void CreateSelectionIndicator() 
    {
        if (selectionIndicatorPrefab != null)
        {
            selectionIndicatorInstance = Instantiate(
                selectionIndicatorPrefab,
                Vector3.zero,
                Quaternion.identity
            );
        }
    }
    
    void UpdateSelectionVisual()
    {
        Tile selectedTile = GetTileAt(selectedX, selectedY);

        if (selectedTile != null && selectionIndicatorInstance != null)
        {
            selectionIndicatorInstance.transform.position = selectedTile.transform.position;

            Debug.Log("Selected tile: " + selectedX + ", " + selectedY);
        }
    }
    
    void HandleSelectionInput()
    {
        int newX = selectedX;
        int newY = selectedY;

        if (Input.GetKeyDown(KeyCode.RightArrow)) newX++;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) newX--;
        if (Input.GetKeyDown(KeyCode.UpArrow)) newY++;
        if (Input.GetKeyDown(KeyCode.DownArrow)) newY--;
        
        newX = Mathf.Clamp(newX, 0, width - 1);
        newY = Mathf.Clamp(newY, 0, height - 1);
        
        if (newX != selectedX || newY != selectedY)
        {
            selectedX = newX;
            selectedY = newY;

            UpdateSelectionVisual();
        }
    }
    
    void HandleActionInput()
    {
        Tile selectedTile = GetTileAt(selectedX, selectedY);

        if (selectedTile == null || !selectedTile.isFilled) return;

        // DELETE / PRUNE (SPACE)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("PRUNE at " + selectedX + ", " + selectedY);
            selectedTile.Clear();
        }

        // WATER (A)
        if (Input.GetKeyDown(KeyCode.A) && currentPhase = 3)
        {
            Debug.Log("WATER at " + selectedX + ", " + selectedY);
            selectedTile.Water();
        }

        // FERTILIZE (D)
        if (Input.GetKeyDown(KeyCode.D) && currentPhase = 4)
        {
            Debug.Log("FERTILIZE at " + selectedX + ", " + selectedY);
            selectedTile.Fertilize();
        }
    }
    
    //  -----------------------
    
    IEnumerator GrowthRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            TryGrowRandomTile();
        }
    }

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

            // LIMIT COLORS IN PHASE 1
            if (currentPhase == 1)
            {
                if (!(newColor == Color.white || newColor == Color.red || newColor == Color.blue ||
                      newColor == Color.yellow))
                {
                    newColor = Color.white;
                }
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

    //WATER MECHANIC
    public void ApplyWaterEffect(Tile sourceTile)
    {
        Debug.Log("Applying water effect");
        
        int x = sourceTile.gridX;
        int y = sourceTile.gridY;

        Color baseColor = sourceTile.tileColor;

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

                    if (!neighbor.isFilled)
                    {
                        neighbor.Fill(baseColor); // only cloning (no mutation)
                    }
                }
            }
        }
    }
    
    // FERTILIZER MECHANIC
    public void ApplyFertilizerEffect(Tile sourceTile)
    {
        Debug.Log("Applying fertilizer effect");

        int x = sourceTile.gridX;
        int y = sourceTile.gridY;

        Color baseColor = sourceTile.tileColor;

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

                    if (!neighbor.isFilled)
                    {
                        neighbor.tileColor = baseColor;
                        Color mutated = neighbor.GetMutationColor(); // mutates color
                        neighbor.Fill(mutated);
                    }
                }
            }
        }
    }
    
}