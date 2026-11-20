
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Inventory : MonoBehaviour
{
    public int keys = 0;
    public TMP_Text keyText;

    public bool hasPersimmon = false;
    public bool hasDeliveredPersimmon = false;

    // 감 전달 성공 시 실행할 이벤트
    public UnityEvent onPersimmonSuccess;

    // 감 전달 실패 시 실행할 이벤트
    public UnityEvent onPersimmonFail;

    public void AddKey()
    {
        keys += 1;

        if (keyText != null)
        {
            keyText.text = "제사 음식 " + keys;
        }
    }

    public void AddPersimmon()
    {
        hasPersimmon = true;
        Debug.Log("감을 획득했습니다.");
    }

    public void DeliverPersimmon()
    {
        if (hasDeliveredPersimmon)
        {
            // 이미 전달한 경우 성공 대사 표시
            onPersimmonSuccess?.Invoke();
            return;
        }

        if (!hasPersimmon)
        {
            Debug.Log("감이 없어서 전달할 수 없습니다.");

            // 감이 없을 때 대사 실행
            onPersimmonFail?.Invoke();
            return;
        }

        hasPersimmon = false;
        hasDeliveredPersimmon = true;

        Debug.Log("감 전달 완료!");

        // 감 전달 성공 시 대사 실행
        onPersimmonSuccess?.Invoke();
    }
}
