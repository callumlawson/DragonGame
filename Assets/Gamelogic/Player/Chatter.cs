using Assets.Gamelogic.Map;
using Assets.Gamelogic.UI;
using JetBrains.Annotations;
using UnityEngine;
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
        private ChatManager chatManger;

        [UsedImplicitly]
        public override void OnStartServer()
        {
            chatManger = ChatManager.Instance;
        }

        [Command]
        private void CmdSendChatLine(string line)
        {
            //TODO: Sanatise Input
            chatManger.LogChat(string.Format("Player {0}: {1}", connectionToClient.connectionId + 1, line));
        }
        #endregion
    }
}