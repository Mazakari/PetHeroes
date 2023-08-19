using UnityEngine;

public class DisableGameplayMusic : MonoBehaviour
{
    [SerializeField] private AudioSource _gameplayMusicAudioSource;

    private void OnEnable() => 
        YandexAPI.OnYandexAdsStart += DisableMusicAudioSource;

    private void OnDisable() => 
        YandexAPI.OnYandexAdsStart -= DisableMusicAudioSource;

    private void DisableMusicAudioSource()
    {
        if (_gameplayMusicAudioSource != null)
        {
            _gameplayMusicAudioSource.volume = 0;
        }
        else
        {
            Debug.LogError("AudioSource Component reference is null");
        }
    }
}
