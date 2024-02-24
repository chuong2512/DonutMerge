using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ChuongCustom
{
    public class ShopPanel : MonoBehaviour
    {
        public ShopSO ShopSo;

        public AButton[] shopBtns;
        public GameObject[] tabPanels;

        private void Start()
        {
            for (int i = 0; i < shopBtns.Length; i++)
            {
                var i1 = i;
                shopBtns[i].AddListener(() => ChooseButton(i1));
            }
        }

        private void ChooseButton(int tabID)
        {
            for (int i = 0; i < tabPanels.Length; i++)
            {
                tabPanels[i].SetActive(i == tabID);
                shopBtns[i].Choose(i == tabID);
            }
        }


        private void OnOpen()
        {
            ChooseButton(0);
        }
        
        public void Open()
        {
            transform.DOKill();
            transform.GetComponent<RectTransform>().DOAnchorPosX(0, 0.3f);
            
            OnOpen();
        }

        public void Close()
        {
            transform.DOKill();
            transform.GetComponent<RectTransform>().DOAnchorPosX(2000, 0.3f);
        }
    }
}