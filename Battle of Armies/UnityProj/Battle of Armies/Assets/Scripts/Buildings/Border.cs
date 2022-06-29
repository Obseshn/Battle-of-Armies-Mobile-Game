using UnityEngine;

public class Border : MonoBehaviour
{
    private string TAG_Environment = "Environment";

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag(TAG_Environment))
        {
            Destroy(collision.gameObject);
        }
        
    }
}
