using UnityEngine;

public class DisableGameplayAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _gameplayMusicAudioSource;
    private VolumeControl _volumeControl;

    private void OnEnable()
    {
        _volumeControl = FindObjectOfType<VolumeControl>();

        // Disable sounds on ads start
        // Subscribe ads start callback
    }

    private void OnDisable()
    {
        // Unsubscribe ads start callback
    }

    private void DisableAudio()
    {
        if (_volumeControl != null)
        {
            _volumeControl.DisableAudio();
        }
        else
        {
            Debug.LogError("VolumeControl Component reference is null");
        }
    }
    private void EnableAudio()
    {
        if (_volumeControl != null)
        {
            _volumeControl.EnableAudio();
        }
        else
        {
            Debug.LogError("VolumeControl Component reference is null");
        }
    }
}
