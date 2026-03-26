using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [Header("Goal Settings")]

    public Color goalColor = Color.yellow;
    
    public int targetCount = 10;
    
    //colours can be close enough and still work
    bool ColorsAreEqual(Color a, Color b)
    {
        return Mathf.Approximately(a.r, b.r) &&
               Mathf.Approximately(a.g, b.g) &&
               Mathf.Approximately(a.b, b.b);
    }
    
    bool goalReached = false;

    void Start()
    {
        
    }
    
    void Update()
    {
        CheckGoal();
    }

    void CheckGoal()
    {
        if (goalReached) return;

        int currentCount = GridManager.Instance.CountTilesOfColor(goalColor);
		bool isFull = GridManager.Instance.IsGridFull();

        if (currentCount >= targetCount && isFull)
        {
            Debug.Log("Goal Reached!");
            goalReached = true;
        }
    }
    
    IEnumerator HandlePhaseComplete()
    {
        Debug.Log("Resetting grid...");

        yield return new WaitForSeconds(0.5f); // small pause 

        ResetGrid();
    }
    
    void OnGUI()
    {
        int count = GridManager.Instance.CountTilesOfColor(goalColor);
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one * 2f);
        GUI.Label(new Rect(10, 10, 200, 30), "Yellow: " + count + " / " + targetCount);

        if (goalReached)
        {
            if (GUI.Button(new Rect(10, 50, 120, 40), "Next Phase"))
            {
                StartCoroutine(HandlePhaseComplete());
            }
        }
    }
    
    void ResetGrid()
    {
        GridManager.Instance.ClearGrid();
        GridManager.Instance.SeedCenter();

        goalReached = false;
    }
    
}