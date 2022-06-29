using UnityEngine;
using Photon.Pun;


public class CameraController : MonoBehaviour
{
    private PhotonView view;

    private void Awake()
    {
        view = GetComponent<PhotonView>();

        if (!view.IsMine)
        {
            gameObject.SetActive(false);
        }
       
    }
}
