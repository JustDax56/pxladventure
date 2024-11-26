using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private CoinSystem coinCollector;
    [SerializeField] private TMP_Text coinText;
    public Animator animator;

    private void Start()
    {
        PlayAnimation();

    }

    private void Awake()
    {
        coinCollector.CoinCollected += UpdateCoinUI;
    }

    private void UpdateCoinUI(int newCoinCount)
    {
        coinText.text = " x " + newCoinCount;
    }
    public void PlayAnimation()
    {
        if (animator != null)
        {
            animator.Play("Coin");
        }
    }

}
