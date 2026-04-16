using UnityEngine;

public class FloatingFlower : MonoBehaviour
{
    public float floatSpeed = 2f;
    public float floatAmount = 8f;

    public float rotateAmount = 10f;
    public float rotateSpeed = 1f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
        
        floatSpeed += Random.Range(-0.3f, 0.3f);
        rotateSpeed += Random.Range(-0.3f, 0.3f);
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatAmount;
        transform.localPosition = startPos + new Vector3(0, yOffset, 0);

        float rotation = Mathf.Sin(Time.time * rotateSpeed) * rotateAmount;
        transform.localRotation = Quaternion.Euler(0, 0, rotation);
    }
}
