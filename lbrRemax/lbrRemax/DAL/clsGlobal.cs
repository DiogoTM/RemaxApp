using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;

namespace lbrRemax.DAL
{
    public class clsGlobal
    {
        public static DataSet mySet;
        public static OleDbConnection myCon;
        public static OleDbDataAdapter adpEmp, adpProp, adpClient, adpPhoto, adpSales;       

        public static void addPhoto(PictureBox picBox)
        {
            string imgpath;
            string savedPhoto;
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.FilterIndex = 1;
            dlg.Multiselect = false;
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.bmp) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.bmp";
            dlg.ShowDialog();

            imgpath = dlg.FileName;

            if (imgpath.Length != 0)
            {
                picBox.Image = System.Drawing.Image.FromFile(imgpath);
                if (!File.Exists(@"../../Pictures/" + Path.GetFileName(imgpath)))
                {
                    File.Copy(imgpath, @"../../Pictures/" + Path.GetFileName(imgpath));
                }
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(Form.ActiveForm, "Please select a valid image format.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                picBox.ImageLocation = null;
            }
            savedPhoto = @"../../Pictures/" + Path.GetFileName(imgpath);
            picBox.Tag = savedPhoto;
        }
        public static void updateTb(DataTable myTb, OleDbDataAdapter myAdp)
        {
            OleDbCommandBuilder myBuilder = new OleDbCommandBuilder(myAdp);
            myAdp.Update(myTb);
        }

      
    }

}
