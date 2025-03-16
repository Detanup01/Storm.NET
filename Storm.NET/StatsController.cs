using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using StormDotNet.Echo;
using StormDotNet.Stats;

namespace StormDotNet
{
	// Token: 0x02000069 RID: 105
	public static class StatsController
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x000068D8 File Offset: 0x00004AD8
		internal static void Initialize()
		{
			List<GCHandle> list = StatsController.gcHandles;
			lock (list)
			{
				StatsController.Native.ForwardDetailedStatsToManagedObservers forwardDetailedStatsToManagedObservers = new StatsController.Native.ForwardDetailedStatsToManagedObservers(StatsController.ForwardDetailedStatsToManagedObservers);
				GCHandle gchandle = GCHandle.Alloc(forwardDetailedStatsToManagedObservers, GCHandleType.Normal);
				StatsController.gcHandles.Add(gchandle);
				StatsController.Native.ForwardIntervalStatsToManagedObservers forwardIntervalStatsToManagedObservers = new StatsController.Native.ForwardIntervalStatsToManagedObservers(StatsController.ForwardIntervalStatsToManagedObservers);
				gchandle = GCHandle.Alloc(forwardIntervalStatsToManagedObservers, GCHandleType.Normal);
				StatsController.gcHandles.Add(gchandle);
				StatsController.Native.ForwardQOSStatsToManagedObservers forwardQOSStatsToManagedObservers = new StatsController.Native.ForwardQOSStatsToManagedObservers(StatsController.ForwardQOSStatsToManagedObservers);
				gchandle = GCHandle.Alloc(forwardQOSStatsToManagedObservers, GCHandleType.Normal);
				StatsController.gcHandles.Add(gchandle);
				StatsController.Native.ForwardGlobalQOSStatsToManagedObservers forwardGlobalQOSStatsToManagedObservers = new StatsController.Native.ForwardGlobalQOSStatsToManagedObservers(StatsController.ForwardGlobalQOSStatsToManagedObservers);
				gchandle = GCHandle.Alloc(forwardGlobalQOSStatsToManagedObservers, GCHandleType.Normal);
				StatsController.gcHandles.Add(gchandle);
				StatsController.Native.Initialize(forwardDetailedStatsToManagedObservers, forwardIntervalStatsToManagedObservers, forwardQOSStatsToManagedObservers, forwardGlobalQOSStatsToManagedObservers);
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000069A0 File Offset: 0x00004BA0
		internal static void Uninitialize()
		{
			List<GCHandle> list = StatsController.gcHandles;
			lock (list)
			{
				foreach (GCHandle gchandle in StatsController.gcHandles)
				{
					gchandle.Free();
				}
				StatsController.gcHandles.Clear();
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00006A24 File Offset: 0x00004C24
		[MonoPInvokeCallback(typeof(StatsController.Native.ForwardDetailedStatsToManagedObservers))]
		private unsafe static void ForwardDetailedStatsToManagedObservers(IntPtr frameStatData, ulong length)
		{
			if (frameStatData == IntPtr.Zero)
			{
				throw new ArgumentNullException("frameStatData");
			}
			if (StatsController.observers.Count > 0)
			{
				FrameStat frameStat;
				using (UnmanagedMemoryStream unmanagedMemoryStream = new UnmanagedMemoryStream((byte*)frameStatData.ToPointer(), (long)length))
				{
					using (BinaryReader binaryReader = new BinaryReader(unmanagedMemoryStream, Encoding.ASCII))
					{
						frameStat = new FrameStat(binaryReader);
					}
				}
				List<IStatsObserver> list = StatsController.observers;
				lock (list)
				{
					foreach (IStatsObserver statsObserver in StatsController.observers)
					{
						statsObserver.OnDetailedStatistic(ref frameStat);
					}
				}
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00006B18 File Offset: 0x00004D18
		[MonoPInvokeCallback(typeof(StatsController.Native.ForwardIntervalStatsToManagedObservers))]
		private unsafe static void ForwardIntervalStatsToManagedObservers(IntPtr frameStatData, ulong length)
		{
			if (frameStatData == IntPtr.Zero)
			{
				throw new ArgumentNullException("frameStatData");
			}
			if (StatsController.observers.Count > 0)
			{
				FrameStat frameStat;
				using (UnmanagedMemoryStream unmanagedMemoryStream = new UnmanagedMemoryStream((byte*)frameStatData.ToPointer(), (long)length))
				{
					using (BinaryReader binaryReader = new BinaryReader(unmanagedMemoryStream, Encoding.ASCII))
					{
						frameStat = new FrameStat(binaryReader);
					}
				}
				List<IStatsObserver> list = StatsController.observers;
				lock (list)
				{
					foreach (IStatsObserver statsObserver in StatsController.observers)
					{
						statsObserver.OnIntervalStatistic(ref frameStat);
					}
				}
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00006C0C File Offset: 0x00004E0C
		[MonoPInvokeCallback(typeof(StatsController.Native.ForwardQOSStatsToManagedObservers))]
		private unsafe static void ForwardQOSStatsToManagedObservers(IntPtr peerDescriptorHandle, IntPtr bandwidthRegulatorStatData, ulong length)
		{
			if (peerDescriptorHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("peerDescriptorHandle");
			}
			if (bandwidthRegulatorStatData == IntPtr.Zero)
			{
				throw new ArgumentNullException("bandwidthRegulatorStatData");
			}
			if (StatsController.observers.Count > 0)
			{
				PeerDescriptor peerDescriptor = GCHandle.FromIntPtr(peerDescriptorHandle).Target as PeerDescriptor;
				if (peerDescriptor == null)
				{
					throw new ArgumentException("The peerDescriptor is not of base type PeerDescriptor.", "peerDescriptorHandle");
				}
				QOSStat qosstat;
				using (UnmanagedMemoryStream unmanagedMemoryStream = new UnmanagedMemoryStream((byte*)bandwidthRegulatorStatData.ToPointer(), (long)length))
				{
					using (BinaryReader binaryReader = new BinaryReader(unmanagedMemoryStream, Encoding.ASCII))
					{
						qosstat = new QOSStat(binaryReader);
					}
				}
				List<IStatsObserver> list = StatsController.observers;
				lock (list)
				{
					foreach (IStatsObserver statsObserver in StatsController.observers)
					{
						statsObserver.OnQOSStatistic(peerDescriptor, ref qosstat);
					}
				}
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00006D44 File Offset: 0x00004F44
		[MonoPInvokeCallback(typeof(StatsController.Native.ForwardGlobalQOSStatsToManagedObservers))]
		private unsafe static void ForwardGlobalQOSStatsToManagedObservers(IntPtr bandwidthRegulatorStatData, ulong length)
		{
			if (bandwidthRegulatorStatData == IntPtr.Zero)
			{
				throw new ArgumentNullException("bandwidthRegulatorStatData");
			}
			if (StatsController.observers.Count > 0)
			{
				GlobalQOSStat globalQOSStat;
				using (UnmanagedMemoryStream unmanagedMemoryStream = new UnmanagedMemoryStream((byte*)bandwidthRegulatorStatData.ToPointer(), (long)length))
				{
					using (BinaryReader binaryReader = new BinaryReader(unmanagedMemoryStream, Encoding.ASCII))
					{
						globalQOSStat = new GlobalQOSStat(binaryReader);
					}
				}
				List<IStatsObserver> list = StatsController.observers;
				lock (list)
				{
					foreach (IStatsObserver statsObserver in StatsController.observers)
					{
						statsObserver.OnGlobalQOSStatistic(ref globalQOSStat);
					}
				}
			}
		}

		// Token: 0x04000090 RID: 144
		private static readonly List<GCHandle> gcHandles = new List<GCHandle>();

		// Token: 0x04000091 RID: 145
		private static readonly List<IStatsObserver> observers = new List<IStatsObserver>();

		// Token: 0x0200006A RID: 106
		private static class Native
		{
			// Token: 0x060001ED RID: 493
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "StatsController_Initialize")]
			public static extern void Initialize(StatsController.Native.ForwardDetailedStatsToManagedObservers forwardDetailedStatsToManagedObservers, StatsController.Native.ForwardIntervalStatsToManagedObservers forwardIntervalStatsToManagedObservers, StatsController.Native.ForwardQOSStatsToManagedObservers forwardQOSStatsToManagedObservers, StatsController.Native.ForwardGlobalQOSStatsToManagedObservers forwardGlobalQOSStatsToManagedObservers);

			// Token: 0x0200006B RID: 107
			// (Invoke) Token: 0x060001EF RID: 495
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void ForwardDetailedStatsToManagedObservers(IntPtr frameStatData, ulong length);

			// Token: 0x0200006C RID: 108
			// (Invoke) Token: 0x060001F3 RID: 499
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void ForwardIntervalStatsToManagedObservers(IntPtr frameStatData, ulong length);

			// Token: 0x0200006D RID: 109
			// (Invoke) Token: 0x060001F7 RID: 503
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void ForwardQOSStatsToManagedObservers(IntPtr peerDescriptorHandle, IntPtr bandwidthRegulatorStatData, ulong length);

			// Token: 0x0200006E RID: 110
			// (Invoke) Token: 0x060001FB RID: 507
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void ForwardGlobalQOSStatsToManagedObservers(IntPtr bandwidthRegulatorStatData, ulong length);
		}
	}
}
