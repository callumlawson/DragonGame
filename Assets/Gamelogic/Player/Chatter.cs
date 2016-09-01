using System;
using Assets.Gamelogic.Messaging;
using Assets.Gamelogic.UI;
using JetBrains.Annotations;
using UnityEngine.Networking;

namespace Assets.Gamelogic.Player
{
    [Serializable]
    public class ChatInputMessage : CustomMsg
    {
        public string ChatLine;
    }

    class Chatter : NetworkBehaviour
    {
        #region Clientside
        [UsedImplicitly]
        public override void OnStartLocalPlayer()
        {
            if (isLocalPlayer)
            {
                //Cannot use method groups for commands!
                UIManager.Instance.ChatInput.onEndEdit.AddListener(HandleChatLine);
            }
        }

        private void HandleChatLine(string line)
        {
            CmdSendChatLine(line);
            UIManager.Instance.ChatInput.text = string.Empty;
        }
        #endregion

        #region Serverside
        [Command]
        private void CmdSendChatLine(string line)
        {
            //Send network message to server
            NewMessenger.Broadcast(new ChatInputMessage { ChatLine = string.Format("Player {0}: {1}", connectionToClient.connectionId + 1, line) });
            //Messenger.Broadcast(MessageTypes.ChatInput, string.Format("Player {0}: {1}", connectionToClient.connectionId + 1, line));
        }
        #endregion
    }
}