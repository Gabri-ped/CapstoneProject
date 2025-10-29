using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class ChangeColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private TMP_Text _text;
        [SerializeField] private Color _selectColor;
        private Color _baseColor;

        void Start()
        {
            _text = GetComponent<TMP_Text>();
            _baseColor = _text.color;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _text.color = _selectColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _text.color = _baseColor;
        }
    }
