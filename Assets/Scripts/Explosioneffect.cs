using UnityEngine;

namespace schmup
{
    // Zerstört das Explosions-Partikelobjekt automatisch, sobald das
    // Particle System fertig abgespielt hat.
    [RequireComponent(typeof(ParticleSystem))]
    public class ExplosionEffect : MonoBehaviour
    {
        void Start()
        {
            ParticleSystem ps = GetComponent<ParticleSystem>();
            float lifetime = ps.main.duration + ps.main.startLifetime.constantMax;
            Destroy(gameObject, lifetime);
        }
    }
}
