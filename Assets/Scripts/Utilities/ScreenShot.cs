using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HCore
{
    public static class ScreenShot
    {
        private readonly static Queue<ScreenShotRequest> _requests = new();
        private static Camera _camera;

        public static void Take(ScreenShotRequest request)
        {
            if (_camera == null)
            {
                _camera = Object.FindFirstObjectByType<Camera>();
            }
            _requests.Enqueue(request);
            TryProccesNext();
        }

        private static async void TryProccesNext()
        {
            if (_requests.Count > 0)
            {
                await Awaitable.NextFrameAsync();
                CaptureScreen();
            }
        }

        private static void CaptureScreen()
        {
            var request = _requests.Dequeue();
            var rt = new RenderTexture(request.Width, request.Height, 24);
            _camera.targetTexture = rt;
            var screenShot = new Texture2D(request.Width, request.Height, request.Format, false);
            _camera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, request.Width, request.Height), 0, 0);
            _camera.targetTexture = null;
            RenderTexture.active = null;
            Object.Destroy(rt);
            var data = screenShot.EncodeToPNG();

            request.Callback(data);

            TryProccesNext();
        }
    }

    public class ScreenShotRequest
    {
        public int Width { get; }
        public int Height { get; }
        public TextureFormat Format { get; } = TextureFormat.ARGB32;
        public Action<byte[]> Callback { get; }

        public ScreenShotRequest(int width, int height, Action<byte[]> callback)
        {
            Width = width;
            Height = height;
            Callback = callback;
        }
        public ScreenShotRequest(Action<byte[]> callback) : this(Screen.width, Screen.height, callback) { }
    }
}