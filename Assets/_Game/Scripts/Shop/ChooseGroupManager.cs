using UnityEngine;
using UnityEngine.Serialization;

namespace ChuongCustom
{
    public class ChooseGroupManager : MonoBehaviour
    {
        public ShopChooseButton[] chooseBtns;

        private void Start()
        {
            for (int i = 0; i < chooseBtns.Length; i++)
            {
                var i1 = i;
                chooseBtns[i].AddListener(() => ChooseButton(i1));

                chooseBtns[i].Init(i);
            }
        }

        private void ChooseButton(int tabID)
        {
            if (chooseBtns[tabID].CheckLock())
            {
                return;
            }

            for (int i = 0; i < chooseBtns.Length; i++)
            {
                chooseBtns[i].Choose(i == tabID);
            }
        }
    }
}