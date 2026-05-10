using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LoadingUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private Image filler;
    [SerializeField] private float timeToFill = 4f;

    private void Start()
    {
        gameObject.SetActive(true);
        filler.fillAmount = 0;
        InputManager.Instance.DeActiveInput();
    }

    // Update is called once per frame
    void Update()
    {
        if (filler.fillAmount == 1)
        {
            InputManager.Instance.ActiveInput();
            gameObject.SetActive(false);
            return;
        }
        filler.fillAmount += Time.deltaTime / timeToFill;
        loadingText.text = Mathf.RoundToInt(filler.fillAmount * 100) + "%";
    }
}
