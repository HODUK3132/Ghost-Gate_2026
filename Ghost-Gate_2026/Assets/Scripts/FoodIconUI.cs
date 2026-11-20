
using UnityEngine;

public class FoodIconUI : MonoBehaviour
{
    [Header("현재 스테이지 (1~3)")]
    [Range(1, 3)]
    public int stageNumber = 1;

    [Header("현재 스테이지의 Player")]
    public Inventory inventory;

    [Header("아이콘 이미지 오브젝트")]
    public GameObject[] foodIcons = new GameObject[3];

    private bool iconUnlocked = false;

    void Start()
    {
        // 이전 스테이지 아이콘은 표시
        // 현재 및 이후 스테이지 아이콘은 숨김
        for (int i = 0; i < foodIcons.Length; i++)
        {
            if (foodIcons[i] != null)
            {
                foodIcons[i].SetActive(i < stageNumber - 1);
            }
        }
    }

    void Update()
    {
        if (iconUnlocked || inventory == null)
            return;

        // 기존 감 획득 상태를 감지
        if (inventory.hasPersimmon)
        {
            iconUnlocked = true;

            int index = stageNumber - 1;

            if (index >= 0 &&
                index < foodIcons.Length &&
                foodIcons[index] != null)
            {
                foodIcons[index].SetActive(true);
            }
        }
    }
}
