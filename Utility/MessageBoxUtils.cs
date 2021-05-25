using System;
using System.Windows.Forms;

namespace IPScanner.Utility
{
    class MessageBoxUtils
    {
        public static void Information(string message, string title = "สำเร็จ")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Warning(string message, string title = "แจ้งเตือน")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Error(string message, string title = "ผิดพลาด")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static Boolean Question(string message, string title = "ยืนยัน")
        {
            if (MessageBox.Show(message, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Boolean WarningQuestion(string message, string title = "ยืนยันการแจ้งเตือน")
        {
            if (MessageBox.Show(message, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
