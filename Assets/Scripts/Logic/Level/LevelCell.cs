using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCell : MonoBehaviour
{
    [Header("Level Data")]
    [SerializeField] private Button _levelButton;
    [SerializeField] private TMP_Text _levelNumberText;
    [SerializeField] private GameObject _lockedState;

    private int _levelNumber = 0;
    public int LevelNumber => _levelNumber;

    private string _levelSceneName = string.Empty;
    public string LevelSceneName => _levelSceneName;

    private bool _levelLocked = true;
    public bool LevelLocked => _levelLocked;

    [Space(10)]
    [Header("Artifact Data")]
    [SerializeField] private Image _artifactImage;
    [SerializeField] private GameObject _artifactLockedBody;
    
    private Sprite _artifactSprite;
    public Sprite ArtifactSprite => _artifactSprite;

    private bool _artifactLocked;
    public bool ArtifactLocked => _artifactLocked;

    public static event Action<string> OnLevelCellPress;

    public void SaveCompletedLevel(bool artifactLocked)
    {
        _levelLocked = false;
        _artifactLocked = artifactLocked;
    }

    public void UnlockLevel() => 
        _levelLocked = false;

    public void InitLevelCell(int levelNumber, string levelName, bool levelLocked, Sprite artifactSprite, bool artifactLocked)
    {
        InitLevelData(levelNumber, levelName, levelLocked, artifactSprite, artifactLocked);
        InitArtifactData();
    }

    private void InitLevelData(int levelNumber, string levelName, bool levelLocked, Sprite artifactSprite, bool artifactLocked)
    {
        _levelNumber = levelNumber;
        _levelSceneName = levelName;
        _levelLocked = levelLocked;
        _artifactSprite = artifactSprite;
        _artifactLocked = artifactLocked;

        _levelNumberText.text = _levelNumber.ToString();
        _lockedState.SetActive(_levelLocked);
    }

    private void InitArtifactData()
    {
        _artifactImage.sprite = _artifactSprite;
        _artifactLockedBody.SetActive(_artifactLocked);
    }

    public void LoadButtonLevel() =>
        OnLevelCellPress?.Invoke(_levelSceneName);
}
