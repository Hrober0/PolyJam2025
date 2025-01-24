using HCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPainter : SingletonMB<FloorPainter>, ISingletonAutoFind
{
    public event Action OnFillPercentChange;

    [SerializeField] private Renderer floorRenderer;

    [Header("Space")]
    [SerializeField] private Vector2 floorMin;
    [SerializeField] private float worldSize = 10;
    [SerializeField] private float blurLength = 0.1f;

    [Header("Settings")]
    [SerializeField] private int resolution = 2048;
    [SerializeField] private int fillAcceptTreshhold = 10;

    private Texture2D floorTexture;
    private MaterialPropertyBlock props;
    private float worldToArrayMultiplier;

    private Color32[] txtValues;
    private bool reqiredApply = false;

    private readonly Dictionary<(int, float), int[,]> circleCache = new();

    public float FillPercent { get; private set; }

    private void Start()
    {
        worldToArrayMultiplier = resolution / worldSize;

        floorTexture = new Texture2D(resolution, resolution);
        txtValues = new Color32[floorTexture.width * floorTexture.height];

        props = new MaterialPropertyBlock();
        floorRenderer.GetPropertyBlock(props);
        props.SetTexture("_DirtyMask", floorTexture);
        floorRenderer.SetPropertyBlock(props);

        FillTxt();
        StartCoroutine(BakeFloor());
        FillPercent = 0;
        OnFillPercentChange?.Invoke();
    }

    private IEnumerator BakeFloor()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            if (reqiredApply)
            {
                reqiredApply = false;
                Apply();
                yield return null;
                UpdateFillPercent();
            }
        }
    }

    private void FillTxt()
    {
        for (int i = 0; i < txtValues.Length; i++)
        {
            txtValues[i] = new Color32(255, 255, 255, 255);
        }
        Apply();
    }
    public void ClearFloor(Vector2 pos, float range)
    {
        //pos.DrawPoint(Color.green);
        var min = TxtMinFromCenter(pos);
        var r = range + blurLength * 0.5f;
        RemoveFromTxt(min, r, 1);
        reqiredApply = true;
    }

    private void Apply()
    {
        floorTexture.SetPixels32(txtValues);
        floorTexture.Apply();
    }

    private void RemoveFromTxt(Vector2Int min, float range, float multiplier)
    {
        var hsize = Mathf.RoundToInt(range * worldToArrayMultiplier);

        if (!circleCache.TryGetValue((hsize, multiplier), out var addValues))
        {
            addValues = CreateCircleTexture(range, multiplier);
            circleCache.Add((hsize, multiplier), addValues);
        }

        var ox = min.x - hsize;
        var oy = min.y - hsize;

        var sx = Mathf.Max(ox, 0);
        var sy = Mathf.Max(oy, 0);
        var ex = Mathf.Min(min.x + hsize - 1, resolution);
        var ey = Mathf.Min(min.y + hsize - 1, resolution);

        for (int y = sy; y < ey; y++)
        {
            var yo = y * resolution;
            for (int x = sx; x < ex; x++)
            {
                var index = yo + x;
                var remValue = addValues[x - ox, y - oy];
                var newValue = Mathf.Max(txtValues[index].a - remValue, 0);
                ref var c = ref txtValues[index];
                if (newValue < c.a)
                {
                    c.a = (byte)newValue;
                }
            }
        }
    }

    private Vector2Int TxtMinFromCenter(Vector2 point) => ((point - floorMin) * worldToArrayMultiplier).Floor();

    private int[,] CreateCircleTexture(float range, float multiplier)
    {
        var size = Mathf.RoundToInt(range * 2 * worldToArrayMultiplier);
        var values = new int[size, size];
        var squerRange = Mathf.Pow(range * worldToArrayMultiplier, 2);
        var blurShift = Mathf.Pow((range - blurLength) * worldToArrayMultiplier, 2);
        var invSR = 1 / (squerRange - blurShift);
        var centerX = Mathf.RoundToInt(size / 2);
        var centerY = Mathf.RoundToInt(size / 2);
        var blurToV = multiplier * 255;
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                var squerDist = new Vector2Int(x - centerX, y - centerY).sqrMagnitude;
                if (squerDist < squerRange)
                {
                    var bluredValue = Mathf.Clamp01(1 - (squerDist - blurShift) * invSR);
                    values[x, y] = Mathf.FloorToInt(bluredValue * blurToV);
                }
            }
        }
        return values;
    }

    private void UpdateFillPercent()
    {
        var filledTiles = 0;
        foreach (var c in txtValues)
        {
            if (c.a < fillAcceptTreshhold)
            {
                filledTiles++;
            }
        }

        FillPercent = filledTiles / (float)txtValues.Length;
        OnFillPercentChange?.Invoke();
    }
}
