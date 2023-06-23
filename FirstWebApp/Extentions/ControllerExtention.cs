using FirstWebApp.ServerDatabase;
using FirstWebApp.Session;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApp.Extentions
{
    public static class ControllerExtention
    {
        public static string? GetCurrentPlayerNameFromSession(this Controller controller)
        {
            var playerGuidInString = controller.HttpContext.Session.GetString(SessionVariables.CurrentPlayerGuid);

            return playerGuidInString == null ? null : Database.Users[new Guid(playerGuidInString)];
        }

        public static void SetCurrentPlayerNameFromSession(this Controller controller, string playerName)
        {
            var playerGuid = Database.Users.First((user) => user.Value == playerName).Key;
            
            controller.HttpContext.Session.SetString(SessionVariables.CurrentPlayerGuid, playerGuid.ToString());
        }
    }
}

/*
    Teacher code (C.Andrey)
 */