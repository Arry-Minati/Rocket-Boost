using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("friendly");
                break;
            case "Finish":
                Debug.Log("finished");
                break;
            case "Fuel":
                Debug.Log("Fuel taken");
                break;
            default:
                Debug.Log("crashed");
                break;    
        }
    }
}
