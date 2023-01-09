using UnityEngine;

public class BetSlider : MonoBehaviour
{
    public TMPro.TMP_Text betBttn;
    public UnityEngine.UI.Slider betSlider;
    public Player player;
    void Update()
    {
        betBttn.text = "$" + betSlider.value;
        betSlider.maxValue = player.GetMoney();
    }
}
