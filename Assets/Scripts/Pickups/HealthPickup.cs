using UnityEngine;

public class HealthPickup : MonoBehaviour, IPickup
{
    public void Pickup()
    {
        PlayerPickupEvent pickupEvt = Events.s_PlayerPickupEvent;
        pickupEvt.type = PlayerPickupEvent.PickupType.HEALTH;
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