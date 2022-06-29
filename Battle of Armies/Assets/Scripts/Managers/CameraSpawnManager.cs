using UnityEngine;
using Photon.Pun;

public class CameraSpawnManager : MonoBehaviour
{
    public GameObject[] Camera;

    public Transform[] spawnPositions;

    private Vector3 spawnPos;

    private void Awake()
    {
        /*Debug.Log(PhotonNetwork.PlayerList.Length);
        if (PhotonNetwork.PlayerList.Length >= 2)
        {
            
            Debug.Log("2 camera has spawned");
            spawnPos = spawnPositions[1].transform.position;
            PhotonNetwork.Instantiate(Camera[1].name, spawnPos, *//*Quaternion.Euler(new Vector3(50, 180, 0))*//* Quaternion.identity);
        }
        else
        {*/
            spawnPos = spawnPositions[0].transform.position;
            PhotonNetwork.Instantiate(Camera[0].name, spawnPos, /*Quaternion.Euler(new Vector3(50, 180, 0))*/ Quaternion.identity);
        
        
    }

/*    private Vector3 GetSpawnPos()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            return spawnPositions[1].transform.localPosition;
        }
        else
        {
            return spawnPositions[0].transform.localPosition;
        }
    }*/
}
