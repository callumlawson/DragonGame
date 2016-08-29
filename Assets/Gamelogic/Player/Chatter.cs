using Assets.Gamelogic.Messaging;
using Assets.Gamelogic.UI;
using JetBrains.Annotations;
using UnityEngine.Networking;

namespace Assets.Gamelogic.Player
{
    class Chatter : NetworkBehaviour
    {
        #region Clientside
        [UsedImplicitly]
        public override void OnStartLocalPlayer()
        {
            if (isLocalPlayer)
            {
                //Cannot use method groups for commands!
                //UIState.Instance.ChatInput.onEndEdit.AddListener(HandleChatLine);
            }
        }

        private void HandleChatLine(string line)
        {
            CmdSendChatLine(line);
            //UIState.Instance.ChatInput.text = string.Empty;
        }
        #endregion

        #region Serverside
        [Command]
        private void CmdSendChatLine(string line)
        {
            //Send network message to server
            //Messenger.Broadcast(MessageTypes.ChatInput, string.Format("Player {0}: {1}", connectionToClient.connectionId + 1, line));
        }
        #endregion
    }
}