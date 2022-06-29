using UnityEngine;
using System.Collections;

public class BoostPanel : MonoBehaviour
{
    [SerializeField] private float boostCD = 60f;

    public void StartBoostCD(GameObject boostObj)
    {
        StartCoroutine(HideBoostOnTime(boostObj, boostCD));
    }

    IEnumerator HideBoostOnTime(GameObject boost, float time)
    {
        boost.SetActive(false);
        yield return new WaitForSeconds(time);
        boost.SetActive(true);
    }
}
