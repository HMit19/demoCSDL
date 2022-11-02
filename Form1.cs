using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace DemoCSDL
{
    public partial class Form1 : Form
    {
        string connectString = @"Data Source=DESKTOP-80GA5O2\SQLEXPRESS;Initial Catalog=QLBanGiay;Integrated Security=True";
        SqlConnection connect = null;
        Dictionary<string, Item> itemView = null;
        Dictionary<string, Item> itemFunction = null;
        Dictionary<string, Item> itemProcedure = null;
        int option = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            string[] view = "Option view,View 1,View 2,View 3,View 4,View 5,View 6,View 7,View 8".Split(',');
            cbSQL.DataSource = view;
            panelResult.Hide();
            option = 1;
        }
        private void fillData(string query)
        {
            try
            {
                if (connect == null)
                {
                    connect = new SqlConnection(connectString);
                }
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }
                SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                int rows = dataGridView1.Rows.Count;
                rows = rows > 0 ? rows - 1 : 0;
                lblResult.Text = "Result: ( " + rows + " rows affected)";
                lblStatus.Text = "Completion time: " + DateTime.Now.ToString();
            }
            catch (Exception)
            {
                lblResult.Text = "Error!";
                lblStatus.Text = "Completion time: " + DateTime.Now.ToString();
                MessageBox.Show("Error!", "Query", MessageBoxButtons.OK);
            }
        }

        private void cbSQL_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "";
            if (option == 1)
            {
                switch (cbSQL.SelectedItem)
                {
                    case "View 1":
                        query = "select * from cau1";
                        break;
                    case "View 2":
                        query = "select * from cau2";

                        break;
                    case "View 3":
                        query = "select * from cau3";

                        break;
                    case "View 4":
                        query = "select * from cau4";

                        break;
                    case "View 5":
                        query = "select * from cau5";

                        break;
                    case "View 6":
                        query = "select * from cau6";

                        break;
                    case "View 7":
                        query = "select * from cau7";

                        break;
                    case "View 8":
                        query = "select * from cau8";
                        break;
                    default:
                        query = "Option";
                        break;
                }
                if (query != "Option")
                {
                    txtQuery.Text = query;
                    lblSQL.Text = "Query:  " + itemView[(cbSQL.SelectedItem).ToString()].Sql;
                    lblQuestion.Text = cbSQL.SelectedItem + ":   " + itemView[(cbSQL.SelectedItem).ToString()].Question;
                    fillData(query);
                }
            }
            else if (option == 2)
            {
                switch (cbSQL.SelectedItem)
                {
                    case "Function 1":
                        query = "select * from causo1(2022)";
                        break;
                    case "Function 2":
                        query = "select * from causo2('SP03')";

                        break;
                    case "Function 3":
                        query = "select * from causo3 (N'HDB01')";

                        break;
                    case "Function 4":
                        query = "select * from causo4 (N'NCC01')";

                        break;
                    case "Function 5":
                        query = "SELECT * FROM causo5 ('2022')";

                        break;
                    case "Function 6":
                        query = "SELECT * FROM causo6 ('2022', '09')";

                        break;
                    default:
                        query = "Option";
                        break;
                }
                if (query != "Option")
                {
                    txtQuery.Text = query;
                    lblSQL.Text = "Query:  " + itemFunction[(cbSQL.SelectedItem).ToString()].Sql;
                    lblQuestion.Text = cbSQL.SelectedItem + ":   " + itemFunction[(cbSQL.SelectedItem).ToString()].Question;
                    fillData(query);
                }
            }
            else if (option == 3)
            {
                switch (cbSQL.SelectedItem)
                {
                    case "Procedure 1":
                        query = "DECLARE @tong INT \r\nEXEC SP3 N'KH01', @tong OUTPUT\r\nPRINT @tong";
                        break;
                    case "Procedure 2":
                        query = "DECLARE @Sl INT\r\nEXEC SP4 N'Phạm Minh Quân', @Sl OUTPUT\r\nPRINT @Sl";

                        break;
                    case "Procedure 3":
                        query = "DECLARE @tong INT \r\nEXEC SP3 N'KH01', @tong OUTPUT\r\nPRINT @tong";

                        break;
                    case "Procedure 4":
                        query = "DECLARE @Sl INT\r\nEXEC SP4 N'Phạm Minh Quân', @Sl OUTPUT\r\nPRINT @Sl";

                        break;
                    case "Procedure 5":
                        query = "exec SP5 @a";

                        break;
                    case "Procedure 6":
                        //query = "SP6";

                        break;
                    default:
                        query = "Option";
                        break;
                }
                if (query != "Option")
                {
                    txtQuery.Text = query;
                    lblSQL.Text = "Query:  " + itemProcedure[(cbSQL.SelectedItem).ToString()].Sql;
                    lblQuestion.Text = cbSQL.SelectedItem + ":   " + itemProcedure[(cbSQL.SelectedItem).ToString()].Question;
                    fillDataProcedure(query);
                }
            }
        }

        private void fillDataProcedure(string query)
        {
            // run procedure
            // hien thi gia tri ra label khi dung procedure
        }

        private void btnFunction_Click(object sender, EventArgs e)
        {
            string[] function = "Option function,Function 1,Function 2,Function 3,Function 4,Function 5,Function 6".Split(',');
            cbSQL.DataSource = function;
            panelResult.Hide();
            option = 2;
        }

        private void btnProcedure_Click(object sender, EventArgs e)
        {
            string[] procedure = "Option procedure,Procedure 1,Procedure 2,Procedure 3,Procedure 4,Procedure 5,Procedure 6".Split(',');
            cbSQL.DataSource = procedure;
            panelResult.Show();
            option = 3;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            itemView = new Dictionary<string, Item>();
            itemView.Add("View 1", new Item("tao view dua ra thong tin san pham co size 40",
                "create view cau1 as select MaSP, TenSP,MauSac, SoLuong, DonGiaNhap, DonGiaBan from tSanPham where size = 40"));
            itemView.Add("View 2", new Item("tao view dua ra thong tin san pham hang la nike va nam nhap = 2022",
                "create view cau2\r\nas select sp.MaSP,TenSP,MauSac,Size, SoLuong, DonGiaBan\r\nfrom tSanPham sp join " +
                "tChiTietHDN ctn on sp.MaSP=ctn.MaSP \r\njoin tHoaDonNhap hdn on ctn.MaHDN = hdn.MaHDN\r\njoin tDanhMuc " +
                "dm on sp.MaDM = dm.MaDM\r\nwhere Hang='Nike' and year(NgayNhap)=2022"));
            itemView.Add("View 3", new Item("tao view dua ra thong tin nhan vien la nu ban duoc hang trong ngay 9/9/2022",
                "create view cau3\ras select nv.MaNV, TenNV, NgaySinh, SoDienThoai, CCCD, DiaChi from tNhanVien nv\r\njoin" +
                " tHoaDonBan hdb on nv.MaNV = hdb.MaNV \r\nwhere GioiTinh = N'Nữ' and NgayBan='2022-09-09'"));
            itemView.Add("View 4", new Item("dua ra thong tin 10 khach hang co tong tien mua hang nhieu nhat",
                "create view cau4\ras select top (10) with ties kh.MaKH, TenKH, GioiTinh, DiaChi, DienThoai, TongTien\r\nfrom" +
                " tKhachHang kh\r\njoin tHoaDonBan hdb on kh.MaKH = hdb.MaKH \r\norder by tongtien desc"));
            itemView.Add("View 5", new Item("tao view dua ra thong tin nha cung cap co so luong san pham nhap vao cua hang nhieu nhat",
                "create view cau5\r\nas select top (1) with ties ncc.MaNCC, TenNCC, DiaChi, sum(SLNhap) as TongSLNhap \r\nfrom" +
                " tNhaCungCap ncc join tHoaDonNhap hdn on ncc.MaNCC = hdn.MaNCC\r\njoin tChiTietHDN ctn on hdn.MaHDN=ctn.MaHDN\r\n" +
                "group by ncc.MaNCC, TenNCC, DiaChi\r\norder by TongSLNhap desc"));
            itemView.Add("View 6", new Item("tao view dua ra top 3 san pham duoc ban nhieu nhat",
                "create view cau6\r\nas select top (3) with ties sp.MaSP, TenSP, MauSac, Size, DonGiaBan, sum(SLBan) as TongSLBan\r\n" +
                "from tSanPham sp join tChiTietHDB ctb on sp.MaSP = ctb.MaSP\r\ngroup by sp.MaSP, TenSP, MauSac, Size, DonGiaBan\r\n" +
                "order by TongSLBan desc"));
            itemView.Add("View 7", new Item("tao view dua ra thong tin san pham dung duoc cho tat ca gioi tinh  con hang",
                "create view cau7\ras select sp.MaSP, TenSP, MauSac, Size, DonGiaBan, TinhTrang from\r\n tSanPham sp join " +
                "tDanhMuc dm on sp.MaDM= dm.MaDM\r\n where LoaiSP=N'Unisex' and SoLuong >0"));
            itemView.Add("View 8", new Item("dua ra thong tin khach hang va loai khach hang neu mua tu 2 lan tro len la khach vip, mua duoi 2 lan la khach thuong",
                "create view cau8\r as select kh.MaKH, TenKH, GioiTinh, DiaChi, DienThoai, LoaiKH = case \r\n when count(MaHDB) >=2 then " +
                "'vip'\r else 'normal' end\rfrom tKhachHang kh join tHoaDonBan hdb on kh.MaKH = hdb.MaKH\r\n group by kh.MaKH, TenKH, GioiTinh, DiaChi, DienThoai"));
            itemFunction = new Dictionary<string, Item>();
            itemFunction.Add("Function 1", new Item("tao ham co dau vao la nam nhap hang, dua ra thong tin san pham va so luong san pham nhap trong nam do",
                "CREATE function causo1 (@nam int)\r returns table\r\n as return (\r\n\tselect ctn.MaSP, TenSP, sum(SLNhap) as tongSLN from tChiTietHDN ctn join tHoaDonNhap" +
                " hdn on ctn.MaHDN = hdn.MaHDN\r\n\tjoin tSanPham sp on ctn.MaSP = sp.MaSP\r\n\twhere year(NgayNhap) = @nam\r\n\tgroup by ctn.MaSP,TenSP\r\n )"));
            itemFunction.Add("Function 2", new Item("tao ham co dau vao la ma san pham, dua ra thong tin cua khach hang da mua san pham do", "CREATE function causo2(@masp nvarchar(30))" +
                "\r\nreturns table\r\nas return(\r\n\tselect kh.MaKH, TenKH, GioiTinh, DiaChi, DienThoai from tKhachHang kh\r\n\tjoin tHoaDonBan hdb on kh.MaKH = hdb.MaKH\r\n\tjoin tChiTietHDB" +
                " ctb on hdb.MaHDB = ctb.MaHDB \r\n\twhere MaSP = @masp\r\n)"));
            itemFunction.Add("Function 3", new Item("tạo hàm đầu vào là  mã hóa đơn, đưa ra thông tin khách hàng", "Create function causo3 (@hd nvarchar(10))\r\nReturns table\r\nAs\r\nReturn\r\n" +
                "Select\r\n\tTenKH,\r\n\tDienThoai\r\nFrom tHoaDonBan\r\nJoin tKhachHang On tHoaDonBan.MaKH = tKhachHang.MaKH\r\nWhere MaHDB = @hd"));
            itemFunction.Add("Function 4", new Item("tạo hàm đầu vào là mã nhà cung cấp, đưa ra các mã sản phẩm và số lượng nhập", "Create function causo4 (@ncc nvarchar(10))\r\nReturns table\r\n" +
                "As\r\nReturn\r\nSelect\r\n\tMaSP,\r\n\tSLNhap\r\nFrom tHoaDonNhap\r\nJoin tNhaCungCap On tHoaDonNhap.MaNCC = tNhaCungCap.MaNCC\r\nJoin tChiTietHDN On tHoaDonNhap.MaHDN = tChiTietHDN.MaHDN" +
                "\r\nWhere tNhaCungCap.MaNCC = @ncc"));
            itemFunction.Add("Function 5", new Item("tạo hàm đưa vào năm, đưa ra top sản phẩm bán chạy", "CREATE Function causo5 (@nam int)\r\nRETURNS TABLE\r\nAS\r\nReturn\r\nSelect Top(3)" +
                " WITH TIES\r\n\ttSanPham.MaSP,\r\n\tSum(SLBan) as Doanhso\r\nFrom tChiTietHDB\r\nJoin tSanPham On tSanPham.MaSP = tChiTietHDB.MaSP\r\nJoin tHoaDonBan On tChiTietHDB.MaHDB = tHoaDonBan.MaHDB" +
                "\r\nWhere YEAR(NgayBan) = @nam\r\nGroup By tSanPham.MaSP\r\nOrder BY Doanhso DESC"));
            itemFunction.Add("Function 6", new Item("tạo hàm ra các nhân viên có doanh thu cao nhất tháng", "CREATE function causo6 (@year int, @month int)\r\nRETURNS table\r\nAS\r\nReturn\r\nSelect Top(3) WITH TIES" +
                "\r\n\ttNhanVien.MaNV,\r\n\tSum(SLBan*DonGiaBan) as Doanhthu\r\nFrom tSanPham\r\nJoin tChiTietHDB On tSanPham.MaSP = tChiTietHDB.MaSP\r\nJoin tHoaDonBan On tChiTietHDB.MaHDB = tHoaDonBan.MaHDB\r\nJoin" +
                " tNhanVien On tHoaDonBan.MaNV = tNhanVien.MaNV\r\nWhere MONTH(NgayBan) = @month And YEAR(NgayBan) = @year\r\nGroup By tNhanVien.MaNV\r\nOrder By Doanhthu DESC"));
            itemProcedure = new Dictionary<string, Item>();
            itemProcedure.Add("Procedure 1", new Item("tạo thủ tục xóa các mã sp", "CREATE procedure SP1 @masp nvarchar(10)\r\nAs\r\nBegin\r\n\tDelete tSanPham\r\n\tWhere MaSP = @masp\r\nEND"));
            itemProcedure.Add("Procedure 2", new Item("tạo thủ tục xóa nhân viên đã nghỉ việc", "CREATE procedure SP2 @manv nvarchar (10)\r\nAs\r\nBegin\r\n\tDelete tNhanVien\r\n\tWhere MaNV = @manv\r\nEnd"));
            itemProcedure.Add("Procedure 3", new Item("tạo thủ tục đưa vào mã khách hàng, đưa ra tổng tiền đã mua", "CREATE procedure SP3 @kh nvarchar (30), @tong int output\r\nAs\r\nBegin\r\nSelect\r\n\t@tong = " +
                "SUM(SLBan * DonGiaBan)\r\nFrom tChiTietHDB\r\nJoin tHoaDonBan On tChiTietHDB.MaHDB = tHoaDonBan.MaHDB\r\nJoin tSanPham On tChiTietHDB.MaSP = tSanPham.MaSP\r\nWhere MaKH = @kh\r\nEND"));
            itemProcedure.Add("Procedure 4", new Item("tạo thủ tục đưa vào tên nhân viên, đưa ra số lượng hóa đơn nhân viên đó đã lập", "CREATE Procedure SP4 @ten nvarchar (30), @Sl int output\rAS\t\rBEGIN\rELECT\r\n" +
                "\t@Sl = COUNT(MaHDB)\r\nFrom tNhanVien\r\nJoin tHoaDonBan On tNhanVien.MaNV = tHoaDonBan.MaNV\r\nWhere TenNV = @ten\r\nEND"));
            itemProcedure.Add("Procedure 5", new Item("tạo thủ tục cập nhật giảm giá % sản phẩm", "CREATE procedure SP5 @percent float\t\r\nBegin\r\tDeclare @Tyle DECIMAL (3,2) = 1 - @percent / 100\r\n\tUpdate tSanPham\r" +
                "\n\tSET DonGiaBan = DonGiaBan * @Tyle\r\nEnd"));
            itemProcedure.Add("Procedure 6", new Item("tạo thủ tục đưa vào tháng năm, xuất ra tổng doanh thu", "Create procedure SP6 @month int, @year int, @Doanhthu int OUTPUT\rAs\rBegin\r\nSelect\r\n\t@Doanhthu =  SUM(DonGiaBan*SLBan)" +
                "\r\nFrom tChiTietHDB\r\nJoin tSanPham On tSanPham.MaSP = tChiTietHDB.MaSP\r\nJoin tHoaDonBan On tChiTietHDB.MaHDB = tHoaDonBan.MaHDB\r\nWhere MONTH(NgayBan) = @month And YEAR(NgayBan) = @year\r\nEND"));
            panelResult.Hide();
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            lblSQL.Text = "Query: " + txtQuery.Text;
            fillData(txtQuery.Text);
            panelResult.Hide();
        }
    }
}
