using System;
using System.Runtime.InteropServices;

namespace StormDotNet
{
	// Token: 0x020000A1 RID: 161
	public static class Log
	{
		// Token: 0x06000337 RID: 823 RVA: 0x00009449 File Offset: 0x00007649
		internal static void Initialize()
		{
			Log.ModuleId.Initialize();
			Log.Token.Initialize();
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00009455 File Offset: 0x00007655
		public static EResult SetLogLevel(StringId logId, Log.Level logLevel)
		{
			Log.Native.SetLogLevel(logId.UniqueID, (int)logLevel);
			return EResult.GetLastResult();
		}

		// Token: 0x020000A2 RID: 162
		public enum Level
		{
			// Token: 0x040000C2 RID: 194
			None,
			// Token: 0x040000C3 RID: 195
			Error,
			// Token: 0x040000C4 RID: 196
			Warning,
			// Token: 0x040000C5 RID: 197
			Info,
			// Token: 0x040000C6 RID: 198
			Debug
		}

		// Token: 0x020000A3 RID: 163
		public static class ModuleId
		{
			// Token: 0x06000339 RID: 825 RVA: 0x00002240 File Offset: 0x00000440
			internal static void Initialize()
			{
			}

			// Token: 0x040000C7 RID: 199
			public static readonly StringId Source = new StringId("storm");

			// Token: 0x040000C8 RID: 200
			public static readonly StringId EResult = new StringId("eresult");

			// Token: 0x040000C9 RID: 201
			public static readonly StringId NetObject = new StringId("netobject");

			// Token: 0x040000CA RID: 202
			public static readonly StringId NetObjectSerialize = new StringId("netobjectserialize");

			// Token: 0x040000CB RID: 203
			public static readonly StringId Packer = new StringId("packer");

			// Token: 0x040000CC RID: 204
			public static readonly StringId Connection = new StringId("connection");

			// Token: 0x040000CD RID: 205
			public static readonly StringId Handshaking = new StringId("handshaking");

			// Token: 0x040000CE RID: 206
			public static readonly StringId NetAgent = new StringId("netagent");

			// Token: 0x040000CF RID: 207
			public static readonly StringId StreamSize = new StringId("streamsize");

			// Token: 0x040000D0 RID: 208
			public static readonly StringId Socket = new StringId("socket");

			// Token: 0x040000D1 RID: 209
			public static readonly StringId NoMgrStats = new StringId("nomgrstats");

			// Token: 0x040000D2 RID: 210
			public static readonly StringId NetTrafficStats = new StringId("nettrafficstats");

			// Token: 0x040000D3 RID: 211
			public static readonly StringId NetObjectStats = new StringId("netobjectstats");

			// Token: 0x040000D4 RID: 212
			public static readonly StringId NetObjectUtility = new StringId("NetObjectUtility");

			// Token: 0x040000D5 RID: 213
			public static readonly StringId Voice = new StringId("voice");

			// Token: 0x040000D6 RID: 214
			public static readonly StringId FileDownload = new StringId("filedownload");

			// Token: 0x040000D7 RID: 215
			public static readonly StringId Bandwidth = new StringId("bandwidth");

			// Token: 0x040000D8 RID: 216
			public static readonly StringId BandwidthRegulator = new StringId("bandwidthregulator");

			// Token: 0x040000D9 RID: 217
			public static readonly StringId Emulator = new StringId("emulator");

			// Token: 0x040000DA RID: 218
			public static readonly StringId EmulatorCorruptPacket = new StringId("emulatorcorruptpacket");

			// Token: 0x040000DB RID: 219
			public static readonly StringId LogManager = new StringId("logmanager");

			// Token: 0x040000DC RID: 220
			public static readonly StringId ObjectSimulationManager = new StringId("objectsimulationmanager");

			// Token: 0x040000DD RID: 221
			public static readonly StringId Timer = new StringId("timer");

			// Token: 0x040000DE RID: 222
			public static readonly StringId StreamPacker = new StringId("streampacker");

			// Token: 0x040000DF RID: 223
			public static readonly StringId PluginManager = new StringId("pluginmanager");

			// Token: 0x040000E0 RID: 224
			public static readonly StringId EventDispatcher = new StringId("eventdispatcher");

			// Token: 0x040000E1 RID: 225
			public static readonly StringId Packet = new StringId("packet");

			// Token: 0x040000E2 RID: 226
			public static readonly StringId LanManager = new StringId("lanmanager");

			// Token: 0x040000E3 RID: 227
			public static readonly StringId Engine = new StringId("engine");

			// Token: 0x040000E4 RID: 228
			public static readonly StringId HeartBeat = new StringId("heartbeat");

			// Token: 0x040000E5 RID: 229
			public static readonly StringId ObjectAdapter = new StringId("objectadapter");

			// Token: 0x040000E6 RID: 230
			public static readonly StringId ObjectAdapterReadWrite = new StringId("objectadapterreadwrite");

			// Token: 0x040000E7 RID: 231
			public static readonly StringId SimulationAdapter = new StringId("simulationadapter");

			// Token: 0x040000E8 RID: 232
			public static readonly StringId PeerChannel = new StringId("peerchannel");

			// Token: 0x040000E9 RID: 233
			public static readonly StringId SimulationCamera = new StringId("simulationcamera");

			// Token: 0x040000EA RID: 234
			public static readonly StringId NetInvoke = new StringId("netinvoke");

			// Token: 0x040000EB RID: 235
			public static readonly StringId Memory = new StringId("memory");

			// Token: 0x040000EC RID: 236
			public static readonly StringId DynamicType = new StringId("dynamictype");

			// Token: 0x040000ED RID: 237
			public static readonly StringId QoS = new StringId("qos");

			// Token: 0x040000EE RID: 238
			public static readonly StringId LatencyObject = new StringId("latencyobject");

			// Token: 0x040000EF RID: 239
			public static readonly StringId StreamMetrics = new StringId("streammetrics");

			// Token: 0x040000F0 RID: 240
			public static readonly StringId Handler = new StringId("handler");

			// Token: 0x040000F1 RID: 241
			public static readonly StringId Message = new StringId("message");

			// Token: 0x040000F2 RID: 242
			public static readonly StringId TrySync = new StringId("trysync");

			// Token: 0x040000F3 RID: 243
			public static readonly StringId AtomicReadWriteLock = new StringId("atomicreadwritelock");

			// Token: 0x040000F4 RID: 244
			public static readonly StringId NetObjectSynchronization = new StringId("netobjectsynchronization");

			// Token: 0x040000F5 RID: 245
			public static readonly StringId Statistics = new StringId("statistics");

			// Token: 0x040000F6 RID: 246
			public static readonly StringId Loopback = new StringId("loopback");

			// Token: 0x040000F7 RID: 247
			public static readonly StringId Replay = new StringId("replay");

			// Token: 0x040000F8 RID: 248
			public static readonly StringId Player = new StringId("player");

			// Token: 0x040000F9 RID: 249
			public static readonly StringId ServerQuery = new StringId("serverquery");

			// Token: 0x040000FA RID: 250
			public static readonly StringId Registry = new StringId("registry");

			// Token: 0x040000FB RID: 251
			public static readonly StringId StreamBundler = new StringId("streambundler");

			// Token: 0x040000FC RID: 252
			public static readonly StringId GROManager = new StringId("gromanager");

			// Token: 0x040000FD RID: 253
			public static readonly StringId Session = new StringId("session");

			// Token: 0x040000FE RID: 254
			public static readonly StringId Group = new StringId("group");

			// Token: 0x040000FF RID: 255
			public static readonly StringId GroupBridge = new StringId("groupbridge");

			// Token: 0x04000100 RID: 256
			public static readonly StringId PunchClient = new StringId("punchclient");

			// Token: 0x04000101 RID: 257
			public static readonly StringId PunchDetect = new StringId("punchdetect");

			// Token: 0x04000102 RID: 258
			public static readonly StringId PunchTraversal = new StringId("punchtraversal");

			// Token: 0x04000103 RID: 259
			public static readonly StringId PunchTraversalConnection = new StringId("punchtraversalconnection");

			// Token: 0x04000104 RID: 260
			public static readonly StringId PunchAdapter = new StringId("punchadapter");

			// Token: 0x04000105 RID: 261
			public static readonly StringId PunchServer = new StringId("punchserver");

			// Token: 0x04000106 RID: 262
			public static readonly StringId PunchUpnp = new StringId("punchupnp");

			// Token: 0x04000107 RID: 263
			public static readonly StringId MiniUPnP = new StringId("miniupnp");

			// Token: 0x04000108 RID: 264
			public static readonly StringId Relay = new StringId("relay");

			// Token: 0x04000109 RID: 265
			public static readonly StringId TransmissionHistory = new StringId("transmissionhistory");

			// Token: 0x0400010A RID: 266
			public static readonly StringId StatsTransmissionHistory = new StringId("statstransmissionhistory");

			// Token: 0x0400010B RID: 267
			public static readonly StringId StatsPackets = new StringId("statspackets");

			// Token: 0x0400010C RID: 268
			public static readonly StringId NetworkManager = new StringId("networkmanager");

			// Token: 0x0400010D RID: 269
			public static readonly StringId TransportManager = new StringId("transportmanager");

			// Token: 0x0400010E RID: 270
			public static readonly StringId RoutingProtocolManager = new StringId("routingprotocolmanager");

			// Token: 0x0400010F RID: 271
			public static readonly StringId LinkManager = new StringId("linkmanager");

			// Token: 0x04000110 RID: 272
			public static readonly StringId Security = new StringId("security");

			// Token: 0x04000111 RID: 273
			public static readonly StringId TransportStats = new StringId("transportstats");

			// Token: 0x04000112 RID: 274
			public static readonly StringId PokeManager = new StringId("pokemanager");

			// Token: 0x04000113 RID: 275
			public static readonly StringId PokePacket = new StringId("pokepacket");

			// Token: 0x04000114 RID: 276
			public static readonly StringId ExternalRoutablePacket = new StringId("externalroutablepacket");

			// Token: 0x04000115 RID: 277
			public static readonly StringId SessionPacket = new StringId("sessionpacket");

			// Token: 0x04000116 RID: 278
			public static readonly StringId PackerStreamPacket = new StringId("packerstreampacket");

			// Token: 0x04000117 RID: 279
			public static readonly StringId NTPPacket = new StringId("NTPPacket");

			// Token: 0x04000118 RID: 280
			public static readonly StringId NatPacket = new StringId("natpacket");

			// Token: 0x04000119 RID: 281
			public static readonly StringId PingPacket = new StringId("pingpacket");

			// Token: 0x0400011A RID: 282
			public static readonly StringId PeerChannelPacket = new StringId("peerchannelpacket");

			// Token: 0x0400011B RID: 283
			public static readonly StringId PlayerPacket = new StringId("playerpacket");

			// Token: 0x0400011C RID: 284
			public static readonly StringId RoutingProtocolPacket = new StringId("routingProtocolpacket");

			// Token: 0x0400011D RID: 285
			public static readonly StringId LinkPacket = new StringId("linkpacket");

			// Token: 0x0400011E RID: 286
			public static readonly StringId ConnectionPacket = new StringId("connectionpacket");

			// Token: 0x0400011F RID: 287
			public static readonly StringId SimpleCtrlRdvOperation = new StringId("simplectrlrdvoperation");

			// Token: 0x04000120 RID: 288
			public static readonly StringId SimpleCtrlConnectionState = new StringId("simplectrlconnectionstate");

			// Token: 0x04000121 RID: 289
			public static readonly StringId SimpleCtrlSandbox = new StringId("simplectrlsandboxoperation");

			// Token: 0x04000122 RID: 290
			public static readonly StringId SimpleCtrlSession = new StringId("simplectrlsession");

			// Token: 0x04000123 RID: 291
			public static readonly StringId SimpleCtrlObject = new StringId("simplectrlobject");

			// Token: 0x04000124 RID: 292
			public static readonly StringId SimpleCtrlSimulation = new StringId("simplectrlsimulation");

			// Token: 0x04000125 RID: 293
			public static readonly StringId SimpleCtrlPeerChannel = new StringId("simplectrlpeerchannel");

			// Token: 0x04000126 RID: 294
			public static readonly StringId SimpleCtrlLDN = new StringId("simplectrlldn");

			// Token: 0x04000127 RID: 295
			public static readonly StringId SimpleCtrlVoice = new StringId("simplectrlvoice");

			// Token: 0x04000128 RID: 296
			public static readonly StringId SimpleCtrlPeer = new StringId("simplectrlpeer");

			// Token: 0x04000129 RID: 297
			public static readonly StringId SimpleCtrlPunch = new StringId("simplectrlpunch");

			// Token: 0x0400012A RID: 298
			public static readonly StringId SimpleCtrlGroup = new StringId("simplectrlgroup");

			// Token: 0x0400012B RID: 299
			public static readonly StringId SimpleCtrlTracking = new StringId("simplectrltracking");

			// Token: 0x0400012C RID: 300
			public static readonly StringId SimpleCtrlRouter = new StringId("simplectrlrouter");

			// Token: 0x0400012D RID: 301
			public static readonly StringId SimpleCtrlUbiServices = new StringId("simplectrlubiservices");

			// Token: 0x0400012E RID: 302
			public static readonly StringId SimpleCtrlAsyncOp = new StringId("simplectrlasyncop");

			// Token: 0x0400012F RID: 303
			public static readonly StringId SimpleCtrlServerQuery = new StringId("simplectrlserverquery");

			// Token: 0x04000130 RID: 304
			public static readonly StringId SimpleCtrlGlobal = new StringId("simplectrlglobal");
		}

		// Token: 0x020000A4 RID: 164
		public static class Token
		{
			// Token: 0x0600033B RID: 827 RVA: 0x00002240 File Offset: 0x00000440
			internal static void Initialize()
			{
			}

			// Token: 0x04000131 RID: 305
			public static readonly StringId EVT_IN = new StringId("[[E_IN ]]");

			// Token: 0x04000132 RID: 306
			public static readonly StringId EVT_OUT = new StringId("[[E_OUT]]");

			// Token: 0x04000133 RID: 307
			public static readonly StringId MSG_IN = new StringId("[[M_IN ]]");

			// Token: 0x04000134 RID: 308
			public static readonly StringId MSG_OUT = new StringId("[[M_OUT]]");

			// Token: 0x04000135 RID: 309
			public static readonly StringId WARNING_LOG = new StringId("[[WARN ]]");

			// Token: 0x04000136 RID: 310
			public static readonly StringId ERROR_LOG = new StringId("[[ERROR]]");
		}

		// Token: 0x020000A5 RID: 165
		public struct LogEntry
		{
			// Token: 0x17000039 RID: 57
			// (get) Token: 0x0600033D RID: 829 RVA: 0x00009B17 File Offset: 0x00007D17
			public StringId ModuleId
			{
				get
				{
					return this.moduleId;
				}
			}

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x0600033E RID: 830 RVA: 0x00009B1F File Offset: 0x00007D1F
			public Log.Level Level
			{
				get
				{
					return this.level;
				}
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x0600033F RID: 831 RVA: 0x00009B27 File Offset: 0x00007D27
			public ulong SystemTime
			{
				get
				{
					return this.systemTime;
				}
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x06000340 RID: 832 RVA: 0x00009B2F File Offset: 0x00007D2F
			public string Text
			{
				get
				{
					return this.text;
				}
			}

			// Token: 0x06000341 RID: 833 RVA: 0x00009B37 File Offset: 0x00007D37
			internal LogEntry(StringId sourceId, StringId moduleId, Log.Level level, ulong threadId, ulong systemTime, uint updateId, string text)
			{
				this.sourceId = sourceId;
				this.moduleId = moduleId;
				this.level = level;
				this.threadId = threadId;
				this.systemTime = systemTime;
				this.updateId = updateId;
				this.text = text;
			}

			// Token: 0x04000137 RID: 311
			private StringId sourceId;

			// Token: 0x04000138 RID: 312
			private StringId moduleId;

			// Token: 0x04000139 RID: 313
			private Log.Level level;

			// Token: 0x0400013A RID: 314
			private ulong threadId;

			// Token: 0x0400013B RID: 315
			private ulong systemTime;

			// Token: 0x0400013C RID: 316
			private uint updateId;

			// Token: 0x0400013D RID: 317
			private string text;
		}

		// Token: 0x020000A6 RID: 166
		private static class Native
		{
			// Token: 0x06000342 RID: 834
			[DllImport("Storm", CharSet = CharSet.Ansi)]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool SetLogLevel(uint logId, int logLevel);
		}
	}
}
