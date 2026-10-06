using System;
using System.Text;

namespace JaggedArrayExercises
{
    // Lớp lưu thông tin một thành viên
    class Member
    {
        public int Id;
        public string Name;
        public int Tasks;

        public Member(int id, string name, int tasks)
        {
            Id = id;
            Name = name;
            Tasks = tasks;
        }

        public override string ToString()
        {
            return $"ID: {Id,-5} | Họ tên: {Name,-20} | Số task hoàn thành: {Tasks}";
        }
    }

    class Program
    {
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\n===== MENU CHÍNH =====");
                Console.WriteLine("1. Bài 1: Jagged array khởi tạo sẵn");
                Console.WriteLine("2. Bài 2: Jagged array ngẫu nhiên + các chức năng");
                Console.WriteLine("3. Bài 3: Quản lý thành viên công ty X");
                Console.WriteLine("0. Thoát");
                int choice = ReadInt("Chọn: ");

                switch (choice)
                {
                    case 1: Exercise1(); break;
                    case 2: Exercise2(); break;
                    case 3: Exercise3(); break;
                    case 0: return;
                    default: Console.WriteLine("Lựa chọn không hợp lệ!"); break;
                }
            }
        }

        // ---------- Hàm tiện ích nhập số nguyên an toàn ----------
        static int ReadInt(string message)
        {
            int value;
            Console.Write(message);
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.Write("Giá trị không hợp lệ, nhập lại: ");
            }
            return value;
        }

        static int ReadPositiveInt(string message)
        {
            int v = ReadInt(message);
            while (v <= 0)
            {
                v = ReadInt("Phải lớn hơn 0, nhập lại: ");
            }
            return v;
        }

        // =====================================================
        // BÀI 1
        // =====================================================
        static void Exercise1()
        {
            int[][] arr = new int[4][];
            arr[0] = new int[] { 1, 1, 1, 1, 1 };
            arr[1] = new int[] { 2, 2 };
            arr[2] = new int[] { 3, 3, 3, 3 };
            arr[3] = new int[] { 4, 4 };

            Console.WriteLine("\n--- Bài 1: Jagged array ---");
            PrintJagged(arr);
        }

        static void PrintJagged(int[][] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    Console.Write(arr[i][j] + " ");
                }
                Console.WriteLine();
            }
        }

        // =====================================================
        // BÀI 2
        // =====================================================
        static void Exercise2()
        {
            Console.WriteLine("\n--- Bài 2: Jagged array ngẫu nhiên ---");
            int rows = ReadPositiveInt("Nhập số hàng: ");
            int[][] arr = new int[rows][];

            for (int i = 0; i < rows; i++)
            {
                int cols = ReadPositiveInt($"Nhập số cột của hàng {i}: ");
                arr[i] = new int[cols];
                for (int j = 0; j < cols; j++)
                {
                    arr[i][j] = rnd.Next(1, 101); // số ngẫu nhiên 1..100
                }
            }

            Console.WriteLine("\nMảng vừa tạo:");
            PrintJagged(arr);

            while (true)
            {
                Console.WriteLine("\n--- Menu Bài 2 ---");
                Console.WriteLine("1. In số lớn nhất mỗi hàng và của cả mảng");
                Console.WriteLine("2. Sắp xếp tăng dần từng hàng");
                Console.WriteLine("3. In các số nguyên tố");
                Console.WriteLine("4. Tìm vị trí của một số");
                Console.WriteLine("5. In lại mảng");
                Console.WriteLine("0. Quay lại menu chính");
                int c = ReadInt("Chọn: ");

                if (c == 0) return;
                switch (c)
                {
                    case 1: PrintMax(arr); break;
                    case 2: SortRows(arr); Console.WriteLine("Đã sắp xếp:"); PrintJagged(arr); break;
                    case 3: PrintPrimes(arr); break;
                    case 4: SearchValue(arr); break;
                    case 5: PrintJagged(arr); break;
                    default: Console.WriteLine("Lựa chọn không hợp lệ!"); break;
                }
            }
        }

        // 1. Số lớn nhất mỗi hàng và cả mảng
        static void PrintMax(int[][] arr)
        {
            int globalMax = int.MinValue;
            for (int i = 0; i < arr.Length; i++)
            {
                int rowMax = arr[i][0];
                for (int j = 1; j < arr[i].Length; j++)
                {
                    if (arr[i][j] > rowMax) rowMax = arr[i][j];
                }
                Console.WriteLine($"Số lớn nhất của hàng {i}: {rowMax}");
                if (rowMax > globalMax) globalMax = rowMax;
            }
            Console.WriteLine($"Số lớn nhất của toàn mảng: {globalMax}");
        }

        // 2. Sắp xếp tăng dần từng hàng
        static void SortRows(int[][] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Array.Sort(arr[i]);
            }
        }

        // 3. In các số nguyên tố
        static bool IsPrime(int n)
        {
            if (n < 2) return false;
            for (int i = 2; i * i <= n; i++)
            {
                if (n % i == 0) return false;
            }
            return true;
        }

        static void PrintPrimes(int[][] arr)
        {
            bool found = false;
            Console.WriteLine("Các số nguyên tố trong mảng:");
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    if (IsPrime(arr[i][j]))
                    {
                        Console.WriteLine($"{arr[i][j]} tại [{i}][{j}]");
                        found = true;
                    }
                }
            }
            if (!found) Console.WriteLine("Không có số nguyên tố nào.");
        }

        // 4. Tìm tất cả vị trí của một số
        static void SearchValue(int[][] arr)
        {
            int x = ReadInt("Nhập số cần tìm: ");
            bool found = false;
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    if (arr[i][j] == x)
                    {
                        Console.WriteLine($"Tìm thấy {x} tại vị trí [{i}][{j}]");
                        found = true;
                    }
                }
            }
            if (!found) Console.WriteLine($"Không tìm thấy {x} trong mảng.");
        }

        // =====================================================
        // BÀI 3
        // Cấu trúc dữ liệu: Member[][] (jagged array)
        //   groups[0] có 5 thành viên, groups[1] có 3, groups[2] có 6
        // =====================================================
        static Member[][] groups;

        static void Exercise3()
        {
            Console.WriteLine("\n--- Bài 3: Quản lý thành viên công ty X ---");
            groups = new Member[3][];
            groups[0] = new Member[5];
            groups[1] = new Member[3];
            groups[2] = new Member[6];

            bool initialized = false;

            while (true)
            {
                Console.WriteLine("\n--- Menu Bài 3 ---");
                Console.WriteLine("1. Khởi tạo dữ liệu (có sẵn hoặc nhập từ bàn phím)");
                Console.WriteLine("2. In danh sách tất cả thành viên");
                Console.WriteLine("3. Tìm thành viên theo ID");
                Console.WriteLine("4. In thành viên hoàn thành nhiều task nhất");
                Console.WriteLine("0. Quay lại menu chính");
                int c = ReadInt("Chọn: ");

                if (c == 0) return;
                if (c != 1 && !initialized)
                {
                    Console.WriteLine("Vui lòng khởi tạo dữ liệu trước (chọn 1)!");
                    continue;
                }

                switch (c)
                {
                    case 1: InitMembers(); initialized = true; break;
                    case 2: PrintAllMembers(); break;
                    case 3: FindById(); break;
                    case 4: PrintTopMember(); break;
                    default: Console.WriteLine("Lựa chọn không hợp lệ!"); break;
                }
            }
        }

        // 1. Khởi tạo dữ liệu
        static void InitMembers()
        {
            Console.WriteLine("1. Dùng dữ liệu có sẵn");
            Console.WriteLine("2. Nhập từ bàn phím");
            int c = ReadInt("Chọn: ");

            if (c == 1)
            {
                groups[0][0] = new Member(101, "Nguyen Van An", 12);
                groups[0][1] = new Member(102, "Tran Thi Binh", 20);
                groups[0][2] = new Member(103, "Le Van Cuong", 8);
                groups[0][3] = new Member(104, "Pham Thi Dung", 15);
                groups[0][4] = new Member(105, "Hoang Van Em", 18);

                groups[1][0] = new Member(201, "Vu Thi Giang", 25);
                groups[1][1] = new Member(202, "Dang Van Hai", 10);
                groups[1][2] = new Member(203, "Bui Thi Lan", 14);

                groups[2][0] = new Member(301, "Do Van Minh", 9);
                groups[2][1] = new Member(302, "Ngo Thi Nga", 22);
                groups[2][2] = new Member(303, "Ly Van Phuc", 17);
                groups[2][3] = new Member(304, "Truong Thi Quynh", 11);
                groups[2][4] = new Member(305, "Dinh Van Son", 19);
                groups[2][5] = new Member(306, "Mai Thi Thao", 13);
                Console.WriteLine("Đã khởi tạo dữ liệu có sẵn.");
            }
            else
            {
                for (int g = 0; g < groups.Length; g++)
                {
                    Console.WriteLine($"\n== Nhóm {g + 1} ({groups[g].Length} thành viên) ==");
                    for (int m = 0; m < groups[g].Length; m++)
                    {
                        Console.WriteLine($"Thành viên {m + 1}:");
                        int id = ReadInt("  ID: ");
                        while (FindMember(id) != null) // ID phải là duy nhất
                        {
                            id = ReadInt("  ID đã tồn tại, nhập ID khác: ");
                        }
                        Console.Write("  Họ tên: ");
                        string name = Console.ReadLine();
                        int tasks = ReadInt("  Số task đã hoàn thành: ");
                        while (tasks < 0)
                        {
                            tasks = ReadInt("  Phải >= 0, nhập lại: ");
                        }
                        groups[g][m] = new Member(id, name, tasks);
                    }
                }
            }
        }

        // 2. In danh sách tất cả thành viên
        static void PrintAllMembers()
        {
            for (int g = 0; g < groups.Length; g++)
            {
                Console.WriteLine($"\n== Nhóm {g + 1} ==");
                foreach (Member m in groups[g])
                {
                    if (m != null) Console.WriteLine(m);
                }
            }
        }

        // Tìm thành viên theo ID (trả về null nếu không có)
        static Member FindMember(int id)
        {
            for (int g = 0; g < groups.Length; g++)
            {
                for (int m = 0; m < groups[g].Length; m++)
                {
                    if (groups[g][m] != null && groups[g][m].Id == id)
                        return groups[g][m];
                }
            }
            return null;
        }

        // 3. In thông tin thành viên khi biết ID
        static void FindById()
        {
            int id = ReadInt("Nhập ID cần tìm: ");
            Member found = FindMember(id);
            if (found != null) Console.WriteLine(found);
            else Console.WriteLine("Không tìm thấy thành viên có ID này.");
        }

        // 4. In thành viên có số task hoàn thành cao nhất
        static void PrintTopMember()
        {
            int max = -1;
            for (int g = 0; g < groups.Length; g++)
            {
                foreach (Member m in groups[g])
                {
                    if (m != null && m.Tasks > max) max = m.Tasks;
                }
            }

            Console.WriteLine($"Số task hoàn thành cao nhất: {max}");
            for (int g = 0; g < groups.Length; g++)
            {
                foreach (Member m in groups[g])
                {
                    if (m != null && m.Tasks == max)
                        Console.WriteLine($"Nhóm {g + 1}: {m}");
                }
            }
        }
    }
}