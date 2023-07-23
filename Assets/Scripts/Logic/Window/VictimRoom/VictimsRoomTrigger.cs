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
        // Получить ссылку на BasketControl
        VictimBasketsControl basketsControl = collider.GetComponentInChildren<VictimBasketsControl>();

        // Получить неактивную корзину
        VictimBasket basket = basketsControl.GetInactiveBasket();
        if (basket != null)
        {
            // Положить в неактивную корзину Victim
            Victim victim = _room.GetCurrentVictim();
            basket.PlaceVictimInBasket(victim);

            // Достать следующую жертву
            _room.ActivateNextVictim();

            // Включить неактивную корзину
            basket.gameObject.SetActive(true);
        }
    }
}
