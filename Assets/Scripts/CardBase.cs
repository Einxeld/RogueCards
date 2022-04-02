using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public abstract class CardBase : MonoBehaviour
{
    [SerializeField] protected TextMeshPro cardValueText;
    protected int cardValue;
    [SerializeField] protected float animationTime = 1f;

    public abstract IEnumerator CardAction();

    protected bool cardHighlighted, destroying;

    public void Init(int newCardValue)
    {
        cardValue = newCardValue;
        cardValueText.text = cardValue.ToString();
    }

    void OnMouseDown()
    {
        if (CardPlayer.instance.canMove && CheckIfCanReach())
        {
            StartCoroutine(CardAction());
        }
    }

    void OnMouseOver()
    {
        if (CheckIfCanReach())
        {
            if (!cardHighlighted)
            {
                transform.DOScale(1.08f, 0.3f);
                cardHighlighted = true;
            }
        }
        else
        {
            OnMouseExit();
        }
    }

    void OnMouseExit()
    {
        if (!destroying && cardHighlighted)
        {
            transform.DOScale(1f, 0.3f);
            cardHighlighted = false;
        }
    }

    public bool CheckIfCanReach()
    {
        Vector2 playerPos = Vector2.zero;
        if (CardPlayer.instance != null) playerPos = CardPlayer.instance.transform.position;

        Vector2 extents = Vector2.zero;
        if (CardPlayer.instance != null) extents = BoardManager.instance.coordExtents;

        if (playerPos.x == transform.position.x)
        {
            if (Vector2.Distance(playerPos, transform.position) < extents.y + 0.1f)
            {
                return true;
            }
        }
        else if (playerPos.y == transform.position.y)
        {
            if (Vector2.Distance(playerPos, transform.position) < extents.x + 0.1f)
            {
                return true;
            }
        }

        return false;
    }
}
