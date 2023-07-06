using FirstWebApp.Extentions;
using FirstWebApp.Models;
using FirstWebApp.ServerDatabase;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApp.Controllers
{
    public class GamesController : Controller
    {
        public IActionResult TicTacToe()
        {
            if (
                !this.CheckSessionExist(
                Session.SessionVariablesName.CurrentPlayerGuid |
                Session.SessionVariablesName.EncodedField |
                Session.SessionVariablesName.TableGuid)
            )
            {
                return RedirectToAction("SessionExpiration", "Home");
            }

            var currentPlayerGuid = this.GetCurrentPlayerGuidFromSession() ?? Guid.Empty;
            var tableGuid = this.GetTableGuidFromSession() ?? Guid.Empty;
            var gameInfo = this.GetCurrentGameInfo(tableGuid);
            var model = new TicTacToeModel(gameInfo, currentPlayerGuid);

            return View(model);
        }

        [HttpPost]
        public IActionResult Turn(int fieldIndex)
        {
            if (
                !this.CheckSessionExist(
                Session.SessionVariablesName.CurrentPlayerGuid |
                Session.SessionVariablesName.EncodedField |
                Session.SessionVariablesName.TableGuid)
            )
            {
                return RedirectToAction("SessionExpiration", "Home");
            }

            var currentPlayerGuid = this.GetCurrentPlayerGuidFromSession() ?? Guid.Empty;
            var tableGuid = this.GetTableGuidFromSession() ?? Guid.Empty;
            var gameInfo = this.GetCurrentGameInfo(tableGuid);
            var model = new TicTacToeModel(gameInfo, currentPlayerGuid);

            var encodedField = model.EncodedField;
            var field = Utilities.TicTacToeUtilities.DecodeField(encodedField);

            field[fieldIndex] = model.isPlayerXTurn ? 'X' : 'O';
            model.SetField(field);
            model.SetNextPlayerTurn();

            var gameGuid = Database.Tables[tableGuid] ?? Guid.Empty;
            Database.Games[gameGuid] = (GameInfo)model;

            return RedirectToAction(nameof(TicTacToe));
        }
    }
}
