using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using StormDotNet.Echo;

namespace StormDotNet
{
	// Token: 0x02000094 RID: 148
	public static class VoiceChatController
	{
		// Token: 0x060002D9 RID: 729 RVA: 0x00008554 File Offset: 0x00006754
		internal static void Initialize()
		{
			List<GCHandle> list = VoiceChatController.gcHandles;
			lock (list)
			{
				VoiceChatController.Native.ChatterReadyHandler chatterReadyHandler = new VoiceChatController.Native.ChatterReadyHandler(VoiceChatController.ChatterReadyHandler);
				GCHandle gchandle = GCHandle.Alloc(chatterReadyHandler, GCHandleType.Normal);
				VoiceChatController.gcHandles.Add(gchandle);
				VoiceChatController.Native.ChatterGoneHandler chatterGoneHandler = new VoiceChatController.Native.ChatterGoneHandler(VoiceChatController.ChatterGoneHandler);
				gchandle = GCHandle.Alloc(chatterGoneHandler, GCHandleType.Normal);
				VoiceChatController.gcHandles.Add(gchandle);
				VoiceChatController.Native.ChatterTalkingHandler chatterTalkingHandler = new VoiceChatController.Native.ChatterTalkingHandler(VoiceChatController.ChatterTalkingHandler);
				gchandle = GCHandle.Alloc(chatterTalkingHandler, GCHandleType.Normal);
				VoiceChatController.gcHandles.Add(gchandle);
				VoiceChatController.Native.AudioDeviceActivedHandler audioDeviceActivedHandler = new VoiceChatController.Native.AudioDeviceActivedHandler(VoiceChatController.AudioDeviceActivedHandler);
				gchandle = GCHandle.Alloc(audioDeviceActivedHandler, GCHandleType.Normal);
				VoiceChatController.gcHandles.Add(gchandle);
				VoiceChatController.Native.RelationMutedHandler relationMutedHandler = new VoiceChatController.Native.RelationMutedHandler(VoiceChatController.RelationMutedHandler);
				gchandle = GCHandle.Alloc(relationMutedHandler, GCHandleType.Normal);
				VoiceChatController.gcHandles.Add(gchandle);
				VoiceChatController.Native.Initialize(chatterReadyHandler, chatterGoneHandler, chatterTalkingHandler, audioDeviceActivedHandler, relationMutedHandler);
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00008640 File Offset: 0x00006840
		internal static void Uninitialize()
		{
			List<GCHandle> list = VoiceChatController.gcHandles;
			lock (list)
			{
				foreach (GCHandle gchandle in VoiceChatController.gcHandles)
				{
					gchandle.Free();
				}
				VoiceChatController.gcHandles.Clear();
			}
			VoiceChatController.OnChatterReady = null;
			VoiceChatController.OnChatterGone = null;
			VoiceChatController.OnChatterTalking = null;
			VoiceChatController.OnAudioDeviceActived = null;
			VoiceChatController.OnRelationMuted = null;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000086E4 File Offset: 0x000068E4
		public static void CreateLocalChatter(uint sessionRefId, string chatterName)
		{
			using (StormEngine.SetProfilePoint(null))
			{
				VoiceChatController.Native.CreateLocalChatter(sessionRefId, chatterName);
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000871C File Offset: 0x0000691C
		public static void DeleteLocalChatter()
		{
			using (StormEngine.SetProfilePoint(null))
			{
				VoiceChatController.Native.DeleteLocalChatter();
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00008754 File Offset: 0x00006954
		public static EResult MuteLocalMic(ResultRelayer resultRelayer, bool mute)
		{
			EResult lastResult;
			using (StormEngine.SetProfilePoint(null))
			{
				if (resultRelayer == null || resultRelayer.Handle == IntPtr.Zero)
				{
					throw new ArgumentNullException("resultRelayer");
				}
				VoiceChatController.Native.MuteLocalMic(resultRelayer.Handle, mute);
				lastResult = EResult.GetLastResult();
			}
			return lastResult;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000087B8 File Offset: 0x000069B8
		public static EResult MuteLocalSpeaker(ResultRelayer resultRelayer, bool mute)
		{
			EResult lastResult;
			using (StormEngine.SetProfilePoint(null))
			{
				if (resultRelayer == null || resultRelayer.Handle == IntPtr.Zero)
				{
					throw new ArgumentNullException("resultRelayer");
				}
				VoiceChatController.Native.MuteLocalSpeaker(resultRelayer.Handle, mute);
				lastResult = EResult.GetLastResult();
			}
			return lastResult;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000881C File Offset: 0x00006A1C
		public static EResult MuteRelation(ResultRelayer resultRelayer, uint relationChatterRefId, bool mute)
		{
			EResult lastResult;
			using (StormEngine.SetProfilePoint(null))
			{
				if (resultRelayer == null || resultRelayer.Handle == IntPtr.Zero)
				{
					throw new ArgumentNullException("resultRelayer");
				}
				VoiceChatController.Native.MuteRelation(resultRelayer.Handle, relationChatterRefId, mute);
				lastResult = EResult.GetLastResult();
			}
			return lastResult;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00008884 File Offset: 0x00006A84
		[MonoPInvokeCallback(typeof(VoiceChatController.Native.ChatterReadyHandler))]
		private static void ChatterReadyHandler(uint chatterRefId, ulong chatterId, IntPtr chatterNamePtr, ulong chatterNameLen, bool isLocal, bool audioDeviceActive, bool micMuted, bool speakerMuted)
		{
			using (StormEngine.SetProfilePoint(null))
			{
				if (VoiceChatController.OnChatterReady != null)
				{
					string text = Marshal.PtrToStringAnsi(chatterNamePtr, (int)chatterNameLen);
					VoiceChatController.OnChatterReady(chatterRefId, chatterId, text, isLocal, audioDeviceActive, micMuted, speakerMuted);
				}
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000088DC File Offset: 0x00006ADC
		[MonoPInvokeCallback(typeof(VoiceChatController.Native.ChatterGoneHandler))]
		private static void ChatterGoneHandler(uint chatterRefId, ulong chatterId)
		{
			using (StormEngine.SetProfilePoint(null))
			{
				if (VoiceChatController.OnChatterGone != null)
				{
					VoiceChatController.OnChatterGone(chatterRefId, chatterId);
				}
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00008920 File Offset: 0x00006B20
		[MonoPInvokeCallback(typeof(VoiceChatController.Native.ChatterTalkingHandler))]
		private static void ChatterTalkingHandler(uint chatterRefId, bool talking)
		{
			using (StormEngine.SetProfilePoint(null))
			{
				if (VoiceChatController.OnChatterTalking != null)
				{
					VoiceChatController.OnChatterTalking(chatterRefId, talking);
				}
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00008964 File Offset: 0x00006B64
		[MonoPInvokeCallback(typeof(VoiceChatController.Native.AudioDeviceActivedHandler))]
		private static void AudioDeviceActivedHandler(uint chatterRefId, bool audioDeviceActive)
		{
			using (StormEngine.SetProfilePoint(null))
			{
				if (VoiceChatController.OnAudioDeviceActived != null)
				{
					VoiceChatController.OnAudioDeviceActived(chatterRefId, audioDeviceActive);
				}
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000089A8 File Offset: 0x00006BA8
		[MonoPInvokeCallback(typeof(VoiceChatController.Native.RelationMutedHandler))]
		private static void RelationMutedHandler(uint chatterRefId, uint relationChatterRefId, bool micMuted, bool speakerMuted)
		{
			using (StormEngine.SetProfilePoint(null))
			{
				if (VoiceChatController.OnRelationMuted != null)
				{
					VoiceChatController.OnRelationMuted(chatterRefId, relationChatterRefId, micMuted, speakerMuted);
				}
			}
		}

		// Token: 0x040000AA RID: 170
		public static VoiceChat.ChatterReadyHandler OnChatterReady = null;

		// Token: 0x040000AB RID: 171
		public static VoiceChat.ChatterGoneHandler OnChatterGone = null;

		// Token: 0x040000AC RID: 172
		public static VoiceChat.ChatterTalkingHandler OnChatterTalking = null;

		// Token: 0x040000AD RID: 173
		public static VoiceChat.AudioDeviceActivedHandler OnAudioDeviceActived = null;

		// Token: 0x040000AE RID: 174
		public static VoiceChat.RelationMutedHandler OnRelationMuted = null;

		// Token: 0x040000AF RID: 175
		private static readonly List<GCHandle> gcHandles = new List<GCHandle>();

		// Token: 0x02000095 RID: 149
		private static class Native
		{
			// Token: 0x060002E6 RID: 742
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "VoiceChatController_Initialize")]
			public static extern void Initialize(VoiceChatController.Native.ChatterReadyHandler onChatterReady, VoiceChatController.Native.ChatterGoneHandler onChatterGone, VoiceChatController.Native.ChatterTalkingHandler onChatterTalking, VoiceChatController.Native.AudioDeviceActivedHandler onAudioDeviceActived, VoiceChatController.Native.RelationMutedHandler onRelationMuted);

			// Token: 0x060002E7 RID: 743
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "VoiceChatController_CreateLocalChatter")]
			public static extern void CreateLocalChatter(uint sessionRefId, [MarshalAs(UnmanagedType.LPStr)] string chatterName);

			// Token: 0x060002E8 RID: 744
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "VoiceChatController_DeleteLocalChatter")]
			public static extern void DeleteLocalChatter();

			// Token: 0x060002E9 RID: 745
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "VoiceChatController_MuteLocalMic")]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool MuteLocalMic(IntPtr resultRelayer, [MarshalAs(UnmanagedType.U1)] bool mute);

			// Token: 0x060002EA RID: 746
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "VoiceChatController_MuteLocalSpeaker")]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool MuteLocalSpeaker(IntPtr resultRelayer, [MarshalAs(UnmanagedType.U1)] bool mute);

			// Token: 0x060002EB RID: 747
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "VoiceChatController_MuteRelation")]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool MuteRelation(IntPtr resultRelayer, uint relationChatterRefId, [MarshalAs(UnmanagedType.U1)] bool mute);

			// Token: 0x02000096 RID: 150
			// (Invoke) Token: 0x060002ED RID: 749
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void ChatterReadyHandler(uint chatterRefId, ulong chatterId, IntPtr chatterNamePtr, ulong chatterNameLen, [MarshalAs(UnmanagedType.U1)] bool isLocal, [MarshalAs(UnmanagedType.U1)] bool audioDeviceActive, [MarshalAs(UnmanagedType.U1)] bool micMuted, [MarshalAs(UnmanagedType.U1)] bool speakerMuted);

			// Token: 0x02000097 RID: 151
			// (Invoke) Token: 0x060002F1 RID: 753
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void ChatterGoneHandler(uint chatterRefId, ulong chatterId);

			// Token: 0x02000098 RID: 152
			// (Invoke) Token: 0x060002F5 RID: 757
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void ChatterTalkingHandler(uint chatterRefId, [MarshalAs(UnmanagedType.U1)] bool talking);

			// Token: 0x02000099 RID: 153
			// (Invoke) Token: 0x060002F9 RID: 761
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void AudioDeviceActivedHandler(uint chatterRefId, [MarshalAs(UnmanagedType.U1)] bool audioDeviceActive);

			// Token: 0x0200009A RID: 154
			// (Invoke) Token: 0x060002FD RID: 765
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void RelationMutedHandler(uint chatterRefId, uint relationChatterRefId, [MarshalAs(UnmanagedType.U1)] bool micMuted, [MarshalAs(UnmanagedType.U1)] bool speakerMuted);
		}
	}
}
