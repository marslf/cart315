using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [Header("Goal Settings")]
    public Vector2Int targetPosition = new Vector2Int(7, 7); 
    public Color goalColor = Color.yellow;
    public GameObject goalMarkerPrefab;
    
    //colours can be close enough and still be fine
    bool ColorsAreEqual(Color a, Color b)
    {
        return Mathf.Approximately(a.r, b.r) &&
               Mathf.Approximately(a.g, b.g) &&
               Mathf.Approximately(a.b, b.b);
    }

    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return null; // force to wait 1 frame 
        //PlaceGoalMarker();
    }

    void PlaceGoalMarker()
    {
        Tile targetTile = GridManager.Instance.GetTileAt(targetPosition.x, targetPosition.y);

        if (targetTile != null)
        {
            Debug.Log("Placing marker at: " + targetTile.transform.position);
            Instantiate(goalMarkerPrefab, targetTile.transform.position, Quaternion.identity);
        }
    }
    
    void Update()
    {
        CheckWinCondition();
    }

    void CheckWinCondition()
    {
        Tile targetTile = GridManager.Instance.GetTileAt(targetPosition.x, targetPosition.y);

        if (targetTile != null && targetTile.isFilled && ColorsAreEqual(targetTile.tileColor, goalColor))
        {
            Debug.Log("Level Complete!");
        }
        
         //DEBUGGING WIN CONDITION
        // if (targetTile != null)
        // {
        //     Debug.Log("Checking tile at: " + targetPosition + 
        //               " | Filled: " + targetTile.isFilled + 
        //               " | Color: " + targetTile.tileColor);
        // }
        
    }
}