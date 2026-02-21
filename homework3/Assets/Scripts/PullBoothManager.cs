using UnityEngine;

public class PullBoothManager : MonoBehaviour
{
    public GameObject[] prizes;   // Assign prizes in inspector
    //public StringPull[] strings;  // Assign strings in inspector
    public StringManualPull[] strings;  // Assign strings in inspector
    public Transform teleportPrize;

    private bool boothFinished = false;

    //public void StringSelected(StringPull selectedString)
    public void StringSelected(StringManualPull selectedString)
    {
        if (boothFinished) return;

        boothFinished = true;

        int index = System.Array.IndexOf(strings, selectedString);

        if (index >= 0 && index < prizes.Length)
        {
            SpawnPrize(index);
            //Invoke("SpawnPrize",2f);
        }

        DisableOtherStrings();
    }

    void SpawnPrize(int index)
    {
        GameObject prize = prizes[index];
        prize.SetActive(true);
        prize.transform.position = teleportPrize.position;
        prize.GetComponent<Rigidbody>().useGravity = true;
        if (prize.GetComponent<Collider>() != null) { 
            prize.GetComponent<Collider>().enabled = true; // enables the collider only after teleporting
        }
        else{
            prize.GetComponentInChildren<Collider>().enabled = true;
        }
            
        
    }

    void DisableOtherStrings()
    {
        //foreach (StringPull s in strings)
        foreach (StringManualPull s in strings)
        {
            s.enabled = false;
        }
    }
}
