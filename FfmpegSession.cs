/*
 * Created by SharpDevelop.
 * User: Owner
 * Date: 8/26/2024
 * Time: 11:47 PM
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;  // List
using System.Diagnostics; // Process, ProcessStartInfo
using System.Text.RegularExpressions;  // Regex, RegexOptions

namespace ExpertMultimedia
{
    /// <summary>
    /// Description of FfmpegSession.
    /// </summary>
    public class FfmpegSession
    {
        public FfmpegSession()
        {
        }

        /// <summary>
        /// Gets detailed information about the video file, including duration and stream details.
        /// </summary>
        /// <param name="filename">The path to the video file.</param>
        /// <returns>A VideoInfo object containing information about the video.</returns>
        public static VideoInfo GetVideoInfo(string filename)
        {
            var videoInfo = new VideoInfo();

            try
            {
                // Initialize the process to run ffmpeg
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "ffmpeg",
                        Arguments = String.Format("-i \"{0}\"", filename),
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                // Start the process
                process.Start();

                // Read the standard error output
                string output = process.StandardError.ReadToEnd();
                process.WaitForExit();

                // Split output into lines
                var lines = output.Split('\n');
                bool isInStream = false;
                bool isInMetadata = false;
                StreamInfo currentStreamInfo = null;

                foreach (string line in lines)
                {
                    string trimmedLine = line.Trim();

                    if (string.IsNullOrEmpty(trimmedLine))
                        continue;

                    // Check for duration
                    var durationMatch = Regex.Match(trimmedLine, @"Duration:\s*(\d+):(\d+):(\d+)\.(\d+)", RegexOptions.IgnoreCase);
                    if (durationMatch.Success)
                    {
                        int hours = int.Parse(durationMatch.Groups[1].Value);
                        int minutes = int.Parse(durationMatch.Groups[2].Value);
                        int seconds = int.Parse(durationMatch.Groups[3].Value);
                        int milliseconds = int.Parse(durationMatch.Groups[4].Value);

                        videoInfo.Hours = hours;
                        Debug.WriteLine(String.Format("Hours={0}", hours));
                        videoInfo.Minutes = minutes;
                        Debug.WriteLine(String.Format("Minutes={0}", minutes));
                        videoInfo.Seconds = seconds;
                        Debug.WriteLine(String.Format("Seconds={0}", seconds));
                        videoInfo.Milliseconds = milliseconds;
                        Debug.WriteLine(String.Format("Milliseconds={0}", milliseconds));
                        videoInfo.TotalSeconds = hours * 3600 + minutes * 60 + seconds + milliseconds / 100.0;
                        Debug.WriteLine(String.Format("TotalSeconds={0}", videoInfo.TotalSeconds));
                        continue;
                    }

                    // Check for stream info
                    var streamMatch = Regex.Match(trimmedLine, @"Stream #(\d+):(\d+)", RegexOptions.IgnoreCase);
                    if (streamMatch.Success)
                    {
                        isInStream = true;
                        isInMetadata = false;

                        // Create a new StreamInfo object
                        currentStreamInfo = new StreamInfo
                        {
                            Index = int.Parse(streamMatch.Groups[1].Value),
                            SubIndex = int.Parse(streamMatch.Groups[2].Value),
                            StreamFlags = new List<string>(),
                            StreamMeta = new Dictionary<string, string>()
                        };

                        videoInfo.Streams.Add(currentStreamInfo);

                        var streamDetails = trimmedLine.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < streamDetails.Length; i++)
                        {
                            string detail = streamDetails[i].Trim();
                            // Add details to StreamFlags
                            currentStreamInfo.StreamFlags.Add(detail);
                            var resolutionMatch = Regex.Match(detail, @"(\d+)x(\d+)(?:\s*\[SAR\s(\d+):(\d+)\sDAR\s(\d+):(\d+)\])?", RegexOptions.IgnoreCase);
                            if (detail.Contains("Video"))
                                currentStreamInfo.StreamType = "Video";
                                // such as "Stream #0:0[0x1](und): Video: h264 (Main) (avc1 / 0x31637661)"
                            else if (detail.Contains("Audio"))
                                currentStreamInfo.StreamType = "Audio";
                            // such as "Stream #0:1[0x2](und): Audio: aac (LC) (mp4a / 0x6134706D)"
                            else if (resolutionMatch.Success)
                            {
                                currentStreamInfo.Resolution = String.Format("{0}x{1}", resolutionMatch.Groups[1].Value, resolutionMatch.Groups[2].Value);
                                currentStreamInfo.Width = int.Parse(resolutionMatch.Groups[1].Value);
                                currentStreamInfo.Height = int.Parse(resolutionMatch.Groups[2].Value);
                                // Extract SAR
                                if (resolutionMatch.Groups[3].Success && resolutionMatch.Groups[4].Success)
                                {
                                    currentStreamInfo.SAR = new List<int>();
                                    currentStreamInfo.SAR.Add(int.Parse(resolutionMatch.Groups[3].Value));
                                    currentStreamInfo.SAR.Add(int.Parse(resolutionMatch.Groups[4].Value));
                                }

                                // Extract DAR
                                if (resolutionMatch.Groups[5].Success && resolutionMatch.Groups[6].Success)
                                {
                                    currentStreamInfo.SAR = new List<int>();
                                    currentStreamInfo.DAR.Add(int.Parse(resolutionMatch.Groups[5].Value));
                                    currentStreamInfo.DAR.Add(int.Parse(resolutionMatch.Groups[6].Value));
                                }
                            }
                            // Extract codec, resolution, SAR, DAR, data rate, fps, tbr, tbn, tbc
                            else if (detail.Contains("fps"))
                                currentStreamInfo.Fps = detail.Split(' ')[0];
                            else if (detail.Contains("tbr"))
                                currentStreamInfo.Tbr = detail.Split(' ')[0];
                            else if (detail.Contains("tbn"))
                                currentStreamInfo.Tbn = detail.Split(' ')[0];
                            else if (detail.Contains("tbc"))
                                currentStreamInfo.Tbc = detail.Split(' ')[0];
                            else if (detail.Contains("kb/s") || detail.Contains("kbit/s"))
                                currentStreamInfo.DataRate = detail;
                        }
                        continue;
                    }

                    // Check for metadata info
                    if (trimmedLine.StartsWith("Metadata:"))
                    {
                        isInMetadata = true;
                        isInStream = false;
                        continue;
                    }

                    if (isInMetadata && currentStreamInfo != null)
                    {
                        // Process metadata
                        var metadataParts = trimmedLine.Split(new[] { ':' }, 2);
                        if (metadataParts.Length == 2)
                        {
                            var key = metadataParts[0].Trim();
                            var value = metadataParts[1].Trim();
                            currentStreamInfo.StreamMeta[key] = value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Error: {0}", ex.Message));
            }

            return videoInfo;
        }

        private static List<int> ExtractNumbers(string detail, string prefix)
        {
            var numbers = new List<int>();
            var match = Regex.Match(detail, $@"{prefix}\s(\d+):(\d+)");
            if (match.Success)
            {
                int num1, num2;
                if (int.TryParse(match.Groups[1].Value, out num1))
                    numbers.Add(num1);
                if (int.TryParse(match.Groups[2].Value, out num2))
                    numbers.Add(num2);
            }
            return numbers;
        }
    }
}
