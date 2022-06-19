/*
 * Created by SharpDevelop.
 * User: Owner
 * Date: 3/13/2009
 * Time: 8:12 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace ExpertMultimedia
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		public string sMyName="RotoCanvas";
		
		//Bitmap bmpBack=new Bitmap(640,480,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
		System.Drawing.Point pTopLeft=new Point(0,0);
		//RImage riFrame=new RImage();
		//Bitmap bmpFFMPEG=null;
		RCallback callbackNow=null;
		//public bool bClosing=false;
		string profile = Environment.GetEnvironmentVariable("USERPROFILE");
		string videos = null;
		string devVideoPath = null;
		public RotoCanvas rotocanvasNow=null;
		static System.Timers.Timer testTimer = null;
		
		public MainForm()
		{
			//
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			videos = Path.Combine(this.profile, "Videos");
			devVideoPath = Path.Combine(videos, "PGS_Demo_2000_found-teranex-720p.mp4");
			InitializeComponent();

			callbackNow=new RCallback();
			rotocanvasNow=new RotoCanvas();
			//ProgramFolderThenSlash=Directory.GetCurrentDirectory();
			//if (!ProgramFolderThenSlash.EndsWith(RString.sDirSep)) ProgramFolderThenSlash+=RString.sDirSep;
			
			callbackNow.formX=this;
			callbackNow.lbX=this.lbOut;
			callbackNow.sbX=this.tbStatus;
		}
		
		void DebugWriteLine(string msg) {
			System.Diagnostics.Debug.WriteLine(msg);
		}
		
		void ShowExn(Exception exn, string sParticiple, string sNoun) {
			callbackNow.WriteLine("Couldn't finish "+((sParticiple==null)?"":sParticiple)+" in "+((sNoun==null)?"":sNoun)+": "+exn.ToString());
		}
		
		void MainFormLoad(object sender, EventArgs e) {
			string sFileTheoretical=@"D:\Videos\Projects\Rebel Assault IX\RAIX2b\Scene04 (Speeder Bikes)\shot1 (from left)\2b3_3 manual deshake\RAIX2b-scene-speederbikes-shot1-0001.png";
			tbInput.Text = "";
			if (File.Exists(sFileTheoretical)) tbInput.Text = sFileTheoretical;
			else if (File.Exists(devVideoPath)) tbInput.Text = devVideoPath;
			
			if (tbInput.Text != "") {
				DebugWriteLine("found test file \""+tbInput.Text+"\"");
				Application.DoEvents();
				// this.OpenVideoFromInput();
				testTimer = new System.Timers.Timer();
				testTimer.Interval = 50;
				testTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.testTimer_Tick);
				//TODO: testTimer.Start();
			}
			else {
				DebugWriteLine("There is no test file.");
			}
		}
		
		private void testTimer_Tick(object sender, EventArgs e)
		{
			testTimer.Stop();
			this.OpenVideoFromInput();
		}

		void BtnBrowseClick(object sender, EventArgs e)
		{
			this.ofiledlg.FileName="";
			this.ofiledlg.ShowDialog(this);
		}
		
		void OfiledlgFileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.tbInput.Text=ofiledlg.FileName;//TODO: use FileNames string[]
		}
		

		
		void MainFormResize(object sender, EventArgs e)
		{
			//UpdateSize();
		}
		
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			rotocanvasNow.bCancel=true;//bClosing=true;
		}
		
		void TrackbarFrameScroll(object sender, EventArgs e)
		{
			
		}
		
		void TrackbarFrameValueChanged(object sender, EventArgs e)
		{
			//if (RString.SafeLength(OpenedFile_FullBaseName)>0) {
			//	tbStatus.Text="About to call DrawFrame("+this.trackbarFrame.Value.ToString()+")...";
			//	DrawFrame(this.trackbarFrame.Value);
			//}
			//else tbStatus.Text="No frames exist.";
			rotocanvasNow.GotoFrame(this.trackbarFrame.Value,RConvert.ToInt(this.nudMinDigits.Value),callbackNow);
			panel1.Invalidate();
		}
		
		void NudMinDigitsValueChanged(object sender, EventArgs e)
		{
			string sText="Image Sequence Digits *";
			for (int i=0; i<(int)this.nudMinDigits.Value; i++) {
				sText+="0";
			}
			sText+=".*";
			lblMinDigits.Text=sText;
		}
		
		void OpenVideoToolStripMenuItemClick(object sender, EventArgs e)
		{
			cancelToolStripMenuItem.Enabled=true;
			//GetInputAsync("RotoCanvas.OpenVideo","Choose video file:",new string[]{"OK","Cancel"},MainForm.InputTypeVideo,null);
			
			DialogResult result = DialogResult.Cancel;
			System.Windows.Forms.OpenFileDialog ofd;
			ofd = new System.Windows.Forms.OpenFileDialog();
			ofd.Title = "Open Video";
			// string profile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
			// ^ recent versions of C# only
			ofd.InitialDirectory = videos;
			result = ofd.ShowDialog();
			if (result != DialogResult.OK) {
				return;
			}
			this.OpenVideo(ofd.FileName);
			panel1.Invalidate();
		}
		
		void OpenImageSequenceToolStripMenuItemClick(object sender, EventArgs e)
		{
			cancelToolStripMenuItem.Enabled=true;
			//GetInputAsync("RotoCanvas.OpenSeq","Open Image Sequence:",new string[]{"OK","Cancel"},MainForm.InputTypeImage);
			rotocanvasNow.OpenSeq(tbInput.Text,RConvert.ToInt(this.nudMinDigits.Value),callbackNow);
			this.trackbarFrame.Minimum=rotocanvasNow.get_FirstIndex();
			this.trackbarFrame.Maximum=rotocanvasNow.get_LastIndex();
			this.trackbarFrame.Value=rotocanvasNow.get_CurrentIndex();
		}
		
		void MaskToAlphaToolStripMenuItemClick(object sender, EventArgs e)
		{
			//cancelToolStripMenuItem.Enabled=true;
			//ArrayList alCommand=new ArrayList();
			//alCommand.Add("MaskToAlpha_LoadRGB");
			//alCommand.Add("\""+tbStatus.Text.Replace("\"","\\\"")+"\"");
			//RunCommand(alCommand);
			//GetInputAsync("RotoCanvas.MaskToAlpha_LoadRGB","Choose Image Sequence for RGB channels",new string[]{"OK","Cancel"},MainForm.InputTypeImage);
			tbStatus.Text="NOT YET IMPLEMENTED: Load PNG/JPG/BMP frame sequence (named like: name0000.png, name0001.png...)";
			//rotocanvasNow.
		}
		
		void VideoToSequenceMJPEGLosslessToolStripMenuItemClick(object sender, EventArgs e)
		{
			cancelToolStripMenuItem.Enabled=true;
			rotocanvasNow.MJPEGToSeq(tbInput.Text,(new FileInfo(tbInput.Text)).Directory.FullName,callbackNow);
		}
		
		void CancelToolStripMenuItemClick(object sender, EventArgs e)
		{
			rotocanvasNow.bCancel=true;
			cancelToolStripMenuItem.Enabled=false;
		}
		
		void Panel1Paint(object sender, PaintEventArgs e)
		{
			//if (riFrame!=null) {
			//	riFrame.DrawAs(bmpBack,bmpBack.PixelFormat);
			//}
			if (rotocanvasNow.riFrame!=null) {
				System.Drawing.Size size = new Size(rotocanvasNow.riFrame.Width, rotocanvasNow.riFrame.Height);
				System.Diagnostics.Debug.WriteLine("Panel1Paint {riFrame.iFrame:"+rotocanvasNow.get_CurrentIndex()+"; size: "+size.ToString()+"}");
				if (panel1.Width!=rotocanvasNow.riFrame.Width) panel1.Width=rotocanvasNow.riFrame.Width;
				if (panel1.Height!=rotocanvasNow.riFrame.Height) panel1.Height=rotocanvasNow.riFrame.Height;
				rotocanvasNow.DrawFrame(callbackNow);
//				for (int y=0; y<rotocanvasNow.riFrame.Height; y++) {
//					for (int x=0; x<rotocanvasNow.riFrame.Width; x++) {
//						rotocanvasNow.bmpBack.SetPixel(x,y,rotocanvasNow.riFrame.GetPixel(x,y));
//					}
//				}
				e.Graphics.DrawImageUnscaled(rotocanvasNow.bmpBack,pTopLeft);
			}
			else {
				System.Diagnostics.Debug.WriteLine("Panel1Paint {riFrame:null}");
			}
		}
		
		void OpenVideo(string path) {
			DebugWriteLine("OpenVideo \""+path+"\"...");
			tbInput.Text = path;
			rotocanvasNow.OpenVideo(tbInput.Text,RConvert.ToInt(this.nudMinDigits.Value),callbackNow);
			this.trackbarFrame.Minimum=rotocanvasNow.get_FirstIndex();
			this.trackbarFrame.Maximum=rotocanvasNow.get_LastIndex();
			int frame = rotocanvasNow.get_CurrentIndex();
			if (frame < 0) {
				frame = 0;
			}
			this.trackbarFrame.Value = frame;
			DebugWriteLine("GotoFrame \""+frame.ToString()+"\"...");
			rotocanvasNow.GotoFrame(frame, 0, callbackNow);
			this.panel1.Invalidate();
		}
		
		void OpenVideoFromInput() {
			this.OpenVideo(tbInput.Text);
		}
	}//end MainForm
}//end namespace
