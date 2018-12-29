using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Collections.Generic;
using UnityEngine;


// This was added by the original Plugin Developer and I guess it was never finished, from what I can see it should teleport you to a random
// Barricade upon the command, however it seems it just teleports you to the same one over and over again. Will look into fixing this soon
// If the Original Developer doesn't.


namespace SDPlugins
{
    public class Find : IRocketCommand
    {
        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Player; }
        }

        public string Name
        {
            get { return "find"; }
        }

        public string Help
        {
            get { return "Teleports you to an Object."; }
        }

        public string Syntax
        {
            get { return "/find"; }
        }

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }
        public List<string> Permissions
        {
            get
            {
                return new List<string>
                {
                  "SDPlugins.find"
                };
            }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            foreach (BarricadeRegion br in BarricadeManager.regions)
            {
                foreach (BarricadeData bd in br.barricades)
                {

                    if (Physics.Raycast(bd.point, Vector3.up, RayMasks.GROUND | RayMasks.GROUND2))
                    {
                        ((UnturnedPlayer)caller).Teleport(bd.point, 0);
                        return;
                    }
                }
            }
        }
    }
}
