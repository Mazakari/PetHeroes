using System.Collections;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform _teleportExit;
    private IEnumerator _teleportCoroutine;

    public void TeleportPlayer(Transform player)
    {
        if (_teleportCoroutine != null)
        {
            StopCoroutine(_teleportCoroutine);
            _teleportCoroutine = null;
        }
        _teleportCoroutine = StartTeleport(player);
        StartCoroutine(_teleportCoroutine);
    }
    private IEnumerator StartTeleport(Transform player)
    {
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        
        player.position = _teleportExit.position;
        yield return new WaitForSeconds(2f);

        player.gameObject.SetActive(true);
    }
}
