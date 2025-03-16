using System;

namespace StormDotNet.Echo
{
	// Token: 0x020000B5 RID: 181
	public static class SessionTypeExtensions
	{
		// Token: 0x0600038C RID: 908 RVA: 0x0000B494 File Offset: 0x00009694
		public static StringId ToTypeId(this SessionType sessionType)
		{
			switch (sessionType)
			{
			case SessionType.Party:
				return new StringId("Storm.DotNet.PartySession");
			case SessionType.Team:
				return new StringId("Storm.DotNet.TeamSession");
			case SessionType.Game:
				return new StringId("Storm.DotNet.GameSession");
			case SessionType.Companion:
				return new StringId("Storm.DotNet.CompanionSession");
			default:
				return new StringId(uint.MaxValue);
			}
		}
	}
}
