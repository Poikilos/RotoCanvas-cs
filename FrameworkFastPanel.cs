/*
 * Created by SharpDevelop.
 * User: Owner
 * Date: 4/24/2009
 * Time: 4:25 PM
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ExpertMultimedia
{
    /// <summary>
    /// Description of FrameworkFastPanel.
    /// </summary>
    public partial class FrameworkFastPanel : Control
    {
        const Graphics NO_BUFFER_GRAPHICS = null;
        const Bitmap NO_BACK_BUFFER = null;
        const BufferedGraphics NO_MANAGED_BACK_BUFFER = null;

        Bitmap BackBuffer;
        Graphics BufferGraphics;
        BufferedGraphicsContext GraphicManager;
        BufferedGraphics ManagedBackBuffer;

        DoubleBufferMethod _PaintMethod = DoubleBufferMethod.NoDoubleBuffer;
        GraphicTestMethods _GraphicTest = GraphicTestMethods.DrawTest;

        public FrameworkFastPanel()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            SetStyle( ControlStyles.AllPaintingInWmPaint|ControlStyles.UserPaint, true);
            Application.ApplicationExit += new EventHandler(MemoryCleanup);
        }
    }
}
