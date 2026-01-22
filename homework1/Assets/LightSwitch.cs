using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.Input;
//using UnityEngine.XR;

public class LightSwitch : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Light pointlight;
    public InputActionReference action;
    private bool isOn = true;
    void Start()
    {
        pointlight = GetComponent<Light>();
        if(action != null)
        {
            action.action.Enable();
        }
    }

    void OnDestroy()
    {
        action.action.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        if (action != null && action.action.WasPressedThisFrame())
        {
            ToggleLight();
        }
    }
    void ToggleLight()
    {
        isOn = !isOn;

        if (isOn)
        {
            pointlight.color = new Color( 0.9019f, 0.81568f, 0.988f,1 );
        }
        else
        {
            pointlight.color = new Color(0.1098f, 0.23137f, 0.423529f,1);
        }
    }
}
