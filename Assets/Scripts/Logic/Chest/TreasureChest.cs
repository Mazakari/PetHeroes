using System.Collections;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    [Header("Chest")]
    [SerializeField] private SpriteRenderer _closedTreasureChest;
    [SerializeField] private SpriteRenderer _openedTreasureChest;
    [SerializeField] private SpriteRenderer _treasureChestTrophy;

    [Space(10)]
    [Header("Chest locks")]
    [SerializeField] private SpriteRenderer _redTreasureChestLock;
    [SerializeField] private SpriteRenderer _greenTreasureChestLock;
    [SerializeField] private SpriteRenderer _blueTreasureChestLock;

    private float _lockUnlockDelay = 1f;

    private ILevelCellsService _levelCellsService;

    [Space(10)]
    [Header("Audio")]
    [SerializeField] private ItemSound _itemSound;

    private void OnEnable() => 
        _levelCellsService = AllServices.Container.Single<ILevelCellsService>();

    private void OnDisable() => 
        StopCoroutine(ChestOpenCoroutine());

    public void OpenChest() => 
        StartCoroutine(ChestOpenCoroutine());

    public void InitChest()
    {
        InitArtifactSprite();
        SetChestSpriteToClosedState();
        EnableAllChestKeySprites();
    }

    private void EnableAllChestKeySprites()
    {
        _redTreasureChestLock.enabled = true;
        _greenTreasureChestLock.enabled = true;
        _blueTreasureChestLock.enabled = true;
    }
    private void SetChestSpriteToClosedState()
    {
        _closedTreasureChest.enabled = true;
        _openedTreasureChest.enabled = false;
    }
    private void InitArtifactSprite()
    {
        _treasureChestTrophy.sprite = _levelCellsService.Current.ArtifactSprite;
        _treasureChestTrophy.enabled = false;
    }
    private IEnumerator ChestOpenCoroutine()
    {
        bool chestUnlocked = false;
        while (!chestUnlocked)
        {
            yield return new WaitForSeconds(_lockUnlockDelay);
            _redTreasureChestLock.enabled = false;

            yield return new WaitForSeconds(_lockUnlockDelay);
            _greenTreasureChestLock.enabled = false;

            yield return new WaitForSeconds(_lockUnlockDelay);
            _blueTreasureChestLock.enabled = false;

            yield return new WaitForSeconds(_lockUnlockDelay);
            _treasureChestTrophy.enabled = true;

            PlayItemSound();

            yield return new WaitForSeconds(_lockUnlockDelay);
            chestUnlocked = true;
            // Send callback to LevelCanvas to open level complete popup with artifactLocked bool parameter
            LevelState.OnLevelResultShow?.Invoke(false);
        }

        yield break;
    }
    private void PlayItemSound()
    {
        if (_itemSound)
        {
            _itemSound.Play();
        }
    }
}
