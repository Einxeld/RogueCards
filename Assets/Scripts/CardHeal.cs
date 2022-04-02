using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardHeal : CardBase
{
    public override IEnumerator CardAction()
    {
        destroying = true;
        CardPlayer player = CardPlayer.instance;
        player.canMove = false;
        Vector2 cardPosition = player.transform.position;

        yield return player.ModifyHealth(cardValue);
        yield return transform.DOScale(0f, animationTime).WaitForCompletion();
        yield return player.transform.DOMove(transform.position, animationTime).WaitForCompletion();
        
        yield return BoardManager.instance.SpawnNewCard(cardPosition);

        GameManager.instance.AddScore();

        player.canMove = true;

        Destroy(gameObject);
    }
}
