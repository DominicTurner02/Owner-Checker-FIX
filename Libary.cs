using System.Net;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using Steamworks;
using Rocket.API;

namespace SDPlugins
{
    public class Library
    {
        public static void TellInfo(IRocketPlayer caller, CSteamID ownerid, CSteamID group)
        {
            string charname = null;
            
            UnturnedPlayer owner = UnturnedPlayer.FromCSteamID(ownerid);
            
            bool isOnline = owner.Player != null ? owner.Player.channel != null ? true : false : false;

            if (!isOnline)
                charname = GetCharName(ownerid);

            if (Init.Instance.Configuration.Instance.SayPlayerID)
                UnturnedChat.Say(caller, "Owner ID: " + ownerid.ToString());

            if (Init.Instance.Configuration.Instance.SayPlayerCharacterName)
            {
                if (isOnline)
                    UnturnedChat.Say(caller, "Character Name: " + owner.CharacterName);
                else if (charname != null)
                    UnturnedChat.Say(caller, "Character Name: " + charname);
                else
                    UnturnedChat.Say(caller, "Could not get character name.");
            }
            if (Init.Instance.Configuration.Instance.SayPlayerSteamName)
                UnturnedChat.Say(caller, "Steam Name: " + SteamRequest(ownerid.ToString()));
            if (group != CSteamID.Nil)
            {
                string GroupName = SteamGroupRequest(group.ToString());
                if (Init.Instance.Configuration.Instance.SayGroupID)
                    UnturnedChat.Say(caller, "Group ID: " + group.ToString());
                if (Init.Instance.Configuration.Instance.SayGroupName)
                    UnturnedChat.Say(caller, "Group Name: " + GroupName);
            }
        }
        public static string GetCharName(CSteamID id)
        {
            string dname = null;
            if (Init.Instance.Configuration.Instance.usePlayerInfoLib)
            {
                if (Init.IsDependencyLoaded("PlayerInfoLib"))
                {
                    PlayerInfoLibrary.PlayerData data = PlayerInfoLibrary.PlayerInfoLib.Database.QueryById(id, true);
                    dname = data.CharacterName;
                }
            }
            return dname;
        }
        public static string WebClientRequest(string url)
        {
            WebClient client = new WebClient();

            string text = client.DownloadString(url);
            return text;
        }
        public static string SteamGroupRequest(string input)
        {
            string html = WebClientRequest("http://steamcommunity.com/gid/" + input + "/memberslistxml?xml=1");
            string data = getBetween(html, "<groupName>", "</groupName>").Replace(" ", "");
            data = data.Replace("<![CDATA[", "").Replace("]]>", "");
            return data;
        }
        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else 
            {
                return "";
            }
        }
        public static string SteamRequest(string input)
        {
            string html = WebClientRequest("http://steamcommunity.com/profiles/" + input + "?xml=1");
            string data = getBetween(html, "<steamID>", "</steamID>").Replace(" ", "");
            data = data.Replace("<![CDATA[", "").Replace("]]>", "");
            return data;
        }
    }
}
