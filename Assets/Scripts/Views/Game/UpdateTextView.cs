using UnityEngine;
using UnityEngine.UI;

public class UpdateTextView : MonoBehaviour
{
    [SerializeField] 
    private Text _text;
    [SerializeField] 
    private string _stringInText;
    [SerializeField] 
    private bool _isFirstString;

    public void UpdateText(int value)
    {
        if (_isFirstString)
        {
            _text.text = _stringInText + " " + value;
        }
        else
        {
            _text.text = value + " " + _stringInText;
        }
    }
}
