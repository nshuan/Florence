using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScratcherInterpolateInverse : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private Material material;
    [SerializeField] private Image targetImage;
    [SerializeField] private float transparencyRadius = 0.1f; // Adjust for desired transparency radius
    [SerializeField] private RenderTexture maskTexture;

    public Action OnComplete;

    private Texture2D _maskTexture2D;
    private Vector2? lastUVPosition = null; // Track the last UV position for interpolation

    void Start()
    {
        // Initialize the mask texture as a blank Texture2D
        _maskTexture2D = new Texture2D(maskTexture.width, maskTexture.height, TextureFormat.Alpha8, false);
        ClearMaskTexture(); // Start with a fully opaque mask
        Graphics.Blit(_maskTexture2D, maskTexture); // Copy to the RenderTexture

        // Assign the mask texture to the shader
        material.SetTexture("_AlphaMask", maskTexture);
    }

    public void SetMaterial()
    {
        targetImage.material = material;
    }

    public void SetImageColorAlpha(float alpha)
    {
        var c = targetImage.color;
        c.a = alpha;
        targetImage.color = c;
    }
    
    public void ProcessScratch(PointerEventData eventData)
    {
        Vector2 localPoint;
        // Convert screen position to local coordinates relative to the Image's RectTransform
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            targetImage.rectTransform,
            eventData.position,
            eventData.pressEventCamera, // Pass the camera if using Screen Space - Camera, otherwise pass null
            out localPoint
        );

        // Convert local point to normalized UV coordinates
        var rect = targetImage.rectTransform.rect;
        var uv = new Vector2(
            (localPoint.x - rect.x) / rect.width,
            (localPoint.y - rect.y) / rect.height
        );

        if (lastUVPosition.HasValue)
        {
            // Interpolate between the last position and the current position
            var startUV = lastUVPosition.Value;
            var endUV = uv;
            var distance = Vector2.Distance(startUV, endUV);
            
            // Adjust the step size to reduce the number of interpolated points
            var step = Mathf.Max(transparencyRadius * 0.5f, distance / 5f); // For example, use max of radius or a fraction of distance

            // Loop through points along the line to fill gaps
            for (float t = 0; t <= distance; t += step)
            {
                var intermediateUV = Vector2.Lerp(startUV, endUV, t / distance);
                ApplyTransparency(intermediateUV);
            }
        }

        // Set the current UV as the last position
        lastUVPosition = uv;

        // Also apply transparency at the current UV
        ApplyTransparency(uv);
    }

    private void ApplyTransparency(Vector2 uv)
    {
        // Convert UV to texture pixel coordinates
        var x = (int)(uv.x * _maskTexture2D.width);
        var y = (int)(uv.y * _maskTexture2D.height);

        // Draw a transparent circle in the mask texture at this location
        DrawTransparentCircle(x, y, transparencyRadius * _maskTexture2D.width);

        // Apply the updated texture
        _maskTexture2D.Apply();
        Graphics.Blit(_maskTexture2D, maskTexture); // Copy the changes to the RenderTexture
    }
    
    private void DrawTransparentCircle(int centerX, int centerY, float radius)
    {
        var rSquared = (int)(radius * radius);

        for (var y = -Mathf.CeilToInt(radius); y <= Mathf.CeilToInt(radius); y++)
        {
            for (var x = -Mathf.CeilToInt(radius); x <= Mathf.CeilToInt(radius); x++)
            {
                var dx = x + centerX;
                var dy = y + centerY;

                if (dx >= 0 && dx < _maskTexture2D.width && dy >= 0 && dy < _maskTexture2D.height)
                {
                    if (x * x + y * y <= rSquared)
                    {
                        _maskTexture2D.SetPixel(dx, dy, new Color(0, 0, 0, 1)); // Transparent pixel
                    }
                }
            }
        }
    }

    private void ClearMaskTexture()
    {
        // Set the mask texture to fully opaque initially
        var pixels = _maskTexture2D.GetPixels();
        for (var i = 0; i < pixels.Length; i++)
            pixels[i] = Color.clear; // White = fully opaque
        _maskTexture2D.SetPixels(pixels);
        _maskTexture2D.Apply();
    }
    
    private bool IsImageFullyOpaque(Texture2D texture, float required = 0.9f)
    {
        // Get all the pixels in the texture
        var pixels = texture.GetPixels();

        // Get total transparent pixels
        var totalOpaque = pixels.Count(pixel => pixel.a > 0.99f);

        return (float)totalOpaque / pixels.Length >= required; // Required percentage of pixels are opaque
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (targetImage.material != material) return;
        ProcessScratch(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (targetImage.material != material) return;
        ProcessScratch(eventData);
    }

    private bool isComplete;
    public void OnPointerUp(PointerEventData eventData)
    {
        lastUVPosition = null;
        
        if (isComplete) return;
        if (IsImageFullyOpaque(_maskTexture2D))
        {
#if UNITY_EDITOR
            Debug.Log("The image is fully opaque.");
#endif
            isComplete = true;
            OnComplete?.Invoke();
        }
    }
}