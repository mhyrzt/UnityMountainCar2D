using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.SideChannels;
public class SettingSideChannel : SideChannel
{  
    [SerializeField] private Ground ground;
    [SerializeField] private CarAgent carAgent;

    public SettingSideChannel() {
        ChannelId = new System.Guid("621f0a70-4f87-11ea-a6bf-784f4387d1f7");
    }    

    protected override void OnMessageReceived(IncomingMessage msg)
    {
        var receivedString = msg.ReadString();
        Debug.Log("From Python : " + receivedString);
    }
}
