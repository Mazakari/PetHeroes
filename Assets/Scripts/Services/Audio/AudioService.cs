public class AudioService : IAudioService
{
    public VolumeControl VolumeControl { get; private set; }

    public AudioService(VolumeControl volumeControl)
    {
        VolumeControl = volumeControl;
    }
}
