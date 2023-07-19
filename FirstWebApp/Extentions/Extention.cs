using FirstWebApp.Models;
using FirstWebApp.ServerDatabase;
using FirstWebApp.Session;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApp.Extentions
{
    public static class Extention
    {
        public static GameInfo GetCurrentGameInfo(this Controller _, Guid tableGuid, Database dataBase)
        {
            var gameGuid = dataBase.Tables[tableGuid];

            if (gameGuid.HasValue)
            {
                return dataBase.Games[gameGuid.Value];
            }
            else
            {
                throw new ArgumentException("Игра еще не началась");
            }
        }

        public static bool CheckSessionExist(this Controller controller, SessionVariablesName value)
        {
            var checkDictianory = new Dictionary<SessionVariablesName, Func<Controller, bool>>
            {
                {SessionVariablesName.CurrentPlayerGuid, (controller) => controller.GetCurrentPlayerGuidFromSession() == null },
                {SessionVariablesName.TableGuid, (controller) => controller.GetTableGuidFromSession() == null },
                {SessionVariablesName.EncodedField, (controller) => controller.GetEncodedFieldFromSession() == null }
            };
            var enums = Enum.GetValues<SessionVariablesName>();

            foreach (var val in enums)
            {
                if ((value & val) == val)
                {
                    if (checkDictianory[val](controller))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
