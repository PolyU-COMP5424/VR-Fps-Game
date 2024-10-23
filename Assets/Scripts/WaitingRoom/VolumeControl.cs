using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    // BGM（背景音乐）滑动条和对应的文本
    public Slider BGMSlider;
    public Text BGMText;

    // SE（音效）滑动条和对应的文本
    public Slider SESlider;
    public Text SEText;

    void Start()
    {
        // 为滑动条添加事件监听器
        BGMSlider.onValueChanged.AddListener(OnBGMSliderChanged);
        SESlider.onValueChanged.AddListener(OnSESliderChanged);

        // 初始化文本显示
        OnBGMSliderChanged(BGMSlider.value);
        OnSESliderChanged(SESlider.value);
    }

    // 当 BGM 滑动条的值改变时调用
    void OnBGMSliderChanged(float value)
    {
        int mappedValue = Mathf.RoundToInt(MapValue(value, BGMSlider.minValue, BGMSlider.maxValue, 0, 100));
        BGMText.text = "音量：" + mappedValue;
    }

    // 当 SE 滑动条的值改变时调用
    void OnSESliderChanged(float value)
    {
        int mappedValue = Mathf.RoundToInt(MapValue(value, SESlider.minValue, SESlider.maxValue, 0, 100));
        SEText.text = "音效：" + mappedValue;
    }

    // 将滑动条的值映射到 0 - 100 的函数
    float MapValue(float value, float inMin, float inMax, float outMin, float outMax)
    {
        // 防止除以零
        if (Mathf.Approximately(inMax - inMin, 0))
            return outMin;

        return (value - inMin) / (inMax - inMin) * (outMax - outMin) + outMin;
    }
}
