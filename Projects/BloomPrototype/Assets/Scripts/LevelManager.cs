using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class ColorGoal
    {
        public Color color;
        public int targetCount;
    }

    [Header("Goal Settings")]
    public ColorGoal[] goals;
    
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

    //previous checkgoal (before i started working on phase 2
    /*void CheckGoal()
    {
        if (goalReached) return;

        int currentCount = GridManager.Instance.CountTilesOfColor(goalColor);
		bool isFull = GridManager.Instance.IsGridFull();

        if (currentCount >= targetCount && isFull)
        {
            Debug.Log("Goal Reached!");
            goalReached = true;
        }
    }*/
    
    void CheckGoal()
    {
        if (goalReached) return;

        bool allGoalsMet = true;

        foreach (ColorGoal goal in goals)
        {
            int count = GridManager.Instance.CountTilesOfColor(goal.color);

            if (count < goal.targetCount)
            {
                allGoalsMet = false;
                break;
            }
        }

        bool isFull = GridManager.Instance.IsGridFull();

        if (allGoalsMet && isFull)
        {
            Debug.Log("Goal Reached!");
            goalReached = true;
        }
    }
    
    IEnumerator HandlePhaseComplete()
    {
        Debug.Log("Phase complete...");

        yield return new WaitForSeconds(0.5f); 

        // ResetGrid(); // fordebugging  / testing before phase 2 was added 

		SceneManager.LoadScene("Phase2");
    }
    
    /*void OnGUI()
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
    }*/
    
    string GetColorName(Color color)
    {
        if (ColorsAreEqual(color, Color.red)) return "Red";
        if (ColorsAreEqual(color, Color.blue)) return "Blue";
        if (ColorsAreEqual(color, Color.yellow)) return "Yellow";

        if (ColorsAreEqual(color, new Color(0f, 1f, 0f))) return "Green";
        if (ColorsAreEqual(color, new Color(1f, 0.5f, 0f))) return "Orange";
        if (ColorsAreEqual(color, new Color(0.5f, 0f, 0.5f))) return "Purple";

        if (ColorsAreEqual(color, Color.white)) return "White";

        return "Unknown";
    }
    
    //NEW GUI
    void OnGUI()
    {
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one * 2f);

        float yOffset = 10;

        foreach (ColorGoal goal in goals)
        {
            int count = GridManager.Instance.CountTilesOfColor(goal.color);

            GUI.Label(new Rect(10, yOffset, 300, 30),
                GetColorName(goal.color) +  count + " / " + goal.targetCount);

            yOffset += 30;
        }

        yOffset += 40;

        if (goalReached)
        {
            if (GUI.Button(new Rect(10, yOffset, 150, 50), "Next Phase"))
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