using Assets.Gamelogic.Messaging;
using Assets.Gamelogic.Player;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Assets.Gamelogic.Map
{
    [NetworkSettings(channel = 2)]
    public class ChatManager : NetworkBehaviour
    {
        #region Persistant State
        [SerializeField] public SyncListString ChatLog = new SyncListString();
        #endregion

        #region Clientside
        [SerializeField] private Text chatHistory;
       
        [UsedImplicitly]
        public override void OnStartClient()
        {
            ChatLog.Callback = OnChatUpdated;
        }

        private void OnChatUpdated(SyncListString.Operation op, int index)
        {
            var newChat = string.Empty;
            for (int lineIndex = 0; lineIndex < ChatLog.Count; lineIndex++)
            {
                newChat = newChat + ChatLog[lineIndex] + "\n";
            }
            chatHistory.text = newChat;
        }
        #endregion

        #region Serverside
        [UsedImplicitly]
        public override void OnStartServer()
        {
            NetworkMessenger.AddListener<ChatInputMessage>(chatMsg => ChatLog.Add(chatMsg.ChatLine));
        }
        #endregion
    }
}