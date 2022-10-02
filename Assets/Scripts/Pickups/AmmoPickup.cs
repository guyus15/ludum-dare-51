using UnityEngine;

public class AmmoPickup : MonoBehaviour, IPickup
{
    public void Pickup()
    {
        PlayerPickupEvent pickupEvt = Events.s_PlayerPickupEvent;
        pickupEvt.type = PlayerPickupEvent.PickupType.AMMO;
        EventManager.Broadcast(pickupEvt);

        // Destroy itself
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Pickup();
        }
    }
}