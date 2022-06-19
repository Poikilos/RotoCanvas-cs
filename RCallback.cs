/*
 * Created by SharpDevelop.
 * User: Jake Gustafson, all rights reserved (Owner)
 * Date: 11/26/2005
 * Time: 7:25 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Windows.Forms;

namespace ExpertMultimedia {
	/// <summary>
	/// Class to be used as thread-safe reporting delegate.
	/// </summary>
	public class RCallback {
		public System.Windows.Forms.Form formX=null;
		public System.Windows.Forms.ListBox lbX=null;
		public System.Windows.Forms.TextBox sbX=null;
		public bool bFreeze=false;//whether to keep message on top
		public RCallback() {
			//formX=null;
			//sbX=null;
			//bFreeze=false
		}
		public string sStatus {
			set {
				UpdateStatus(value);
			}
		}
		public bool UpdateForm() {
			try {
				if (formX!=null) formX.Refresh();
			}
			catch (Exception exn) {
				return false;
			}
			return true;
		}
		public void UpdateStatus(string msg) {
			bool bGood=true;
			if (msg != null) {
				System.Diagnostics.Debug.WriteLine(msg);  // such as to Debug window of SharpDevelop
			}
			else {
				throw new ArgumentNullException("msg is null.");
			}
			try {
				if (bFreeze) {
					return;
				}
				if (sbX != null) {
					// sbX.Text = msg;
					// sbX.BeginInvoke(delegate { sbX.Text = msg; });
					/* ^ "Cannot convert anonymous method to type 'System.Delegate' because it is not a delegate type (CS1660) - C:\Users\Jatlivecom\GitHub\RotoCanvas-cs\RCallback.cs:53,23"
					*/
					if (sbX.InvokeRequired) {
						// StringDelegate sd = new StringDelegate(sbX.Items.Add);
						// ^ "'int System.Windows.Forms.ListBox.ObjectCollection.Add(object)' has the wrong return type (CS0407) - C:\Users\Jatlivecom\GitHub\RotoCanvas-cs\RCallback.cs:84,30"
						// this.Invoke(sd, new object[] { msg });
						// so see <https://stackoverflow.com/a/925073>:
						// sbX.Invoke(new MethodInvoker(UpdateStatus), new object[] { msg });
						// ^ "'bool ExpertMultimedia.RCallback.UpdateStatus()' has the wrong return type (CS0407) - C:\Users\Jatlivecom\GitHub\RotoCanvas-cs\RCallback.cs:63,18"
						// Uh oh! Fixing the return type wouldn't help, because that's an overload not this function!
						// so <https://stackoverflow.com/a/729439>:
						sbX.Invoke((MethodInvoker)delegate { UpdateStatus(msg); });
					}
					else {
						sbX.Text = msg;
					}
					
					
				}
				//bGood=UpdateStatus();
				UpdateStatus();
			}
			catch (Exception exn) {
				System.Diagnostics.Debug.WriteLine(exn.Message);
				Console.Error.WriteLine(exn.Message);
				// return false;
			}
			if (bGood) bGood=UpdateForm();
			// return bGood;
		}
		public void WriteLine(string msg) {
			this.WriteLine(msg, true);
		}
		private delegate void StringDelegate(string s);
		public void WriteLine(string msg, bool refresh_enable) {
			if (msg!=null) {
				System.Diagnostics.Debug.WriteLine(msg);  // such as to Debug window of SharpDevelop
				Console.Error.WriteLine(msg);
				if (lbX != null) {
					// lbX.Items.Add(msg);
					/* ^ "System.InvalidOperationException: Cross-thread
					operation not valid: Control 'lbOut' accessed from a
					thread other than the thread it was created on."
					*/
					// lbX.BeginInvoke(delegate { lbX.Items.Add(msg); });
					// ^ is not a delegate
					// so see <https://stackoverflow.com/a/925067>:
					if (lbX.InvokeRequired) {
						// StringDelegate sd = new StringDelegate(lbX.Items.Add);
						// ^ "'int System.Windows.Forms.ListBox.ObjectCollection.Add(object)' has the wrong return type (CS0407) - C:\Users\Jatlivecom\GitHub\RotoCanvas-cs\RCallback.cs:84,30"
						// this.Invoke(sd, new object[] { msg });
						// so see <https://stackoverflow.com/a/925073>:
						// lbX.Invoke(new MethodInvoker(WriteLine), new object[] { msg });
						// ^ "No overload for 'WriteLine' matches delegate 'System.Windows.Forms.MethodInvoker' (CS0123) - C:\Users\Jatlivecom\GitHub\RotoCanvas-cs\RCallback.cs:88,21"
						// so <https://stackoverflow.com/a/729439>:
						lbX.Invoke((MethodInvoker)delegate {
							WriteLine(msg, refresh_enable);
						});			
					}
					else {
						lbX.Items.Add(msg);
					}
					if (refresh_enable) {
						//if (formX!=null) {
						//	formX.Refresh();
						//}
						Application.DoEvents();
					}
				}
			}
			else {
				throw new ArgumentNullException("msg is null.");
			}
		}
		public string GetStatus() {
			bool bGood=true;
			string sReturn="";
			try {
				if (sbX!=null) sReturn=sbX.Text;
			}
			catch (Exception exn) {
				return "Can't get status.";
			}
			return sReturn;
		}

		public bool UpdateStatus() {
			try {
				if (formX!=null) formX.Refresh();
			}
			catch (Exception exn) {
				return false;
			}
			return true;
		}
	}
}
