using System;

class TongHopDeQuy
{
    static long TinhS1_DeQuy(int n)
    {
        if (n <= 1) return n;
        return TinhS1_DeQuy(n - 1) + (long)n;
    }

    static double TinhS2_DeQuy(long n)
    {
        if (n <= 1) return n;
        return TinhS2_DeQuy(n - 1) + (double)(n * n);
    }

    static double TinhS3_DeQuy(int n)
    {
        if (n == 1) return 1.0;
        return TinhS3_DeQuy(n - 1) + 1.0 / n;
    }

    static double TinhS4_DeQuy(double x, int n)
    {
        if (n == 0) return 1;
        return TinhS4_DeQuy(x, n - 1) + Math.Pow(x, n);
    }

    static int UocLeMax(int n)
    {
        if (n % 2 != 0) return n;
        return UocLeMax(n / 2);
    }

    static long UocChungLonNhat(long a, long b)
    {
        if (a % b == 0) return b;
        return UocChungLonNhat(b, a % b);
    }

    static long DemSoChuSo(int n)
    {
        if (n < 10) return 1;
        return DemSoChuSo(n / 10) + 1;
    }

    static double TinhTongCan(int n, int i = 1)
    {
        if (i == n) return Math.Sqrt(n);
        return Math.Sqrt(i + TinhTongCan(n, i + 1));
    }

    static void Main(string[] args)
    {
        Console.WriteLine("=== BAI 1 ===");
        Console.Write("Nhap n: ");
        int n1 = int.Parse(Console.ReadLine());
        Console.WriteLine($"S1 = {TinhS1_DeQuy(n1)}\n");

        Console.WriteLine("=== BAI 2 ===");
        Console.Write("Nhap n: ");
        long n2 = long.Parse(Console.ReadLine());
        Console.WriteLine($"S2 = {TinhS2_DeQuy(n2)}\n");

        Console.WriteLine("=== BAI 3 ===");
        Console.Write("Nhap n: ");
        int n3 = int.Parse(Console.ReadLine());
        Console.WriteLine($"S3 = {TinhS3_DeQuy(n3)}\n");

        Console.WriteLine("=== BAI 4 ===");
        Console.Write("Nhap x: ");
        double x4 = double.Parse(Console.ReadLine());
        Console.Write("Nhap n: ");
        int n4 = int.Parse(Console.ReadLine());
        Console.WriteLine($"S4 = {TinhS4_DeQuy(x4, n4)}\n");

        Console.WriteLine("=== BAI 5 ===");
        Console.Write("Nhap n: ");
        int n5 = int.Parse(Console.ReadLine());
        Console.WriteLine($"Uoc le max cua {n5} la: {UocLeMax(n5)}\n");

        Console.WriteLine("=== BAI 6 ===");
        Console.Write("Nhap a: ");
        long a6 = long.Parse(Console.ReadLine());
        Console.Write("Nhap b: ");
        long b6 = long.Parse(Console.ReadLine());
        Console.WriteLine($"UCLN({a6}, {b6}) = {UocChungLonNhat(a6, b6)}\n");

        Console.WriteLine("=== BAI 7 ===");
        Console.Write("Nhap n: ");
        int n7 = int.Parse(Console.ReadLine());
        Console.WriteLine($"So chu so cua {n7} la: {DemSoChuSo(n7)}\n");

        Console.WriteLine("=== BAI 8 ===");
        Console.Write("Nhap n: ");
        int n8 = int.Parse(Console.ReadLine());
        Console.WriteLine($"Tong can bac hai cua {n8} la: {TinhTongCan(n8)}");
    }
}