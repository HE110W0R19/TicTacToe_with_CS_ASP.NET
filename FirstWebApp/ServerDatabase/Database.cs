using FirstWebApp.Models;

namespace FirstWebApp.ServerDatabase
{
	public class Database
	{
		public Dictionary<Guid, string> Users = new Dictionary<Guid, string>();
		public Dictionary<Guid, GameInfo> Games = new Dictionary<Guid, GameInfo>();
		public Dictionary<Guid, Guid?> Tables = new Dictionary<Guid, Guid?>();

		public Database()
		{
			for (var i = 0; i < 3; ++i)
			{
				Tables[Guid.NewGuid()] = null;
			}

			Users[Guid.Empty] = "Никаво";
		}
	}
}
