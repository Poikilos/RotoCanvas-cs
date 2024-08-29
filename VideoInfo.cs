/*
 * Created by SharpDevelop.
 * User: Owner
 * Date: 8/26/2024
 * Time: 11:46 PM
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;  // List
using System.Diagnostics;  // Debug

namespace ExpertMultimedia
{
    /// <summary>
    /// Description of VideoInfo.
    /// </summary>
    public class VideoInfo
    {
        public int Hours = 0;
        public int Minutes = 0;
        public int Seconds = 0;
        public int Milliseconds = 0;
        public double TotalSeconds = 0;
        public string Fps = null;
        public List<StreamInfo> Streams = new List<StreamInfo>();
        public int SelectedStreamIndex = -1;

        public StreamInfo SelectedStream
        {
            get
            {
                if (SelectedStreamIndex == -1)
                {
                    // Find and return the first video stream
                    // return Streams.FirstOrDefault(stream => stream.StreamType == "Video");
                    // ^ FirstOrDefault returns null on no match (if using a reference type such as object in this case)
                    //   but is invalid syntax (object does not contain a definition for FirstOrDefault) in .NET 3.5?
                    foreach (var stream in Streams)
                    {
                        if (stream.StreamType == "Video")
                        {
                            return stream;
                        }
                    }
                    return null;
                }
                else
                {
                    // Return the stream at the selected index
                    if (SelectedStreamIndex >= 0 && SelectedStreamIndex < Streams.Count)
                    {
                        return Streams[SelectedStreamIndex];
                    }
                }
                // Return null if no matching stream is found
                return null;
            }
        }

        public static int FrameCount(string fps, double seconds)
        {
            if (fps == null)
                return -1;
            int i = (int)Math.Floor(Convert.ToDouble(fps) * seconds);
            Debug.WriteLine(String.Format("FrameCount: return {0}", i));
            return i;
        }
        public int FrameCount() {
            StreamInfo stream = this.SelectedStream;
            if (stream == null)
                return -1;
            return VideoInfo.FrameCount(stream.Fps, TotalSeconds);
        }
    }
}
