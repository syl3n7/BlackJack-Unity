using UnityEngine;

public class BetSlider : MonoBehaviour
{
    public TMPro.TMP_Text betBttn;
    public UnityEngine.UI.Slider betSlider;
    
    void Update()
    {
        betBttn.text = betSlider.value.ToString();
    }
}
