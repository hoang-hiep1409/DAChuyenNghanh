using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DACNQuanLyKTX.Models;

namespace DACNQuanLyKTX
{
    public partial class FormQLKTX : Form
    {
        private string username, ten;
        FormDangNhap dangnhap = new FormDangNhap();
        ModelQLKTX db = new ModelQLKTX();
        public FormQLKTX()
        {
            InitializeComponent();
            CustomizeDesing();
            HideTab();
            
        }

        public FormQLKTX(String user, FormDangNhap fdangnhap)
        {
            this.username = user;
            this.ten = db.QuanLies.Find(user).HoTenQL;
            this.dangnhap = fdangnhap;
            InitializeComponent();
            CustomizeDesing();
            HideTab();
        }
      

        private void KhoiTao_DDK()
        {
            cboPhong_DKM.Items.Clear();
            txtMSSV_DKM.Text = "";
            txtHoTen_DKM.Text = "";
            txtCMND_DKM.Text = "";
            txtSDT_DKM.Text = "";
            txtEmail_DKM.Text = "";
            rbNam_DKM.Checked = true;
            dtpNgaySinh_DKM.Value = DateTime.Now;
            dtpNgayVao_DKM.Value = DateTime.Now;
            for (int i = 1; i <= 4; i++)
            {
                cboThoiGian_DKM.Items.Add(i);
            }
            cboThoiGian_DKM.SelectedIndex = 0;
            List<Phong> dsPhong = db.Phongs.ToList();
            foreach (var x in dsPhong)
            {
                cboPhong_DKM.Items.Add(x);
            }
            cboPhong_DKM.SelectedIndex = 0;
        }

        private void KhoiTao_ThemPhong()
        {
            cboLoaiPhong_ThemPhong.Items.Clear();
            cboKhu_ThemPhong.Items.Clear();
            txtMaPhong_ThemPhong.Text = "";
            txtTenPhong_ThemPhong.Text = "";
            List<LoaiPhong> dsLoaiPhong = db.LoaiPhongs.ToList();
            foreach (LoaiPhong x in dsLoaiPhong)
            {
                cboLoaiPhong_ThemPhong.Items.Add(x);
            }
            cboLoaiPhong_ThemPhong.SelectedIndex = 0;
            List<Khu> dsKhu = db.Khus.ToList();
            foreach (Khu x in dsKhu)
            {
                cboKhu_ThemPhong.Items.Add(x);
            }
            cboKhu_ThemPhong.SelectedIndex = 0;
        }

        private void KhoiTao_DSPhong()
        {

            cboLoaiPhong_DSPhong.Items.Clear();
            cboKhu_DSPhong.Items.Clear();
            txtMaPhong_DSPhong.Text = "";
            List<Phong> dsPhong = db.Phongs.ToList();
            List<LoaiPhong> dsLoaiPhong = db.LoaiPhongs.ToList();
            foreach (var x in dsLoaiPhong)
            {
                cboLoaiPhong_DSPhong.Items.Add(x);
            }

            List<Khu> dsKhu = db.Khus.ToList();
            foreach (var x in dsKhu)
            {
                cboKhu_DSPhong.Items.Add(x);
            }
            HienThiPhong_DSPhong(dsPhong);
        }

        private void KhoiTao_DSSV()
        {
            txtMSSV_DSSV.Text = "";
            cboPhong_DSSV.Items.Clear();
            List<Phong> dsPhong = db.Phongs.ToList();
            List<SinhVien> dsSV = db.SinhViens.ToList();
            foreach (var x in dsPhong)
            {
                cboPhong_DSSV.Items.Add(x);
            }
            HienThi_DSSV(dsSV);
        }

        private void KhoiTao_DSDDK()
        {
            List<DonDangKy> dsDDK = db.DonDangKies.ToList();
            txtMaQL_DSDDK.Text = "";
            txtTenQL_DSDDK.Text = "";
            HienThi_DSDDK(dsDDK);
        }

        private void HienThi_DSDDK(List<DonDangKy> dsDDK)
        {
            dataDSDDK.Rows.Clear();
            foreach(var x in dsDDK)
            {
                dataDSDDK.Rows.Add(new object[] {
                    x.MaDonDangKy,
                    x.MaQL,
                    x.MSSV,
                    x.NgayVao.ToShortDateString(),
                    x.ThoiGian,
                    x.NgayLamDon.ToShortDateString()});
            }    
        }

        private void HienThi_DSSV(List<SinhVien> dsSV)
        {
            dataDSSV.Rows.Clear();
            List<Phong> dsPhong = db.Phongs.ToList();                     
            foreach (var x in dsSV)
            {
                dataDSSV.Rows.Add(new object[] {
                    x.MSSV,
                    x.HoTenSV,
                    x.GioiTinh?"Nam":"Nữ",
                    x.NgaySinh.ToShortDateString(),
                    x.CMND,
                    x.SDT,
                    x.DiaChi,
                    dsPhong.Find(a=>a.MaPhong==x.MaPhong).TenPhong});
            }
            
        }

