using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCopyApps.Utilities
{
    internal class ExcelUtils
    {
        public static ISheet init(string path) {
            return init(path, 0);
        }

        public static ISheet init(string path, int sheetIndex)
        {
            using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            IWorkbook workbook = new XSSFWorkbook(fs); // Use XSSFWorkbook for .xlsx files

            ISheet worksheet = workbook.GetSheetAt(sheetIndex); // Assuming data is in the first sheet
            return worksheet;
        }

        public static T PopulateModelFromRow<T>(IRow currentRow) where T : new()
        {
            T model = new T();

            foreach (var property in typeof(T).GetProperties())
            {
                // Menentukan indeks kolom berdasarkan nama properti
                int columnIndex = GetColumnIndex(currentRow.Sheet, property.Name);

                if (columnIndex != -1)
                {
                    // Mendapatkan nilai sel dari baris saat ini
                    ICell cell = currentRow.GetCell(columnIndex);

                    // Mengisi nilai properti dengan nilai dari sel
                    if (cell != null)
                    {
                        // Konversi nilai sel sesuai tipe data properti
                        object cellValue = GetCellValue(cell, property.PropertyType);
                        property.SetValue(model, cellValue);
                    }
                }
            }

            return model;
        }

        // Metode untuk mendapatkan indeks kolom berdasarkan nama properti
        private static int GetColumnIndex(ISheet sheet, string columnName)
        {
            // Loop melalui sel-sel pada baris pertama untuk mencari nama kolom
            IRow firstRow = sheet.GetRow(0);
            for (int i = 0; i < firstRow.LastCellNum; i++)
            {
                ICell cell = firstRow.GetCell(i);
                if (cell.StringCellValue == columnName)
                {
                    return i;
                }
            }
            return -1; // Return -1 jika nama kolom tidak ditemukan
        }

        // Metode untuk mendapatkan nilai dari sel dan mengkonversinya sesuai tipe data properti
        private static object GetCellValue(ICell cell, Type propertyType)
        {
            if (propertyType == typeof(string))
            {
                return cell.ToString();
            }
            else if (propertyType == typeof(int))
            {
                return (int)cell.NumericCellValue;
            }
            else if (propertyType == typeof(long))
            {
                return (long)cell.NumericCellValue;
            }
            // Tambahkan tipe data lainnya sesuai kebutuhan

            // Jika tidak sesuai dengan tipe data yang diketahui, kembalikan null
            return null;
        }

    }
}
