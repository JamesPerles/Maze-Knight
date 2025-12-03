using UnityEngine;

namespace Scenes.Scripts
{
    public class PlayerInvincibility : MonoBehaviour
    {
        public float ignoreDuration = 5f; 
        public float ignoreCooldown = 30f; 
        public LayerMask ignoredLayers; 

        private float _ignoreTimeRemaining; 
        private float _cooldownTimeRemaining; 

        
        private void Start()
        {
            _ignoreTimeRemaining = 0f;
            _cooldownTimeRemaining = 0f;
        }

        
        private void Update()
        {
            
            if (_ignoreTimeRemaining > 0f)
            {
                
                _ignoreTimeRemaining -= Time.deltaTime;

                
                Physics2D.IgnoreLayerCollision(gameObject.layer, ignoredLayers);
            }
            else
            {
               
                Physics2D.IgnoreLayerCollision(gameObject.layer, ignoredLayers, false);
            }

            
            if (_cooldownTimeRemaining > 0f)
            {
                
                _cooldownTimeRemaining -= Time.deltaTime;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                
                _ignoreTimeRemaining = ignoreDuration;
                _cooldownTimeRemaining = ignoreCooldown;
            }
        }
    }
}