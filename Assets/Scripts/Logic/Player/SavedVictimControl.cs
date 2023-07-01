using UnityEngine;

public class SavedVictimControl : MonoBehaviour
{
    [SerializeField] private JoinVictim [] _savedVictim;
   
    void Start()
    {
        DeactivateSavedVictimObjects();
    }

    private void DeactivateSavedVictimObjects()
    {
        for (int i = 0; i < _savedVictim.Length; i++)
        {
            _savedVictim[i].gameObject.SetActive(false);
        }
    }



}
