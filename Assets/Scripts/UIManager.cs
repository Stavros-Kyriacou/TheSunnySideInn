using System.Collections;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Notify Text")]
    [SerializeField] private TextMeshProUGUI notifyText;
    [SerializeField] private Color32 startColour = new Color32(255, 255, 255, 255);
    [SerializeField] private Color32 endColour = new Color32(255, 255, 255, 0);
    private float elapsedTime = 0f;
    private Coroutine fadingText = null;

    private void Awake()
    {
        Instance = this;
    }
    public void DisplayNotifyText(string message)
    {
        if (fadingText != null)
        {
            StopAllCoroutines();
            notifyText.color = startColour;
            notifyText.enabled = false;
            fadingText = StartCoroutine(FadeText(message));
        }

        fadingText = StartCoroutine(FadeText(message));
    }
    private IEnumerator FadeText(string message)
    {
        elapsedTime = 0f;

        notifyText.enabled = true;
        notifyText.text = message;

        while (elapsedTime < 1)
        {
            notifyText.color = Color32.Lerp(startColour, endColour, elapsedTime / 1);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        notifyText.color = endColour;
        notifyText.text = "";

        fadingText = null;
    }

}
