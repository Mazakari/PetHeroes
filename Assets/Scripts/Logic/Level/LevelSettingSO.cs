using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettingsSO", menuName = "Levels/Level Settings", order = 1)]
public class LevelSettingSO : ScriptableObject
{
    [Header("Level Settings")]
    [SerializeField] private string _levelSceneName = string.Empty;
    public string LevelSceneName => _levelSceneName;

    [SerializeField] private bool _levelLocked = false;
    public bool LevelLocked => _levelLocked;


    [Header("Artifact Settings")]
    [Space(10)]
    [SerializeField] private Sprite _levelArtifactSprite;
    public Sprite LevelArtifactSprite => _levelArtifactSprite;

    [SerializeField] private bool _artifactLocked = false;
    public bool ArtifactLocked => _artifactLocked;
}
