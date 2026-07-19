using UnityEngine;

namespace schmup
{
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