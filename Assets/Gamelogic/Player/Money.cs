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
                //UIState.Instance.MoneyUI.text = "Money: " + newValue;
            }
        }
        #endregion

        //Move to server logic - Passive income behaviour.
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
