using UnityEngine;
using System.Collections.Generic;

public class DamageManager : MonoBehaviour
{
    public static DamageManager Instance;

    [SerializeField] private DamageNumber damageNumberPrefab;
    [SerializeField] private int poolSize = 50;

    private Queue<DamageNumber> pool = new();

    void Awake()
    {
        Instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            DamageNumber number = Instantiate(damageNumberPrefab, transform);

            number.Initialize(this);
            number.gameObject.SetActive(false);

            pool.Enqueue(number);
        }
    }

    public void ShowDamage(float damage, Vector3 position)
    {
        if (pool.Count == 0) return;

        DamageNumber number = pool.Dequeue();

        Vector3 offset = new Vector3(Random.Range(-0.3f, 0.3f), 0.5f, 0f);

        number.transform.position = position + offset;

        number.Show(damage);
        number.gameObject.SetActive(true);
    }

    public void ReturnDamageNumber(DamageNumber number)
    {
        number.gameObject.SetActive(false);
        pool.Enqueue(number);
    }
}
