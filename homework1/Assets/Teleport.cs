using UnityEngine;
using UnityEngine.InputSystem;

public class Teleport : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public InputActionReference action; // example right trigger
    private bool isInside = true; // player is inside at the start
    public GameObject newpoint;
    public GameObject player;
    void Start()
    {
        if(action != null)
        {
            action.action.Enable();
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if (action != null && action.action.WasPressedThisFrame())
        {
            TeleportPlayer();
        }
        
    }

    void TeleportPlayer()
    {
        isInside = !isInside;
        if (isInside)
        {
            transform.position = player.transform.position; //teleport inside where "player" object's original spot is
        }
        else
        {
            transform.position = newpoint.transform.position; //teleport outside to newpoint
        }
    }

     void OnDestroy()
    {
        action.action.Disable();
    }
}
