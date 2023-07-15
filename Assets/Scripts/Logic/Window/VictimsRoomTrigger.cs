using UnityEngine;

public class VictimsRoomTrigger : MonoBehaviour
{
    [SerializeField] private int _playerLayer;
    [SerializeField] private VictimsRoom _room;

    private void OnTriggerEnter2D(Collider2D collider) => 
        TrySaveVictim(collider);

    private void TrySaveVictim(Collider2D collider)
    {
        if (collider.gameObject.layer == _playerLayer)
        {
            PutVictimInBasketAndActivateNextVictim(collider);
        }
    }

    private void PutVictimInBasketAndActivateNextVictim(Collider2D collider)
    {
        // �������� ������ �� BasketControl
        VictimBasketsControl basketsControl = collider.GetComponentInChildren<VictimBasketsControl>();

        // �������� ���������� �������
        VictimBasket basket = basketsControl.GetInactiveBasket();
        if (basket != null)
        {
            // �������� � ���������� ������� Victim
            Victim victim = _room.GetCurrentVictim();
            basket.PlaceVictimInBasket(victim);

            // ������� ��������� ������
            _room.ActivateNextVictim();

            // �������� ���������� �������
            basket.gameObject.SetActive(true);
        }
    }
}