        private void HienThiPhong_DSPhong(List<Phong> dsPhong)
        {
            dataPhong.Rows.Clear();
            List<LoaiPhong> dsLoaiPhong = db.LoaiPhongs.ToList();
            List<Khu> dsKhu = db.Khus.ToList();
            DataTable dt = new DataTable();
            foreach (var x in dsPhong)
            {
                dataPhong.Rows.Add(new object[] {
                    x.MaPhong,
                    x.TenPhong,
                    x.SoGiuongDaO, 
                    x.SoGiuongTrong, 
                    dsLoaiPhong.Find(a=>a.MaLoaiPhong==x.MaLoaiPhong).TenLoai,
                    dsKhu.Find(a=>a.MaKhu==x.MaKhu).TenKhu, 
                    x.MaQL, 
                    x.MaTruongPhong });
            }
        }

        private void CustomizeDesing()
        {
            panelSubDonDangKy.Visible = false;
            panelSubHoaDon.Visible = false;
            panelSubPhong.Visible = false;
            indicatorSubPanelDDK.Visible = false;
            indicatorSubPanelHD.Visible = false;
            indicatorSubPanelPhong.Visible = false;
            label1.Text = ten;

        }
        
        private void HideTab()
        {
            tabControlKTX.Appearance = TabAppearance.FlatButtons;
            tabControlKTX.ItemSize = new Size(0, 1);
            tabControlKTX.SizeMode = TabSizeMode.Fixed;
            tabControlKTX.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControlKTX.Region = new Region(new RectangleF(tpThongKe.Left, tpThongKe.Top, tpThongKe.Width, tpThongKe.Height));
            foreach (TabPage tab in tabControlKTX.TabPages)
            {
                tab.Text = "";
            }
        }

        private void MoveIndicator(Control control)
        {
            indicatorSubPanelDDK.Visible = false;
            indicatorSubPanelHD.Visible = false;
            indicatorSubPanelPhong.Visible = false;
            indicator.Visible = true;
            indicator.Top = control.Top;
            indicator.Height = control.Height;
        }

        private void MoveIndicatorSubPanelDDK(Control control)
        {
            indicatorSubPanelDDK.Visible = true;
            indicatorSubPanelHD.Visible = false;
            indicatorSubPanelPhong.Visible = false;
            indicator.Visible = false;
            indicatorSubPanelDDK.Top = control.Top;
            indicatorSubPanelDDK.Height = control.Height;
        }

        private void MoveIndicatorSubPanelHD(Control control)
        {
            indicatorSubPanelDDK.Visible = false;
            indicatorSubPanelHD.Visible = true;
            indicatorSubPanelPhong.Visible = false;
            indicator.Visible = false;
            indicatorSubPanelHD.Top = control.Top;
            indicatorSubPanelHD.Height = control.Height;
        }

        private void MoveIndicatorSubPanelPhong(Control control)
        {
            indicatorSubPanelDDK.Visible = false;
            indicatorSubPanelHD.Visible = false;
            indicatorSubPanelPhong.Visible = true;
            indicator.Visible = false;
            indicatorSubPanelPhong.Top = control.Top;
            indicatorSubPanelPhong.Height = control.Height;
        }


        private void btnDonDangKy_Click(object sender, EventArgs e)
        {
            MoveIndicator((Control)sender);
            if (panelSubDonDangKy.Visible == false)
            {
                panelSubDonDangKy.Visible = true;
                btnDonDangKy.Image = DACNQuanLyKTX.Properties.Resources.icons8_collapse_arrow_16;
            }
            else
            {
                panelSubDonDangKy.Visible = false;
                btnDonDangKy.Image = DACNQuanLyKTX.Properties.Resources.icons8_expand_arrow_16;
            }

        }

        
        private void btnThongKe(object sender, EventArgs e)
        {
            MoveIndicator((Control)sender);
            tabControlKTX.SelectedTab = tpThongKe;
        }

        

        private void btnDangKyMoi(object sender, EventArgs e)
        {
            MoveIndicatorSubPanelDDK((Control)sender);
            tabControlKTX.SelectedTab = tpDangKyMoi;
            KhoiTao_DDK();
        }

        private void btnDanhSachDon(object sender, EventArgs e)
        {
            MoveIndicatorSubPanelDDK((Control)sender);
            tabControlKTX.SelectedTab = tpDanhSachDon;
            KhoiTao_DSDDK();
        }

        private void btnSinhVien(object sender, EventArgs e)
        {
            MoveIndicator((Control)sender);
            tabControlKTX.SelectedTab = tpDSSV;
            KhoiTao_DSSV();
        }

        

        private void btnThemPhong_Click(object sender, EventArgs e)
        {
            MoveIndicatorSubPanelPhong((Control)sender);
            tabControlKTX.SelectedTab = tpThemPhong;
            KhoiTao_ThemPhong();
        }
        


