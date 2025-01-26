using HCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPainter : SingletonMB<FloorPainter>, ISingletonAutoFind
{
    private const int NONE_OWNER = -1;
    
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

    private int[] ownedFileds;
    private Color32[] txtValues;
    private bool reqiredApply = false;

    private readonly Dictionary<(int, float), float[,]> shapeCache = new();

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

        ownedFileds = new int[txtValues.Length];
        Array.Fill(ownedFileds, NONE_OWNER);

        FillTxt();
        StartCoroutine(BakeFloor());
        FillPercent = 0;
        OnFillPercentChange?.Invoke();
    }

    private IEnumerator BakeFloor()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.03f);
            if (reqiredApply)
            {
                reqiredApply = false;
                Apply();
                yield return null;
                UpdateFillPercent();
            }
        }
    }

    public void FillTxt()
    {
        for (int i = 0; i < txtValues.Length; i++)
        {
            txtValues[i] = new Color32(255, 255, 255, 255);
        }
        Apply();
    }
    public void ClearFloor(Vector2 pos, float range, Color playerColor, int playerId = NONE_OWNER)
    {
        range += blurLength * 0.5f;

        pos.DrawPoint(Color.green);
        var min = TxtMinFromCenter(pos);
        var multiplier = 1f;
        var hsize = Mathf.RoundToInt(range * worldToArrayMultiplier);
        var playerColor32 = (Color32)playerColor;

        if (!shapeCache.TryGetValue((hsize, multiplier), out var shapeValues))
        {
            shapeValues = CreateCircleTexture(range, multiplier);
            shapeCache.Add((hsize, multiplier), shapeValues);
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
                var changePercent = shapeValues[x - ox, y - oy];
                var remValue = (byte)(changePercent * 255);
                ref var c = ref txtValues[index];
                var newValue = Mathf.Max(c.a - remValue, 0);
                if (newValue < fillAcceptTreshhold && ownedFileds[index] == NONE_OWNER)
                {
                    ownedFileds[index] = playerId;
                    c.r = (byte)Mathf.Lerp(c.r, playerColor32.r, changePercent);
                    c.g = (byte)Mathf.Lerp(c.g, playerColor32.g, changePercent);
                    c.b = (byte)Mathf.Lerp(c.b, playerColor32.b, changePercent);
                }
                c.a = (byte)newValue;
            }
        }

        reqiredApply = true;
    }

    private void Apply()
    {
        floorTexture.SetPixels32(txtValues);
        floorTexture.Apply();
    }

    private Vector2Int TxtMinFromCenter(Vector2 point) => ((point - floorMin) * worldToArrayMultiplier).Floor();

    private float[,] CreateCircleTexture(float range, float multiplier)
    {
        var size = Mathf.RoundToInt(range * 2 * worldToArrayMultiplier);
        var values = new float[size, size];
        var squerRange = Mathf.Pow(range * worldToArrayMultiplier, 2);
        var blurShift = Mathf.Pow((range - blurLength) * worldToArrayMultiplier, 2);
        var invSR = 1 / (squerRange - blurShift);
        var centerX = Mathf.RoundToInt(size / 2);
        var centerY = Mathf.RoundToInt(size / 2);
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                var squerDist = new Vector2Int(x - centerX, y - centerY).sqrMagnitude;
                if (squerDist < squerRange)
                {
                    var bluredValue = Mathf.Clamp01(1 - (squerDist - blurShift) * invSR);
                    values[x, y] = bluredValue * multiplier;
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
