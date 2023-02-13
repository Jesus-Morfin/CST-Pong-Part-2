using UnityEngine;

public class powerUps : MonoBehaviour
{
    public GameManager gm;


    private void OnTriggerEnter(Collider other)
    {

        if(other.name == "Ball")
        {
            gm.gameChanger();
            Destroy(gameObject);
           
        }

    }
}
