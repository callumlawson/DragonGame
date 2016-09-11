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

    internal class Chatter : NetworkBehaviour
    {
        #region Clientside

        [UsedImplicitly]
        public override void OnStartClient()
        {
            //Cannot use method groups for commands!
            UIManager.Instance.ChatInput.onEndEdit.AddListener(HandleChatLine);
        }

        private void HandleChatLine(string line)
        {
            Messenger.Broadcast(new ChatInputMessage { ChatLine = string.Format(line) });
            UIManager.Instance.ChatInput.text = string.Empty;
        }

        #endregion
    }
}