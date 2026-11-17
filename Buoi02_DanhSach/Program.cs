using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TongHopTatCaBaiTap
{
    struct NhanVien
    {
        public string MaNV;
        public string Hoten;
        public int Namsinh;
        public string Phongban;
        public int LuongCB;
        public int Thuong;
    }

    class Node
    {
        public int Data;
        public Node Next;
        public Node(int data)
        {
            Data = data;
            Next = null;
        }
    }

    class Nut
    {
        public int GiaTri;
        public Nut TiepTheo;
        public Nut(int giaTri)
        {
            GiaTri = giaTri;
            TiepTheo = null;
        }
    }

    class WebNode
    {
        public string Url;
        public WebNode Prev, Next;
        public WebNode(string url) { Url = url; }
    }

    class SongNode
    {
        public string TenBaiHat;
        public SongNode Prev, Next;
        public SongNode(string ten) { TenBaiHat = ten; }
    }

    class OrderNode
    {
        public string MaDon;
        public string TrangThai;
        public OrderNode Prev, Next;
        public OrderNode(string ma) { MaDon = ma; TrangThai = "Chờ xử lý"; }

        public void ChuyenTrangThai()
        {
            if (TrangThai == "Chờ xử lý") TrangThai = "Đang giao";
            else if (TrangThai == "Đang giao") TrangThai = "Hoàn thành";
            else Console.WriteLine("Đơn hàng đã hoàn thành trước đó!");
        }
    }

    class Program
    {
        // ==================== BÀI 1 ====================
        static void Bai1()
        {
            Console.Write("Nhap n: ");
            int n = int.Parse(Console.ReadLine());
            int[] a = new int[n];
            for (int i = 0; i < n; i++)
            {
                Console.Write($"a[{i}] = ");
                a[i] = int.Parse(Console.ReadLine());
            }

            for (int i = 0; i < a.Length; i++)
                Console.Write(a[i] + " ");

            Console.Write("\nNhap x: ");
            int x = int.Parse(Console.ReadLine());
            Console.Write("Nhap vi tri k: ");
            int k = int.Parse(Console.ReadLine());

            int[] tmp = new int[a.Length + 1];
            for (int i = 0; i <= k - 1; i++) tmp[i] = a[i];
            tmp[k] = x;
            for (int i = k; i <= a.Length - 1; i++) tmp[i + 1] = a[i];
            a = tmp;

            for (int i = 0; i < a.Length; i++)
                Console.Write(a[i] + " ");
            Console.WriteLine();
        }

        // ==================== BÀI 2 ====================
        static NhanVien[] DS;

        static void NhapDS()
        {
            Console.Write("Nhập số lượng nhân viên: ");
            int n = int.Parse(Console.ReadLine());
            DS = new NhanVien[n];
            for (int i = 0; i < DS.Length; i++)
            {
                Console.WriteLine($"\n--- Nhập nhân viên thứ {i + 1} ---");
                Console.Write("Mã NV: "); DS[i].MaNV = Console.ReadLine();
                Console.Write("Họ và tên: "); DS[i].Hoten = Console.ReadLine();
                Console.Write("Năm sinh: "); DS[i].Namsinh = int.Parse(Console.ReadLine());
                Console.Write("Phòng ban: "); DS[i].Phongban = Console.ReadLine();
                Console.Write("Lương cơ bản: "); DS[i].LuongCB = int.Parse(Console.ReadLine());
                Console.Write("Thưởng: "); DS[i].Thuong = int.Parse(Console.ReadLine());
            }
        }

        static void InNV(NhanVien nv)
        {
            Console.WriteLine($"{nv.MaNV,-8} | {nv.Hoten,-20} | {nv.Namsinh,-6} | {nv.Phongban,-12} | LCB: {nv.LuongCB,-10} | Thưởng: {nv.Thuong,-10} | Thực lĩnh: {nv.LuongCB + nv.Thuong}");
        }

        static int thuclinh(int x)
        {
            return DS[x].LuongCB + DS[x].Thuong;
        }

        static long TongThucLinh()
        {
            long tong = 0;
            for (int i = 0; i < DS.Length; i++)
                tong += thuclinh(i);
            return tong;
        }

        static void InLuongCBThapNhat()
        {
            int min = int.MaxValue;
            for (int i = 0; i < DS.Length; i++)
            {
                if (min > DS[i].LuongCB)
                    min = DS[i].LuongCB;
            }
            Console.WriteLine($"\nb. Nhân viên có lương cơ bản thấp nhất ({min}):");
            for (int i = 0; i < DS.Length; i++)
            {
                if (DS[i].LuongCB == min)
                    InNV(DS[i]);
            }
        }

        static int DemThuong()
        {
            int dem = 0;
            for (int i = 0; i < DS.Length; i++)
            {
                if (DS[i].Thuong >= 1200000)
                    dem++;
            }
            return dem;
        }

        static void SapXepVaIn()
        {
            for (int i = 0; i < DS.Length - 1; i++)
            {
                for (int j = i + 1; j < DS.Length; j++)
                {
                    int cmpPB = string.Compare(DS[i].Phongban, DS[j].Phongban, StringComparison.OrdinalIgnoreCase);
                    bool canDoiCho = (cmpPB > 0) || (cmpPB == 0 && string.Compare(DS[i].MaNV, DS[j].MaNV, StringComparison.OrdinalIgnoreCase) < 0);
                    if (canDoiCho)
                    {
                        NhanVien tmp = DS[i];
                        DS[i] = DS[j];
                        DS[j] = tmp;
                    }
                }
            }
            Console.WriteLine("\nd. Danh sách sau khi sắp xếp (Phòng ban tăng dần, Mã NV giảm dần):");
            for (int i = 0; i < DS.Length; i++)
                InNV(DS[i]);
        }

        static void TangLuong5PhanTram()
        {
            Console.WriteLine("\ne. Danh sách sau khi tăng lương cơ bản lên 5%:");
            for (int i = 0; i < DS.Length; i++)
            {
                DS[i].LuongCB = (int)(DS[i].LuongCB * 1.05);
                InNV(DS[i]);
            }
        }

        static void Bai2()
        {
            NhapDS();
            Console.WriteLine($"\na. Tổng thực lĩnh tháng của công ty: {TongThucLinh()}");
            InLuongCBThapNhat();
            Console.WriteLine($"\nc. Số nhân viên có mức thưởng >= 1.200.000: {DemThuong()}");
            SapXepVaIn();
            TangLuong5PhanTram();
        }

        // ==================== BÀI 3 ====================
        static int[,] NhapMaTran(out int n)
        {
            do
            {
                Console.Write("Nhập cấp ma trận vuông n (0 < n < 100): ");
                n = int.Parse(Console.ReadLine());
            } while (n <= 0 || n >= 100);
            int[,] a = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"a[{i},{j}] = ");
                    a[i, j] = int.Parse(Console.ReadLine());
                }
            }
            return a;
        }

        static void XuatMaTran(int[,] a, int n)
        {
            Console.WriteLine("\nMa trận vừa nhập:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"{a[i, j],5}");
                }
                Console.WriteLine();
            }
        }

        static int TongMaTran(int[,] a, int n)
        {
            int t = 0;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    t += a[i, j];
            return t;
        }

        static int TimDuongNhoNhat(int[,] a, int n)
        {
            int min = int.MaxValue;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (a[i, j] > 0 && a[i, j] < min)
                        min = a[i, j];
            return min;
        }

        static int TongCheoPhu(int[,] a, int n)
        {
            int tong = 0;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (j == (n - 1) - i)
                        tong += a[i, j];
            return tong;
        }

        static bool KiemTraCheoChinhTangDan(int[,] a, int n)
        {
            int[] tg = new int[n];
            for (int i = 0; i < n; i++)
                tg[i] = a[i, i];

            for (int i = 0; i <= n - 2; i++)
                for (int j = i + 1; j <= n - 1; j++)
                    if (tg[i] >= tg[j])
                        return false;
            return true;
        }

        static void Bai3()
        {
            int n;
            int[,] a = NhapMaTran(out n);
            XuatMaTran(a, n);
            Console.WriteLine($"\na. Tổng tất cả phần tử của ma trận: {TongMaTran(a, n)}");
            int minDuong = TimDuongNhoNhat(a, n);
            if (minDuong == int.MaxValue)
                Console.WriteLine("b. Ma trận không có phần tử dương nào.");
            else
                Console.WriteLine($"b. Giá trị dương nhỏ nhất của ma trận: {minDuong}");
            Console.WriteLine($"c. Tổng các phần tử trên đường chéo phụ: {TongCheoPhu(a, n)}");
            bool tangDan = KiemTraCheoChinhTangDan(a, n);
            if (tangDan)
                Console.WriteLine("d. Các phần tử trên đường chéo chính TĂNG DẦN.");
            else
                Console.WriteLine("d. Các phần tử trên đường chéo chính KHÔNG tăng dần.");
        }

        // ==================== BÀI 4 ====================
        static Node pHead = null;

        static void ThemCuoi(int data)
        {
            Node newNode = new Node(data);
            if (pHead == null)
            {
                pHead = newNode;
                return;
            }
            Node p = pHead;
            while (p.Next != null)
                p = p.Next;
            p.Next = newNode;
        }

        static void NhapDanhSach()
        {
            Console.Write("Nhập số lượng phần tử n: ");
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                int val;
                do
                {
                    Console.Write($"Nhập phần tử dương thứ {i + 1}: ");
                    val = int.Parse(Console.ReadLine());
                } while (val <= 0);
                ThemCuoi(val);
            }
        }

        static void XuatDanhSach()
        {
            Node p = pHead;
            while (p != null)
            {
                Console.Write(p.Data + " ");
                p = p.Next;
            }
            Console.WriteLine();
        }

        static int DemSoBangX(int x)
        {
            int d = 0;
            Node p = pHead;
            while (p != null)
            {
                if (p.Data == x) d = d + 1;
                p = p.Next;
            }
            return d;
        }

        static int TimDuongNhoNhat4()
        {
            int minduong = int.MaxValue;
            Node p = pHead;
            while (p != null)
            {
                if (p.Data > 0 && p.Data < minduong)
                    minduong = p.Data;
                p = p.Next;
            }
            return minduong;
        }

        static void XoaPhanTuXDauTien(int x)
        {
            Node p = pHead;
            Node t = null;
            while (p != null && p.Data != x)
            {
                t = p;
                p = p.Next;
            }
            if (p == null)
            {
                Console.WriteLine($"Không tìm thấy phần tử {x} trong danh sách!");
                return;
            }
            if (p == pHead)
                pHead = pHead.Next;
            else
                t.Next = p.Next;
            Console.WriteLine($"Đã xóa phần tử {x} xuất hiện đầu tiên.");
        }

        static void Bai4()
        {
            pHead = null;
            NhapDanhSach();
            Console.Write("\nDanh sách vừa nhập: ");
            XuatDanhSach();

            Console.Write("\nNhập giá trị x cần đếm: ");
            int xDem = int.Parse(Console.ReadLine());
            Console.WriteLine($"a. Số lượng phần tử bằng {xDem} là: {DemSoBangX(xDem)}");

            int minDuong = TimDuongNhoNhat4();
            if (minDuong == int.MaxValue)
                Console.WriteLine("\nb. Danh sách không có phần tử dương.");
            else
                Console.WriteLine($"\nb. Phần tử dương nhỏ nhất trong danh sách là: {minDuong}");

            Console.Write("\nNhập giá trị x cần xóa (lần xuất hiện đầu tiên): ");
            int xXoa = int.Parse(Console.ReadLine());
            XoaPhanTuXDauTien(xXoa);
            Console.Write("Danh sách sau khi xóa: ");
            XuatDanhSach();
        }
        // ==================== BÀI 5 ====================
        static Nut dauDS = null;

        static void ChenCuoi(int giaTri)
        {
            Nut nutMoi = new Nut(giaTri);
            if (dauDS == null)
            {
                dauDS = nutMoi;
                return;
            }
            Nut hienTai = dauDS;
            while (hienTai.TiepTheo != null)
                hienTai = hienTai.TiepTheo;
            hienTai.TiepTheo = nutMoi;
        }

        static void NhapDuLieu()
        {
            Console.Write("Nhập số lượng phần tử: ");
            int soLuong = int.Parse(Console.ReadLine());
            for (int chiSo = 0; chiSo < soLuong; chiSo++)
            {
                Console.Write($"Nhập phần tử thứ {chiSo + 1}: ");
                int soNhap = int.Parse(Console.ReadLine());
                ChenCuoi(soNhap);
            }
        }

        static void HienThiDS()
        {
            Nut hienTai = dauDS;
            while (hienTai != null)
            {
                Console.Write(hienTai.GiaTri + " ");
                hienTai = hienTai.TiepTheo;
            }
            Console.WriteLine();
        }

        static int LayGiaTriLonNhat()
        {
            int lonNhat = int.MinValue;
            Nut hienTai = dauDS;
            while (hienTai != null)
            {
                if (hienTai.GiaTri > lonNhat)
                    lonNhat = hienTai.GiaTri;
                hienTai = hienTai.TiepTheo;
            }
            return lonNhat;
        }

        static int LayTongPhanTu()
        {
            int tongCong = 0;
            Nut hienTai = dauDS;
            while (hienTai != null)
            {
                tongCong += hienTai.GiaTri;
                hienTai = hienTai.TiepTheo;
            }
            return tongCong;
        }

        static bool LaSoNguyenTo(int soCanKiemTra)
        {
            if (soCanKiemTra < 2) return false;
            for (int uoc = 2; uoc <= Math.Sqrt(soCanKiemTra); uoc++)
                if (soCanKiemTra % uoc == 0) return false;
            return true;
        }

        static int DemSoNguyenTo()
        {
            int soLuongNT = 0;
            Nut hienTai = dauDS;
            while (hienTai != null)
            {
                if (LaSoNguyenTo(hienTai.GiaTri))
                    soLuongNT++;
                hienTai = hienTai.TiepTheo;
            }
            return soLuongNT;
        }

        static void ThongKeAmDuongKhong()
        {
            int slAm = 0, slDuong = 0, slKhong = 0;
            Nut hienTai = dauDS;
            while (hienTai != null)
            {
                if (hienTai.GiaTri < 0) slAm++;
                else if (hienTai.GiaTri > 0) slDuong++;
                else slKhong++;
                hienTai = hienTai.TiepTheo;
            }
            Console.WriteLine($"d. Số âm: {slAm} | Số bằng 0: {slKhong} | Số dương: {slDuong}");
        }

        static int DemPhanTuBangX(int giaTriTim)
        {
            int ketQuaDem = 0;
            Nut hienTai = dauDS;
            while (hienTai != null)
            {
                if (hienTai.GiaTri == giaTriTim)
                    ketQuaDem++;
                hienTai = hienTai.TiepTheo;
            }
            return ketQuaDem;
        }

        static int LayDuongNhoNhat()
        {
            int duongNhoNhat = int.MaxValue;
            Nut hienTai = dauDS;
            while (hienTai != null)
            {
                if (hienTai.GiaTri > 0 && hienTai.GiaTri < duongNhoNhat)
                    duongNhoNhat = hienTai.GiaTri;
                hienTai = hienTai.TiepTheo;
            }
            return duongNhoNhat;
        }

        static void Bai5()
        {
            dauDS = null;
            NhapDuLieu();
            Console.Write("\nDanh sách vừa nhập: ");
            HienThiDS();
            if (dauDS != null)
                Console.WriteLine($"\na. Phần tử lớn nhất: {LayGiaTriLonNhat()}");
            else
                Console.WriteLine("\na. Danh sách rỗng.");
            Console.WriteLine($"b. Tổng các phần tử: {LayTongPhanTu()}");
            Console.WriteLine($"c. Số lượng số nguyên tố: {DemSoNguyenTo()}");
            ThongKeAmDuongKhong();
            Console.Write("\nNhập giá trị x cần đếm: ");
            int giaTriX = int.Parse(Console.ReadLine());
            Console.WriteLine($"e. Số lượng phần tử bằng {giaTriX}: {DemPhanTuBangX(giaTriX)}");
            int kqDuongMin = LayDuongNhoNhat();
            if (kqDuongMin == int.MaxValue)
                Console.WriteLine("f. Danh sách không có số dương.");
            else
                Console.WriteLine($"f. Phần tử dương nhỏ nhất: {kqDuongMin}");
        }
        // ==================== BÀI 7 ====================
        static void Bai7()
        {
            Console.Write("Nhập số ngày n: ");
            int n = int.Parse(Console.ReadLine());
            double[] nhietDo = new double[n];
            for (int i = 0; i < n; i++)
            {
                Console.Write($"Nhập nhiệt độ ngày {i + 1}: ");
                nhietDo[i] = double.Parse(Console.ReadLine());
            }
            double max = nhietDo[0], min = nhietDo[0], tong = 0;
            int ngayMax = 1, ngayMin = 1;
            for (int i = 0; i < n; i++)
            {
                if (nhietDo[i] > max) { max = nhietDo[i]; ngayMax = i + 1; }
                if (nhietDo[i] < min) { min = nhietDo[i]; ngayMin = i + 1; }
                tong += nhietDo[i];
            }
            Console.WriteLine($"Ngày nóng nhất: Ngày {ngayMax} ({max}°C)");
            Console.WriteLine($"Ngày lạnh nhất: Ngày {ngayMin} ({min}°C)");
            Console.WriteLine($"Nhiệt độ trung bình: {tong / n:F2}°C");
        }
        // ==================== BÀI 8 ====================
        static void Bai8()
        {
            Console.WriteLine("Nhập chuỗi văn bản:");
            string input = Console.ReadLine();
            string clean = Regex.Replace(input.ToLower(), @"[^\p{L}\p{N}\s]", "");
            string[] words = clean.Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, int> freq = new Dictionary<string, int>();
            foreach (string w in words)
            {
                if (freq.ContainsKey(w)) freq[w]++;
                else freq[w] = 1;
            }
            Console.WriteLine("\nThống kê tần suất từ:");
            foreach (var item in freq.OrderByDescending(x => x.Value))
                Console.WriteLine($"{item.Key}: {item.Value} lần");
        }
        // ==================== BÀI 9 ====================
        static bool LaSoHoanHao(int x)
        {
            if (x <= 1) return false;
            int tongUoc = 1;
            for (int i = 2; i <= x / 2; i++)
                if (x % i == 0) tongUoc += i;
            return tongUoc == x;
        }
        static void Bai9()
        {
            Console.Write("Nhập số lượng phần tử n: ");
            int n = int.Parse(Console.ReadLine());
            int[] a = new int[n];
            for (int i = 0; i < n; i++)
            {
                Console.Write($"a[{i}] = ");
                a[i] = int.Parse(Console.ReadLine());
            }
            List<int> dsNT = new List<int>(), dsChan = new List<int>(), dsHH = new List<int>();
            foreach (int x in a)
            {
                if (LaSoNguyenTo(x)) dsNT.Add(x);
                if (x % 2 == 0) dsChan.Add(x);
                if (LaSoHoanHao(x)) dsHH.Add(x);
            }
            Console.WriteLine($"Số nguyên tố ({dsNT.Count}): {string.Join(", ", dsNT)}");
            Console.WriteLine($"Số chẵn ({dsChan.Count}): {string.Join(", ", dsChan)}");
            Console.WriteLine($"Số hoàn hảo ({dsHH.Count}): {string.Join(", ", dsHH)}");
        }
        // ==================== BÀI 10 ====================
        static void Bai10()
        {
            Console.Write("Nhập số hàng m: "); int m = int.Parse(Console.ReadLine());
            Console.Write("Nhập số cột n: "); int n = int.Parse(Console.ReadLine());
            int[,] matrix = new int[m, n];
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"matrix[{i},{j}] = ");
                    matrix[i, j] = int.Parse(Console.ReadLine());
                }
            int top = 0, bottom = m - 1, left = 0, right = n - 1;
            Console.Write("\nThứ tự xoắn ốc: ");
            while (top <= bottom && left <= right)
            {
                for (int j = left; j <= right; j++) Console.Write(matrix[top, j] + " ");
                top++;
                for (int i = top; i <= bottom; i++) Console.Write(matrix[i, right] + " ");
                right--;
                if (top <= bottom)
                {
                    for (int j = right; j >= left; j--) Console.Write(matrix[bottom, j] + " ");
                    bottom--;
                }
                if (left <= right)
                {
                    for (int i = bottom; i >= top; i--) Console.Write(matrix[i, left] + " ");
                    left++;
                }
            }
            Console.WriteLine();
        }
        // ==================== BÀI 11 ====================
        static void Bai11()
        {
            Console.Write("Nhập cấp ma trận vuông n: ");
            int n = int.Parse(Console.ReadLine());
            int[,] matrix = new int[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"matrix[{i},{j}] = ");
                    matrix[i, j] = int.Parse(Console.ReadLine());
                }
            bool laDonVi = true, laDoiXung = true;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if ((i == j && matrix[i, j] != 1) || (i != j && matrix[i, j] != 0))
                        laDonVi = false;
                    if (matrix[i, j] != matrix[j, i])
                        laDoiXung = false;
                }
            }
            Console.WriteLine($"Ma trận đơn vị: {(laDonVi ? "Có" : "Không")}");
            Console.WriteLine($"Ma trận đối xứng: {(laDoiXung ? "Có" : "Không")}");
        }
        // ==================== BÀI 12 ====================
        static void Bai12()
        {
            Console.Write("Nhập số hàng m: "); int m = int.Parse(Console.ReadLine());
            Console.Write("Nhập số cột n: "); int n = int.Parse(Console.ReadLine());
            Console.Write("Nhập số lượng mìn: "); int soMin = int.Parse(Console.ReadLine());
            int[,] board = new int[m, n];
            Random rnd = new Random();
            int dat = 0;
            while (dat < soMin && dat < m * n)
            {
                int r = rnd.Next(m), c = rnd.Next(n);
                if (board[r, c] != -1) { board[r, c] = -1; dat++; }
            }
            int[] dr = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dc = { -1, 0, 1, -1, 1, -1, 0, 1 };
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (board[i, j] == -1) continue;
                    int count = 0;
                    for (int k = 0; k < 8; k++)
                    {
                        int ni = i + dr[k], nj = j + dc[k];
                        if (ni >= 0 && ni < m && nj >= 0 && nj < n && board[ni, nj] == -1)
                            count++;
                    }
                    board[i, j] = count;
                }
            }
            Console.WriteLine("\nBảng Minesweeper (* là mìn):");
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write(board[i, j] == -1 ? " * " : $" {board[i, j]} ");
                Console.WriteLine();
            }
        }
        // ==================== BÀI 13 ====================
        static void Bai13()
        {
            Node head = new Node(1) { Next = new Node(2) { Next = new Node(3) { Next = new Node(4) } } };
            Console.Write("Ban đầu: ");
            for (Node p = head; p != null; p = p.Next) Console.Write(p.Data + " -> ");
            Console.WriteLine("null");
            Node prev = null, curr = head, next = null;
            while (curr != null)
            {
                next = curr.Next;
                curr.Next = prev;
                prev = curr;
                curr = next;
            }
            head = prev;
            Console.Write("Sau khi đảo ngược: ");
            for (Node p = head; p != null; p = p.Next) Console.Write(p.Data + " -> ");
            Console.WriteLine("null");
        }
        // ==================== BÀI 14 ====================
        static void Bai14()
        {
            Node l1 = new Node(1) { Next = new Node(4) { Next = new Node(7) } };
            Node l2 = new Node(2) { Next = new Node(3) { Next = new Node(8) } };
            Node dummy = new Node(0);
            Node tail = dummy;
            while (l1 != null && l2 != null)
            {
                if (l1.Data <= l2.Data) { tail.Next = l1; l1 = l1.Next; }
                else { tail.Next = l2; l2 = l2.Next; }
                tail = tail.Next;
            }
            tail.Next = (l1 != null) ? l1 : l2;
            Console.Write("Danh sách sau khi gộp: ");
            for (Node p = dummy.Next; p != null; p = p.Next) Console.Write(p.Data + " ");
            Console.WriteLine();
        }
        // ==================== BÀI 15 ====================
        static void Bai15()
        {
            Node n2 = new Node(2), n3 = new Node(3), n4 = new Node(4), n5 = new Node(5), n6 = new Node(6);
            n2.Next = n3; n3.Next = n4; n4.Next = n5; n5.Next = n6; n6.Next = n2;
            Node rua = n2, tho = n2;
            bool coChuTrinh = false;
            while (tho != null && tho.Next != null)
            {
                rua = rua.Next;
                tho = tho.Next.Next;
                if (rua == tho) { coChuTrinh = true; break; }
            }
            Console.WriteLine($"Danh sách 2->3->4->5->6->2 có chu trình không? {(coChuTrinh ? "Có" : "Không")}");
        }
        // ==================== BÀI 16 ====================
        static void Bai16()
        {
            WebNode current = null;
            void Visit(string url)
            {
                WebNode node = new WebNode(url);
                if (current != null) { current.Next = node; node.Prev = current; }
                current = node;
                Console.WriteLine($"Truy cập: {current.Url}");
            }
            void Back()
            {
                if (current?.Prev != null) { current = current.Prev; Console.WriteLine($"Back -> {current.Url}"); }
            }
            void Forward()
            {
                if (current?.Next != null) { current = current.Next; Console.WriteLine($"Forward -> {current.Url}"); }
            }
            Visit("google.com");
            Visit("vnexpress.net");
            Visit("github.com");
            Back();
            Back();
            Forward();
        }
        // ==================== BÀI 17 ====================
        static void Bai17()
        {
            SongNode head = null, tail = null, current = null;
            void Them(string ten)
            {
                SongNode node = new SongNode(ten);
                if (head == null) head = tail = current = node;
                else { tail.Next = node; node.Prev = tail; tail = node; }
            }
            void Xoa(string ten)
            {
                SongNode p = head;
                while (p != null && p.TenBaiHat != ten) p = p.Next;
                if (p == null) return;
                if (p == current) current = (p.Next != null) ? p.Next : p.Prev;
                if (p.Prev != null) p.Prev.Next = p.Next; else head = p.Next;
                if (p.Next != null) p.Next.Prev = p.Prev; else tail = p.Prev;
            }
            Them("Bài 1 - Lạc Trôi");
            Them("Bài 2 - Nơi Này Có Anh");
            Them("Bài 3 - Cơn Mưa Ngang Qua");
            Console.WriteLine($"Đang phát: {current.TenBaiHat}");
            if (current.Next != null) current = current.Next;
            Console.WriteLine($"Next -> Đang phát: {current.TenBaiHat}");
            if (current.Prev != null) current = current.Prev;
            Console.WriteLine($"Previous -> Đang phát: {current.TenBaiHat}");
            Xoa("Bài 1 - Lạc Trôi");
            Console.WriteLine($"Sau khi xóa Bài 1, đang phát: {current.TenBaiHat}");
        }
        // ==================== BÀI 18 ====================
        static void Bai18()
        {
            OrderNode dh1 = new OrderNode("DH01");
            OrderNode dh2 = new OrderNode("DH02");
            dh1.Next = dh2; dh2.Prev = dh1;
            OrderNode current = dh1;
            Console.WriteLine($"Đơn {current.MaDon}: {current.TrangThai}");
            current.ChuyenTrangThai();
            Console.WriteLine($"Đơn {current.MaDon} sau cập nhật: {current.TrangThai}");
            if (current.Next != null) current = current.Next;
            current.ChuyenTrangThai();
            current.ChuyenTrangThai();
            Console.WriteLine($"Đơn {current.MaDon}: {current.TrangThai}");
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Console.WriteLine("================ BÀI 1 ================");
            Bai1();
            Console.WriteLine("\n================ BÀI 2 ================");
            Bai2();
            Console.WriteLine("\n================ BÀI 3 ================");
            Bai3();
            Console.WriteLine("\n================ BÀI 4 ================");
            Bai4();
            Console.WriteLine("\n================ BÀI 5 ================");
            Bai5();
            Console.WriteLine("\n================ BÀI 7 ================");
            Bai7();
            Console.WriteLine("\n================ BÀI 8 ================");
            Bai8();
            Console.WriteLine("\n================ BÀI 9 ================");
            Bai9();
            Console.WriteLine("\n================ BÀI 10 ================");
            Bai10();
            Console.WriteLine("\n================ BÀI 11 ================");
            Bai11();
            Console.WriteLine("\n================ BÀI 12 ================");
            Bai12();
            Console.WriteLine("\n================ BÀI 13 ================");
            Bai13();
            Console.WriteLine("\n================ BÀI 14 ================");
            Bai14();
            Console.WriteLine("\n================ BÀI 15 ================");
            Bai15();
            Console.WriteLine("\n================ BÀI 16 ================");
            Bai16();
            Console.WriteLine("\n================ BÀI 17 ================");
            Bai17();
            Console.WriteLine("\n================ BÀI 18 ================");
            Bai18();
        }
    }
}