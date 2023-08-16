using UnityEngine;

public class LevelLoseSound : MonoBehaviour
{
    [SerializeField] ItemSound _sound;
    private void OnEnable() => 
        PlayLevelLoseSound();

    private void PlayLevelLoseSound()
    {
        try
        {
            _sound.Play();
        }
        catch
        {
            Debug.LogError("ItemSound reference not set");
        }
    }
}
