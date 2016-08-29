using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Networking;

namespace Assets.Gamelogic.Messaging
{
    [Serializable]
    public class CustomMsg : MessageBase { }

    public static class MessageTypes
    {
        //TODO: Build this automagically?
        public static readonly Dictionary<int, Type> MessageIndex = new Dictionary<int, Type>
        {
            { 1, typeof(TestMessage)},
            { 2, typeof(AnotherTestMessage)}
        };

        public static int GetTypeId(CustomMsg payload)
        {
            return MessageIndex.Where(keyValue => keyValue.Value == payload.GetType()).Select(keyValue => keyValue.Key).First();
        }
    }

    #region Custom Messages
    [Serializable]
    public class TestMessage : CustomMsg
    {
        public int TestInt;
    }

    [Serializable]
    public class AnotherTestMessage : CustomMsg
    {
        public string TestString;
    }
    #endregion
}
