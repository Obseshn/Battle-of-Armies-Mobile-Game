using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public static void ChangeColor(GameObject gameObj, Color color)
    {
        gameObj.transform.GetComponentInChildren<MeshRenderer>().material.color = color;
    }

    public static Color GenerateRedGreenOrBlueColor()
    {

        int index = Random.Range(0, 3); // 3 because RED(0) GREEN(1) or BlUE(2)

        switch (index)
        {
            case 0:
                return Color.red;
            case 1:
                return Color.green;
            case 2:
                return Color.blue;
            default:
                return Color.green;
               
        }
    }
}
