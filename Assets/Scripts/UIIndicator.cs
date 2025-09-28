using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIIndicator : MonoBehaviour
{
    public RectTransform leftBar;
    public RectTransform rightBar;

    public Transform targetTransform;

    public float maxWidth = 200f;

    public float rotationFactor = 90f;

    void Update()
    {
        if (targetTransform == null || leftBar == null || rightBar == null)
        {
            Debug.LogError("UI Bar problem");
            return;
        }

        float currentAngle = targetTransform.localEulerAngles.z;

        if (currentAngle > 180f)
        {
            currentAngle -= 360f;
        }

        float normalizedValue = currentAngle / rotationFactor;

        normalizedValue = Mathf.Clamp(normalizedValue, -1f, 1f);

        float absoluteValue = Mathf.Abs(normalizedValue);

        float targetWidth = absoluteValue * maxWidth;

        if (normalizedValue > 0)
        {
            rightBar.sizeDelta = new Vector2(targetWidth, rightBar.sizeDelta.y);
            leftBar.sizeDelta = new Vector2(0, leftBar.sizeDelta.y);
        }
        else if (normalizedValue < 0)
        {
            leftBar.sizeDelta = new Vector2(targetWidth, leftBar.sizeDelta.y);
            rightBar.sizeDelta = new Vector2(0, rightBar.sizeDelta.y);
        }
        else
        {
            rightBar.sizeDelta = new Vector2(0, rightBar.sizeDelta.y);
            leftBar.sizeDelta = new Vector2(0, leftBar.sizeDelta.y);
        }
    }
}