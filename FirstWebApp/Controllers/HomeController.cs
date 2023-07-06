using FirstWebApp.Extentions;
using FirstWebApp.Models;
using FirstWebApp.ServerDatabase;
using FirstWebApp.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FirstWebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public IActionResult Index()
		{
			//var TestGame = new GameInfo(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

			BoardModel.restart();
			return View(new LoginDataModel());
		}

		[HttpPost]
		public IActionResult Index(LoginDataModel dataModel)
		{
			if (ModelState.IsValid)
			{
				return this.GoToAddUserPage(dataModel);
			}

			return View(dataModel);
		}

		[HttpGet]
		public IActionResult WaitingPlayerConnect()
		{
			if (!this.CheckSessionExist(Session.SessionVariablesName.TableGuid))
			{
				return RedirectToAction(nameof(SessionExpiration));
			}

			var currentPlayerGuid = this.GetCurrentPlayerGuidFromSession() ?? Guid.Empty;
			var tableGuid = this.GetTableGuidFromSession() ?? Guid.Empty;
			var gameInfo = this.GetCurrentGameInfo(tableGuid);

			if (gameInfo.isSecondPlayerInGame)
			{
				return RedirectToAction("TicTacToe", "Games");
			}


			return View();
		}

		[HttpPost]
		public IActionResult GoToGame(Guid tableGuid, int field)
		{
			this.SetEncodedFieldFromSession(field);
			this.SetTableGuidFromSession(tableGuid);

			if (!this.CheckSessionExist(Session.SessionVariablesName.CurrentPlayerGuid))
			{
				return RedirectToAction(nameof(SessionExpiration));
			}

			var currentPlayerGuid = this.GetCurrentPlayerGuidFromSession() ?? Guid.Empty;
			var gameGuid = Database.Tables[tableGuid];

			if (gameGuid.HasValue)
			{
				var gameInfo = Database.Games[gameGuid.Value];

				if (gameInfo.isSecondPlayerInGame)
				{
					return this.EnroleObserver(currentPlayerGuid, tableGuid, field);
				}
				else
				{
					return this.EnroleSecondPlayer(currentPlayerGuid, gameInfo);
				}
			}
			else
			{
				return this.EnroleFirstPlayer(currentPlayerGuid, tableGuid, field);
			}
		}

		[HttpGet]
		[Route("Home/GamePage")]
		public IActionResult GetGamePage()
		{
			if (
				!this.CheckSessionExist(
				Session.SessionVariablesName.CurrentPlayerGuid |
				Session.SessionVariablesName.EncodedField |
				Session.SessionVariablesName.TableGuid)
			)
			{
				return RedirectToAction(nameof(SessionExpiration));
			}

			var playerName = this.GetCurrentPlayerNameFromSession();
			var field = this.GetEncodedFieldFromSession();
			//var tableGuid = this.GetTableGuidFromSession();

			BoardModel.boardInfo.boardRandom = TicTacToeUtilities.DecodeField((int)field).Cast<char>().ToArray();
			BoardModel.boardInfo.makeMoveName = playerName;

			return View(nameof(GamePage), BoardModel.boardInfo);
		}

		[HttpPost]
		public IActionResult GamePage([FromForm(Name = "index")] string index = "-1")
		{
			int indexInt = Convert.ToInt32(index);

			if (indexInt >= 0 && indexInt <= 8)
			{
				putPoint(indexInt);
				if (isWinnerChecker(BoardModel.boardInfo.boardRandom) == true)
				{
					return View("WinerPage", BoardModel.boardInfo);
				}
			}

			return View(BoardModel.boardInfo);
		}

		public IActionResult refreshGamePage()
		{
			BoardModel.restart();
			return View("GamePage", BoardModel.boardInfo);
		}

		public void putPoint(int index)
		{
			if (BoardModel.isX == true)
			{
				BoardModel.boardInfo.boardRandom[index] = 'X';
				BoardModel.isX = false;
			}
			else
			{
				BoardModel.boardInfo.boardRandom[index] = 'O';
				BoardModel.isX = true;
			}
		}

		[HttpGet]
		public IActionResult AddUser(string playerName)
		{
			if (!Database.Users.ContainsValue(playerName))
			{
				Database.Users.Add(Guid.NewGuid(), playerName);
			}

			this.SetCurrentPlayerNameFromSession(playerName);

			return RedirectToAction(nameof(Lobby));
		}

		[HttpGet]
		public IActionResult SessionExpiration()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Lobby()
		{
			var playerName = this.GetCurrentPlayerNameFromSession();

			if (playerName == null)
			{
				return RedirectToAction(nameof(SessionExpiration));
			}

			var lobbyModel = new LobbyPageModel(playerName ?? string.Empty);

			return View(lobbyModel);
		}

		public bool isWinnerChecker(char[] ticTactToeBoardModel)
		{
			if (
				((ticTactToeBoardModel[0] == ticTactToeBoardModel[4]) & (ticTactToeBoardModel[4] == ticTactToeBoardModel[8]) &
				(ticTactToeBoardModel[0] != ' ' & ticTactToeBoardModel[4] != ' ' & ticTactToeBoardModel[8] != ' ')) ||

				((ticTactToeBoardModel[2] == ticTactToeBoardModel[4]) & (ticTactToeBoardModel[4] == ticTactToeBoardModel[6]) &
				(ticTactToeBoardModel[2] != ' ' & ticTactToeBoardModel[4] != ' ' & ticTactToeBoardModel[6] != ' ')) ||

				((ticTactToeBoardModel[2] == ticTactToeBoardModel[5]) & (ticTactToeBoardModel[5] == ticTactToeBoardModel[8]) &
				(ticTactToeBoardModel[2] != ' ' & ticTactToeBoardModel[5] != ' ' & ticTactToeBoardModel[8] != ' ')) ||

				((ticTactToeBoardModel[1] == ticTactToeBoardModel[4]) & (ticTactToeBoardModel[4] == ticTactToeBoardModel[7]) &
				(ticTactToeBoardModel[1] != ' ' & ticTactToeBoardModel[4] != ' ' & ticTactToeBoardModel[7] != ' ')) ||

				((ticTactToeBoardModel[0] == ticTactToeBoardModel[3]) & (ticTactToeBoardModel[3] == ticTactToeBoardModel[6]) &
				(ticTactToeBoardModel[0] != ' ' & ticTactToeBoardModel[3] != ' ' & ticTactToeBoardModel[6] != ' ')) ||

				((ticTactToeBoardModel[6] == ticTactToeBoardModel[7]) & (ticTactToeBoardModel[7] == ticTactToeBoardModel[8]) &
				(ticTactToeBoardModel[6] != ' ' & ticTactToeBoardModel[7] != ' ' & ticTactToeBoardModel[8] != ' ')) ||

				((ticTactToeBoardModel[3] == ticTactToeBoardModel[4]) & (ticTactToeBoardModel[4] == ticTactToeBoardModel[5]) &
				(ticTactToeBoardModel[3] != ' ' & ticTactToeBoardModel[4] != ' ' & ticTactToeBoardModel[5] != ' ')) ||

				((ticTactToeBoardModel[0] == ticTactToeBoardModel[1]) & (ticTactToeBoardModel[1] == ticTactToeBoardModel[2]) &
				(ticTactToeBoardModel[0] != ' ' & ticTactToeBoardModel[1] != ' ' & ticTactToeBoardModel[2] != ' '))
				)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}