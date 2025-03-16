using System;

namespace StormDotNet.Echo
{
	// Token: 0x020000AF RID: 175
	public static class TransportMode
	{
		// Token: 0x04000262 RID: 610
		public static readonly StringId UnreliableEvent = new StringId("Unreliable");

		// Token: 0x04000263 RID: 611
		public static readonly StringId ReliableEvent = new StringId("Reliable");

		// Token: 0x04000264 RID: 612
		public static readonly StringId ReliableOrderedEvent = new StringId("ReliableOrdered");

		// Token: 0x04000265 RID: 613
		public static readonly StringId DataIndependent = new StringId("NetDataIndependent");

		// Token: 0x04000266 RID: 614
		public static readonly StringId DataDependent = new StringId("NetDataDependent");
	}
}
