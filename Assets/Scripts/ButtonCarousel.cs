using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class ButtonCarousel : MonoBehaviour
{
    public ARFilterManager filterManager;

    public GameObject buttonPrefab;      // Kéo Prefab Button vào đây
    public Transform contentParent;      // Gán object "Content"

    private List<Button> buttons = new List<Button>();
    private Button activeButton = null;

    [Header("Scale Settings")]
    public Vector3 normalScale = Vector3.one;
    public Vector3 activeScale = new Vector3(1.2f, 1.2f, 1f);  // Scale khi active

    void Start()
    {
        for (int i = 0; i < filterManager.filters.Length; i++)
        {
            GameObject btnObj = Instantiate(buttonPrefab, contentParent);
            btnObj.transform.Find("Image").GetComponent<Image>().sprite = filterManager.filters[i].filterSprite;
            Button btn = btnObj.GetComponent<Button>();
            buttons.Add(btn);

            int index = i;
            btn.onClick.AddListener(() => OnButtonClicked(btn, index));
        }
    }

    void OnButtonClicked(Button clickedBtn, int index)
    {
        foreach (Button btn in buttons)
        {
            btn.transform.localScale = normalScale;
        }

        clickedBtn.transform.localScale = activeScale;
        activeButton = clickedBtn;

        // Xóa tất cả face hiện tại
        foreach (var face in filterManager.faceManager.trackables)
        {
            Destroy(face.gameObject);
        }

        // Đặt prefab mới
        StartCoroutine(SetNewFilter(index));
    }

    IEnumerator SetNewFilter(int index)
    {
        filterManager.faceManager.enabled = false;

        foreach (var face in filterManager.faceManager.trackables)
        {
            Destroy(face.gameObject);
        }

        yield return null;

        filterManager.faceManager.facePrefab = filterManager.filters[index].filter;
        Debug.Log("New filter applied: " + filterManager.filters[index].filter.name);

        filterManager.faceManager.enabled = true;
    }
}
