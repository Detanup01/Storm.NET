using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using StormDotNet.Common;

namespace StormDotNet
{
	// Token: 0x02000003 RID: 3
	public abstract class INetProperty
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020A4 File Offset: 0x000002A4
		internal static void Initialize()
		{
			List<GCHandle> list = INetProperty.gcHandles;
			lock (list)
			{
				INetProperty.Native.CreateNetProperty createNetProperty = new INetProperty.Native.CreateNetProperty(INetProperty.Create);
				GCHandle gchandle = GCHandle.Alloc(createNetProperty, GCHandleType.Normal);
				INetProperty.gcHandles.Add(gchandle);
				INetProperty.Native.ReleaseNetProperty releaseNetProperty = new INetProperty.Native.ReleaseNetProperty(INetProperty.Release);
				gchandle = GCHandle.Alloc(releaseNetProperty, GCHandleType.Normal);
				INetProperty.gcHandles.Add(gchandle);
				INetProperty.Native.CopyNetProperty copyNetProperty = new INetProperty.Native.CopyNetProperty(INetProperty.Copy);
				gchandle = GCHandle.Alloc(copyNetProperty, GCHandleType.Normal);
				INetProperty.gcHandles.Add(gchandle);
				INetProperty.Native.SerializeNetProperty serializeNetProperty = new INetProperty.Native.SerializeNetProperty(INetProperty.Serialize);
				gchandle = GCHandle.Alloc(serializeNetProperty, GCHandleType.Normal);
				INetProperty.gcHandles.Add(gchandle);
				INetProperty.Native.DeserializeNetProperty deserializeNetProperty = new INetProperty.Native.DeserializeNetProperty(INetProperty.Deserialize);
				gchandle = GCHandle.Alloc(deserializeNetProperty, GCHandleType.Normal);
				INetProperty.gcHandles.Add(gchandle);
				INetProperty.Native.OnAccept onAccept = new INetProperty.Native.OnAccept(INetProperty.OnAccept);
				gchandle = GCHandle.Alloc(onAccept, GCHandleType.Normal);
				INetProperty.gcHandles.Add(gchandle);
				INetProperty.Native.Initialize(createNetProperty, releaseNetProperty, copyNetProperty, serializeNetProperty, deserializeNetProperty, onAccept);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021B4 File Offset: 0x000003B4
		internal static void Uninitialize()
		{
			List<GCHandle> list = INetProperty.gcHandles;
			lock (list)
			{
				foreach (GCHandle gchandle in INetProperty.gcHandles)
				{
					gchandle.Free();
				}
				INetProperty.gcHandles.Clear();
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002238 File Offset: 0x00000438
		internal IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x0600000A RID: 10
		public abstract void Set(INetProperty property);

		// Token: 0x0600000B RID: 11 RVA: 0x00002240 File Offset: 0x00000440
		public virtual void OnAccept(IntPtr opHandle)
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002242 File Offset: 0x00000442
		public virtual EResult Serialize(Stream stream)
		{
			return EResult.Create(EResult.ECode.OK, "");
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002242 File Offset: 0x00000442
		public virtual EResult Deserialize(Stream stream)
		{
			return EResult.Create(EResult.ECode.OK, "");
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000224F File Offset: 0x0000044F
		public static bool operator ==(INetProperty a, INetProperty b)
		{
			return a == b || (a != null && b != null && a.Equals(b));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002266 File Offset: 0x00000466
		public static bool operator !=(INetProperty a, INetProperty b)
		{
			return a != b && (a == null || b == null || !a.Equals(b));
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002280 File Offset: 0x00000480
		[MonoPInvokeCallback(typeof(INetProperty.Native.CreateNetProperty))]
		private static IntPtr Create(IntPtr netProperty, uint netProtocolEntryId)
		{
			if (netProperty == IntPtr.Zero)
			{
				throw new ArgumentNullException("netProperty");
			}
			Type type;
			if (!DynamicTypeRegistry.TryGet(netProtocolEntryId, out type))
			{
				throw new ArgumentException("The net property type cannot be found for the specified protocol entry ID. Make sure the type was registered correctly in the DynamicTypeRegistry", "netProtocolEntryId");
			}
			ConstructorInfo constructor = type.GetConstructor(new Type[] { typeof(IntPtr) });
			if (constructor == null)
			{
				throw new MissingMethodException(type.FullName + " does not implement a valid constructor.");
			}
			return GCHandle.ToIntPtr(GCHandle.Alloc(constructor.Invoke(new object[] { netProperty }), GCHandleType.Normal));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002318 File Offset: 0x00000518
		[MonoPInvokeCallback(typeof(INetProperty.Native.ReleaseNetProperty))]
		private static void Release(IntPtr netPropertyhandle)
		{
			if (netPropertyhandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("netPropertyhandle");
			}
			GCHandle.FromIntPtr(netPropertyhandle).Free();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000234C File Offset: 0x0000054C
		[MonoPInvokeCallback(typeof(INetProperty.Native.CopyNetProperty))]
		private static void Copy(IntPtr netPropertyHandle, IntPtr copyNetPropertyHandle)
		{
			if (netPropertyHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("netPropertyHandle");
			}
			if (copyNetPropertyHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("copyNetPropertyHandle");
			}
			INetProperty netProperty = GCHandle.FromIntPtr(netPropertyHandle).Target as INetProperty;
			INetProperty netProperty2 = GCHandle.FromIntPtr(copyNetPropertyHandle).Target as INetProperty;
			netProperty.Set(netProperty2);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023B8 File Offset: 0x000005B8
		[MonoPInvokeCallback(typeof(INetProperty.Native.SerializeNetProperty))]
		private static int Serialize(IntPtr netPropertyHandle, IntPtr streamHandle)
		{
			if (netPropertyHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("netPropertyHandle");
			}
			if (streamHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("streamHandle");
			}
			int errorCode;
			using (Stream stream = new Stream(streamHandle))
			{
				errorCode = (int)(GCHandle.FromIntPtr(netPropertyHandle).Target as INetProperty).Serialize(stream).GetErrorCode();
			}
			return errorCode;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002438 File Offset: 0x00000638
		[MonoPInvokeCallback(typeof(INetProperty.Native.DeserializeNetProperty))]
		private static int Deserialize(IntPtr netPropertyHandle, IntPtr streamHandle)
		{
			if (netPropertyHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("netPropertyHandle");
			}
			if (streamHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("streamHandle");
			}
			int errorCode;
			using (Stream stream = new Stream(streamHandle))
			{
				errorCode = (int)(GCHandle.FromIntPtr(netPropertyHandle).Target as INetProperty).Deserialize(stream).GetErrorCode();
			}
			return errorCode;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024B8 File Offset: 0x000006B8
		[MonoPInvokeCallback(typeof(INetProperty.Native.OnAccept))]
		private static void OnAccept(IntPtr netPropertyHandle, IntPtr opHandle)
		{
			if (netPropertyHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("netPropertyHandle");
			}
			if (opHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("opHandle");
			}
			(GCHandle.FromIntPtr(netPropertyHandle).Target as INetProperty).OnAccept(opHandle);
		}

		// Token: 0x04000004 RID: 4
		protected static readonly List<GCHandle> gcHandles = new List<GCHandle>();

		// Token: 0x04000005 RID: 5
		protected IntPtr handle;

		// Token: 0x02000004 RID: 4
		private static class Native
		{
			// Token: 0x06000017 RID: 23
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "NetProperty_Initialize")]
			public static extern void Initialize(INetProperty.Native.CreateNetProperty createNetProperty, INetProperty.Native.ReleaseNetProperty releaseNetProperty, INetProperty.Native.CopyNetProperty copyNetProperty, INetProperty.Native.SerializeNetProperty serializeNetProperty, INetProperty.Native.DeserializeNetProperty deserializeNetProperty, INetProperty.Native.OnAccept onAccept);

			// Token: 0x02000005 RID: 5
			// (Invoke) Token: 0x06000019 RID: 25
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate IntPtr CreateNetProperty(IntPtr handle, uint netProtocolEntryId);

			// Token: 0x02000006 RID: 6
			// (Invoke) Token: 0x0600001D RID: 29
			[UnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void ReleaseNetProperty(IntPtr handle);

			// Token: 0x02000007 RID: 7
			// (Invoke) Token: 0x06000021 RID: 33
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void CopyNetProperty(IntPtr netPropertyHandle, IntPtr copyNetPropertyHandle);

			// Token: 0x02000008 RID: 8
			// (Invoke) Token: 0x06000025 RID: 37
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate int SerializeNetProperty(IntPtr netPropertyHandle, IntPtr streamHandle);

			// Token: 0x02000009 RID: 9
			// (Invoke) Token: 0x06000029 RID: 41
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate int DeserializeNetProperty(IntPtr netPropertyHandle, IntPtr streamHandle);

			// Token: 0x0200000A RID: 10
			// (Invoke) Token: 0x0600002D RID: 45
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void OnAccept(IntPtr netPropertyHandle, IntPtr opHandle);
		}
	}
}
