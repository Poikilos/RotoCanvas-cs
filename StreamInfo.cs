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
        public int Index = 0;
        public int SubIndex = 0;
        public string SubIndexName = null;
        public string StreamType = null;
        public string Codec = null;
        public string Resolution = null;
        public int Width = 0;
        public int Height = 0;
        public List<int> SAR = new List<int>();
        public List<int> DAR = new List<int>();
        public string DataRate= null;
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
        public string Tbr = null;
        public string Tbn = null;
        public string Tbc = null;
    }
}
