using System;
using System.Collections.Generic;
using System.Data;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
/// <summary>
/// Summary description for IO
/// </summary>
/// 
namespace BLL
{
    public static class IO
    {
        public static int rowcounter = 0;
        public static int errorcounter = 0;
        public static string encryptNumber(String Incometxt)
        {
            String EText = "";
            var IText = Incometxt.ToCharArray();
            for (int s = 0; s < IText.Length; s++)
            {
                switch (IText[s])
                {
                    case '0':
                        EText += "A";
                        break;
                    case '1':
                        EText += "B";
                        break;
                    case '2':
                        EText += "C";
                        break;
                    case '3':
                        EText += "D";
                        break;
                    case '4':
                        EText += "E";
                        break;
                    case '5':
                        EText += "F";
                        break;
                    case '6':
                        EText += "G";
                        break;
                    case '7':
                        EText += "H";
                        break;
                    case '8':
                        EText += "I";
                        break;
                    case '9':
                        EText += "J";
                        break;
                }
            }
            return EText;
        }
        public static string DecryptNumber(String Incometxt)
        {
            String EText = "";
            var IText = Incometxt.ToCharArray();
            for (int s = 0; s < IText.Length; s++)
            {
                switch (IText[s])
                {
                    case 'A':
                        EText += "0";
                        break;
                    case 'B':
                        EText += "1";
                        break;
                    case 'C':
                        EText += "2";
                        break;
                    case 'D':
                        EText += "3";
                        break;
                    case 'E':
                        EText += "4";
                        break;
                    case 'F':
                        EText += "5";
                        break;
                    case 'G':
                        EText += "6";
                        break;
                    case 'H':
                        EText += "7";
                        break;
                    case 'I':
                        EText += "8";
                        break;
                    case 'J':
                        EText += "9";
                        break;
                }
            }
            return EText;
        }
        public static string EncryptText(string strToEncrypt, string strKey)
        {
            try
            {
                TripleDESCryptoServiceProvider objDESCrypto =
                    new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                string strTempKey = strKey;
                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = ASCIIEncoding.ASCII.GetBytes(strToEncrypt);
                return Convert.ToBase64String(objDESCrypto.CreateEncryptor().
                    TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            }
            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }
        }
        public static string DecryptText(string strEncrypted, string strKey)
        {
            try
            {
                TripleDESCryptoServiceProvider objDESCrypto =
                    new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                string strTempKey = strKey;
                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = Convert.FromBase64String(strEncrypted);
                string strDecrypted = ASCIIEncoding.ASCII.GetString
                (objDESCrypto.CreateDecryptor().TransformFinalBlock
                (byteBuff, 0, byteBuff.Length));
                objDESCrypto = null;
                return strDecrypted;
            }
            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }
        }
        public static void WriteerrorLog(String Error, String Sendor, string Path)
        {
            TextWriter writeerror = new StreamWriter(Path + Sendor + ".txt", true);
            writeerror.WriteLine(Error);
            writeerror.Close();
        }
        private static string GetExcelConnString(string excelFile, bool hasHeader)
        {
            if (excelFile.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
            {
                ////return "Provider=Microsoft.Jet.OLEDB.4.0;" +
                ////    "Data Source=" + excelFile + ";Extended Properties='Excel 8.0;IMEX=1;" + (hasHeader ? "'" : "HDR=No;'");
                return "Provider=Microsoft.ACE.OLEDB.12.0;" +
                     "Data Source=" + excelFile + ";Extended Properties='Excel 8.0;IMEX=1;" + (hasHeader ? "'" : "HDR=No;'");
            }
            else
            {
                return "Provider=Microsoft.ACE.OLEDB.12.0;" +
                    "Data Source=" + excelFile + ";Extended Properties='Excel 12.0;IMEX=1;" + (hasHeader ? "'" : "HDR=No;'");
            }
        }
        /// <summary>
        /// This mehtod retrieves the excel sheet names from 
        /// an excel workbook.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <returns>String[]</returns>
        public static String[] GetExcelSheetNames(string excelFile)
        {
            OleDbConnection objConn = null;
            System.Data.DataTable dt = null;
            try
            {
                // Connection String. Change the excel file to the file you
                // will search.
                //String connString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                //    "Data Source=" + excelFile + ";Extended Properties=Excel 8.0;";
                String connString = GetExcelConnString(excelFile, false);
                // Create connection object by using the preceding connection string.
                objConn = new OleDbConnection(connString);
                // Open connection with the database.
                objConn.Open();
                // Get the data table containg the schema guid.
                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dt == null)
                {
                    return null;
                }
                String[] excelSheets = new String[dt.Rows.Count];
                int i = 0;
                // Add the sheet name to the string array.
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();
                    i++;
                }
                // Loop through all of the sheets if you want too...
                for (int j = 0; j < excelSheets.Length; j++)
                {
                    // Query each excel sheet.
                }
                return excelSheets;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                // Clean up.
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        public static DataSet ReadExcelFile(string excelFile, string sheetName, Boolean hasHeader, string criteria)
        {
            string connString = GetExcelConnString(excelFile, hasHeader);
            //string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excelFile + "; Extended Properties='Excel 8.0; HDR=No; IMEX=1;'";
            // Create the connection object 
            OleDbConnection oledbConn = null;
            //try
            //{
            oledbConn = new OleDbConnection(connString);
            // Open connection
            oledbConn.Open();
            // Create OleDbCommand object and select data from worksheet Sheet1
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheetName + "$" + "] " + (criteria != "" ? " WHERE " + criteria : ""), oledbConn);
            //OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheetName + "] " + (criteria != "" ? " WHERE " + criteria : "") , oledbConn);
            // Create new OleDbDataAdapter 
            OleDbDataAdapter oleda = new OleDbDataAdapter();
            oleda.SelectCommand = cmd;
            // Create a DataSet which will hold the data extracted from the worksheet.
            DataSet ds = new DataSet();
            // Fill the DataSet from the data extracted from the worksheet.
            oleda.Fill(ds);
            oledbConn.Close();
            return ds;
            //}
            //catch
            //{
            //    return null;
            //}
            //finally
            //{
            //    if (oledbConn != null)
            //    {
            //        // Close connection
            //        oledbConn.Close();
            //    }
            //}   
        }
        public static void WriteToFile(string row, string filePath, bool replace)
        {
            //FileStream stream=null;
            try
            {
                //Create the file if not exists
                if (replace)
                {
                    FileStream stream = File.Create(filePath);
                    stream.Close();
                }
                File.AppendAllText(filePath, "/n" + row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }
        public static List<string[]> ReadDelitmedFile(string path, char delimiter)
        {
            List<string[]> parsedData = new List<string[]>();
            try
            {
                using (StreamReader readFile = new StreamReader(path))
                {
                    string line;
                    string[] row;
                    while ((line = readFile.ReadLine()) != null)
                    {
                        row = line.Split(delimiter);
                        parsedData.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return parsedData;
        }
        public static DataTable ReadDelitmedFile(string path, char delimiter, bool hasHeader)
        {
            DataTable dt = new DataTable();
            bool headerRow = hasHeader;
            try
            {
                List<string[]> list = ReadDelitmedFile(path, delimiter);
                #region Add Columun Headers
                string[] firstRow = list[0];
                if (hasHeader)
                {
                    foreach (string colHeader in firstRow)
                    {
                        if (colHeader.Length != 0)
                            dt.Columns.Add(colHeader);
                        else
                            dt.Columns.Add(GetNextColumnHeader(dt));
                    }
                }
                else
                {
                    for (int i = 1; i < firstRow.Count(); i++)
                    {
                        dt.Columns.Add(GetNextColumnHeader(dt));
                    }
                }
                #endregion
                foreach (string[] row in list)
                {
                    if (row != null)
                    {
                        if (!headerRow)
                        {
                            dt.Rows.Add(row);
                        }
                        else
                        {
                            headerRow = false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return dt;
        }
        private static string GetNextColumnHeader(DataTable table)
        {
            int c = table.Columns.Count;
            while (true)
            {
                string h = "F" + c++;
                if (!table.Columns.Contains(h))
                    return h;
            }
        }
        #region For Smart Bank Files only
        public static List<string[]> ReadSB_NewLoanFile(string path, string[] accountTypes, string SBbranchCode)
        {
            List<string[]> parsedData = ReadDelitmedFile(path, '|');
            List<string[]> loanData = new List<string[]>();
            try
            {
                if (parsedData == null || parsedData.Count == 0)
                {
                    return null;
                }
                else
                {
                    //Add Header Row
                    loanData.Add(parsedData.First());
                    //Add data rows
                    foreach (string[] row in parsedData)
                    {
                        if (row[3].StartsWith(SBbranchCode) && accountTypes.Contains(row[1]))
                        {
                            loanData.Add(row);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return loanData;
        }
        public static DataTable ReadSB_NewLoanFileDTable(string path, string[] accountTypes, string SBbranchCode)
        {
            DataTable dt = new DataTable();
            bool headerRow = true;
            try
            {
                List<string[]> list = ReadSB_NewLoanFile(path, accountTypes, SBbranchCode);
                #region Add Columun Headers
                string[] firstRow = list[0];
                foreach (string colHeader in firstRow)
                {
                    if (colHeader.Length != 0)
                        dt.Columns.Add(colHeader);
                    else
                        dt.Columns.Add(GetNextColumnHeader(dt));
                }
                #endregion
                #region Add Row data
                foreach (string[] row in list)
                {
                    if (row != null)
                    {
                        if (!headerRow)
                        {
                            dt.Rows.Add(row);
                        }
                        else
                        {
                            headerRow = false;
                        }
                    }
                }
                #endregion
            }
            catch (Exception e)
            {
                throw e;
            }
            return dt;
        }
        public static DataSet ReadSB_NewLoanExcelFile(string path, string sheetName, string[] accountTypes, string SBbranchCode)
        {
            string criteria1 = " LEFT([Account Num],3) = '" + SBbranchCode + "'";
            StringBuilder criteria2 = new StringBuilder();
            foreach (string accountType in accountTypes)
            {
                if (criteria2.Length == 0)
                    criteria2.Append("'" + accountType + "'");
                else
                    criteria2.Append(", '" + accountType + "'");
            }
            return ReadExcelFile(path, sheetName, true, criteria1 + " AND [Account Type] IN (" + criteria2.ToString() + ")");
        }
        public static List<string[]> ReadSB_LoanFile(string path, string SBbranchCode, int AccountNoField)
        {
            List<string[]> parsedData = ReadDelitmedFile(path, '|');
            List<string[]> loanData = new List<string[]>();
            try
            {
                if (parsedData == null || parsedData.Count == 0)
                {
                    return null;
                }
                else
                {
                    //Add Header Row
                    loanData.Add(parsedData.First());
                    //Add data rows
                    foreach (string[] row in parsedData)
                    {
                        if (row[AccountNoField].StartsWith(SBbranchCode))
                        {
                            loanData.Add(row);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return loanData;
        }
        public static DataTable ReadSB_LoanFileDTable(string path, string SBbranchCode, int AccountNoField)
        {
            DataTable dt = new DataTable();
            bool headerRow = true;
            try
            {
                List<string[]> list = ReadSB_LoanFile(path, SBbranchCode, AccountNoField);
                #region Add Columun Headers
                string[] firstRow = list[0];
                foreach (string colHeader in firstRow)
                {
                    if (colHeader.Length != 0)
                        dt.Columns.Add(colHeader);
                    else
                        dt.Columns.Add(GetNextColumnHeader(dt));
                }
                #endregion
                #region Add Row data
                foreach (string[] row in list)
                {
                    if (row != null)
                    {
                        if (!headerRow)
                        {
                            dt.Rows.Add(row);
                        }
                        else
                        {
                            headerRow = false;
                        }
                    }
                }
                #endregion
            }
            catch (Exception e)
            {
                throw e;
            }
            return dt;
        }
        public static DataSet ReadSB_LoanTranFile(string pathTran, string pathLimit, string SBbranchCode)
        {
            DataSet ds = new DataSet();
            try
            {
                DataTable dtTran = ReadSB_LoanFileDTable(pathTran, SBbranchCode, 2);
                DataTable dtLimit = new DataTable();
                dtLimit.Columns.Add("AccountNo");
                dtLimit.Columns.Add("Limit");
                dtLimit.Columns.Add("TranDate");
                using (DataTable dt = ReadSB_LoanFileDTable(pathLimit, SBbranchCode, 3))
                {
                    if (dt.Rows[2] != null) dtLimit.Rows.Add(new string[] { dt.Rows[2].ToString(), dt.Rows[7].ToString(), dt.Rows[5].ToString() });
                }
                ds.Tables.Add(dtTran);
                ds.Tables.Add(dtLimit);
            }
            catch (Exception e)
            {
                throw e;
            }
            return ds;
        }
        #endregion
        //public static void WriteToFile(DataRow row, string fileName,string OtherCommentToAdd)
        //{
        //    int i = 0;            
        //    StreamWriter sw = null;
        //    try            
        //    {
        //    if (File.Exists(fileName))
        //    {
        //    }
        //    else
        //    {
        //        //Create the file
        //        File.Create(fileName);
        //        //Append Data
        //        sw=new StreamWriter(fileName,true);
        //        for (i = 0; i < row.Table.Columns - 1; i++)               
        //        {
        //            sw.Write(dt.row.Table.Columns[i].ColumnName + ",");
        //        } 
        //        sw.Write("Remark");                
        //        sw.WriteLine();
        //    }
        //    }
        //    catch (Exception ex)            
        //    {                
        //    }
        //    finally 
        //    {
        //        if (sw=!null)
        //        {
        //            sw.Close();
        //        }
        //    }
        //}
    }
}