using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private Transform Camera;

    private string TAG_MainCamera = "MainCamera";

    private void Start()
    {
        Camera = GameObject.FindGameObjectWithTag(TAG_MainCamera).transform;
    }
    private void LateUpdate()
    {
        if (Camera != null)
        {
            transform.LookAt(transform.position - Camera.forward);
        }
        
    }
}
