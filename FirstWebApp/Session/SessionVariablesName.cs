namespace FirstWebApp.Session
{
	[Flags]
	public enum SessionVariablesName
	{
		CurrentPlayerGuid = 1 << 0,
		EncodedField = 1 << 1,
		TableGuid = 1 << 2,
	}
}
