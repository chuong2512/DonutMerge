using GameCore.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class ButtonClick : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private bool vibration = true;
        [SerializeField] private bool sound = true;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if(vibration) NiceVibration.Instance.Light();
            if(sound) AudioManager.Instance.PlaySFX("click");
        }
    }
}