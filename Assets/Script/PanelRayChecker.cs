using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Smarteye {
    public class PanelRaycastChecker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        public UnityEvent onEnterHoverRaycast;
        public UnityEvent onExitHoverRaycast;

        public void OnPointerEnter(PointerEventData eventData) {
            onEnterHoverRaycast?.Invoke();
        }
        public void OnPointerExit(PointerEventData eventData) {
            onExitHoverRaycast?.Invoke();
        }
    }
}
