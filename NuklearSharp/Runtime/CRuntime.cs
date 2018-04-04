using System;

namespace KlearUI
{
    internal static unsafe class CRuntime
    {
        public const long DblExpMask = 0x7ff0000000000000L;
        public const int DblMantBits = 52;
        public const long DblSgnMask = -1 - 0x7fffffffffffffffL;
        public const long DblMantMask = 0x000fffffffffffffL;
        public const long DblExpClrMask = DblSgnMask | DblMantMask;

        public static void* Malloc(ulong size)
        {
            return Operations.Malloc((long)size);
        }

        public static void Memcpy(void* a, void* b, long size)
        {
            Operations.Memcpy(a, b, size);
        }

        public static void Memcpy(void* a, void* b, ulong size)
        {
            Memcpy(a, b, (long)size);
        }

        public static void Memmove(void* a, void* b, long size)
        {
            Operations.MemMove(a, b, size);
        }

        public static void Memmove(void* a, void* b, ulong size)
        {
            Memmove(a, b, (long)size);
        }

        public static int Memcmp(void* a, void* b, long size)
        {
            return Operations.Memcmp(a, b, size);
        }

        public static int Memcmp(void* a, void* b, ulong size)
        {
            return Memcmp(a, b, (long)size);
        }

        public static void Free(void* a)
        {
            Operations.Free(a);
        }

        public static void Memset(void* ptr, int value, long size)
        {
            byte* bptr = (byte*)ptr;
            var bval = (byte)value;
            for (long i = 0; i < size; ++i)
            {
                *bptr++ = bval;
            }
        }

        public static void Memset(void* ptr, int value, ulong size)
        {
            Memset(ptr, value, (long)size);
        }

        public static uint _lrotl(uint x, int y)
        {
            return x << y | x >> 32 - y;
        }

        public static void* Realloc(void* ptr, long newSize)
        {
            return Operations.Realloc(ptr, newSize);
        }

        public static void* Realloc(void* ptr, ulong newSize)
        {
            return Realloc(ptr, (long)newSize);
        }

        public static int Abs(int v)
        {
            return Math.Abs(v);
        }

        /// <summary>
        /// This code had been borrowed from here: https://github.com/MachineCognitis/C.math.NET
        /// </summary>
        /// <param name="number"></param>
        /// <param name="exponent"></param>
        /// <returns></returns>
        public static double Frexp(double number, int* exponent)
        {
            var bits = BitConverter.DoubleToInt64Bits(number);
            var exp = (int)((bits & DblExpMask) >> DblMantBits);
            *exponent = 0;

            if (exp == 0x7ff || number == 0D)
                number += number;
            else
            {
                // Not zero and finite.
                *exponent = exp - 1022;
                if (exp == 0)
                {
                    // Subnormal, scale number so that it is in [1, 2).
                    number *= BitConverter.Int64BitsToDouble(0x4350000000000000L); // 2^54
                    bits = BitConverter.DoubleToInt64Bits(number);
                    exp = (int)((bits & DblExpMask) >> DblMantBits);
                    *exponent = exp - 1022 - 54;
                }
                // Set exponent to -1 so that number is in [0.5, 1).
                number = BitConverter.Int64BitsToDouble(bits & DblExpClrMask | 0x3fe0000000000000L);
            }

            return number;
        }

        public static double Pow(double a, double b)
        {
            return Math.Pow(a, b);
        }

        public static float Fabs(double a)
        {
            return (float)Math.Abs(a);
        }

        public static double Ceil(double a)
        {
            return Math.Ceiling(a);
        }


        public static double Floor(double a)
        {
            return Math.Floor(a);
        }

        public static double Log(double value)
        {
            return Math.Log(value);
        }

        public static double Exp(double value)
        {
            return Math.Exp(value);
        }

        public static double Cos(double value)
        {
            return Math.Cos(value);
        }

        public static double Acos(double value)
        {
            return Math.Acos(value);
        }

        public static double Sin(double value)
        {
            return Math.Sin(value);
        }

        public static int Memcmp(byte* a, byte[] b, ulong size)
        {
            fixed (void* bptr = b)
            {
                return Operations.Memcmp(a, bptr, (long)size);
            }
        }

        public static double Ldexp(double number, int exponent)
        {
            return number * Math.Pow(2, exponent);
        }

        public delegate int QSortComparer(void* a, void* b);

        private static void QsortSwap(byte* data, long size, long pos1, long pos2)
        {
            var a = data + size * pos1;
            var b = data + size * pos2;

            for (long k = 0; k < size; ++k)
            {
                var tmp = *a;
                *a = *b;
                *b = tmp;

                a++;
                b++;
            }
        }

        private static long QsortPartition(byte* data, long size, QSortComparer comparer, long left, long right)
        {
            void* pivot = data + size * left;
            var i = left - 1;
            var j = right + 1;
            for (; ; )
            {
                do
                {
                    ++i;
                } while (comparer(data + size * i, pivot) < 0);

                do
                {
                    --j;
                } while (comparer(data + size * j, pivot) > 0);

                if (i >= j)
                {
                    return j;
                }

                QsortSwap(data, size, i, j);
            }
        }


        private static void QsortInternal(byte* data, long size, QSortComparer comparer, long left, long right)
        {
            if (left < right)
            {
                var p = QsortPartition(data, size, comparer, left, right);

                QsortInternal(data, size, comparer, left, p);
                QsortInternal(data, size, comparer, p + 1, right);
            }
        }

        public static void Qsort(void* data, ulong count, ulong size, QSortComparer comparer)
        {
            QsortInternal((byte*)data, (long)size, comparer, 0, (long)count - 1);
        }

        public static double Sqrt(double val)
        {
            return Math.Sqrt(val);
        }

        public static double Fmod(double x, double y)
        {
            return x % y;
        }

        public static ulong Strlen(sbyte* str)
        {
            var ptr = str;

            while (*ptr != '\0')
            {
                ptr++;
            }

            return (ulong)ptr - (ulong)str - 1;
        }
    }
}