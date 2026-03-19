using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Tile targetTile;
    public Color goalColor = Color.yellow;

    public float growthSpeed = 0.5f;

    void Update()
    {
        CheckWinCondition();
    }

    void CheckWinCondition()
    {
        if (targetTile != null && targetTile.isFilled && targetTile.tileColor == goalColor)
        {
            Debug.Log("Level Complete!");
        }
    }
}