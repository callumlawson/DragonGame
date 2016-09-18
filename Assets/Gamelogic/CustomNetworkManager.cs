using Assets.Gamelogic.Messaging;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Gamelogic
{
    [UsedImplicitly]
    public class CustomNetworkManager : NetworkManager
    {
        public static NetworkConnection ClientsNetworkConnection;

        //A client has connected to the server.
        public override void OnServerConnect(NetworkConnection conn)
        {
            //Possible issue where this is called twice :( https://issuetracker.unity3d.com/issues/onserverconnect-function-is-called-twice
            Debug.Log(string.Format("A client has connected to the server with id: {0}, host id: {1}", conn.connectionId, conn.hostId));
            NetworkMessenger.AddListener<TestMessage>(Handler);

        }

        //A server has connected tot he client
        public override void OnClientConnect(NetworkConnection conn)
        {
            ClientsNetworkConnection = conn;
            Debug.Log("A server has connected to the client");
            NetworkMessenger.Broadcast(new TestMessage {TestInt = 1337});
        }

        private void Handler(TestMessage message)
        {
            Debug.Log("Victory!: " + message.TestInt);
        }
    }
}
