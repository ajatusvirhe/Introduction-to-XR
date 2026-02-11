using UnityEngine;

public class LensFollow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform lense;
    // Update is called once per frame
    void Update()
    {
        transform.position= lense.position;
        
    }
}
