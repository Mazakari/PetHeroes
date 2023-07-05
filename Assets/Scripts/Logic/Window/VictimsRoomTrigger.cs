using UnityEngine;

public class VictimsRoomTrigger : MonoBehaviour
{
    [SerializeField] private VictimsRoom _room;

    private void OnTriggerEnter2D(Collider2D collider) => 
        TrySaveVictim(collider);

    private void TrySaveVictim(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            // �������� ������ �� BasketControl
            VictimBasketsControl basketsControl = collider.GetComponentInChildren<VictimBasketsControl>();

            // �������� ���������� �������
            VictimBasket basket = basketsControl.GetInactiveBasket();
            if (basket != null)
            {
                // �������� � ���������� ������� Victim
                Victim victim = _room.SaveCurrentVictim();
                basket.PlaceVictimInBasket(victim);

                // �������� ���������� �������
                basket.gameObject.SetActive(true);
            }

        }
    }
}
