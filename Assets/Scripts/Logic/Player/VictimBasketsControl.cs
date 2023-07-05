using UnityEngine;

public class VictimBasketsControl : MonoBehaviour
{
    [SerializeField] private VictimBasket [] _baskets;
    public VictimBasket[] Basket => _baskets;

    void Start() => 
        DeactivateBaskets();

    public VictimBasket GetInactiveBasket()
    {
        VictimBasket basket = null;
        for (int i = 0; i < _baskets.Length; i++)
        {
            if (_baskets[i].gameObject.activeSelf == false)
            {
                return _baskets[i];
            }
        }

        return basket;
    }
    

    private void DeactivateBaskets()
    {
        for (int i = 0; i < _baskets.Length; i++)
        {
            _baskets[i].gameObject.SetActive(false);
        }
    }



}
