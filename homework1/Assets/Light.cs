using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Input;

public class Light : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Light light;
    void Start()
    {
        light = GetComponent<Light>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.primaryButton = true)
        {
            light.intensity = 5;
        }
    }
}
