using TMPro;
using UnityEngine;

public class CurrentLevelDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _numberText;

    public void SetLevelNumber(int number) => 
        _numberText.text = number.ToString();
}
