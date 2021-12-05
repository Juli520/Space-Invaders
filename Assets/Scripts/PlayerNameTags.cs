using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerNameTags : MonoBehaviourPun
{
    [SerializeField] private TextMeshProUGUI nameText;
    
    private void Start()
    {
        if(!photonView.IsMine) return;

        photonView.RPC("SetNameRPC", RpcTarget.All);
    }

    [PunRPC]
    public void SetNameRPC()
    {
        nameText.text = photonView.Owner.NickName;
    }
}
