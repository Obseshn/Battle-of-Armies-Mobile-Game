using UnityEngine;

public class BombDropChecker : MonoBehaviour
{
    public delegate void GetCloseToBuild();
    public event GetCloseToBuild getCloseToBuild;

    private string TAG_building = "Building";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TAG_building))
        {
            getCloseToBuild?.Invoke();
            Debug.Log("Checker has founded a building. Let's drop something!");
        }
    }
}
