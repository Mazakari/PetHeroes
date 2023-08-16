using UnityEngine;

public class LevelLosePopup : MonoBehaviour
{
    public void RestartLevel() =>
      GameplayCanvas.OnRestartLevel?.Invoke();
}
