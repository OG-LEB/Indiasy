using UnityEngine;

public class KotelParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    public void PlayParticles() 
    {
        _particleSystem.Play();
    }
}
