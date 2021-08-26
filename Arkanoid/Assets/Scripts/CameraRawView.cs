using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//за час сделала
//скрипт как обновлять render texture гуглила. 
//сделано через render texture, raw image. Основная проблема была, в том что алфа канал не обновлялся, поэтому 
//картинка постоянно обновляется в upd, если бы не outline, можно было бы просто поставить фон в тот же rawimg
public class CameraRawView : MonoBehaviour
{
    [SerializeField] private Vector2Int imgRes = new Vector2Int(256, 256);
    [SerializeField] private RawImage img;

    private Camera cam;

    private RenderTexture tex;
    private void Update()
    {
        cam = GetComponent<Camera>();

        tex = new RenderTexture(imgRes.x, imgRes.y, 32);
        cam.targetTexture = tex;

        img.texture = tex;
    }
}
