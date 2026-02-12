using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isFilled = false;
    public Color tileColor; //adding color tiles

    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.black; // empty at start
    }

    public void SetCoordinates(int x, int y)
    {
        
    }

    public void Fill(Color newColor)
    {
        isFilled = true;
        
        //sr.color = Color.white;
        
        //colorful tiles
        tileColor = newColor;
        sr.color = newColor;
    }

    public void Clear()
    {
        isFilled = false;
        sr.color = Color.black;
    }
}