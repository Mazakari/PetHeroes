using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerYandexID : MonoBehaviour
{
    [SerializeField] private RawImage _playerAvatar;
    [SerializeField] private TMP_Text _playerName;

    public void InitID(string name, string avatarUrl)
    {
        SetPlayerName(name);
        SetAvatarImage(avatarUrl);
    }

    private void SetPlayerName(string name) => 
        _playerName.text = name;

    private void SetAvatarImage(string url) => 
        StartCoroutine(DownloadImage(url));

    private IEnumerator DownloadImage(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            _playerAvatar.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }
}
