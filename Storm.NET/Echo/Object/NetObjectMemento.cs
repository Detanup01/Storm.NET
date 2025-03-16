using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace StormDotNet.Echo.Object
{
	// Token: 0x020000E3 RID: 227
	public class NetObjectMemento : DataContainer
	{
		// Token: 0x06000476 RID: 1142 RVA: 0x0000D350 File Offset: 0x0000B550
		internal new static void Initialize()
		{
			List<GCHandle> list = NetObjectMemento.gcHandles;
			lock (list)
			{
				NetObjectMemento.Native.CreateObjectMemento createObjectMemento = new NetObjectMemento.Native.CreateObjectMemento(NetObjectMemento.CreateObjectMemento);
				GCHandle gchandle = GCHandle.Alloc(createObjectMemento, GCHandleType.Normal);
				NetObjectMemento.gcHandles.Add(gchandle);
				NetObjectMemento.Native.ReleaseObjectMemento releaseObjectMemento = new NetObjectMemento.Native.ReleaseObjectMemento(NetObjectMemento.ReleaseObjectMemento);
				gchandle = GCHandle.Alloc(releaseObjectMemento, GCHandleType.Normal);
				NetObjectMemento.gcHandles.Add(gchandle);
				NetObjectMemento.Native.Initialize(createObjectMemento, releaseObjectMemento);
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000D3D0 File Offset: 0x0000B5D0
		internal new static void Uninitialize()
		{
			List<GCHandle> list = NetObjectMemento.gcHandles;
			lock (list)
			{
				foreach (GCHandle gchandle in NetObjectMemento.gcHandles)
				{
					gchandle.Free();
				}
				NetObjectMemento.gcHandles.Clear();
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000D454 File Offset: 0x0000B654
		[MonoPInvokeCallback(typeof(NetObjectMemento.Native.CreateObjectMemento))]
		private static IntPtr CreateObjectMemento(IntPtr objectMemento, uint netProtocolEntryId)
		{
			if (objectMemento == IntPtr.Zero)
			{
				throw new ArgumentNullException("objectMemento");
			}
			Type type;
			if (!ObjectController.TryGetMementoType(netProtocolEntryId, out type))
			{
				throw new ArgumentException("The managed memento type cannot be found for the specified protocol entry ID. Make sure ObjectController.AddObjectMementoEntry() was called properly.", "netProtocolEntryId");
			}
			ConstructorInfo constructor = type.GetConstructor(new Type[] { typeof(IntPtr) });
			if (constructor == null)
			{
				throw new MissingMethodException(string.Format("{0} does not implement a valid constructor.", type.FullName));
			}
			return GCHandle.ToIntPtr(GCHandle.Alloc(constructor.Invoke(new object[] { objectMemento }), GCHandleType.Normal));
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000D4EC File Offset: 0x0000B6EC
		[MonoPInvokeCallback(typeof(NetObjectMemento.Native.ReleaseObjectMemento))]
		private static void ReleaseObjectMemento(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("handle");
			}
			GCHandle.FromIntPtr(handle).Free();
		}

		// Token: 0x040002A4 RID: 676
		private static readonly List<GCHandle> gcHandles = new List<GCHandle>();

		// Token: 0x020000E4 RID: 228
		private static class Native
		{
			// Token: 0x0600047B RID: 1147
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "NetObjectMemento_Initialize")]
			public static extern void Initialize(NetObjectMemento.Native.CreateObjectMemento createObjectMemento, NetObjectMemento.Native.ReleaseObjectMemento releaseObjectMemento);

			// Token: 0x020000E5 RID: 229
			// (Invoke) Token: 0x0600047D RID: 1149
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate IntPtr CreateObjectMemento(IntPtr objectMemento, uint netProtocolEntryId);

			// Token: 0x020000E6 RID: 230
			// (Invoke) Token: 0x06000481 RID: 1153
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void ReleaseObjectMemento(IntPtr handle);
		}
	}
}
