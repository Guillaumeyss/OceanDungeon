using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public float timeToDisplay = 0.6f;
    public TextMeshProUGUI textMeshPro;
    Color startingColor;
    float timeElapsed = 0.0f;
    Vector3 up = new Vector3(0,1,0);

    public float floatSpeed = 100f;

    RectTransform rectTransform;

    private void Start() {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
        startingColor = textMeshPro.color;
    }

    private void Update() {
        timeElapsed += Time.deltaTime;

        rectTransform.position += up * floatSpeed * Time.deltaTime;
        textMeshPro.color = new Color(startingColor.r, startingColor.g, startingColor.b, 1 - (timeElapsed / timeToDisplay)) ;

        if (timeElapsed > timeToDisplay)
        {
            Destroy(gameObject);
        }
    }
}
