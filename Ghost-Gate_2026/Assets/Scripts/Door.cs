
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform target;
    public GameObject prompt;
    public DialogueBox dialogueBox;
    public int needKeys = 0;
    public Talk lockedTalk;

    // 감 전달이 필요한 문인지 설정
    public bool requirePersimmonDelivery = false;

    private Transform player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            prompt.SetActive(true);
            dialogueBox.nearDoor = this;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            prompt.SetActive(false);
            dialogueBox.nearDoor = null;
        }
    }

    public void Use()
    {
        if (player == null) return;

        Inventory inventory = player.GetComponent<Inventory>();

        if (inventory == null)
        {
            Debug.LogWarning("Player에 Inventory가 없습니다.");
            return;
        }

        // 감을 전달해야 하는 문인데 아직 전달하지 않은 경우
        if (requirePersimmonDelivery &&
            !inventory.hasDeliveredPersimmon)
        {
            if (lockedTalk != null)
                lockedTalk.Say();

            return;
        }

        // 기존 제사 음식 개수 조건
        if (inventory.keys >= needKeys)
        {
            player.position = target.position;
        }
        else
        {
            if (lockedTalk != null)
                lockedTalk.Say();
        }
    }
}
