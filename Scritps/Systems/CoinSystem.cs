using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{

    public delegate void OnCoinCollected(int newCoinCount);
    public event OnCoinCollected CoinCollected;

    [SerializeField] private int totalCoins = 0;

    public void CollectCoin()
    {
        totalCoins++;
        CoinCollected?.Invoke(totalCoins); 
    }
}
