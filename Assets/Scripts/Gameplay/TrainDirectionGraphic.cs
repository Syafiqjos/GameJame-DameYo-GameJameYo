using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainDirectionGraphic : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteRight;
    public Sprite spriteLeft;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector3 euler = transform.rotation.eulerAngles;
        if (euler.z >= 0)
        {
            spriteRenderer.sprite = spriteUp;
        }
    }
}
