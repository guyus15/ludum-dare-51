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
        _fireSound.Play();
    }

    private void PlayPickupSound(PlayerPickupEvent evt)
    {
        _ammoPickupSound.Play();
    }
}
