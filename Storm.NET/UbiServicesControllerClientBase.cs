using System;

namespace StormDotNet
{
	// Token: 0x02000092 RID: 146
	public class UbiServicesControllerClientBase : UbiServicesControllerBase
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00008215 File Offset: 0x00006415
		private bool IsValidCredentials
		{
			get
			{
				return !this.badCredentials;
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00008220 File Offset: 0x00006420
		public UbiServicesControllerClientBase()
		{
			base.Enabled = false;
			this.isLogged = false;
			this.lastLoginAttempt = 0;
			this.reloginInProgress = false;
			this.badCredentials = false;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000824C File Offset: 0x0000644C
		public override EResult Enable()
		{
			if (base.Enabled)
			{
				return EResult.Create(EResult.ECode.OK, "Already Enabled.");
			}
			base.Enabled = true;
			EResult eresult = this.RequestLogin();
			if (StormEngine.Failed(eresult))
			{
				base.Enabled = false;
				return eresult;
			}
			return EResult.Create(EResult.ECode.OK, "");
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00008298 File Offset: 0x00006498
		public override EResult Disable()
		{
			if (!base.Enabled)
			{
				return EResult.Create(EResult.ECode.OK, "Already disabled");
			}
			base.Enabled = false;
			EResult eresult = this.RequestLogout();
			if (StormEngine.Failed(eresult))
			{
				return eresult;
			}
			return EResult.Create(EResult.ECode.OK, "");
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x000082DC File Offset: 0x000064DC
		public override bool Relogin()
		{
			if (!base.Enabled)
			{
				if (this.IsLogged)
				{
					this.RequestLogout();
				}
				this.CreateOrRecreateFacade();
				return false;
			}
			if (!this.reloginInProgress)
			{
				this.reloginInProgress = true;
				this.DeleteSession(delegate(int code)
				{
					this.CreateOrRecreateFacade();
					UbiServicesControllerBase.FacadeResetEventArgs facadeResetEventArgs = new UbiServicesControllerBase.FacadeResetEventArgs();
					base.OnFacadeReset(this, facadeResetEventArgs);
					this.lastLoginAttempt = Environment.TickCount;
					this.badCredentials = false;
					this.CreateSession(new UbiServicesControllerClientBase.LoginOrLogoutResponse(this.OnUSRequestCompleted));
				});
				return true;
			}
			return false;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000832C File Offset: 0x0000652C
		public override void Update()
		{
			bool flag = this.IsLogged;
			if (this.isLogged != flag)
			{
				this.isLogged = flag;
				if (this.isLogged && this.reloginInProgress)
				{
					this.reloginInProgress = false;
				}
				EResult eresult = EResult.Create(EResult.ECode.OK, this.isLogged ? "Logged into UbiServices" : "Logged out from UbiServices");
				if (this.isLogged)
				{
					UbiServicesControllerBase.ServerConnectedEventArgs serverConnectedEventArgs = new UbiServicesControllerBase.ServerConnectedEventArgs(eresult);
					base.OnServerConnected(this, serverConnectedEventArgs);
				}
				else
				{
					UbiServicesControllerBase.ServerDisconnectedEventArgs serverDisconnectedEventArgs = new UbiServicesControllerBase.ServerDisconnectedEventArgs(eresult);
					base.OnServerDisconnected(this, serverDisconnectedEventArgs);
					this.lastLoginAttempt = Environment.TickCount;
				}
			}
			if (base.Enabled && !this.isLogged && !this.IsValidCredentials && Environment.TickCount - this.lastLoginAttempt >= 30000)
			{
				this.RequestLogin();
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000083E8 File Offset: 0x000065E8
		public override void ResetFacade(object newFacade)
		{
			if (newFacade != null)
			{
				this.ResetFacadeInternal(newFacade);
				UbiServicesControllerBase.FacadeResetEventArgs facadeResetEventArgs = new UbiServicesControllerBase.FacadeResetEventArgs();
				base.OnFacadeReset(this, facadeResetEventArgs);
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x000074D5 File Offset: 0x000056D5
		protected virtual bool IsErrorCodeAuthMissingParameter(int errorCode)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000074D5 File Offset: 0x000056D5
		protected virtual bool IsErrorCodeAuthInvalidParameter(int errorCode)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000074D5 File Offset: 0x000056D5
		protected virtual bool IsErrorCodeAuthUnanthorized(int errorCode)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000074D5 File Offset: 0x000056D5
		protected virtual bool IsErrorCodeAuthForbidden(int errorCode)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000074D5 File Offset: 0x000056D5
		protected virtual void CancelProcessingLogin()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000074D5 File Offset: 0x000056D5
		protected virtual void CancelProcessingLogout()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000074D5 File Offset: 0x000056D5
		protected virtual void CreateSession(UbiServicesControllerClientBase.LoginOrLogoutResponse whenDone)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002CE RID: 718 RVA: 0x000074D5 File Offset: 0x000056D5
		protected virtual void DeleteSession(UbiServicesControllerClientBase.LoginOrLogoutResponse whenDone)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000074D5 File Offset: 0x000056D5
		protected virtual void CreateOrRecreateFacade()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000074D5 File Offset: 0x000056D5
		protected virtual void ResetFacadeInternal(object newFacade)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00008410 File Offset: 0x00006610
		private EResult RequestLogin()
		{
			if (this.IsLogged)
			{
				EResult eresult = EResult.Create(EResult.ECode.OK, "Already connected to UbiServices");
				UbiServicesControllerBase.ServerConnectedEventArgs serverConnectedEventArgs = new UbiServicesControllerBase.ServerConnectedEventArgs(eresult);
				base.OnServerConnected(this, serverConnectedEventArgs);
				return eresult;
			}
			this.lastLoginAttempt = Environment.TickCount;
			this.CancelProcessingLogin();
			this.CancelProcessingLogout();
			this.badCredentials = false;
			this.CreateSession(new UbiServicesControllerClientBase.LoginOrLogoutResponse(this.OnUSRequestCompleted));
			return EResult.Create(EResult.ECode.OK_Async, "");
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000847C File Offset: 0x0000667C
		private EResult RequestLogout()
		{
			if (!this.IsLogged)
			{
				EResult eresult = EResult.Create(EResult.ECode.OK, "Already logout from UbiServices");
				UbiServicesControllerBase.ServerDisconnectedEventArgs serverDisconnectedEventArgs = new UbiServicesControllerBase.ServerDisconnectedEventArgs(eresult);
				base.OnServerDisconnected(this, serverDisconnectedEventArgs);
				return eresult;
			}
			this.CancelProcessingLogin();
			this.CancelProcessingLogout();
			this.DeleteSession(new UbiServicesControllerClientBase.LoginOrLogoutResponse(this.OnUSRequestCompleted));
			return EResult.Create(EResult.ECode.OK_Async, "");
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x000084D5 File Offset: 0x000066D5
		private void OnUSRequestCompleted(int errorCode)
		{
			if (!this.IsLogged)
			{
				this.badCredentials = this.IsErrorCodeAuthMissingParameter(errorCode) || this.IsErrorCodeAuthInvalidParameter(errorCode) || this.IsErrorCodeAuthUnanthorized(errorCode) || this.IsErrorCodeAuthForbidden(errorCode);
			}
		}

		// Token: 0x040000A6 RID: 166
		private bool isLogged;

		// Token: 0x040000A7 RID: 167
		private int lastLoginAttempt;

		// Token: 0x040000A8 RID: 168
		private bool reloginInProgress;

		// Token: 0x040000A9 RID: 169
		private bool badCredentials;

		// Token: 0x02000093 RID: 147
		// (Invoke) Token: 0x060002D6 RID: 726
		public delegate void LoginOrLogoutResponse(int errorCode);
	}
}
