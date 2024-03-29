using UnityEngine;

public class PickUpItems : MonoBehaviour
{
    public enum ItemType
    {
        ExtraBomb,
        BlastRadius,
        SpeedIncrease,
    }


    public ItemType type;
    AudioManager audioManager;
    private void Awake()
    {
         audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }

    private void OnItemPickup(GameObject player)
    {
        switch (type)
        {
            case ItemType.ExtraBomb:
                player.GetComponent<Bomb_Controller>().AddBom();
                break;

            case ItemType.BlastRadius:
                player.GetComponent<Bomb_Controller>().explosionRadius++;
                break;

            case ItemType.SpeedIncrease:
                player.GetComponent<Movement_Controller>().speed++;
                break;
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.collect);
            OnItemPickup(other.gameObject);
        }
    }

}
