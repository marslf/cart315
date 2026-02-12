using UnityEngine;
 
public class Tile : MonoBehaviour
{
    public bool isFilled = false;
 
    private SpriteRenderer sr;
 
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
 
    public void SetCoordinates(int x, int y)
    {
         
    }
 
    public void Fill()
    {
        isFilled = true;
        sr.color = Color.white;
    }
 
    void OnMouseDown()
    {
        // click to clear
        isFilled = false;
        sr.color = Color.black;
    }
}