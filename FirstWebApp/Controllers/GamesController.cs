using FirstWebApp.Extentions;
using FirstWebApp.Models;
using FirstWebApp.ServerDatabase;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApp.Controllers
{
    public class GamesController : Controller
    {
        Database dataBase;
        public GamesController(Database dataBase) 
        {
            this.dataBase = dataBase;
        }
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
            var gameInfo = this.GetCurrentGameInfo(tableGuid, dataBase);
            var model = new TicTacToeModel(gameInfo, currentPlayerGuid, dataBase);

            return View(model);
        }

        public IActionResult ObserverPage()
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
            var gameInfo = this.GetCurrentGameInfo(tableGuid, dataBase);
            var model = new TicTacToeModel(gameInfo, currentPlayerGuid, dataBase);
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
            var gameInfo = this.GetCurrentGameInfo(tableGuid, dataBase);
            var model = new TicTacToeModel(gameInfo, currentPlayerGuid, dataBase);

            var encodedField = model.EncodedField;
            var field = Utilities.TicTacToeUtilities.DecodeField(encodedField);

            field[fieldIndex] = model.isPlayerXTurn ? 'X' : 'O';
            model.SetField(field);

            var gameGuid = dataBase.Tables[tableGuid] ?? Guid.Empty;
            dataBase.Games[gameGuid] = (GameInfo)model;

            if (model.isVictory)
            {
                return RedirectToAction("EndGame", "Home");
            }
            else
            {
                dataBase.Games[gameGuid].SetNextPlayerTurn();
                return RedirectToAction(nameof(TicTacToe));
            }
        }
    }
}
