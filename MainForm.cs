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
        string profile = null;
        string videos = null;
        string devVideoPath = null;
        public RotoCanvas rotocanvasNow=null;
        // System.Timers.Timer is not thread safe by default, so:
        static System.Windows.Forms.Timer testTimer = null;
        System.Windows.Forms.Timer startTimer = null;
        bool _started = false;
        bool _testStarted = false;

        public MainForm()
        {
            // profile = Environment.GetEnvironmentVariable("USERPROFILE");
            profile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

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
            // callbackNow.sbX=this.tbStatus;
            callbackNow.tsslX=this.tbStatus;
            if (rotocanvasNow.error != null) {
                rotocanvasNow.error = null;
                this.tbStatus.Text = String.Format("RotoCanvas couldn't start: {0}", rotocanvasNow.error);
            }
        }

        void DebugWriteLine(string msg) {
            System.Diagnostics.Debug.WriteLine(msg);
        }

        void ShowExn(Exception exn, string sParticiple, string sNoun) {
            callbackNow.WriteLine("Couldn't finish "+((sParticiple==null)?"":sParticiple)+" in "+((sNoun==null)?"":sNoun)+": "+exn.ToString());
        }

        void MainFormLoad(object sender, EventArgs e) {
            // string tryPath = @"D:\Videos\Projects\Rebel Assault IX\RAIX2b\Scene04 (Speeder Bikes)\shot1 (from left)\2b3_3 manual deshake\RAIX2b-scene-speederbikes-shot1-0001.png";
            string profile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string tryPath = Path.Combine(profile, @"Videos\The Secret of Cooey\media\Darkness Ethereal - The Secret of Cooey - 1998.mp4");
            tbInput.Text = "";
            if (File.Exists(tryPath)) tbInput.Text = tryPath;
            else if (File.Exists(devVideoPath)) tbInput.Text = devVideoPath;

            if (tbInput.Text != "") {
                DebugWriteLine("found test file \""+tbInput.Text+"\"");  // loaded in testTimer_Tick
                Application.DoEvents();
                // this.OpenVideoFromInput();  // called in testTimer_Tick
                testTimer = new System.Windows.Forms.Timer();
                testTimer.Interval = 50;
                // System.Threading.Timer uses:
                // testTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.testTimer_Tick);
                testTimer.Tick += new EventHandler(this.testTimer_Tick);
                testTimer.Start();
                this.startTimer = new System.Windows.Forms.Timer();
                startTimer.Interval = 1;
                startTimer.Tick += new EventHandler(this.startTimer_Tick);
                startTimer.Start();
            }
            else {
                DebugWriteLine("There is no test file.");
            }
        }

        private void testTimer_Tick(object sender, EventArgs e)
        {
            if (_testStarted) return;
            _testStarted = true;
            testTimer.Stop();
            this.OpenVideoFromInput();
        }
        private void startTimer_Tick(object sender, EventArgs e)
        {
            startTimer.Stop();
            if (_started) return;
            _started = true;
            UpdateSize();
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
            UpdateSize();
        }

        void UpdateSize() {
            progressbarMain.Width = this.ClientSize.Width - tbStatus.Width - scrubStatus.Width;
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
            //    tbStatus.Text="About to call DrawFrame("+this.trackbarFrame.Value.ToString()+")...";
            //    DrawFrame(this.trackbarFrame.Value);
            //}
            //else tbStatus.Text="No frames exist.";
            this.GotoFrame(this.trackbarFrame.Value);
            panel1.Invalidate();
        }

        void ShowScrubStatus() {  // a.k.a. ShowFrame, UpdateFrame
            // FIXME: remove useless calls after calling functions that start threads (This is called from tbStatus_TextChanged now)
            string frame = String.Format("{0}/{1}", rotocanvasNow.FrameNumber, rotocanvasNow.FrameCount);
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
            // ^ recent versions of C# only (older versions may have MyDocuments)
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
            this.trackbarFrame.Minimum=rotocanvasNow.FirstFrameNumber;
            this.trackbarFrame.Maximum=rotocanvasNow.LastFrameNumber;
            Debug.WriteLine("trackbarFrame.Minimum=={0}", trackbarFrame.Minimum);
            Debug.WriteLine("trackbarFrame.Minimum=={0}", trackbarFrame.Minimum);
            Debug.WriteLine("trackbarFrame.Maximum=={0}", trackbarFrame.Maximum);

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
            //    riFrame.DrawAs(bmpBack,bmpBack.PixelFormat);
            //}
            if (rotocanvasNow.riFrame!=null) {
                System.Drawing.Size size = new Size(rotocanvasNow.riFrame.Width, rotocanvasNow.riFrame.Height);
                Debug.WriteLine("Panel1Paint rotocanvasNow.FrameNumber={0}; size: {1}",
                    rotocanvasNow.FrameNumber, size);
                if (panel1.Width!=rotocanvasNow.riFrame.Width) panel1.Width=rotocanvasNow.riFrame.Width;
                if (panel1.Height!=rotocanvasNow.riFrame.Height) panel1.Height=rotocanvasNow.riFrame.Height;
                rotocanvasNow.DrawFrame(callbackNow);
//                for (int y=0; y<rotocanvasNow.riFrame.Height; y++) {
//                    for (int x=0; x<rotocanvasNow.riFrame.Width; x++) {
//                        rotocanvasNow.bmpBack.SetPixel(x,y,rotocanvasNow.riFrame.GetPixel(x,y));
//                    }
//                }
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
            this.trackbarFrame.Minimum=rotocanvasNow.FirstFrameNumber;
            Debug.WriteLine("rotocanvasNow.FirstFrameNumber: {0}", rotocanvasNow.FirstFrameNumber);
            this.trackbarFrame.Maximum=rotocanvasNow.LastFrameNumber;
            Debug.WriteLine("rotocanvasNow.LastFrameNumber: {0}", rotocanvasNow.LastFrameNumber);
            int frame = rotocanvasNow.FrameNumber;
            if (frame < 0) {
                frame = 0;
            }
            this.trackbarFrame.Value = frame;
            DebugWriteLine(String.Format("GotoFrame {0}...", frame));
            this.GotoFrame(frame);
            if (rotocanvasNow.FrameNumber != frame) {
                // throw new ApplicationException("failed");  // TODO: allow async instead.
                Debug.WriteLine("Warning: frame is still {0} not {1} (async or failed)", rotocanvasNow.FrameNumber, frame);
            }
            this.panel1.Invalidate();
        }

        bool GotoFrame(int frame)
        {
            if (!rotocanvasNow.GotoFrame(frame, RConvert.ToInt(this.nudMinDigits.Value), callbackNow))
            {
                return false;
            }
            ShowScrubStatus();  // show actual frame rotocanvas could obtain, rather than the tried one
            // scrubStatus.Text = String.Format("{0}/{1}", rotocanvasNow.FrameNumber, rotocanvasNow.FrameCount);
            return true;
        }

        void OpenVideoFromInput() {
            this.OpenVideo(tbInput.Text);
        }
        void Panel1Click(object sender, EventArgs e)
        {

        }

        private void tbStatus_Click(object sender, EventArgs e)
        {

        }

        private void tbStatus_TextChanged(object sender, EventArgs e)
        {
            ShowScrubStatus();
        }
    }//end MainForm
}//end namespace
