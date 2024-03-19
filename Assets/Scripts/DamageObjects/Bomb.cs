using System.Collections;
using Interfaces;
using UnityEngine;

namespace DamageObjects
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private ParticleSystem explosionEffect;
        [SerializeField] private Transform mesh;
        [SerializeField] private AudioSource sound;
    
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out IDamageable obj)) return;
            Destroy(mesh.gameObject);
            StartCoroutine(PlayEffect());
        }

        private IEnumerator PlayEffect()
        {
            explosionEffect.Play();
            sound.Play();
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
    }
}
