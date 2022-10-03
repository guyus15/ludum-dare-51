using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _fireSound;
    [SerializeField] private AudioSource _ammoPickupSound;

    private void Awake()
    {
        EventManager.AddListener<PlayerFireWeaponEvent>(PlayFireSound);
        EventManager.AddListener<PlayerPickupEvent>(PlayPickupSound);
    }

    private void PlayFireSound(PlayerFireWeaponEvent evt)
    {
        if (_fireSound != null)
        { 
            _fireSound.Play();
        }
    }
    
    private void PlayPickupSound(PlayerPickupEvent evt)
    {
        if (_ammoPickupSound != null)
        {
            _ammoPickupSound.Play();
        }
    }
}
