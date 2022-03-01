using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    [SerializeField] private GameObject figure;
    [SerializeField] private float speed = 20;

    private Vector3 startPos;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 dif = new Vector3();
            dif.y = Input.mousePosition.x - startPos.x;
            dif.x = Input.mousePosition.y - startPos.y;
            dif.y *= -1;
            figure.transform.RotateAround(figure.transform.position, dif, speed * Time.deltaTime);
            startPos = Input.mousePosition;
        }
    }
}
