using UnityEngine;
using UnityEngine.UI;

public class SanityUI : MonoBehaviour
{

    public SanitySystem SS;

    public GameObject MediumCrack;
    public GameObject LowCrack;
    public GameObject BloodAni;
    
    public RectTransform LiquidAni;
    public float moveSpeed = 5f;
    public Vector2 StartPos;
    public Vector2 TargetPos;

    private Image LiquidImage;
    public float ColorSpeed = 2f;
    private Color TargetColor;
    private Color HighColor = new Color(0, 1f, 0f, 0.2f);
    private Color MediumColor = new Color(1f, 0.65f, 0f, 0.2f);
    private Color LowColor = new Color(1f, 0f, 0f, 0.2f);

    private void Awake()
    {
        MediumCrack.SetActive(false);
        LowCrack.SetActive(false);
        BloodAni.SetActive(false);

        StartPos = LiquidAni.anchoredPosition;
        TargetPos = StartPos;

        LiquidImage = LiquidAni.GetComponent<Image>();
        TargetColor = HighColor;
    }

    // Update is called once per frame
    void Update()
    {
        LiquidAni.anchoredPosition = Vector2.Lerp(LiquidAni.anchoredPosition, TargetPos, moveSpeed * Time.deltaTime);

        if (LiquidImage != null)
        {
            LiquidImage.color = Color.Lerp(LiquidImage.color, TargetColor, ColorSpeed * Time.deltaTime);
        }

        if (SS.currentState == SanitySystem.SanityState.High)
        {
            MediumCrack.SetActive(false);
            LowCrack.SetActive(false);
            BloodAni.SetActive(false);
            TargetPos = StartPos;
            TargetColor = HighColor;
        }
        else if (SS.currentState == SanitySystem.SanityState.Medium)
        {
            MediumCrack.SetActive(true);
            LowCrack.SetActive(false);
            BloodAni.SetActive(false);
            TargetPos = StartPos + new Vector2(0, -50);
            TargetColor = MediumColor;
        }
        else if (SS.currentState == SanitySystem.SanityState.Low)
        {
            MediumCrack.SetActive(false);
            LowCrack.SetActive(true);
            BloodAni.SetActive(true);
            TargetPos = StartPos + new Vector2(0, -95);
            TargetColor = LowColor;
        }
    }
}
