using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Gamelogic.Actors
{
    class ActorManager : NetworkBehaviour
    {
        private static int _actorIdCount = -1;
        private static ActorManager _instance;

        public static ActorManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ActorManager>();
                }
                return _instance;
            }
        }

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        [UsedImplicitly]
        public override void OnStartClient()
        {
            Debug.Log("Registering prefabs!");
            ClientScene.RegisterPrefab(Resources.Load("Actors/Company") as GameObject);
            ClientScene.RegisterPrefab(Resources.Load("Actors/Dragon") as GameObject);
        }

        public static int SpawnActor(string prefabName)
        {
            var prefab = Instantiate(Resources.Load("Actors/" + prefabName) as GameObject);
            NetworkServer.Spawn(prefab);
            _actorIdCount += 1;
            return _actorIdCount;
        }
    }
}
