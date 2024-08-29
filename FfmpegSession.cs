/*
 * Created by SharpDevelop.
 * User: Owner
 * Date: 8/26/2024
 * Time: 11:47 PM
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
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

                // Regex patterns
                var durationRegex = new Regex(@"Duration:\s*(\d+):(\d+):(\d+)\.(\d+)", RegexOptions.IgnoreCase);
                var streamRegex = new Regex(@"Stream #(\d+):(\d+).*:.*", RegexOptions.IgnoreCase);
                var metadataRegex = new Regex(@"Metadata:\s*(.*)", RegexOptions.IgnoreCase);

                // Extract duration
                var durationMatch = durationRegex.Match(output);
                if (durationMatch.Success)
                {
                    int hours = int.Parse(durationMatch.Groups[1].Value);
                    int minutes = int.Parse(durationMatch.Groups[2].Value);
                    int seconds = int.Parse(durationMatch.Groups[3].Value);
                    int milliseconds = int.Parse(durationMatch.Groups[4].Value);

                    videoInfo.Hours = hours;
                    videoInfo.Minutes = minutes;
                    videoInfo.Seconds = seconds;
                    videoInfo.Milliseconds = milliseconds;
                    videoInfo.TotalSeconds = hours * 3600 + minutes * 60 + seconds + milliseconds / 100.0;
                }

                // Process stream info
                MatchCollection streamMatches = streamRegex.Matches(output);
                var lines = output.Split('\n');

                foreach (Match match in streamMatches)
                {
                    StreamInfo streamInfo = new StreamInfo();
                    int index = int.Parse(match.Groups[1].Value);
                    int subIndex = int.Parse(match.Groups[2].Value);
                    streamInfo.Index = index;
                    streamInfo.SubIndex = subIndex;

                    // Extract stream details
                    int streamStartLine = output.IndexOf(match.Value);
                    string streamLine = lines[streamStartLine];
                    var streamDetails = streamLine.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var detail in streamDetails)
                    {
                        var trimmedDetail = detail.Trim();
                        streamInfo.StreamFlags.Add(trimmedDetail);

                        if (trimmedDetail.Contains("fps")) {
                            streamInfo.Fps = trimmedDetail.Split(' ')[0];
                        }
                        if (trimmedDetail.Contains("tbr"))
                            streamInfo.Tbr = trimmedDetail.Split(' ')[0];
                        if (trimmedDetail.Contains("tbn"))
                            streamInfo.Tbn = trimmedDetail.Split(' ')[0];
                        if (trimmedDetail.Contains("tbc"))
                            streamInfo.Tbc = trimmedDetail.Split(' ')[0];
                    }

                    if (streamInfo.StreamFlags.Count > 0)
                    {
                        var codec = streamInfo.StreamFlags[0];
                        streamInfo.Codec = codec;
                        if (streamInfo.StreamFlags.Count > 1)
                            streamInfo.Resolution = streamInfo.StreamFlags[1];

                        if (streamInfo.StreamFlags.Count > 2)
                        {
                            var sarDar = streamInfo.StreamFlags[2];
                            var sarDarParts = sarDar.Split(new[] { '[', ']', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (sarDarParts.Length >= 4)
                            {
                                streamInfo.SAR.Add(int.Parse(sarDarParts[0]));
                                streamInfo.SAR.Add(int.Parse(sarDarParts[1]));
                                streamInfo.DAR.Add(int.Parse(sarDarParts[2]));
                                streamInfo.DAR.Add(int.Parse(sarDarParts[3]));
                            }
                        }

                        if (streamInfo.StreamFlags.Count > 3)
                            streamInfo.DataRate = streamInfo.StreamFlags[3];
                    }

                    // Extract metadata if available
                    int metadataStartLine = output.IndexOf("Metadata:");
                    if (metadataStartLine > 0)
                    {
                        string metadataLine = lines[metadataStartLine + 1];
                        var metadataParts = metadataLine.Split(new[] { ':' }, 2);
                        if (metadataParts.Length == 2)
                        {
                            var key = metadataParts[0].Trim();
                            var value = metadataParts[1].Trim();
                            streamInfo.StreamMeta[key] = value;
                        }
                    }

                    videoInfo.Streams.Add(streamInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Error: {0}", ex.Message));
            }

            return videoInfo;
        }
    }
}
