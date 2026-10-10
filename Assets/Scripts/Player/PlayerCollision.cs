using System.Collections;
using UnityEngine;

public class PlayerCollision : PlayerStats, IDamageable
{
    public float _currentHealth;

    float _maxHealth;

    SpriteRenderer _spriteRenderer;

    [SerializeField] Sprite tombstoneSprite;

    [SerializeField] float invincibleTime = 0.5f;

    float blinkInterval = 0.1f;

    private bool _isInvincible = false; 

    HandleHealthBar healthBar;

    protected override void Awake()
    {
      base.Awake();
       _spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar = GameObject.Find("GameManager").GetComponent<HandleHealthBar>();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _maxHealth = health;
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Death();
    }


    public void TakeDamage(float damage)
    {
        if (_isInvincible) return; 

        int _roundedDamage = Mathf.RoundToInt(damage);

        _currentHealth -= _roundedDamage / defenseModifier;

        healthBar.UpdateHealthBar(_maxHealth, _currentHealth);

        StartCoroutine(InvicibilityFrames());
    }

    IEnumerator InvicibilityFrames()
    {

        _isInvincible = true;

        float timer = 0f; 

        while(timer < invincibleTime)
        {
            Color color = _spriteRenderer.color;
            color.a = color.a == 1f ? 0.3f : 1f;
            _spriteRenderer.color = color;

            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval; 
        }

        Color finalColor = _spriteRenderer.color;
        finalColor.a = 1f;
        _spriteRenderer.color = finalColor;

        _isInvincible = false; 

        yield return null; 
        
    }

    public void Death()
    {
        if(_currentHealth <= 0)
        {
            _spriteRenderer.sprite = tombstoneSprite;
            Time.timeScale = 0f;

            // Change the tag to "Untagged" to prevent further collisions

            //End Game Logic Since It Is The Player
        }
    }
}
