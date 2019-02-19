using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace autoMouseMovement
{
    public partial class Form1 : Form
    {
        //set a timer
     //   private Timer timer;
        public Form1()
        {
            InitializeComponent();

            ForceSystemAwake();
        }      
        private void chkJiggleMouse_CheckStateChanged(object sender, EventArgs e)
        {
            

            if (chkJiggleMouse.Checked)
            {

                ForceSystemAwake();

            }

            else
            {

                ResetSystemDefault();
            }


        }
   

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Check to set your computer to stay awake to prevent screen savers and lock", "Info");
        }

        public static void ForceSystemAwake()
        {
            NativeMethods.SetThreadExecutionState(NativeMethods.EXECUTION_STATE.ES_CONTINUOUS |
                                                  NativeMethods.EXECUTION_STATE.ES_DISPLAY_REQUIRED |
                                                  NativeMethods.EXECUTION_STATE.ES_SYSTEM_REQUIRED |
                                                  NativeMethods.EXECUTION_STATE.ES_AWAYMODE_REQUIRED);
        }

        public static void ResetSystemDefault()
        {
            NativeMethods.SetThreadExecutionState(NativeMethods.EXECUTION_STATE.ES_CONTINUOUS);
        }

        internal static partial class NativeMethods
        {
            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

            [FlagsAttribute]
            public enum EXECUTION_STATE : uint
            {
                ES_AWAYMODE_REQUIRED = 0x00000040,
                ES_CONTINUOUS = 0x80000000,
                ES_DISPLAY_REQUIRED = 0x00000002,
                ES_SYSTEM_REQUIRED = 0x00000001

                // Legacy flag, should not be used.
                // ES_USER_PRESENT = 0x00000004
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ResetSystemDefault();
        }
    }
}
