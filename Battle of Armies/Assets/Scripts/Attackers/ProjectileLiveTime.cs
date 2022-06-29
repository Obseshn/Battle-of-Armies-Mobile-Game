using System.Collections;
using UnityEngine;

public class ProjectileLiveTime : MonoBehaviour
{
    [SerializeField] private float LiveTime = 4f;
    private void Start()
    {
        StartCoroutine(LiveCounter(LiveTime));
    }
    IEnumerator LiveCounter(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
