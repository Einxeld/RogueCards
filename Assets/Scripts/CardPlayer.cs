using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class CardPlayer : MonoBehaviour
{
    public static CardPlayer instance;

    public int hp { get; private set; }
    [SerializeField] TextMeshPro healthText;

    public bool canMove;

    [SerializeField] float animationTime;

    void Awake()
    {
        instance = this;
        healthText.text = hp.ToString();
    }

    public IEnumerator ModifyHealth(int addHealth)
    {
        hp += addHealth;
        healthText.text = hp.ToString();

        if (hp <= 0)
        {
            StartCoroutine(GameManager.instance.GameOver());
        }

        yield return null;
    }
}
