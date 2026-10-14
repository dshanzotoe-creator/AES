using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    [SerializeField] private float lifeTime = 0.7f;
    [SerializeField] private float floatSpeed = 1.5f;

    private float timer;
    private Color originalColor;

    private DamageManager manager;


    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
        originalColor = text.color;
    }


    public void Initialize(DamageManager poolManager)
    {
        manager = poolManager;
    }

    public void Show(float damage)
    {
        text.text = damage.ToString("0");
        text.color = originalColor;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        Color color = originalColor;
        color.a = 1f - Mathf.Clamp01(timer / lifeTime);

        text.color = color;

        if (timer >= lifeTime)
        {
            manager.ReturnDamageNumber(this);
        }
    }
}
