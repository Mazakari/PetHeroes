using System.Collections.Generic;
using UnityEngine;

public interface IGameFactory : IService
{
    public List<ISavedProgressReader> ProgressReaders { get; }
    public List<ISavedProgress> ProgressWriters { get; }

    GameObject CreatePlayer(GameObject at);

    void CreateLevelHud();
    void CreateMainMenulHud();
    GameObject CreateLevelCell();
    void CreateVolumeControl();
    void Cleanup();
}
