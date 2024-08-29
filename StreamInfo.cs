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

namespace ExpertMultimedia
{
    /// <summary>
    /// Description of StreamInfo.
    /// </summary>
    public class StreamInfo
    {
        private string fps = null;
        public List<string> StreamFlags = new List<string>();
        public Dictionary<string, string> StreamMeta = new Dictionary<string, string>();
        public int Index { get; set; }
        public int SubIndex { get; set; }
        public string SubIndexName { get; set; }
        public string StreamType { get; set; }
        public string Codec { get; set; }
        public string Resolution { get; set; }
        public List<int> SAR = new List<int>();
        public List<int> DAR = new List<int>();
        public string DataRate { get; set; }
        public string Fps
        {
            get { return fps; }
            set
            {
                if (fps == null)
                {
                    fps = value;
                }
            }
        }
        public string Tbr { get; set; }
        public string Tbn { get; set; }
        public string Tbc { get; set; }
    }
}
