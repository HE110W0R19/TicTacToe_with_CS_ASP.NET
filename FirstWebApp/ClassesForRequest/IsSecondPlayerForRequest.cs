namespace FirstWebApp.ClassesForRequest
{
	public struct IsSecondPlayerForRequest
	{
		public bool IsSecondPlayer { get; set; }
		public bool IsSessionExpiration { get; set; }

		public IsSecondPlayerForRequest(bool isSecondPlayer, bool isSessionExpiration) 
		{
			IsSecondPlayer = isSecondPlayer;
			IsSessionExpiration = isSessionExpiration;
		}
	}
}
