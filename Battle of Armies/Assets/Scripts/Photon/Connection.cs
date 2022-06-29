using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class Connection : MonoBehaviourPunCallbacks
{


    private readonly string region = "ru";

    [SerializeField] InputField nameRoomInpField;
    [SerializeField] private GameObject ErrorPanel;
    [SerializeField] private RoomListItem list;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject winMenu;

    List<RoomInfo> allRoomsInfo = new List<RoomInfo>();
    private void Awake()
    {
        
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.ConnectToRegion(region);
        }
       
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Main Menu");
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
        
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("You have disconnected");
        PhotonNetwork.Disconnect();
    }

    public void CreaetRoom()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }

        if (nameRoomInpField.text.Length > 3 && PhotonNetwork.IsConnected)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 2;
            PhotonNetwork.CreateRoom(nameRoomInpField.text, roomOptions);
          //  PhotonNetwork.LoadLevel("GamePlay");
        }
        else
        {
            ErrorPanel.SetActive(true);
        }

    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room has created, name: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Can't create room");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            for (int i = 0; i < allRoomsInfo.Count; i++)
            {
                if (allRoomsInfo[i].masterClientId == info.masterClientId)
                {
                    return;
                }
            }
            RoomListItem roomListItem = Instantiate(list, content);
            if (roomListItem != null)
            {
                roomListItem.SetButtonInfo(info);
                allRoomsInfo.Add(info);
            }
             
            
        }
    }

    public override void OnJoinedRoom()
    {
        if(PhotonNetwork.PlayerList.Length == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.LoadLevel("GamePlay");

        }
        else
        {
            PhotonNetwork.LoadLevel("Waiting Room");
        }
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void JoinRoom()
    {
        if (nameRoomInpField.text.Length > 3)
        {
            PhotonNetwork.JoinRoom(nameRoomInpField.text);
        }
        else
        {
            ErrorPanel.SetActive(true);
        }
    }

    public void LeaveRoomButton()
    {
        Debug.Log("You leave from the room");
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
        PhotonNetwork.LeaveRoom();
         PhotonNetwork.Disconnect();
        /*PhotonNetwork.LoadLevel("Main Menu");*/
    }

    public override void OnLeftRoom()
    {
        
        PhotonNetwork.LoadLevel("Main Menu");
        base.OnLeftRoom();

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
            PhotonNetwork.LoadLevel("GamePlay");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
            winMenu.SetActive(true);
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
    }

   public void RejoinMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
