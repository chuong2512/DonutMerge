using UnityEngine;

namespace _Game.Scripts.Game
{
    [CreateAssetMenu(fileName = "Skin 0", menuName = "ScriptableObjects/SkinSO")]
    public class SkinSO : ScriptableObject
    {
        [field: SerializeField] public Sprite[] skins;

        public Sprite this[int level] => skins[level];
    }
}