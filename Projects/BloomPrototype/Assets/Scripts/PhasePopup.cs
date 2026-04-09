using UnityEngine;

public class PhasePopup : MonoBehaviour
{
    public GameObject popupUI;

    private bool isActive = false;


    void Update()
    {
        if (!isActive) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            ClosePopup();
        }
    }

    public void ShowPopup()
    {
        Debug.Log("SHOWING POPUP");

        if (popupUI == null)
        {
            Debug.LogError("popupUI is NULL");
            return;
        }
        
        popupUI.SetActive(true);
        isActive = true;
        Time.timeScale = 0f;
    }

    public void ClosePopup()
    {
        popupUI.SetActive(false);
        isActive = false;
        Time.timeScale = 1f;
    }
}