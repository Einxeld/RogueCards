using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoardManager : MonoBehaviour
{
    public static BoardManager instance;

    [SerializeField] GameObject playerPrefab, greenCardPrefab, redCardPrefab;
    Dictionary<Vector2, GameObject> cardsOnBoard = new Dictionary<Vector2, GameObject>();

    [SerializeField] float cardSpawnAnimSpeed = 0.3f;

    public Vector2 coordExtents { get; private set; }

    void Awake()
    {
        instance = this;
        coordExtents = new Vector2(2.5f, 3.5f);
    }

    public void InitBoard()
    {
        StartCoroutine(InitBoardIE());
    }

    public IEnumerator InitBoardIE()
    {
        for (float y = coordExtents.y; y >= -coordExtents.y; y -= coordExtents.y)
        {
            for (float x = -coordExtents.x; x <= coordExtents.x; x += coordExtents.x)
            {
                bool spawnPlayer = (x + y == 0);
                StartCoroutine(SpawnNewCard(new Vector2(x, y), spawnPlayer));
                yield return new WaitForSeconds(cardSpawnAnimSpeed / 2f);
            }
        }

        CardPlayer.instance.canMove = true;
    }

    /*
    Вероятность выпадения красной карты = (Текущий уровень сложности) * 0.1.
    При этом максимальная вероятность выпадения красной карты не может быть больше 50%, вне зависимости от уровня сложности.
    Значения выпадающих карт - всегда целые числа (от 1).
    Уровень сложности влияет на допустимый диапазон значений рандомизации (Диапазон = [0..Текущий уровень сложности]).
    Конечное значение карты: 1 + Случайное число из Диапазона.
    */
    public IEnumerator SpawnNewCard(Vector2 spawnPosition, bool spawnPlayer = false)
    {
        GameObject newCard;
        if (spawnPlayer)
        {
            newCard = Instantiate(playerPrefab);
        }
        else
        {
            float redChance = Mathf.Clamp(GameManager.instance.level*0.1f, 0f, 0.5f);
            int cardValue = Random.Range(0, GameManager.instance.level) + 1;

            float random01f = Random.Range(0f, 1f);
            newCard = Instantiate(random01f < redChance ? redCardPrefab : greenCardPrefab);
            newCard.GetComponent<CardBase>().Init(cardValue);
        }

        cardsOnBoard[spawnPosition] = newCard;

        newCard.transform.position = spawnPosition;

        newCard.transform.localScale = Vector3.zero;
        yield return newCard.transform.DOScale(Vector3.one, cardSpawnAnimSpeed).SetEase(Ease.OutQuad).WaitForCompletion();
    }
}
