using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isFilled = false;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.black; // empty at start
    }

    public void SetCoordinates(int x, int y)
    {
        
    }

    public void Fill()
    {
        isFilled = true;
        sr.color = Color.white;
    }

    public void Clear()
    {
        isFilled = false;
        sr.color = Color.black;
    }
}