using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System.Collections.Generic;
using UnityEngine;

namespace SDPlugins
{
    public class CheckOwner : IRocketCommand
    {
        RaycastHit hit;
        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Player; }
        }

        public string Name
        {
            get { return "checkowner"; }
        }

        public string Help
        {
            get { return "Check the owner of a certain object"; }
        }

        public string Syntax
        {
            get { return "/checkowner"; }
        }

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }
        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            if (Physics.Raycast(player.Player.look.aim.position, player.Player.look.aim.forward, out hit, 10, RayMasks.BARRICADE_INTERACT))
            {
                byte x;
                byte y;

                ushort plant;
                ushort index;

                BarricadeRegion r;
                StructureRegion s;

                Transform transform = hit.transform;
                InteractableVehicle vehicle = transform.gameObject.GetComponent<InteractableVehicle>();

                if (transform.GetComponent<InteractableDoorHinge>() != null)
                {
                    transform = transform.parent.parent;
                }

                if (BarricadeManager.tryGetInfo(transform, out x, out y, out plant, out index, out r))
                {

                    var bdata = r.barricades[index];
                    
                    Library.TellInfo(caller, (CSteamID)bdata.owner, (CSteamID)bdata.group);
                }

                else if (StructureManager.tryGetInfo(transform, out x, out y, out index, out s))
                {
                    var sdata = s.structures[index];
                    
                    Library.TellInfo(caller, (CSteamID)sdata.owner, (CSteamID)sdata.group);
                }

                else if (vehicle != null)
                {
                    if (vehicle.lockedOwner != CSteamID.Nil)
                    { 
                        Library.TellInfo(caller, vehicle.lockedOwner, vehicle.lockedGroup);
                        return;
                    }   
                    UnturnedChat.Say(caller, "Vehicle does not have an owner.");
                }
            }
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>
                {
                  "SDPlugins.checkowner"
                };
            }
        }
    }
}
