using System;

[Serializable]
public class PlayerProgress
{
    public GameMetaData gameData;

    public PlayerProgress(string initialLevel)
    {
        gameData = new GameMetaData(initialLevel);
    }
}
