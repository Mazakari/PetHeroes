using UnityEngine;
using UnityEngine.EventSystems;

public class HypertextLink : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private string _link;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_link.Contains("www"))
        {
            Debug.Log($"String {_link} click!");
            Application.OpenURL(_link);
        }

    }
}
