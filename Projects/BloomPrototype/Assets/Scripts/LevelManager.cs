using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    int currentPhase = 1; // define phase (goal setting)

    bool ColorsAreEqual(Color a, Color b)
    {
        return Mathf.Approximately(a.r, b.r) &&
               Mathf.Approximately(a.g, b.g) &&
               Mathf.Approximately(a.b, b.b);
    }

    bool goalReached = false;

    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Phase1") currentPhase = 1;
        else if (sceneName == "Phase2") currentPhase = 2;
        else if (sceneName == "Phase3") currentPhase = 3;
        else if (sceneName == "Phase4") currentPhase = 4;
    }

    void Update()
    {
        CheckGoal();
    }


    void CheckGoal()
    {
        if (goalReached) return;

        bool isFull = GridManager.Instance.IsGridFull();

        if (currentPhase == 1) //PHASE 1 OBJECTIVES
        {
            int yellow = GridManager.Instance.CountTilesOfColor(Color.yellow);

            if (yellow >= 10 && isFull)
            {
                Debug.Log("Phase 1 Complete!");
                goalReached = true;
            }
        }
        else if (currentPhase == 2) //PHASE 2 OBJECTIVES
        {
            int blue = GridManager.Instance.CountTilesOfColor(Color.blue);
            int purple = GridManager.Instance.CountTilesOfColor(new Color(0.5f, 0f, 0.5f));

            if (blue >= 10 && purple >= 5 && isFull)
            {
                Debug.Log("Phase 2 Complete!");
                goalReached = true;
            }
        }
        else if (currentPhase == 3) //PHASE 3 OBJECTIVES //TEMPORARY = CHANGE LATER + DONT FORGET TO UPDATE GUI
        {
            int blue = GridManager.Instance.CountTilesOfColor(Color.blue);
            int purple = GridManager.Instance.CountTilesOfColor(new Color(0.5f, 0f, 0.5f));

            if (blue >= 5 && purple >= 5 && isFull)
            {
                Debug.Log("Phase 3 Complete!");
                goalReached = true;
            }
        }
        else if (currentPhase == 4) //PHASE 4 OBJECTIVES //TEMPORARY = CHANGE LATER + DONT FORGET TO UPDATE GUI
        {
            int blue = GridManager.Instance.CountTilesOfColor(Color.blue);
            int purple = GridManager.Instance.CountTilesOfColor(new Color(0.5f, 0f, 0.5f));

            if (blue >= 5 && purple >= 5 && isFull)
            {
                Debug.Log("Phase 4 Complete!");
                goalReached = true;
            }
        }
    }

    IEnumerator HandlePhaseComplete()
    {
        Debug.Log("Phase complete...");

        yield return new WaitForSeconds(0.5f);

        // ResetGrid(); // for debugging 

        SceneManager.LoadScene("Phase2");
    }


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

    void OnGUI()
    {
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one * 2f);

        float yOffset = 10;

        if (currentPhase == 1)
        {
            int yellow = GridManager.Instance.CountTilesOfColor(Color.yellow);
            GUI.Label(new Rect(10, yOffset, 300, 30), "Yellow " + yellow + " / 10");
        }
        else if (currentPhase == 2)
        {
            int blue = GridManager.Instance.CountTilesOfColor(Color.blue);
            int purple = GridManager.Instance.CountTilesOfColor(new Color(0.5f, 0f, 0.5f));

            GUI.Label(new Rect(10, yOffset, 300, 30), "Blue " + blue + " / 10");
            yOffset += 30;
            GUI.Label(new Rect(10, yOffset, 300, 30), "Purple " + purple + " / 5");
        }
        else if (currentPhase == 3)
        {
            int blue = GridManager.Instance.CountTilesOfColor(Color.blue);
            int purple = GridManager.Instance.CountTilesOfColor(new Color(0.5f, 0f, 0.5f));

            GUI.Label(new Rect(10, yOffset, 300, 30), "Blue " + blue + " / 5"); //CHANGE LATER
            yOffset += 30;
            GUI.Label(new Rect(10, yOffset, 300, 30), "Purple " + purple + " / 5"); //CHANGE LATER
        }
        else if (currentPhase == 4)
        {
            int blue = GridManager.Instance.CountTilesOfColor(Color.blue);
            int purple = GridManager.Instance.CountTilesOfColor(new Color(0.5f, 0f, 0.5f));

            GUI.Label(new Rect(10, yOffset, 300, 30), "Blue " + blue + " / 5"); //CHANGE LATER
            yOffset += 30;
            GUI.Label(new Rect(10, yOffset, 300, 30), "Purple " + purple + " / 5"); //CHANGE LATER
        }

        yOffset += 50;

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