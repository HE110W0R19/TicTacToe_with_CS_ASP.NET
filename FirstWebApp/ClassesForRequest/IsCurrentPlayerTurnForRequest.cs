namespace FirstWebApp.ClassesForRequest
{
	public struct IsCurrentPlayerTurnForRequest
	{
		public bool IsCurrentPlayerTurn { get; set; }
		public bool IsSessionExpiration { get; set; }

		public IsCurrentPlayerTurnForRequest(bool isCurrentPlayerTurn, bool isSessionExpiration)
		{
			IsCurrentPlayerTurn = isCurrentPlayerTurn;
			IsSessionExpiration = isSessionExpiration;
		}
	}
}
