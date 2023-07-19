using FirstWebApp.Controllers;
using FirstWebApp.Models;
using FirstWebApp.ServerDatabase;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApp.Extentions
{
	public static class HomeControllerExtention
	{
		public static IActionResult GoToAddUserPage(this HomeController homeController, LoginDataModel dataModel)
		{
			QueryBuilder query = new QueryBuilder();
			query.Add(nameof(dataModel.playerName), dataModel.playerName ?? string.Empty);

			var currentUri = homeController.HttpContext.Request.GetEncodedUrl();

			UriBuilder uri = new UriBuilder(currentUri);
			uri.Path = homeController.Url.Action(nameof(homeController.AddUser));
			uri.Query = query.ToString();

			return homeController.Redirect(uri.Uri.ToString());
		}

		public static IActionResult EnroleFirstPlayer(this HomeController homeController, Guid currentPlayerGuid, Guid tableGuid, int encodedField, Database database)
		{
			var newGameGuid = Guid.NewGuid();
			database.Games[newGameGuid] = new GameInfo(currentPlayerGuid, encodedField, database);
			database.Tables[tableGuid] = newGameGuid;

			return homeController.RedirectToAction(nameof(homeController.WaitingPlayerConnect));
		}

		public static IActionResult EnroleSecondPlayer(this HomeController homeController, Guid currentPlayerGuid, GameInfo gameInfo)
		{
			gameInfo.SetSecodPlayer(currentPlayerGuid);
			return homeController.RedirectToAction("TicTacToe", nameof(GamesController).Replace("Controller", ""));
		}

		public static IActionResult EnroleObserver(this HomeController homeController, Guid currentPlayerGuid, Guid tableGuid, int field)
		{
			throw new NotImplementedException();
		}
	}
}
