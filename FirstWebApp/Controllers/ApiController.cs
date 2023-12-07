using FirstWebApp.ClassesForRequest;
using FirstWebApp.Extentions;
using FirstWebApp.Models;
using FirstWebApp.ServerDatabase;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FirstWebApp.Controllers
{
    public class ApiController : Controller
    {
        Database database;
        public ApiController(Database database)
        {
            this.database = database;
        }

        public Guid[] GetTables()
        {
            return database.Tables.Keys.ToArray();
        }

        public Guid[] GetUsers()
        {
            return database.Users.Keys.ToArray();
        }

        public class GetUserNameRequest
        {
            [JsonProperty]
            public Guid userGuid { get; set; }
        }

        [HttpPost]
        public string GetUserName([FromBody] GetUserNameRequest requestValue)
        {
            return database.Users[requestValue.userGuid];
        }

        [HttpPost]
        public IsSecondPlayerForRequest IsSecondPlayerInGame()
        {
            if (
                !this.CheckSessionExist(
                Session.SessionVariablesName.CurrentPlayerGuid |
                Session.SessionVariablesName.EncodedField |
                Session.SessionVariablesName.TableGuid)
            )
            {
                return new(isSecondPlayer: false, isSessionExpiration: true);
            }

            var tableGuid = this.GetTableGuidFromSession() ?? Guid.Empty;
            var gameTableInfo = database.Tables[tableGuid] ?? Guid.Empty;
            var gameInfo = database.Games[gameTableInfo] ?? new GameInfo();

            return new(isSecondPlayer: gameInfo.isSecondPlayerInGame, isSessionExpiration: false);
        }

        [HttpPost]
        public IsCurrentPlayerTurnForRequest IsCurrentPlayerTurn()
        {
            if (
                !this.CheckSessionExist(
                Session.SessionVariablesName.CurrentPlayerGuid |
                Session.SessionVariablesName.EncodedField |
                Session.SessionVariablesName.TableGuid)
            )
            {
                return new(isCurrentPlayerTurn: false, isSessionExpiration: true);
            }

            var currentPlayerGuid = this.GetCurrentPlayerGuidFromSession() ?? Guid.Empty;
            var tableGuid = this.GetTableGuidFromSession() ?? Guid.Empty;
            var gameTableInfo = database.Tables[tableGuid] ?? Guid.Empty;
            var gameInfo = database.Games[gameTableInfo] ?? new GameInfo();

            TicTacToeModel gameInfoModel = new TicTacToeModel(gameInfo, currentPlayerGuid, database);

            return new(isCurrentPlayerTurn: gameInfoModel.IsCurrentPlayerTurn, isSessionExpiration: false);
        }

        [HttpPost]
        public IActionResult? GetTableHTML([FromRoute(Name="id")]Guid tableGuid)
        {
			if (!this.CheckSessionExist(Session.SessionVariablesName.CurrentPlayerGuid))
			{
                return NotFound();
			}

			return this.PartialView("../Partial/LobbyTable", new LobbyTableModel(tableGuid, database));
        }
    }
}
