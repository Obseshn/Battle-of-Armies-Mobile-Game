using UnityEngine;
using Photon.Pun;

public class AttackersSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToSpawn;
    [SerializeField] private MoneyManager moneyManager;
    [SerializeField] private string TAG_army;
    [SerializeField] private int maxUnits = 15;
    [SerializeField] private float xBord1 = 6f;
    [SerializeField] private float xBord2 = 18f;
    [SerializeField] private float yPos = 0.38f;
    [SerializeField] private float zPos = -12f;
    private readonly float airplaneFlyDistance = 5f;
    [SerializeField] private int aliveArmy;


    public void SpawnObject(int index)
    {
        
        if (aliveArmy < maxUnits && moneyManager.HaveMoneyForVehicles(index)) // Have money for vihicles function also deduct money if money manager has enough
        {
            if (index == (int)VehiclesIndex.Airplane)
            {
                PhotonNetwork.Instantiate(objectsToSpawn[index].name,
                    GetSpawnPosition(xBord1, xBord2, yPos + airplaneFlyDistance, zPos), Quaternion.identity);
            }
            else
            {
                PhotonNetwork.Instantiate(objectsToSpawn[index].name, GetSpawnPosition(xBord1, xBord2, yPos, zPos), Quaternion.identity);
            }
        }
        else
        {
            Debug.Log("You haven't enough money");
        }
    }

    private Vector3 GetSpawnPosition(float xBorder1,float xBorder2, float yPos, float zPos)
    {
        float xPos = Random.Range(xBorder1, xBorder2);
        return new Vector3(xPos, yPos, zPos);
    }

}


public enum VehiclesIndex
{
    Airplane = 0,
    ArmoredLorry = 1,
    Tank = 2
}


