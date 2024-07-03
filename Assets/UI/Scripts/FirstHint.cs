using UnityEngine.UI;
using UnityEngine;

public class FirstHint : MonoBehaviour
{
    private Text _hint;
    private Color _color;

    private float BlinkFadeInTime = 0.4f;
    private float BlinkStayTime = 0.6f;
    private float BlinkFadeOutTime = 1f;

    private float timeChecker = 0f;

    private void Start()
    {
        _hint = GetComponent<Text>();
        _color = _hint.color;
    }

    private void Update()
    {
        timeChecker += Time.deltaTime;

        if(timeChecker < BlinkFadeInTime)
        {
            _hint.color = new Color(_color.r, _color.g, _color.b, timeChecker / BlinkFadeInTime);
        }
        else if (timeChecker < BlinkFadeInTime + BlinkStayTime)
        {
            _hint.color = new Color(_color.r, _color.g, _color.b, 1);
        }
        else if (timeChecker < BlinkFadeInTime + BlinkStayTime + BlinkFadeOutTime)
        {
            _hint.color = new Color(_color.r, _color.g, _color.b, 
                1 - (timeChecker - (BlinkFadeInTime + BlinkStayTime)) / BlinkFadeOutTime);
        }
        else
        {
            timeChecker = 0;
        }
    }
}
