using UnityEngine;
using UnityEngine.Serialization;

namespace ChuongCustom
{
    public class HomeTab : MonoBehaviour
    {
        public AButton[] homeBtns;

        private void Start()
        {
            for (int i = 0; i < homeBtns.Length; i++)
            {
                var i1 = i;
                homeBtns[i].AddListener(() => ChooseButton(i1));
            }
        }

        private void ChooseButton(int tabID)
        {
            for (int i = 0; i < homeBtns.Length; i++)
            {
                homeBtns[i].Choose(i == tabID);
            }
        }
    }
}