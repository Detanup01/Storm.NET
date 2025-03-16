using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace StormDotNet.Echo.Object
{
	// Token: 0x020000E7 RID: 231
	public class NetObjectMessage : DataContainer
	{
		// Token: 0x06000484 RID: 1156 RVA: 0x0000D52C File Offset: 0x0000B72C
		internal new static void Initialize()
		{
			List<GCHandle> list = NetObjectMessage.gcHandles;
			lock (list)
			{
				NetObjectMessage.Native.CreateObjectMessage createObjectMessage = new NetObjectMessage.Native.CreateObjectMessage(NetObjectMessage.CreateObjectMessage);
				GCHandle gchandle = GCHandle.Alloc(createObjectMessage, GCHandleType.Normal);
				NetObjectMessage.gcHandles.Add(gchandle);
				NetObjectMessage.Native.ReleaseObjectMessage releaseObjectMessage = new NetObjectMessage.Native.ReleaseObjectMessage(NetObjectMessage.ReleaseObjectMessage);
				gchandle = GCHandle.Alloc(releaseObjectMessage, GCHandleType.Normal);
				NetObjectMessage.gcHandles.Add(gchandle);
				NetObjectMessage.Native.Initialize(createObjectMessage, releaseObjectMessage);
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000D5AC File Offset: 0x0000B7AC
		internal new static void Uninitialize()
		{
			List<GCHandle> list = NetObjectMessage.gcHandles;
			lock (list)
			{
				foreach (GCHandle gchandle in NetObjectMessage.gcHandles)
				{
					gchandle.Free();
				}
				NetObjectMessage.gcHandles.Clear();
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000D630 File Offset: 0x0000B830
		[MonoPInvokeCallback(typeof(NetObjectMessage.Native.CreateObjectMessage))]
		private static IntPtr CreateObjectMessage(IntPtr objectMessage, uint netProtocolEntryId)
		{
			if (objectMessage == IntPtr.Zero)
			{
				throw new ArgumentNullException("objectMessage");
			}
			Type type;
			if (!ObjectController.TryGetMessageType(netProtocolEntryId, out type))
			{
				throw new ArgumentException("The managed message type cannot be found for the specified protocol entry ID. Make sure ObjectController.AddObjectMessageEntry() was called properly.", "netProtocolEntryId");
			}
			ConstructorInfo constructor = type.GetConstructor(new Type[] { typeof(IntPtr) });
			if (constructor == null)
			{
				throw new MissingMethodException(string.Format("{0} does not implement a valid constructor.", type.FullName));
			}
			return GCHandle.ToIntPtr(GCHandle.Alloc(constructor.Invoke(new object[] { objectMessage }), GCHandleType.Normal));
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000D6C8 File Offset: 0x0000B8C8
		[MonoPInvokeCallback(typeof(NetObjectMessage.Native.ReleaseObjectMessage))]
		private static void ReleaseObjectMessage(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("handle");
			}
			GCHandle.FromIntPtr(handle).Free();
		}

		// Token: 0x040002A5 RID: 677
		private static readonly List<GCHandle> gcHandles = new List<GCHandle>();

		// Token: 0x020000E8 RID: 232
		private static class Native
		{
			// Token: 0x06000489 RID: 1161
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "NetObjectMessage_Initialize")]
			public static extern void Initialize(NetObjectMessage.Native.CreateObjectMessage createObjectMessage, NetObjectMessage.Native.ReleaseObjectMessage releaseObjectMessage);

			// Token: 0x020000E9 RID: 233
			// (Invoke) Token: 0x0600048B RID: 1163
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate IntPtr CreateObjectMessage(IntPtr objectMessage, uint netProtocolEntryId);

			// Token: 0x020000EA RID: 234
			// (Invoke) Token: 0x0600048F RID: 1167
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void ReleaseObjectMessage(IntPtr handle);
		}
	}
}
