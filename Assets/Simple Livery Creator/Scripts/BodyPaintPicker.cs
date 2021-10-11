using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class BodyPaintPicker : MonoBehaviour
{
    public Color Color { get { return _color; } set { Setup(value); } }
    public void SetOnValueChangeCallback(Action<Color> onValueChange)
    {
        _onValueChange = onValueChange;
    }
    Color _color = Color.red;
    [HideInInspector]
    public Color resultColor;
    private Action<Color> _onValueChange;
    [HideInInspector]
    public Action _update, idle;
    LiveryCreator liveryCreator;
    [HideInInspector]
    public int UVChanged = 0;
    [HideInInspector]
    public bool overwrite = true;
    public bool hflip;
    public bool vflip;

    void Start()
    {
        liveryCreator = FindObjectOfType<LiveryCreator>();
        liveryCreator.canvasMaterial.SetTexture("_DetailAlbedoMap", null);
        liveryCreator.canvasMaterial.SetFloat("_FirstTexUVSet",0);
    }

    private static void RGBToHSV(Color color, out float h, out float s, out float v)
    {
        var cmin = Mathf.Min(color.r, color.g, color.b);
        var cmax = Mathf.Max(color.r, color.g, color.b);
        var d = cmax - cmin;
        if (d == 0)
        {
            h = 0;
        }
        else if (cmax == color.r)
        {
            h = Mathf.Repeat((color.g - color.b) / d, 6);
        }
        else if (cmax == color.g)
        {
            h = (color.b - color.r) / d + 2;
        }
        else
        {
            h = (color.r - color.g) / d + 4;
        }
        s = cmax == 0 ? 0 : d / cmax;
        v = cmax;
    }

    private static bool GetLocalMouse(GameObject go, out Vector2 result)
    {
        var rt = (RectTransform)go.transform;
        var mp = rt.InverseTransformPoint(Input.mousePosition);
        result.x = Mathf.Clamp(mp.x, rt.rect.min.x, rt.rect.max.x);
        result.y = Mathf.Clamp(mp.y, rt.rect.min.y, rt.rect.max.y);
        return rt.rect.Contains(mp);
    }

    private static Vector2 GetWidgetSize(GameObject go)
    {
        var rt = (RectTransform)go.transform;
        return rt.rect.size;
    }

    private GameObject GO(string name)
    {
        return transform.Find(name).gameObject;
    }

    public void Setup(Color inputColor)
    {
        var satvalGO = GO("SaturationValue");
        var satvalKnob = GO("SaturationValue/Knob");
        var hueGO = GO("Hue");
        var hueKnob = GO("Hue/Knob");
        var result = GO("Result");
        var hueKnobImg = GO("Hue/Knob/Image");
        var hexCode = GO("HEX CODE");
        var hueColors = new Color[] {
            Color.red,
            Color.yellow,
            Color.green,
            Color.cyan,
            Color.blue,
            Color.magenta,
        };
        var satvalColors = new Color[] {
            new Color( 0.1f, 0.1f, 0.1f ),
            new Color( 0.1f, 0.1f, 0.1f ),
            new Color( 0.9f, 0.9f, 0.9f ),
            hueColors[0],
        };
        var hueTex = new Texture2D(1, 7);
        for (int i = 0; i < 7; i++)
        {
            hueTex.SetPixel(0, i, hueColors[i % 6]);
        }
        hueTex.Apply();
        hueGO.GetComponent<Image>().sprite = Sprite.Create(hueTex, new Rect(0, 0.5f, 1, 6), new Vector2(0.5f, 0.5f));
        var hueSz = GetWidgetSize(hueGO);
        var satvalTex = new Texture2D(2, 2);
        satvalGO.GetComponent<Image>().sprite = Sprite.Create(satvalTex, new Rect(0.5f, 0.5f, 1, 1), new Vector2(0.5f, 0.5f));
        Action resetSatValTexture = () =>
        {
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < 2; i++)
                {
                    satvalTex.SetPixel(i, j, satvalColors[i + j * 2]);
                }
            }
            satvalTex.Apply();
        };
        var satvalSz = GetWidgetSize(satvalGO);
        float Hue, Saturation, Value;
        RGBToHSV(inputColor, out Hue, out Saturation, out Value);
        Action applyHue = () =>
        {
            var i0 = Mathf.Clamp((int)Hue, 0, 5);
            var i1 = (i0 + 1) % 6;
            var resultColor = Color.Lerp(hueColors[i0], hueColors[i1], Hue - i0);
            satvalColors[3] = resultColor;
            resetSatValTexture();
        };
        Action applySaturationValue = () =>
        {
            var sv = new Vector2(Saturation, Value);
            var isv = new Vector2(1 - sv.x, 1 - sv.y);
            var c0 = isv.x * isv.y * satvalColors[0];
            var c1 = sv.x * isv.y * satvalColors[1];
            var c2 = isv.x * sv.y * satvalColors[2];
            var c3 = sv.x * sv.y * satvalColors[3];
            resultColor = c0 + c1 + c2 + c3;
            var resImg = result.GetComponent<Image>();
            var hki = hueKnobImg.GetComponent<Image>();
            var hc = hexCode.GetComponent<Text>();
            resImg.color = resultColor;
            hki.color = resultColor;
            hc.text = ColorUtility.ToHtmlStringRGBA(resultColor);
            if (_color != resultColor)
            {
                if (_onValueChange != null)
                {
                    _onValueChange(resultColor);
                }
                _color = resultColor;
            }
        };
        applyHue();
        applySaturationValue();
        satvalKnob.transform.localPosition = new Vector2(Saturation * satvalSz.x, Value * satvalSz.y);
        hueKnob.transform.localPosition = new Vector2(hueKnob.transform.localPosition.x, Hue / 6 * satvalSz.y);
        Action dragH = null;
        Action dragSV = null;
        idle = () =>
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mp;
                if (GetLocalMouse(hueGO, out mp))
                {
                    _update = dragH;
                }
                else if (GetLocalMouse(satvalGO, out mp))
                {
                    _update = dragSV;
                }
            }
        };
        dragH = () =>
        {
            Vector2 mp;
            GetLocalMouse(hueGO, out mp);
            Hue = mp.y / hueSz.y * 6;
            applyHue();
            applySaturationValue();
            hueKnob.transform.localPosition = new Vector2(hueKnob.transform.localPosition.x, mp.y);
            if (Input.GetMouseButtonUp(0))
            {
                _update = idle;
            }
        };
        dragSV = () =>
        {
            Vector2 mp;
            GetLocalMouse(satvalGO, out mp);
            Saturation = mp.x / satvalSz.x;
            Value = mp.y / satvalSz.y;
            applySaturationValue();
            satvalKnob.transform.localPosition = mp;
            if (Input.GetMouseButtonUp(0))
            {
                _update = idle;
            }
        };
        _update = idle;
    }


    void Awake()
    {
        Color = Color.white;
    }

    void Update()
    {
        _update();
    }

    public void MetallicChange(float v)
    {
        liveryCreator = FindObjectOfType<LiveryCreator>();
        liveryCreator.canvasMaterial.SetFloat("_Metallic", GetComponentsInChildren<Slider>()[0].value);
    }
    public void SmoothnessChange(float v)
    {
        liveryCreator = FindObjectOfType<LiveryCreator>();
        liveryCreator.canvasMaterial.SetFloat("_Glossiness", GetComponentsInChildren<Slider>()[1].value);
    }

    public void HQMetal(Texture t)
    {
        liveryCreator = FindObjectOfType<LiveryCreator>();
        if (liveryCreator.canvasMaterial.GetTexture("_SecondTex") != null)
        {
            //liveryCreator.canvasMaterial.EnableKeyword("_DETAIL_MULX2");
            liveryCreator.canvasMaterial.SetTexture("_SecondTex", null);
            //liveryCreator.canvasMaterial.SetFloat("_Metallic", 0.02f);

        }

        else
        {
            //liveryCreator.canvasMaterial.EnableKeyword("_DETAIL_MULX2");
            liveryCreator.canvasMaterial.SetTexture("_SecondTex", t);
            //liveryCreator.canvasMaterial.SetFloat("_Metallic", 0.8f);

        }

    }
    public void UVSet()
    {
        UVChanged++; 
        if (UVChanged % 2 == 0)
                liveryCreator.canvasMaterial.SetFloat("_FirstTexUVSet", 0);
else
                liveryCreator.canvasMaterial.SetFloat("_FirstTexUVSet", 1);


    }

    public void Overwrite()
    {
        overwrite = !overwrite;
    }
    public void HFlip()
    {
        hflip = !hflip;
    }public void VFlip()
    {
        vflip = !vflip;
    }

}
