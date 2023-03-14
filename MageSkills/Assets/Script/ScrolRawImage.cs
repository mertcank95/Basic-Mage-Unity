using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrolRawImage : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed, verticalSpeed;
    private RawImage rawImage;
    void Start()
    {
        rawImage = GetComponent<RawImage>();
    }
    void Update()
    {
        Rect currentUv = rawImage.uvRect;
        currentUv.x -= Time.deltaTime * horizontalSpeed;
        currentUv.y -= Time.deltaTime * verticalSpeed;
        if (currentUv.x <= -1f || currentUv.x >= 1f)
        {
            currentUv.x = 0;
        }
        if (currentUv.y <= -1f || currentUv.y >= 1f)
        {
            currentUv.y = 0;
        }
        rawImage.uvRect = currentUv;
    }
}
