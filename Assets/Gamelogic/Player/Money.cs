using Assets.Gamelogic.UI;
using JetBrains.Annotations;
using UnityEngine.Networking;

namespace Assets.Gamelogic.Player
{
    public class Money : NetworkBehaviour {

        #region Persistant State
        [SyncVar(hook = "OnBalanceUpdated")] public int CurrentBalance;
        #endregion

        #region Clientside
        [UsedImplicitly]
        public void OnBalanceUpdated(int newValue)
        {
            if (isLocalPlayer)
            {
                UIManager.Instance.MoneyUI.text = "Money: " + newValue;
            }
        }
        #endregion

        #region Serverside
        void Update()
        {
            if (isServer)
            {
                CurrentBalance += 1;
            }    
        }
        #endregion
    }
}
