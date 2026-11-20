
using UnityEngine;

public class Item : MonoBehaviour
{
    // 체크하면 감 아이템으로 처리
    public bool isPersimmon = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inventory = other.GetComponent<Inventory>();

            if (inventory == null)
            {
                Debug.LogWarning("Player에 Inventory가 없습니다.");
                return;
            }

            if (isPersimmon)
            {
                // 감 획득
                inventory.AddPersimmon();
            }
            else
            {
                // 기존 제사 음식 획득
                inventory.AddKey();
            }

            Destroy(gameObject);
        }
    }
}
