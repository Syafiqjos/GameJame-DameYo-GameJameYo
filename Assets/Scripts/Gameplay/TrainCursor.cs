using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCursor : MonoBehaviour
{
    public Transform train;
    public float cursorLength = 2.0f;

    private LineRenderer directionRenderer;

    private Camera cameraV;

    private void Start()
    {
        cameraV = Camera.main;
        directionRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        PointerController();
    }

    private void PointerController()
    {
        if (directionRenderer)
        {
            Vector2 point = cameraV.ScreenToWorldPoint(Input.mousePosition);
            Vector2 centerScreen = cameraV.ScreenToWorldPoint(new Vector2(Screen.width / 2.0f, Screen.height / 2.0f));
            Vector3 direction = point - centerScreen;

            direction = Vector3.ClampMagnitude(direction, cursorLength);

            directionRenderer.SetPosition(0, transform.position);
            directionRenderer.SetPosition(1, transform.position + direction);
        }
    }
}
