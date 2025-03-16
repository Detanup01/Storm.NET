using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace StormDotNet.Echo.Object
{
	// Token: 0x020000C9 RID: 201
	public class NetObject
	{
		// Token: 0x060003F7 RID: 1015 RVA: 0x0000C410 File Offset: 0x0000A610
		internal static void Initialize()
		{
			List<GCHandle> list = NetObject.gcHandles;
			lock (list)
			{
				NetObject.Native.CreateObject createObject = new NetObject.Native.CreateObject(NetObject.CreateObject);
				GCHandle gchandle = GCHandle.Alloc(createObject, GCHandleType.Normal);
				NetObject.gcHandles.Add(gchandle);
				NetObject.Native.ReleaseObject releaseObject = new NetObject.Native.ReleaseObject(NetObject.ReleaseObject);
				gchandle = GCHandle.Alloc(releaseObject, GCHandleType.Normal);
				NetObject.gcHandles.Add(gchandle);
				NetObject.Native.SetObjectHandler setObjectHandler = new NetObject.Native.SetObjectHandler(NetObject.SetObjectHandler);
				gchandle = GCHandle.Alloc(setObjectHandler, GCHandleType.Normal);
				NetObject.gcHandles.Add(gchandle);
				NetObject.Native.Initialize(createObject, releaseObject, setObjectHandler);
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000C4B4 File Offset: 0x0000A6B4
		internal static void Uninitialize()
		{
			List<GCHandle> list = NetObject.gcHandles;
			lock (list)
			{
				foreach (GCHandle gchandle in NetObject.gcHandles)
				{
					gchandle.Free();
				}
				NetObject.gcHandles.Clear();
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000C538 File Offset: 0x0000A738
		[MonoPInvokeCallback(typeof(NetObject.Native.CreateObject))]
		private static IntPtr CreateObject(uint netObjectTypeName)
		{
			Type type;
			if (!ObjectController.TryGetObjectType(netObjectTypeName, out type))
			{
				throw new ArgumentException("The managed object type cannot be found for the specified object type's name. Make sure ObjectController.AddObjectType() was called properly.", "netObjectTypeName");
			}
			ConstructorInfo constructor = type.GetConstructor(new Type[0]);
			if (constructor == null)
			{
				throw new MissingMethodException(string.Format("{0} does not implement a valid constructor.", type.FullName));
			}
			return GCHandle.ToIntPtr(GCHandle.Alloc(constructor.Invoke(new object[0]), GCHandleType.Normal));
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000C5A0 File Offset: 0x0000A7A0
		[MonoPInvokeCallback(typeof(NetObject.Native.ReleaseObject))]
		private static void ReleaseObject(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("handle");
			}
			GCHandle gchandle = GCHandle.FromIntPtr(handle);
			NetObject netObject = gchandle.Target as NetObject;
			if (netObject == null)
			{
				throw new ArgumentException("The network object is not of base type NetObject.", "handle");
			}
			netObject.objectHandler = null;
			gchandle.Free();
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000C5F8 File Offset: 0x0000A7F8
		[MonoPInvokeCallback(typeof(NetObject.Native.SetObjectHandler))]
		private static void SetObjectHandler(IntPtr objectHandle, IntPtr objectHandlerHandle)
		{
			if (objectHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("objectHandle");
			}
			if (objectHandlerHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("objectHandlerHandle");
			}
			GCHandle gchandle = GCHandle.FromIntPtr(objectHandle);
			GCHandle gchandle2 = GCHandle.FromIntPtr(objectHandlerHandle);
			NetObject netObject = gchandle.Target as NetObject;
			NetObjectHandler netObjectHandler = gchandle2.Target as NetObjectHandler;
			if (netObject == null)
			{
				throw new ArgumentException("The network object is not of base type NetObject.", "objectHandle");
			}
			if (netObjectHandler == null)
			{
				throw new ArgumentException("The network object handler is not of base type NetObjectHandler.", "objectHandlerHandle");
			}
			netObject.objectHandler = netObjectHandler;
		}

		// Token: 0x04000298 RID: 664
		private static readonly List<GCHandle> gcHandles = new List<GCHandle>();

		// Token: 0x04000299 RID: 665
		private NetObjectHandler objectHandler;

		// Token: 0x020000CA RID: 202
		private static class Native
		{
			// Token: 0x060003FD RID: 1021
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "NetObject_Initialize")]
			public static extern void Initialize(NetObject.Native.CreateObject createObject, NetObject.Native.ReleaseObject releaseObject, NetObject.Native.SetObjectHandler setObjectHandler);

			// Token: 0x020000CB RID: 203
			// (Invoke) Token: 0x060003FF RID: 1023
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate IntPtr CreateObject(uint netObjectTypeName);

			// Token: 0x020000CC RID: 204
			// (Invoke) Token: 0x06000403 RID: 1027
			[UnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void ReleaseObject(IntPtr handle);

			// Token: 0x020000CD RID: 205
			// (Invoke) Token: 0x06000407 RID: 1031
			[UnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void SetObjectHandler(IntPtr objectHandle, IntPtr objectHandlerHandle);
		}

		// Token: 0x020000CE RID: 206
		// (Invoke) Token: 0x0600040B RID: 1035
		public delegate void ObjectCreatedHandler(uint sessionRefId, NetObjectHandler objectHandler, NetObjectRole objectRole, EResult result);

		// Token: 0x020000CF RID: 207
		// (Invoke) Token: 0x0600040F RID: 1039
		public delegate void ObjectDeletedHandler(uint sessionRefId, NetObjectDescriptor objectDescriptor, NetObjectRole objectRole, EResult result);
	}
}
