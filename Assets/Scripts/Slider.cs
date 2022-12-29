using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Slider : MonoBehaviour
{
    public UnityEngine.UI.Slider slider;
    public UnityEngine.UI.Image background;
    public UnityEngine.UI.Slider.SliderEvent onValueChanged;
    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ValueChangeCheck()
    {
        background.color = Color.HSVToRGB(slider.value, 1, 1);
    }
}
