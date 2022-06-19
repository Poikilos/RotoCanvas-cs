/*
 * Created by SharpDevelop.
 * User: Owner
 * Date: 6/8/2013
 * Time: 7:52 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;

namespace ExpertMultimedia {
	/// <summary>
	/// Description of RotoCanvas.
	/// </summary>
	public class RotoCanvas {
		#region variables
		private string OpenedFile_FullName="";
		private string OpenedFile_Ext="";
		private string OpenedFile_FullBaseName="";
		private int OpenedFile_MinDigits=0;
		private int iFirstIndex=0;
		private int iLastIndex=1;
		private Bitmap bmpFFMPEG=null;
		private int iFrame=-2;
		private int FrameLastDrawn=-1;
		public static string tempsDir = System.IO.Path.GetTempPath();
		private string myTempDir=null;
		private string tempFramePath = null;
		
		public RImage riFrame=null;
		public RImage riInterface=null;
		public bool bCancel=false;
		public Bitmap bmpBack=new Bitmap(640,480,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
		public static string sCommand_ffmpeg = @"C:\PortableApps\winbuilds\bin\ffmpeg.exe"; // @"E:\progs\video\gui4ffmpeg\ffmpeg.exe";
		#endregion variables
		
		
		#region constructors
		public RotoCanvas() {
			this.myTempDir = Path.Combine(RotoCanvas.tempsDir, "RotoCanvas");
			if (!Directory.Exists(this.myTempDir)) {
				Directory.CreateDirectory(this.myTempDir);
			}
			this.tempFramePath = Path.Combine(myTempDir, "tempframe.jpg");
		}
		#endregion constructors
		
		#region get and set
		public int get_FirstIndex() {
			return iFirstIndex;
		}
		public int get_LastIndex() {
			return iLastIndex;
		}
		public int get_CurrentIndex() {
			return iFrame;
		}
		#endregion get and set
		
		#region file open and close
		public void OpenVideo(string sFilein, int iMinDigits, RCallback callbackNow) {//RString.StartsWith(FunctionName,"RotoCanvas.OpenVideo")) {
			OpenedFile_MinDigits=iMinDigits;//shouldn't matter with video
			int iDot=RString.LastIndexOf(sFilein,'.');
			if (iDot>0) {
				OpenedFile_FullBaseName=RString.SafeSubstring(sFilein,0,iDot);
				OpenedFile_Ext=RString.SafeSubstring(sFilein,iDot+1);
				iFirstIndex=0;
				//if (OpenedFile_FullBaseName.Contains("RebelAssaultIX0a(raw,1996version,fixed-beginning-version)")) this.trackbarFrame.Maximum=7307;//TODO: detect this
				//else {
					iLastIndex=9999;
				//}
				iFrame=0;
				GotoFrame(iFrame,iMinDigits,callbackNow);
			}
			else {
				OpenedFile_FullBaseName="";
				OpenedFile_Ext="";
			}
		}//end OpenVideo
		
		public void OpenSeq(string sFilein, int iMinDigits, RCallback callbackNow) { //if (RString.StartsWith(FunctionName,"RotoCanvas.OpenSeq")) {
			//callbackNow.SetStatus(MainForm.sMyName+": "+FrameworkFormInputValue);
			OpenedFile_MinDigits=iMinDigits;
			int iFrameStart=0;
			callbackNow.WriteLine("splitting name...");
			OpenedFile_FullBaseName=RString.SeqFrameToBaseName(out iFrameStart, out OpenedFile_Ext, sFilein);
			int iDigitsMin=4;
			int iFrameNow=iFrameStart;
			callbackNow.WriteLine("counting image files...");
			while (File.Exists(OpenedFile_FullBaseName+(iFrameNow+1).ToString("D"+iDigitsMin.ToString())+"."+OpenedFile_Ext)) {
				callbackNow.UpdateStatus("found frame "+OpenedFile_FullBaseName+(iFrameNow+1).ToString("D"+iDigitsMin.ToString())+"."+OpenedFile_Ext);
				iFrameNow++;
			}
			iLastIndex=iFrameNow;
			iFirstIndex=iFrameStart;
			iFrame=iFrameStart;
			callbackNow.WriteLine("drawing frame...");
			GotoFrame(iFrame,iMinDigits,callbackNow);
			callbackNow.UpdateStatus("OpenSeq...OK (frames:"+(iLastIndex-iFirstIndex+1).ToString()+")");
		}//end OpenSeq
		#endregion file open and close

		public void MJPEGToSeq(string sFilein, string sDestFolder, RCallback callbackNow) {// (RString.StartsWith(FunctionName,"RotoCanvas.MJPEGToSeq")) {
			callbackNow.WriteLine("MJPEGToSeq...");
			if (sDestFolder==null) sDestFolder="";
			sDestFolder=sDestFolder.Trim();
			//int DestFolderParamIndex=RString.IndexOf(ParamNameStrings,"DestFolder");
			
			string SourceFile_FullName=sFilein;
			//string sDestFolder="";
			//if (InputParamIndex>-1) {//(RString.SafeLength(FrameworkFormInputValue)>0) {
			//	SourceFile_FullName=ParamValueStrings[InputParamIndex];
			//}
			if (string.IsNullOrEmpty(sDestFolder)||!Directory.Exists(sDestFolder)) //if (DestFolderParamIndex>-1) sDestFolder=ParamValueStrings[DestFolderParamIndex];
			//else 
			{
				callbackNow.WriteLine("Using alternate folder since folder does not exist:"+sDestFolder);
				sDestFolder=SourceFile_FullName+"_frames";//use filename as folder name
			}
			
			callbackNow.WriteLine("Checking source...");
			if (SourceFile_FullName!=null&&SourceFile_FullName!=""&&File.Exists(SourceFile_FullName)) {
				callbackNow.WriteLine("Checking destination...");
				DirectoryInfo diDest=null;
				FileInfo fiSrc=new FileInfo(SourceFile_FullName);
				if (!Directory.Exists(sDestFolder)) diDest=Directory.CreateDirectory(sDestFolder);
				else diDest=new DirectoryInfo(sDestFolder);
				sDestFolder=diDest.FullName;
				string DestBase_Name=fiSrc.Name;
				int iDot=RString.LastIndexOf(DestBase_Name,'.');
				if (iDot>-1) {
					DestBase_Name=RString.SafeSubstring(DestBase_Name,0,iDot);
				}
				string DestBase_FullName=sDestFolder+RString.sDirSep+DestBase_Name; //SourceFile_FullName;
				callbackNow.WriteLine("using DestBase_FullName "+RReporting.StringMessage(DestBase_FullName,true)+"...");
				int iFrame=0;
				bool bFoundFrame=true;
				//i.e.: ffmpeg -i "E:\Videos\Projects\Demo Reel\Rebel Assault IX\RebelAssaultIX0a(raw,1996version,fixed-beginning-version).avi" -vframes 1 -r 3352 -f mjpeg "E:\Videos\Projects\Demo Reel\Rebel Assault IX\RebelAssaultIX0a(raw,1996version,fixed-beginning-version)_ffmpeg3352.jpg"
				bCancel=false;
				while (bFoundFrame) {
					string DestFrameNow_FullName=DestBase_FullName+iFrame.ToString("D4")+".jpg";
					
					SaveFrame(DestFrameNow_FullName,SourceFile_FullName,iFrame,callbackNow);
					if (File.Exists(DestFrameNow_FullName)) {
						FileInfo fiResult=new FileInfo(DestFrameNow_FullName);
						if (fiResult.Length<=8) {
							bFoundFrame=false;
							callbackNow.WriteLine("End of data found before frame "+iFrame.ToString()+" (length was "+fiResult.Length.ToString()+")");
						}
					}
					else {
						callbackNow.WriteLine("End of data found before frame "+iFrame.ToString()+" (no frame)");
						bFoundFrame=false;
					}
					//if (MainForm.bClosing) {
					//	callbackNow.WriteLine("Cancelled operation--user is exiting.");
					//	break;
					//}
					if (bCancel) {
						callbackNow.WriteLine("Operation was cancelled by user.");
						break;
					}
					iFrame++;
				}//end while bFoundFrame
			}
			else {
				callbackNow.WriteLine("File does not exist: "+RReporting.StringMessage(sDestFolder,true));
			}
		}//end MJPEGToSeq  //end if RString.StartsWith(FunctionName,"RotoCanvas.MJPEGToSeq")
		
		bool SaveFrame(string DestFrameNow_FullName, string SourceFile_FullName, int iFrame, RCallback callbackNow) {
			bool bGood=false;
			decimal dFPS=29.97m;
			int iFPS=30;
			bool bDropFrame=true;
			string sTimeCode="";
			System.Diagnostics.ProcessStartInfo psi=null;
			//TODO: check frame accuracy (do automatic image comparison of a whole MJPEG animation [compare to MediaStudio png sequence output]?)
			//"+iFrame.ToString()+"
			//ffmpeg -i swing.avi -s 320×240 -vframes 1 -f singlejpeg swing.jpg
			//ffmpeg -i swing.avi -s 320×240 -vframes 1 -f mjpeg swing.jpg
			//-ss 0:0:20 gets frame at a time signature
			//THIS GETS ALL FRAMES USING ONE CALL TO FFMPEG:
			//ffmpeg -i input.dv -r 25 -f image2 images%05d.png
			//MY VERSION:
			//ffmpeg "-i \""+SourceFile_FullName+"\" -r 29.97 -f mjpeg \""+DestBase_FullName+"%05d.jpg\""
			//%05d is for 5 digits
			try {
				psi=new System.Diagnostics.ProcessStartInfo(sCommand_ffmpeg);
				psi.RedirectStandardOutput = true;
				psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
				psi.UseShellExecute = false;
				//TODO:? -s 640x480 sets size
				//TODO:? ask user for framerate?
				//-r 30 forces 30fps
				//-y overwrites destination
				sTimeCode=RConvert.FrameToHMSDotMs(iFrame,iFPS,true);
				psi.Arguments="-ss "+sTimeCode+" -i \""+SourceFile_FullName+"\" -r "+dFPS.ToString("#.#")+" -vframes 1 -f mjpeg -y \""+DestFrameNow_FullName+"\"";//"-i \""+SourceFile_FullName+"\" -vframes 1 -f mjpeg \""+sDestFolder+RString.sDirSep+sFileBaseName+iFrame.ToString("D4")+".jpg\""; //D4 is for decimal system and four digits
				//Environment.CurrentDirectory=sDestFolder;
				//psi.Arguments="-i \""+SourceFile_FullName+"\" -r 29.97 -f mjpeg "+DestBase_Name+"%05d.jpg";//"-i \""+SourceFile_FullName+"\" -vframes 1 -f mjpeg \""+sDestFolder+RString.sDirSep+sFileBaseName+iFrame.ToString("D4")+".jpg\""; //D4 is for decimal system and four digits
				
				System.Diagnostics.Process procFFMPEG=null;
				callbackNow.WriteLine("SaveFrame Running Command ("+iFrame.ToString()+"="+sTimeCode+") >"+sCommand_ffmpeg+" "+psi.Arguments);
				procFFMPEG = System.Diagnostics.Process.Start(psi);
				System.IO.StreamReader myOutput = procFFMPEG.StandardOutput;
				procFFMPEG.WaitForExit();
				//if (procFFMPEG.HasExited) {
					//string output = myOutput.ReadToEnd();
					//string sLine="";
					//int iCursor=0;
					//while ( (sLine=RString.StringReadLine(output,ref iCursor)) != null ) {
					//	callbackNow.WriteLine(sLine);
					//}
				//}
				if (DestFrameNow_FullName!=null&&DestFrameNow_FullName!=""&&File.Exists(DestFrameNow_FullName)
					&&(new FileInfo(DestFrameNow_FullName)).Length>0) {
					bGood=true;
				}
			}
			catch (Exception exn) {
				callbackNow.WriteLine("SaveFrame could not finish "+"saving frame "+RReporting.StringMessage(DestFrameNow_FullName,true));//ShowExn(exn,"saving frame "+RReporting.StringMessage(DestFrameNow_FullName,true),"RotoCanvas SaveFrame");
			}
			return bGood;
		}//end SaveFrame
		
		Bitmap GetVideoFrameAsBitmap(string SourceFile_FullName, int iFrame, RCallback callbackNow) {
			Bitmap bmpReturn=null;
			bool bGood=false;
			decimal dFPS=29.97m;
			int iFPS=30;
			bool bDropFrame=true;
			string sTimeCode="";
			
			// TODO: Test via image comparison of a whole MJPEG animation [output from sequential frame dump command].
			//   THIS GETS ALL FRAMES USING ONE CALL TO FFMPEG:
			//   ffmpeg -i input.dv -r 25 -f image2 images%05d.png
			
			//"+iFrame.ToString()+"
			//ffmpeg -i swing.avi -s 320×240 -vframes 1 -f singlejpeg swing.jpg
			//ffmpeg -i swing.avi -s 320×240 -vframes 1 -f mjpeg swing.jpg
			//-ss 0:0:20 gets frame at a time signature
			//MY VERSION:
			//ffmpeg "-i \""+SourceFile_FullName+"\" -r 29.97 -f mjpeg \""+DestBase_FullName+"%05d.jpg\""
			//%05d is for 5 digits
			
			string inputFile = SourceFile_FullName;
			string outputFile = tempFramePath;
			// See <https://stackoverflow.com/a/50697975>:
		
			if (sCommand_ffmpeg.Contains(char.ToString(Path.DirectorySeparatorChar))) {
				// It is a full path rather using the system path, so fail if not present.
				if (!File.Exists(sCommand_ffmpeg)) {
					callbackNow.UpdateStatus("Error: \""+sCommand_ffmpeg+"\" does not exist.");
					return null;
				}
			}
			System.Diagnostics.Process procFFMPEG = new System.Diagnostics.Process();
			procFFMPEG.StartInfo.FileName = sCommand_ffmpeg;
			// procFFMPEG.StartInfo.RedirectStandardInput = true;
			procFFMPEG.StartInfo.UseShellExecute=false; 
			procFFMPEG.StartInfo.RedirectStandardOutput=true;
			procFFMPEG.StartInfo.RedirectStandardError=true;
			procFFMPEG.StartInfo.CreateNoWindow=true;
			// procFFMPEG.StartInfo.WindowStyle=System.Diagnostics.ProcessWindowStyle.Hidden;// psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

			procFFMPEG.EnableRaisingEvents = true;
			//TODO:? -s 640x480 sets size
			//TODO:? ask user for framerate?
			//-r 30 forces 30fps
			//-y overwrites destination
			sTimeCode = RConvert.FrameToHMSDotMs(iFrame, iFPS, true);
			procFFMPEG.StartInfo.Arguments="-i \""+SourceFile_FullName+"\" -ss "+sTimeCode+" -r "+dFPS.ToString("#.#")+" -nostdin -y -vframes 1 -f mjpeg -";//"-i \""+SourceFile_FullName+"\" -vframes 1 -f mjpeg \""+sDestFolder+RString.sDirSep+sFileBaseName+iFrame.ToString("D4")+".jpg\""; //D4 is for decimal system and four digits
			//Environment.CurrentDirectory=sDestFolder;
			//psi.Arguments="-i \""+SourceFile_FullName+"\" -r 29.97 -f mjpeg "+DestBase_Name+"%05d.jpg";//"-i \""+SourceFile_FullName+"\" -vframes 1 -f mjpeg \""+sDestFolder+RString.sDirSep+sFileBaseName+iFrame.ToString("D4")+".jpg\""; //D4 is for decimal system and four digits
			//ImageConverter ic=new ImageConverter();
			
			procFFMPEG.ErrorDataReceived += (sender, eventArgs) =>
			{
				if (eventArgs.Data != null) {
					// callbackNow.WriteLine(eventArgs.Data);
					System.Diagnostics.Debug.WriteLine(eventArgs.Data);
				}
			};
			
			callbackNow.UpdateStatus("GetVideoFrameAsBitmap Running Command ("+iFrame.ToString()+"="+sTimeCode+") >"+sCommand_ffmpeg+" "+procFFMPEG.StartInfo.Arguments);//+psi.Arguments);
			if (!procFFMPEG.Start()) {
				callbackNow.UpdateStatus("Starting the process failed.");
				return null;
			}
			procFFMPEG.BeginErrorReadLine(); // See ErrorDataReceived handler
			bool embeddedColorMgMt = true;
			bool validateImageData = true;
			/*
			var inputTask = Task.Run(() =>
			{
				using (var input = new FileStream(inputFile, FileMode.Open))
				{
					input.CopyTo(procFFMPEG.StandardInput.BaseStream);
					procFFMPEG.StandardInput.Close();
				}
			});
			
			*/
			// procFFMPEG.StandardInput.Close();
			
			var outputTask = Task.Run(() =>
			{
				using (var output = new FileStream(outputFile, FileMode.Create))
				{
					// procFFMPEG.StandardOutput.BaseStream.CopyTo(output);
					bmpReturn = (Bitmap)Bitmap.FromStream(procFFMPEG.StandardOutput.BaseStream, embeddedColorMgMt, validateImageData);
				}
			});
		   
			Task.WaitAll(
		   		// inputTask,
		   		outputTask
		   	);
		   	callbackNow.WriteLine("waiting for ffmpeg...");
			procFFMPEG.WaitForExit();
			
			
			
			
			callbackNow.WriteLine("getting bitmap stream...");
			// StreamReader streamIn = procFFMPEG.StandardOutput;
			// BinaryReader br=new BinaryReader(streamIn);
			// MemoryStream memsIn=new MemoryStream(byarrData);
			// Image img=Image.FromStream(streamIn);
			callbackNow.WriteLine("getting bitmap...");
			
			/*
			if (procFFMPEG.StandardOutput.BaseStream.Length == 0) {
				callbackNow.WriteLine("Error: The output length is 0");
				return null;
			}
			else if (procFFMPEG.StandardOutput.BaseStream.Position == procFFMPEG.StandardOutput.BaseStream.Length) {
				callbackNow.WriteLine("The output was fully read to a stream.");
				return null;
			}
			elif (procFFMPEG.StandardOutput.BaseStream.Position != 0) {
				callbackNow.WriteLine("The output was partially read to a stream.");
				return null;
			}
			*/
			// ^ "System.NotSupportedException: Stream does not support seeking."
			try {
				/*
				using (StreamWriter sw = new System.IO.StreamWriter(outputFile))
				{
					sw.Write(streamIn.ReadToEnd());
					callbackNow.WriteLine("wrote \""+outputFile+"\"");
				}
				*/

				// Console.Write(streamIn.BaseStream);
				// Console.Out.Flush();
				// bmpReturn = (Bitmap)Bitmap.FromStream(procFFMPEG.StandardOutput.BaseStream, embeddedColorMgMt, validateImageData);
				// procFFMPEG.WaitForExit();
				// procFFMPEG.StandardOutput.Close();
				
			}
			catch (System.ArgumentException ex) {
				callbackNow.WriteLine("The image data from ffmpeg was invalid. See the console output.");
				// Console.Write(streamIn.BaseStream);
				// Console.Out.Flush();
				return null;
			}
			
			
			//bmpReturn=new Bitmap(
			callbackNow.WriteLine("returning bitmap.");
			
			//StreamReader streamInErr=procFFMPEG.StandardError;
			//string sErr=streamInErr.ReadToEnd();
			//int iCursor=0;
			//string sLine;
			//while ( (sLine=RString.StringReadLine(sErr,ref iCursor)) != null ) {
			//	callbackNow.WriteLine(sLine);
			//}
			
			//if (procFFMPEG.HasExited) {
				//string output = streamIn.ReadToEnd();
				//string sLine="";
				//int iCursor=0;
				//while ( (sLine=RString.StringReadLine(output,ref iCursor)) != null ) {
				//	callbackNow.WriteLine(sLine);
				//}
			//}
			//if (DestFrameNow_FullName!=null&&DestFrameNow_FullName!=""&&File.Exists(DestFrameNow_FullName)
			//	&&(new FileInfo(DestFrameNow_FullName)).Length>0) {
			//	bGood=true;
			//}
			/*
			
			catch (Exception exn) {
				callbackNow.WriteLine("could not finish GetFrameBitmap("+iFrame.ToString()+"): "+exn.ToString()); //ShowExn(exn,"saving frame","RotoCanvas GetFrameBitmap("+iFrame.ToString()+")");
			}
			*/
			return bmpReturn;
		}//end GetFrameBitmap

		
		public bool GotoFrame(int iFrameX, int iMinDigits, RCallback callbackNow) {//formerly DrawFrame
			bool bGood=false;
			string sFileNow="";
		
			System.Drawing.Imaging.ImageFormat imgfmt = RImage.ImageFormatFromExt(OpenedFile_Ext);
			if (imgfmt.Guid != Guid.Empty) {//imgfmt!=System.Drawing.Imaging.ImageFormat.MemoryBmp) {//image
				callbackNow.UpdateStatus("Loading frame using image format GUID "+imgfmt.Guid.ToString()+"...");
				sFileNow=OpenedFile_FullBaseName+(iFrameX).ToString("D"+iMinDigits.ToString())+"."+OpenedFile_Ext;
				if (File.Exists(sFileNow)) {
					if (riFrame==null) riFrame=new RImage(sFileNow,4);
					else bGood=riFrame.Load(sFileNow,4);
				}
				//else if (File.Exists(OpenedFile_FullBaseName+"."+OpenedFile_Ext)) {
				//	riFrame.Load(OpenedFile_FullBaseName+"."+OpenedFile_Ext);
				//}
				else {
					callbackNow.WriteLine("Unable to load "+RReporting.StringMessage(sFileNow,true));
					return false;
				}
				callbackNow.UpdateStatus("Loading frame using image format GUID...OK");
			}
			else { //else NOT an image, so try ffmpeg
				if (File.Exists(tempFramePath)) File.Delete(tempFramePath);
				
				//Next 2 lines check whether frame exists!  //TODO: make it into a separate method
				// this.SaveFrame(tempFramePath, OpenedFile_FullBaseName+"."+OpenedFile_Ext, iFrameX);
				// bGood=File.Exists(tempFramePath) && ((new FileInfo(tempFramePath)).Length>0);
				// callbackNow.WriteLine("Looking for frame output..."+(bGood?"OK":"FAILED"));
				bGood = true; 
				if (bGood) {
					callbackNow.UpdateStatus("GotoFrame "+iFrame.ToString()+" (FFMPEG)...");
					string framePrefix = OpenedFile_FullBaseName+"."+OpenedFile_Ext;
					bmpFFMPEG = GetVideoFrameAsBitmap(framePrefix, iFrameX, callbackNow);
					if (bmpFFMPEG == null) {
						// Status should already be set by GetVideoFrameAsBitmap in case of an error.
						// callbackNow.UpdateStatus("Error: \"" + framePrefix + "\" frame " + iFrameX + " couldn't be loaded.");
						return false;
					}
					callbackNow.UpdateStatus("GotoFrame...done load image from frame "+iFrame.ToString());
					// bmpFFMPEG=new Bitmap(streamIn);
					RReporting.iDebugLevel = RReporting.DebugLevel_Max;
					RReporting.sParticiple = "calling riFrame.Load";
					if (riFrame == null) riFrame = new RImage(sFileNow,4);
					bGood = riFrame.Load(bmpFFMPEG, 4);
					try {
						bmpFFMPEG.Dispose();
						bmpFFMPEG = null;
					}
					catch {}
					
					//riFrame.Load(tempFramePath, 4);
					/*
					if (riFrame.Width>0&&riFrame.Height>0) {
						if (bmpBack.Width!=riFrame.Width||bmpBack.Height!=riFrame.Height) {
							bmpBack=new Bitmap(riFrame.Width,riFrame.Height,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
						}
					}
					else callbackNow.UpdateStatus("Could not process frame format {Width:"+riFrame.Width+";"+"Height:"+riFrame.Height+"}");
					*/
				}
				//else this.tbStatus.Text="Could not find frame "+iFrame.ToString();
				callbackNow.UpdateStatus("Loading frame using FFMPEG..."+(bGood?"OK":"FAILED"));
			}
			
			/*
			catch (Exception exn) {
				callbackNow.UpdateStatus("DrawFrame could not finish: "+exn.ToString());
				//ShowExn(exn, "drawing frame", "RotoCanvas DrawFrame");
			}
			*/
			return bGood;
		}//end GotoFrame
		
		public void ReloadFrameBitmap(RCallback callbackNow) {
			riFrame.DrawAs(bmpBack,bmpBack.PixelFormat);  //, callbackNow);
			callbackNow.WriteLine("ReloadFrameBitmap {iFrame:"+iFrame.ToString()+"}");
			FrameLastDrawn=iFrame;
		}
		
		public void DrawFrame(RCallback callbackNow) {
			if (riFrame.Width>0&&riFrame.Height>0) {
				if (bmpBack.Width!=riFrame.Width||bmpBack.Height!=riFrame.Height) {
					bmpBack=new Bitmap(riFrame.Width,riFrame.Height,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
					ReloadFrameBitmap(callbackNow);
				}
			}
			else callbackNow.UpdateStatus("Could not process frame format {Width:"+riFrame.Width+";"+"Height:"+riFrame.Height+"}");
			if (FrameLastDrawn!=iFrame) {
				callbackNow.UpdateStatus("Reloading frame "+iFrame+" (was "+FrameLastDrawn.ToString()+")");
				ReloadFrameBitmap(callbackNow);
			}
			callbackNow.UpdateStatus("Frame "+iFrame.ToString()+" drawn");
		}
	}//end RotoCanvas
}//end RotoCanvas
