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
            // Получить ссылку на BasketControl
            VictimBasketsControl basketsControl = collider.GetComponentInChildren<VictimBasketsControl>();

            // Получить неактивную корзину
            VictimBasket basket = basketsControl.GetInactiveBasket();
            if (basket != null)
            {
                // Положить в неактивную корзину Victim
                Victim victim = _room.SaveCurrentVictim();
                basket.PlaceVictimInBasket(victim);

                // Включить неактивную корзину
                basket.gameObject.SetActive(true);
            }

        }
    }
}
