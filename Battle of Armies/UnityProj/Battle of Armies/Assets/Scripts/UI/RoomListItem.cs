using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] private Text roomNameText;
    [SerializeField] private Text roomPlayerCount;
    public void SetButtonInfo(RoomInfo roomInfo)
    {
        roomNameText.text = roomInfo.Name;
        roomPlayerCount.text = roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers; ;
    }

    public void JoinToListRoom()
    {
       PhotonNetwork.JoinRoom(roomNameText.text);
    }
}