        private void btnDSPhong_Click(object sender, EventArgs e)
        {
            MoveIndicatorSubPanelPhong((Control)sender);
            tabControlKTX.SelectedTab = tpDanhSachPhong;
            KhoiTao_DSPhong();
        }

        private void btnPhong_Click(object sender, EventArgs e)
        {
            MoveIndicator((Control)sender);
            if (panelSubPhong.Visible == false)
            {
                panelSubPhong.Visible = true;
                btnPhong.Image = DACNQuanLyKTX.Properties.Resources.icons8_collapse_arrow_16;

            }
            else
            {
                panelSubPhong.Visible = false;
                btnPhong.Image = DACNQuanLyKTX.Properties.Resources.icons8_collapse_arrow_16;
            }
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            MoveIndicator((Control)sender);
            if (panelSubHoaDon.Visible == false)
            {
                panelSubHoaDon.Visible = true;
                btnHoaDon.Image = DACNQuanLyKTX.Properties.Resources.icons8_collapse_arrow_16;
            }
            else
            {
                panelSubHoaDon.Visible = false;
                btnHoaDon.Image = DACNQuanLyKTX.Properties.Resources.icons8_expand_arrow_16;
            }
        }

        private void btnHoaDonPhong_Click(object sender, EventArgs e)
        {
            MoveIndicatorSubPanelHD((Control)sender);
        }

        private void btnHoaDonDien_Click(object sender, EventArgs e)
        {
            MoveIndicatorSubPanelHD((Control)sender);
        }

        private void btnThem_ThemPhong_Click(object sender, EventArgs e)
        {
            if (txtTenPhong_ThemPhong.Text == "" || txtMaPhong_ThemPhong.Text == "")
            {
                DialogResult result = MessageBox.Show("Không được để trống", "Không thêm được", MessageBoxButtons.OK);
            }else
            {
                Phong p = new Phong();
                p.MaPhong = txtMaPhong_ThemPhong.Text;
                p.TenPhong = txtTenPhong_ThemPhong.Text;
                p.MaKhu = ((Khu)cboKhu_ThemPhong.SelectedItem).MaKhu;
                p.MaLoaiPhong = ((LoaiPhong) cboLoaiPhong_ThemPhong.SelectedItem).MaLoaiPhong;
                p.SoGiuongDaO = 0;
                p.SoGiuongTrong = ((LoaiPhong)cboLoaiPhong_ThemPhong.SelectedItem).TongSoGiuong;
                p.MaQL = "QL01";
                
                db.Phongs.Add(p);
                
                db.SaveChanges();
                KhoiTao_ThemPhong();
            }    
        }

        private void btnDangKy_DKM_Click(object sender, EventArgs e)
        {
            if (txtHoTen_DKM.Text == "" || txtMSSV_DKM.Text == "" || txtCMND_DKM.Text == "" || txtEmail_DKM.Text == "" || txtSDT_DKM.Text == "") 
            {
                DialogResult result = MessageBox.Show("Không được để trống", "Không thêm được", MessageBoxButtons.OK);
            }
            else
            {
                //Thêm sinh viên
                List<SinhVien> dsSV = db.SinhViens.ToList();
                SinhVien sv = dsSV.Find(a => a.MSSV == txtMSSV_DKM.Text);
                if (!(dsSV.Contains(sv)))
                {
                    sv = new SinhVien();
                    sv.MSSV = txtMSSV_DKM.Text;
                    sv.HoTenSV = txtHoTen_DKM.Text;
                    if (rbNam_DKM.Checked)
                        sv.GioiTinh = true;
                    else
                        sv.GioiTinh = false;
                    sv.NgaySinh = dtpNgaySinh_DKM.Value;
                    sv.SDT = txtSDT_DKM.Text;
                    sv.CMND = txtCMND_DKM.Text;
                    sv.DiaChi = txtEmail_DKM.Text;
                    sv.MaPhong = ((Phong)cboPhong_DKM.SelectedItem).MaPhong;                   
                    db.SinhViens.Add(sv);
                }    
                //Thêm đơn đăng ký mới
                DonDangKy donDK = new DonDangKy();
                donDK.MSSV = txtMSSV_DKM.Text;
                donDK.MaDonDangKy = "DDK" + db.DonDangKies.Count();
                donDK.MaQL = "QL01";
                donDK.MSSV = sv.MSSV;
                donDK.NgayVao = dtpNgayVao_DKM.Value;
                donDK.ThoiGian = (int)cboThoiGian_DKM.SelectedItem;
                donDK.NgayLamDon = DateTime.Now;
                db.DonDangKies.Add(donDK);
                db.SaveChanges();
                KhoiTao_DDK();
            }
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            FormDangNhap dn = new FormDangNhap();           
            dn.ShowDialog();
            this.Hide();
            FormDangNhap f = new FormDangNhap();
            f.Closed += (s, args) => this.Close();
            f.Show();
        }
    }
}
