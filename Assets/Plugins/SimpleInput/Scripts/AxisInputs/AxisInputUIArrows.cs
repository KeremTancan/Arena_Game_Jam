using UnityEngine;
using UnityEngine.EventSystems;

namespace SimpleInputNamespace
{
    public class AxisInputUIArrows : MonoBehaviour, ISimpleInputDraggable
    {
        public SimpleInput.AxisInput xAxis = new SimpleInput.AxisInput("Horizontal");
        public SimpleInput.AxisInput yAxis = new SimpleInput.AxisInput("Vertical"); // Add vertical axis input

        public float valueMultiplier = 1f;

#pragma warning disable 0649
        [Tooltip("Radius of the deadzone at the center of the arrows that will yield no input")]
        [SerializeField]
        private float deadzoneRadius;
        private float deadzoneRadiusSqr;
#pragma warning restore 0649

        private RectTransform rectTransform;

        private Vector2 m_value = Vector2.zero;
        public Vector2 Value { get { return m_value; } }

        private void Awake()
        {
            rectTransform = (RectTransform)transform;
            gameObject.AddComponent<SimpleInputDragListener>().Listener = this;

            deadzoneRadiusSqr = deadzoneRadius * deadzoneRadius;
        }

        private void OnEnable()
        {
            xAxis.StartTracking();
            yAxis.StartTracking(); // Start tracking vertical input
        }

        private void OnDisable()
        {
            xAxis.StopTracking();
            yAxis.StopTracking(); // Stop tracking vertical input
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            deadzoneRadiusSqr = deadzoneRadius * deadzoneRadius;
        }
#endif

        public void OnPointerDown(PointerEventData eventData)
        {
            CalculateInput(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            CalculateInput(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            m_value = Vector2.zero;
            xAxis.value = 0f;
            yAxis.value = 0f; // Reset vertical input
        }

        private void CalculateInput(PointerEventData eventData)
        {
            Vector2 pointerPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out pointerPos);

            if (Mathf.Abs(pointerPos.x) <= deadzoneRadiusSqr) // Check horizontal deadzone
                m_value.x = 0f;
            else
                m_value.x = pointerPos.x >= 0f ? valueMultiplier : -valueMultiplier;

            if (Mathf.Abs(pointerPos.y) <= deadzoneRadiusSqr) // Check vertical deadzone
                m_value.y = 0f;
            else
                m_value.y = pointerPos.y >= 0f ? valueMultiplier : -valueMultiplier;

            xAxis.value = m_value.x;
            yAxis.value = m_value.y; // Update vertical input
        }
    }
}
