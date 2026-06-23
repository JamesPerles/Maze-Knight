using UnityEngine;
    public class Frozen : MonoBehaviour
    {
        public float freezeDuration = 2f; 
        public Sprite dizzySprite; 
        Rigidbody2D _rb; 
        bool _isFrozen = false; 
        float _freezeTimer = 0f; 
        Color _originalColor; 
        PlayerMovement _playerMovement; 
        SpriteRenderer _spriteRenderer; 
        Sprite _originalSprite; 
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _originalColor = _spriteRenderer.color;
            _playerMovement = GetComponent<PlayerMovement>();
            _originalSprite = _spriteRenderer.sprite;
        }
        void Update()
        {
            if (_isFrozen)
            {
                _freezeTimer += Time.deltaTime;

                if (_freezeTimer >= freezeDuration)
                {
                    _isFrozen = false;
                    _freezeTimer = 0f;
                    _spriteRenderer.sprite = _originalSprite; 
                    _spriteRenderer.color = _originalColor; 
                    _playerMovement.enabled = true; 
                }
                else
                {
                    _spriteRenderer.sprite = dizzySprite; 
                    _spriteRenderer.color = Color.blue;
                    _rb.linearVelocity = Vector2.zero;
                    _playerMovement.enabled = false; 
                }
            }
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("IceBlock")) 
            {
                _isFrozen = true; 
            }
        }
    }
