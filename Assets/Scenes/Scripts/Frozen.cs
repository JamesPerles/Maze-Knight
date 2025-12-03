using UnityEngine;

namespace Scenes.Scripts
{
    public class Frozen : MonoBehaviour
    {
        public float freezeDuration = 2f; 
        public Sprite dizzySprite; 

        private Rigidbody2D _rb; 
        private bool _isFrozen = false; 
        private float _freezeTimer = 0f; 
        private Color _originalColor; 
        private PlayerMovement _playerMovement; 
        private SpriteRenderer _spriteRenderer; 
        private Sprite _originalSprite; 


        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _originalColor = _spriteRenderer.color;
            _playerMovement = GetComponent<PlayerMovement>();
            _originalSprite = _spriteRenderer.sprite;
        }


        private void Update()
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
                    _rb.velocity = Vector2.zero;
                    _playerMovement.enabled = false; 
                }
            }
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("IceBlock")) 
            {
                _isFrozen = true; 
            }
        }
    }
}
