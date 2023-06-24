using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameMetaData
{
    public string nextLevel;

    [Serializable]
    public struct LevelCellsData
    {
        public int number;
        public string sceneName;
        public bool locked;

        public Sprite artifactSprite;
        public bool artifactLocked;
    }

    public List<LevelCellsData> levels;

    public float musicVolume;
    public bool musicToggle;

    public float soundVolume;
    public bool soundToggle;

    public bool gameRated;

    public GameMetaData(string initialLevel)
	{
        nextLevel = initialLevel;
        levels = new List<LevelCellsData>();

        musicVolume = 0.5f;
        musicToggle = true;

        soundVolume = 0.5f;
        soundToggle = true;
    }
}
