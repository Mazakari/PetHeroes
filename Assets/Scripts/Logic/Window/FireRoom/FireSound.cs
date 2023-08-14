using UnityEngine;

public class FireSound : MonoBehaviour
{
    [SerializeField] ItemSound _itemSound;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] private AudioClip _fireGrowSound;
    [SerializeField] private AudioClip _fireDownSound;

    public void PlayFireGrowSound()
    {
        SetAudioClip(_fireGrowSound);
        _itemSound.Play();
    }

    public void PlayFireDownSound()
    {
        SetAudioClip(_fireDownSound);
        _itemSound.Play();
    }

    private void SetAudioClip(AudioClip clip) =>
       _audioSource.clip = clip;
}
