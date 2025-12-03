using UnityEngine;

namespace Scenes.Scripts
{
    public class Cooldown : MonoBehaviour
    {
        public float attackCooldown = 30f;      

        private PlayerAttack _playerAttack;      
        private float _cooldownTimeRemaining;    
        
        private void Start()
        {
            
            _playerAttack = GetComponent<PlayerAttack>();

            
            _cooldownTimeRemaining = 0f;
        }

        
        private void Update()
        {
            if (!_playerAttack.IsAttacking && _cooldownTimeRemaining <= 0f)
            {
               
                _playerAttack.enabled = true;
            }
            else
            {
                
              if (_playerAttack.IsAttacking)
                {
                   
                    _playerAttack.enabled = false;
                }

               if (_cooldownTimeRemaining > 0f)
                {
                    
                    _cooldownTimeRemaining -= Time.deltaTime;
                }
               else if (_playerAttack.IsAttacking)
                {
                   
                    _cooldownTimeRemaining = attackCooldown;
                }
            }
        }
    }
}