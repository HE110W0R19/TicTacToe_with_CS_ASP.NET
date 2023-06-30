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

		public static Guid? GetCurrentPlayerGuidFromSession(this Controller controller)
		{
			var playerGuidInString = controller.HttpContext.Session.GetString(SessionVariables.CurrentPlayerGuid);

			return playerGuidInString == null ? null : new Guid(playerGuidInString);
		}

		public static int? GetEncodedFieldFromSession(this Controller controller)
		{
			return controller.HttpContext.Session.GetInt32(SessionVariables.EncodedField);
		}

		public static void SetEncodedFieldFromSession(this Controller controller, int encodedField)
		{
            controller.HttpContext.Session.SetInt32(SessionVariables.EncodedField, encodedField);
		}

		public static Guid? GetTableGuidFromSession(this Controller controller)
		{
			var tableGuid =  controller.HttpContext.Session.GetString(SessionVariables.TableGuid);

			return tableGuid == null ? null : new Guid(tableGuid.ToString());
		}

		public static void SetTableGuidFromSession(this Controller controller, Guid gameGuid)
		{
			controller.HttpContext.Session.SetString(SessionVariables.TableGuid, gameGuid.ToString());
		}
	}
}

/*
    Teacher code (C.Andrey)
 */