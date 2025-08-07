using UnityEngine;
using UnityEngine.EventSystems;

namespace Honjo
{
    public class UIRotateByMouse : MonoBehaviour, IDragHandler
    {
        RectTransform rectTransform;
        Vector2 centerScreenPos;

        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            centerScreenPos = RectTransformUtility.WorldToScreenPoint(null, rectTransform.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 prevPos = eventData.position - eventData.delta;
            Vector2 dirPrev = prevPos - centerScreenPos;
            Vector2 dirNow = eventData.position - centerScreenPos;

            float angle = Vector2.SignedAngle(dirPrev, dirNow);
            rectTransform.Rotate(0, 0, angle); // ZŽ²‰ñ“]
        }
    }
}
