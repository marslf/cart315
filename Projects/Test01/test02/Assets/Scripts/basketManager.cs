using UnityEngine;

public class basketManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(left)) 
        {
            //move left
            basket_x -= 0.01f;
        }
        if (Input.GetKeyDown(right)) 
        {
            //move right
            basket_x += 0.01f;
        }
        transform.position = new Vector3(basket_x)
    }
}
