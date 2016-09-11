using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Gamelogic.Messaging
{
    public delegate void MessageCallback<T>(T arg1);

    /// <summary>
    /// This is a slow and allocation riddled crime. However, it does protoype the API I want to test. 
    /// </summary>
    public class Messenger : NetworkBehaviour
    {
        private static Dictionary<Type, Delegate> listenerTable = new Dictionary<Type, Delegate>();
      
        private readonly MethodInfo deserializeMethod = typeof(Messenger).GetMethod("DeserializeJsonMessage", BindingFlags.NonPublic | BindingFlags.Instance);

        #region Client and Server
        public static void Broadcast(CustomMsg message)
        {
            //TODO: Add server to server version. if(isServer) ...stuff
            CustomNetworkManager.ClientsNetworkConnection.Send(MessengerMessageId, new MessengerMessage(message));
        }
        #endregion

        public static void AddListener<T>(MessageCallback<T> callback)
        {
            if (!listenerTable.ContainsKey(typeof(T)))
            {
                listenerTable.Add(typeof(T), callback);
            }
            else
            {
                listenerTable[typeof(T)] = Delegate.Combine(listenerTable[typeof(T)], callback);
            }
        }

        #region Serverside
        public override void OnStartServer()
        {
            NetworkServer.RegisterHandler(MessengerMessageId, OnMessagingMsgReceived);
            Debug.Log("Messager server started");
        }

        private void OnMessagingMsgReceived(NetworkMessage netmsg)
        {
            var msg = netmsg.ReadMessage<MessengerMessage>();
            Debug.Log(string.Format("Got message. Id: {0}, {1}", msg.MessageId, msg.PayloadMessage));
            var messageType = MessageTypes.MessageIndex[msg.MessageId];
            var genericMethod = deserializeMethod.MakeGenericMethod(messageType);
            var innerMessage = genericMethod.Invoke(this, new object[] {msg.PayloadMessage});

            foreach (var keyValuePair in listenerTable)
            {
                if (keyValuePair.Key == messageType)
                {
                    keyValuePair.Value.DynamicInvoke(innerMessage);
                }
            }
        }

        [UsedImplicitly]
        private T DeserializeJsonMessage<T>(string payload)
        {
            return JsonUtility.FromJson<T>(payload);
        }
        #endregion

        #region MessageDfn
        private const short MessengerMessageId = 1000;
        private class MessengerMessage : MessageBase
        {
            public MessengerMessage() { }

            public MessengerMessage(CustomMsg payload)
            {
                PayloadMessage = JsonUtility.ToJson(payload);
                MessageId = MessageTypes.GetTypeId(payload);
            }

            public readonly int MessageId;
            public readonly string PayloadMessage;
        }
        #endregion
    }
}
