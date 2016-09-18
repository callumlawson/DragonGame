using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Networking;

public class CompanyManager : NetworkBehaviour
{
    public int NumberOfCompanies = 2;

    #region Persistant State
    [System.Serializable]
    public struct CompanyState
    {
        public int CurrentBalance;
    }
    private List<CompanyState> Companies;
    #endregion

    #region Clientside
    #endregion

    //Move to server logic - Passive income behaviour.
    #region Serverside
    public override void OnStartServer()
    {
        Companies = new List<CompanyState>();
        for (int playerNumber = 0; playerNumber < NumberOfCompanies; playerNumber++)
        {
            Companies.Add(new CompanyState());
        }
    }
    #endregion
}
