using System;
using System.Linq;
using System.Text;

namespace CubeCoverLib
{
    public class TruthTable : ITruthTable
    {
        private const byte NotFound = 0xff;
        private const byte WordSize = 8;
        private bool[,] _table;

        public TruthTable(bool[,] table)
        {
            byte rowCount;
            byte colCount;
            byte pow;
            if (!CheckTableDims(table, out rowCount, out colCount, out pow))
                throw new ArgumentOutOfRangeException("table",
                                                      string.Format(
                                                          "wrong demension sizes. rows = {0} columns = {1} pow = {2}",
                                                          rowCount, colCount, pow));
            if (!CheckTableContent(table))
                throw new ArgumentOutOfRangeException("table",
                                                      "wrong set of arguments. Function should be defined once for every set of arguments");
            Table = table;
            ArgCount = pow;
        }

        public TruthTable(byte argNum, bool[] funcVal)
        {
            var rowCount = RaiseTwoToXPow(argNum);
            var colCount = (byte) (argNum + 1);
            if (rowCount != funcVal.Length)
                throw new ArgumentOutOfRangeException(
                    string.Format(
                        "the size of array of function's values = {0}. Expected size (based on the number of arguments) = {1}",
                        funcVal.Length, rowCount));
            Table = new bool[rowCount,colCount];
            ArgCount = argNum;

            for (byte i = 0x00; i < rowCount; i++)
            {
                ReplaceRowWithWord(ref _table, i, i, 1);
                Table[i, colCount - 1] = funcVal[i];
            }
        }

        public byte ArgCount { get; private set; }

        public bool[,] Table
        {
            get { return _table; }
            set { _table = value; }
        }

        public ICoverage GetNullCoverage()
        {
            throw new NotImplementedException();
        }

        public void Sort()
        {
            throw new NotImplementedException();
        }

        private static bool IsPowerOfTwo(byte x, out byte pow)
        {
            pow = 0;
            while (((x & 1) == 0) && x > 1) /* While x is even and > 1 */
            {
                x >>= 1;
                pow++;
            }
            return (x == 1);
            //return ((x != 0x00) && ((x & (~x + 1)) == x));
        }

        private static byte RaiseTwoToXPow(byte x)
        {
            const byte res = 0x01;
            return (byte) (res << x);
        }

        private static bool CheckTableDims(bool[,] table, out byte rowCount, out byte colCount, out byte pow)
        {
            rowCount = (byte) table.GetLength(0);
            colCount = (byte) table.GetLength(1);
            if (!IsPowerOfTwo(rowCount, out pow))
            {
                return false;
            }
            return colCount == (pow + 1);
        }

        private bool CheckTableContent(bool[,] table)
        {
            var rowCount = (byte) table.GetLength(0);
            var colCount = (byte) table.GetLength(1);
            for (byte i = 0x00; i < rowCount; i++)
            {
                var pattern = ConvertToBoolArray(i).Take(colCount - 1).ToArray();
                //System.Console.WriteLine(string.Join(",", pattern));
                if (SearchPerRows(table, pattern) == NotFound)
                    return false;
            }
            return true;
        }

        private static byte SearchPerRows(bool[,] table, bool[] pattern)
        {
            var rowCount = (byte) table.GetLength(0);
            var colCount = (byte) table.GetLength(1);
            if ((colCount - 1) != pattern.Length)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format(
                        "the size of table's row should be greater then pattern by one bit colCount = {0}, patternt length = {1}",
                        colCount, pattern.Length));
            }
            for (byte i = 0; i < rowCount; i++)
            {
                var eq = true;
                for (byte j = 0; j < colCount - 1; j++)
                {
                    if (table[i, j].Equals(pattern[j])) continue;
                    eq = false;
                    break;
                }
                if (eq) return i;
            }
            return NotFound;
        }

        private static bool[] ConvertToBoolArray(byte word)
        {
            var shiftWord = word;
            var retArr = new bool[WordSize];
            for (byte i = 0; i < WordSize; i++)
            {
                retArr[i] = (shiftWord & 1) != 0;
                shiftWord >>= 1;
            }
            return retArr;
        }

        //private static bool[] ConvertToBoolArray<T>(T x)
        //    where T: struct, IComparable<T>
        //{
        //    int size = System.Runtime.InteropServices.Marshal.SizeOf(x);
        //    bool[] retArr = new bool[size];
        //    for (byte i = 0; i < size; i++)
        //    {
        //        retArr[i] = (x & 1) == 0 ? false : true;
        //        x >>= x;
        //    }
        //}

        public override string ToString()
        {
            var rowCount = (byte) Table.GetLength(0);
            var colCount = (byte) Table.GetLength(1);
            var retStr = new StringBuilder();
            for (byte i = 0; i < rowCount; i++)
            {
                for (byte j = 0; j < colCount - 1; j++)
                {
                    retStr.Append(Table[i, j] ? '1' : '0');
                }
                retStr.Append("|");
                bool lastElemInLine = Table[i, colCount - 1];
                retStr.Append(lastElemInLine ? '1' : '0');
                retStr.Append("\n");
            }
            return retStr.ToString();
        }


        private static void ReplaceRowWithWord(ref bool[,] table, byte rowNum, byte word,
                                               byte shiftWordLeft = 0)
        {
            var wordByteSet = ConvertToBoolArray(word);
            var colCount = (byte) table.GetLength(1);
            for (byte i = 0; i < colCount - shiftWordLeft; i++)
            {
                table[rowNum, colCount - 1 - i - shiftWordLeft] = wordByteSet[i];
            }
        }
    }
}