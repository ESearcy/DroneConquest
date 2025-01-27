﻿using System;
using Sandbox.ModAPI;

namespace DroneConquest
{
    public class ChatMessageHandler
    {
        private static ChatMessageHandler _instance = null;
        private static string gMessage = null;
        private GameCommands _status = GameCommands.On;

        public void HandleMessageContents()
        {
            if (gMessage == null)
                return;


            var message = gMessage.ToLower();
            gMessage = null;

            int countToSpawn = 0;
            int sizeFlags = 0;

            if (message.Contains("help"))
            {
                Util.GetInstance().Log("[ChatMessageHandler.HandleMessageContents] Displaying Help");
                DisplayGameCommands();
                return;
            }

            if (message.Contains("on"))
            {
                _status = GameCommands.On;
                Util.GetInstance().Log("[ChatMessageHandler.HandleMessageContents] Setting game Mode On");
                return;
            }

            if (message.Contains("off"))
            {
                _status = GameCommands.Off;
                Util.GetInstance().Log("[ChatMessageHandler.HandleMessageContents] Setting game Mode Off");
                return;
            }
            if (!Util.GetInstance().DebuggingOn)
                return;

            if (message.Contains("clear"))
            {
                _status = GameCommands.Clearing;
                Util.GetInstance().Log("[ChatMessageHandler.HandleMessageContents] Setting game Mode Clearing");
                return;
            }
        }

        public static void HandleMessage(String message, ref bool displayMessage)
        {
            if (!MyAPIGateway.Multiplayer.IsServer || !message.Trim().ToLower().StartsWith("dc"))
            {
                displayMessage = true;
                return;
            }
            gMessage = message.Trim().Substring(2).Trim();
            displayMessage = false;
        }

        //register the message handler and init the spawner
        

        private void DisplayGameCommands()
        {
            Util.GetInstance().Help();
        }

        public GameCommands GetStatus()
        {
            return _status;
        }

        public void SetStatus(GameCommands val)
        {
            _status = val;
        }
    }
}
