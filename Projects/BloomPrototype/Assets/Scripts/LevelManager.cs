using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public TextMeshProUGUI goalText;
    
    int currentPhase = 1; 

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
        UpdateGoalUI();
    }

// PHASE OBJECTIVES!!
void CheckGoal()
{
    if (goalReached) return;
    if (GridManager.Instance == null) return;

    bool isFull = GridManager.Instance.IsGridFull();

    if (currentPhase == 1) // PHASE 1: 8 yellow
    {
        int yellow = GridManager.Instance.CountTilesOfColor(Color.yellow);

        if (yellow >= 8 && isFull)
        {
            Debug.Log("Phase 1 Complete!");
            goalReached = true;
        }
    }
    else if (currentPhase == 2) // PHASE 2: 6 orange
    {
        int orange = GridManager.Instance.CountTilesOfColor(new Color(1f, 0.5f, 0f));

        if (orange >= 6 && isFull)
        {
            Debug.Log("Phase 2 Complete!");
            goalReached = true;
        }
    }
    else if (currentPhase == 3) // PHASE 3: 12 red + 5 purple
    {
        int red = GridManager.Instance.CountTilesOfColor(Color.red);
        int purple = GridManager.Instance.CountTilesOfColor(new Color(0.5f, 0f, 0.5f));

        if (red >= 12 && purple >= 5 && isFull)
        {
            Debug.Log("Phase 3 Complete!");
            goalReached = true;
        }
    }
    else if (currentPhase == 4) // PHASE 4: 10 blue + 10 yellow + 5 green
    {
        int blue = GridManager.Instance.CountTilesOfColor(Color.blue);
        int yellow = GridManager.Instance.CountTilesOfColor(Color.yellow);
        int green = GridManager.Instance.CountTilesOfColor(new Color(0f, 1f, 0f));

        if (blue >= 10 && yellow >= 10 && green >= 5 && isFull)
        {
            Debug.Log("Phase 4 Complete!");
            goalReached = true;
        }
    }
    else if (currentPhase == 5) // PHASE 5: 9 of EVERY color (except white)
    {
        int red = GridManager.Instance.CountTilesOfColor(Color.red);
        int blue = GridManager.Instance.CountTilesOfColor(Color.blue);
        int yellow = GridManager.Instance.CountTilesOfColor(Color.yellow);
        int orange = GridManager.Instance.CountTilesOfColor(new Color(1f, 0.5f, 0f));
        int green = GridManager.Instance.CountTilesOfColor(new Color(0f, 1f, 0f));
        int purple = GridManager.Instance.CountTilesOfColor(new Color(0.5f, 0f, 0.5f));

        if (red >= 9 && blue >= 9 && yellow >= 9 &&
            orange >= 9 && green >= 9 && purple >= 9 &&
            isFull)
        {
            Debug.Log("Phase 5 Complete!");
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

    void UpdateGoalUI()
    {
        if (GridManager.Instance == null) return;

        if (currentPhase == 1)
        {
            int yellow = GridManager.Instance.CountTilesOfColor(Color.yellow);
            goalText.text = "Yellow " + yellow + " / 8";
        }
        else if (currentPhase == 2)
        {
            int orange = GridManager.Instance.CountTilesOfColor(new Color(1f, 0.5f, 0f));
            goalText.text = "Orange " + orange + " / 6";
        }
        else if (currentPhase == 3)
        {
            int red = GridManager.Instance.CountTilesOfColor(Color.red);
            int purple = GridManager.Instance.CountTilesOfColor(new Color(0.5f, 0f, 0.5f));

            goalText.text = "Red " + red + " / 12\n" +
                            "Purple " + purple + " / 5";
        }
        else if (currentPhase == 4)
        {
            int blue = GridManager.Instance.CountTilesOfColor(Color.blue);
            int yellow = GridManager.Instance.CountTilesOfColor(Color.yellow);
            int green = GridManager.Instance.CountTilesOfColor(new Color(0f, 1f, 0f));

            goalText.text = "Blue " + blue + " / 10\n" +
                            "Yellow " + yellow + " / 10\n" +
                            "Green " + green + " / 5";
        }
        else if (currentPhase == 5)
        {
            int red = GridManager.Instance.CountTilesOfColor(Color.red);
            int blue = GridManager.Instance.CountTilesOfColor(Color.blue);
            int yellow = GridManager.Instance.CountTilesOfColor(Color.yellow);
            int orange = GridManager.Instance.CountTilesOfColor(new Color(1f, 0.5f, 0f));
            int green = GridManager.Instance.CountTilesOfColor(new Color(0f, 1f, 0f));
            int purple = GridManager.Instance.CountTilesOfColor(new Color(0.5f, 0f, 0.5f));

            goalText.text = "Red " + red + " / 9\n" +
                            "Blue " + blue + " / 9\n" +
                            "Yellow " + yellow + " / 9\n" +
                            "Orange " + orange + " / 9\n" +
                            "Green " + green + " / 9\n" +
                            "Purple " + purple + " / 9";
        }
    }

    void ResetGrid()
    {
        GridManager.Instance.ClearGrid();
        GridManager.Instance.SeedCenter();

        goalReached = false;
    }

}