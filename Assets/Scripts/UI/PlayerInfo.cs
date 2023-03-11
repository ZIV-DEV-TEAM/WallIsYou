using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class PlayerInfo : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerName;
        [SerializeField] private TextMeshProUGUI playerDieCount;

        public void SetInfo(string name, string dieCount)
        {
            playerName.text = name;
            playerDieCount.text = dieCount;
        }
    }
}