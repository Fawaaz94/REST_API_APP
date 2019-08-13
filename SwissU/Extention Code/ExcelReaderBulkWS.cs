using SwissU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;       //microsoft Excel 14 object in references-> COM tab

namespace SwissU.Extention_Code
{
    class ExcelReaderBulkWS
    {
        public static List<RowObjectBulkWS> getExcelFile(string ExcelFileLocation)
        {
            List<RowObjectBulkWS> items = new List<RowObjectBulkWS>();

            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(ExcelFileLocation);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            // This for will start at the second row
            for (int i = 2; i <= rowCount; i++)
            {
                // This for will start at the first column
                for (int j = 1; j <= 1; j++)
                {
                    //write the value to the console
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        // Adds items to the List 
                        items.Add(
                            new RowObjectBulkWS
                            {
                                CompanyID = xlRange.Cells[i, j + 1].Value2.ToString(),
                                CompanyName = xlRange.Cells[i, j + 2].Value2.ToString(),
                            }
                        );

                    }
                }
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            return items;
        }// EOM
    }
}
