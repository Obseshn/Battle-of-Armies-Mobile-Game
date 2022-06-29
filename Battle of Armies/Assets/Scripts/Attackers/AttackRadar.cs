using UnityEngine;

public class AttackRadar : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] private string TAG_Target;

    public event TargetFounded targetFounded;
    public delegate void TargetFounded(Transform target);

    public event LostTarget lostTarget;
    public delegate void LostTarget();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TAG_Target) && target == null)
        {
            target = GetClosestTarget(TAG_Target);
            targetFounded?.Invoke(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TAG_Target) && target == null)
        {
            lostTarget?.Invoke();
        }
    }

    private Transform GetClosestTarget(string TAG_Targets)
    {
        GameObject[] targets;

        targets = GameObject.FindGameObjectsWithTag(TAG_Targets);
        float closestDist = Mathf.Infinity;
      //  Transform target = null;

        foreach(GameObject obj in targets)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(gameObject.transform.position, obj.transform.position);
            if (currentDistance < closestDist)
            {
                closestDist = currentDistance;
                target = obj.transform;
            }
        }
        
        Debug.Log("Target founded!");
        return target;
    }
}
