using System;

namespace StormDotNet.Echo
{
	// Token: 0x020000BC RID: 188
	public class VoiceChat
	{
		// Token: 0x020000BD RID: 189
		// (Invoke) Token: 0x060003BA RID: 954
		public delegate void ChatterReadyHandler(uint chatterRefId, ulong chatterId, string chatterName, bool isLocal, bool audioDeviceActive, bool micMuted, bool speakerMuted);

		// Token: 0x020000BE RID: 190
		// (Invoke) Token: 0x060003BE RID: 958
		public delegate void ChatterGoneHandler(uint chatterRefId, ulong chatterId);

		// Token: 0x020000BF RID: 191
		// (Invoke) Token: 0x060003C2 RID: 962
		public delegate void ChatterTalkingHandler(uint chatterRefId, bool talking);

		// Token: 0x020000C0 RID: 192
		// (Invoke) Token: 0x060003C6 RID: 966
		public delegate void AudioDeviceActivedHandler(uint chatterRefId, bool audioDeviceActive);

		// Token: 0x020000C1 RID: 193
		// (Invoke) Token: 0x060003CA RID: 970
		public delegate void RelationMutedHandler(uint chatterRefId, uint relationChatterRefId, bool micMuted, bool speakerMuted);
	}
}
