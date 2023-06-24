using System.Collections.Generic;
using UnityEngine;

public class GameFactory : IGameFactory
{
    private readonly IAssets _assets;

    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

    public GameFactory(IAssets assets) =>
        _assets = assets;

    public GameObject CreatePlayer(GameObject at) =>
        InstantiateRegistered(AssetPath.PLAYER_PREFAB_PATH, at.transform.position);

    public void CreateLevelHud() =>
       InstantiateRegistered(AssetPath.LEVEL_CANVAS_PATH);

    public void CreateMainMenulHud() =>
       InstantiateRegistered(AssetPath.MAIN_MENU_CANVAS_PATH);

    public GameObject CreateLevelCell() =>
      InstantiateRegistered(AssetPath.LEVEL_CELL_PREFAB_PATH);

    public void CreateVolumeControl() =>
        InstantiateRegistered(AssetPath.VOLUME_CONTROL_PREFAB_PATH);

    public void Cleanup()
    {
        ProgressReaders.Clear();
        ProgressWriters.Clear();
    }

    private GameObject InstantiateRegistered(string prefabPath, Transform parent)
    {
        GameObject obj = _assets.Instantiate(prefabPath, parent);
        RegisterProgressWatchers(obj);

        return obj;
    }

    private GameObject InstantiateRegistered(string prefabPath, Vector2 at)
    {
        GameObject obj = _assets.Instantiate(prefabPath, at);
        RegisterProgressWatchers(obj);

        return obj;
    }

    private GameObject InstantiateRegistered(string prefabPath)
    {
        GameObject obj = _assets.Instantiate(prefabPath);
        RegisterProgressWatchers(obj);

        return obj;
    }

    private void Register(ISavedProgressReader progressReader)
    {
        if (progressReader is ISavedProgress progressWriter)
        {
            ProgressWriters.Add(progressWriter);
        }

        ProgressReaders.Add(progressReader);
    }

    private void RegisterProgressWatchers(GameObject obj)
    {
        foreach (ISavedProgressReader progressReader in obj.GetComponentsInChildren<ISavedProgressReader>(true))
        {
            Register(progressReader);
        }
    }
}
