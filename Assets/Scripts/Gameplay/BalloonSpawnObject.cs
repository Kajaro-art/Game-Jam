using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawnObject : MonoBehaviour
{
    public List<Sprite> ballonSprites = new();

    public SpriteRenderer ballonSprite;

    private void Awake()
    {
        var val = Random.Range(0, ballonSprites.Count - 1);

        ballonSprite.sprite = ballonSprites[val];
    }
}