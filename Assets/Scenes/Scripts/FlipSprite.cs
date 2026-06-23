using UnityEngine;
    public class FlipSprite : MonoBehaviour
    {
        SpriteRenderer _spriteRenderer; 
        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>(); 
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _spriteRenderer.flipX = true; 
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _spriteRenderer.flipX = false; 
            }
        }
    }