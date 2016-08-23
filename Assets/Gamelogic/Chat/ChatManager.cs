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
        [SerializeField]
        public SyncListString ChatLog = new SyncListString();
        #endregion

        #region Clientside
        [SerializeField]
        private Text chatHistory;
       
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
        public static ChatManager Instance;

        [UsedImplicitly]
        public override void OnStartServer()
        {
            Instance = this;
        }

        public void LogChat(string line)
        {
            ChatLog.Add(line);
        }
        #endregion
    }
}