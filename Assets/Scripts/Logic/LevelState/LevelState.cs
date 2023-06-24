using System;
using UnityEngine;

public class LevelState : MonoBehaviour
{
    private bool _levelStarted = false;

    [SerializeField] private ChestKeyPanel _chestKeyPanel;
    private TreasureChest _treasureChest;

    // 'event' Can't be invoked from other class
    public static event Action OnLevelStart;
    public static event Action OnLevelLoaded;

    // Can be invoked from other class
    /// <summary>
    /// Show level results popup. Pass true if artifact is locked on level complete. 
    /// </summary>
    public static Action<bool> OnLevelResultShow;

    
    private void OnEnable()
    {
        _treasureChest = FindObjectOfType<TreasureChest>();

        FinishPlatform.OnLevelFinish += OnLevelFinish;
    }

    private void Awake() => 
        OnLevelLoaded?.Invoke();

    private void OnDisable() => 
        FinishPlatform.OnLevelFinish -= OnLevelFinish;

    private void Update() =>
       ReadClickOnStartInput();

    private void OnLevelFinish()
    {
        if (_chestKeyPanel.CheckKeysCollection())
        {
            Debug.Log("Open treasure chest!");
            _treasureChest.OpenChest();
            return;
        }

        Debug.Log("Treasure keys not collected!");
        // Send callback to LevelCanvas to show level complete popup with artifact locked true parameter
        OnLevelResultShow?.Invoke(true);
    }

    private void ReadClickOnStartInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_levelStarted)
            {
                _levelStarted = true;

                OnLevelStart?.Invoke();
            }
        }
    }
}
