using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace QLTK_Nro_Pro
{
    public partial class Form1 : Form
    {
        public static string string_0 = Path.Combine(Path.GetTempPath(), "koi occtiu957", "mod 222");
        public static string string_1 = Path.Combine(string_0, "data");
        public static bool bool_0;
        public static string string_4 = Path.Combine(Application.StartupPath, "Dragonboy_vn_v222.exe");

        public Form1()
        {
            InitializeComponent();
        }
        private int indexSTT = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            docFile();
            
        }
        private void docFile()
        {
            if (!File.Exists(string_1))
            {
                File.Create(string_1).Close();
            }
            try
            {
                indexSTT = dataGridView1.RowCount;
                string[] a = File.ReadAllLines(string_1);
                for (int i = 0; i < a.Length; i++)
                {
                    string[] b = a[i].Split('|');

                    dataGridView1.Rows.Add(new object[]{
                    b[0],
                   b[1],
                   b[2],
                    b[3],
                    b[4],
                });
                    indexSTT++;

                }
            }
            catch { }
        }
        public static string smethod_2(string str, string key)
        {
            byte[] array = Convert.FromBase64String(str);
            byte[] key2 = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(key));
            byte[] bytes = new TripleDESCryptoServiceProvider
            {
                Key = key2,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            }.CreateDecryptor().TransformFinalBlock(array, 0, array.Length);
            return Encoding.UTF8.GetString(bytes);
        }
        public void ghifile(DataGridView gridView)
        {
            if(gridView.RowCount > 0)
            {
                String Text = "";
                for (int i = 0; i < gridView.Rows.Count; i++)
                {
                    Text += i + "|" +
                            gridView.Rows[i].Cells[1].Value.ToString() + "|" +
                            gridView.Rows[i].Cells[2].Value.ToString() + "|" +
                            gridView.Rows[i].Cells[3].Value.ToString() + "|" +
                            gridView.Rows[i].Cells[4].Value.ToString();
                    if (i != gridView.Rows.Count - 1)
                    {
                        Text += '\n';
                    }
                }
                File.WriteAllText(string_1, Text);
            }
        }

        public string Reserver(int x)
        {
            if (x == 13)
            {
                return "Võ đài liên vũ trụ[13]";
            }
            if (x == 14)
            {
                return "Universe1 (14)";
            }
            if (x == 15)
            {
                return "Naga [15]";
            }
            if (x == 16)
            {
                return "Super 1 [16]";
            }
            if (x == 17)
            {
                return "Super 2 [17]";
            }
            if (x == 18)
            {
                return "13 [18]";
            }
            if (x == 19)
            {
                return "VIP 2 [19]";
            }
            if (x == 20)
            {
                return "14 [20]";
            }
            return x.ToString();
        }
        public int server (string x)
        {
            if (x.Equals("Võ đài liên vũ trụ [13]"))
            {
                return 13;
            }
            if (x.Equals("Universe1 (14)"))
            {
                return 14;
            }
            if (x.Equals("Naga [15]"))
            {
                return 15;
            }
            if (x.Equals("Super 1 [16]"))
            {
                return 16;
            }
            if (x.Equals("Super 2 [17]"))
            {
                return 17;
            }
            if (x.Equals("13 [18]"))
            {
                return 18;
            }
            if (x.Equals("VIP 2 [19]"))
            {
                return 19;
            }
            if (x.Equals("14 [20]"))
            {
                return 20;
            }
            return int.Parse(x);
        }
        private void btn_them_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_user.Text) || string.IsNullOrEmpty(txt_server.Text) || string.IsNullOrEmpty(txt_password.Text))
            {
                MessageBox.Show("Nhập đầy đủ vào rồi thêm cục cưng ", "Thông báo",MessageBoxButtons.OK);
                txt_user.Focus();
                return;
            }
            dataGridView1.Rows.Add(new object[]
            {
                indexSTT,
                    txt_user.Text,
                    server(txt_server.Text),
                    smethod_1(txt_password.Text, "ud"),
                    txt_note.Text,
                    
                });
            indexSTT++;
            ghifile(dataGridView1);
            txt_user.Clear();
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {

            if (dataGridView1.CurrentCell != null && dataGridView1.RowCount > 1)
            {
                int index = dataGridView1.CurrentCell.RowIndex;
                dataGridView1.Rows.RemoveAt(index);
                ghifile(dataGridView1);
            }
            else if (dataGridView1.RowCount == 1)
            {
                dataGridView1.Rows.Clear();
                File.WriteAllText(string_1, string.Empty);
            }

            docFile();
        }


        public static string smethod_1(string string_5, string string_6)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(string_5);
            byte[] key = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(string_6));
            byte[] array = new TripleDESCryptoServiceProvider
            {
                Key = key,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            }.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
            return Convert.ToBase64String(array, 0, array.Length);
        }
        private static List<IntPtr> gameWindows = new List<IntPtr>();
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        public void SortWindows()
        {
            int num = 0;  // Vị trí cột
            int num1 = 0; // Vị trí dòng

            foreach (IntPtr hWnd in gameWindows)
            {
                if (hWnd != IntPtr.Zero)
                {
                    // Lấy kích thước gốc của cửa sổ
                    if (GetWindowRect(hWnd, out RECT rect))
                    {
                        int width = rect.Right - rect.Left;
                        int height = rect.Bottom - rect.Top;

                        int x = width * num;
                        if (x + width > Screen.PrimaryScreen.Bounds.Width)
                        {
                            x = 0;
                            num = 0;
                            num1 += height;
                        }

                        MoveWindow(hWnd, x, num1, width, height, true);
                        num++;
                    }
                }
            }
        }

        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }


        [DllImport("user32.dll")]
        private static extern bool SetWindowText(IntPtr hWnd, string windowName);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int SizeW, int SizeH, bool Repaint);
        public static void smethod_3(int int_0)
        {
            if (!bool_0)
            {
                bool_0 = true;
                Process.Start(arguments: File.ReadAllLines(string_1)[int_0 - 1], fileName: string_4);
                Thread.Sleep(200);
                IntPtr hWnd = FindWindow(null, "Dragonboy222");


                if (hWnd != IntPtr.Zero)
                {
                    SetWindowText(hWnd, "ID: " + (int_0 -1).ToString()+ " Cuong Le");
                    gameWindows.Add(hWnd);
                }
                bool_0 = false;
            }
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    int index = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    Thread thread = new Thread((ThreadStart)delegate
                    {
                        smethod_3(index +1);
                    });
                    thread.IsBackground = true;
                    thread.Start();
                    Thread.Sleep(1200);
                    

                }
                catch
                {
                }

            }
        }

        private void check_CheckedChanged(object sender, EventArgs e)
        {
            if (check.Checked)
            {
                txt_password.PasswordChar = '\0';
            }
            if (check.Checked == false)
            {
                txt_password.PasswordChar = '*';
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_user.Text) || string.IsNullOrEmpty(txt_server.Text) || string.IsNullOrEmpty(txt_password.Text))
            {
                MessageBox.Show("Nhập đầy đủ vào rồi thêm cục cưng ", "Thông báo", MessageBoxButtons.OK);
                return;
            }

                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value = txt_user.Text;
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value = smethod_1(txt_password.Text, "ud");
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value = server(txt_server.Text);
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value = txt_note.Text;
            ghifile(dataGridView1);

                MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK);
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
                txt_user.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt_server.Text = Reserver(int.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()));
            txt_password.Text = smethod_2(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString(),"ud");
            txt_note.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btn_dong_Click(object sender, EventArgs e)
        {

            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Dragonboy_vn_v222");
            if (process.Length == 0)
            {
                MessageBox.Show("Đã tắt hết toàn bộ Tab game rồi mà :(", "Cường có điều muốn nói", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            foreach (Process item in process)
            {
                item.Kill();

            }
        }

       
        private void btn_sapXep_Click(object sender, EventArgs e)
        {
            SortWindows();
        }
    }

    
}