namespace Sure.PersonalProject.Utilities
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-8-14
    [explain]: ExcelGeneral ExcelExport
    -----------------------------------------------------------------------*/
    using NPOI.HSSF.UserModel;
    using NPOI.HSSF.UserModel.Contrib;
    using NPOI.HSSF.Util;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.Drawing;
    using System.IO;

    /// <summary>
    /// ExcelHelper
    /// </summary>
    public class ExcelGeneral
    {
        #region Excel 自定义头导出

        /// <summary>
        /// Excel 导出
        /// </summary>
        /// <param name="dataTable">数据源</param>
        /// <param name="fileName">保存路径</param>
        /// <param name="sheetName">sheet 名称</param>
        /// <param name="titleName">标题</param>
        public static void ExportExcel(DataTable dataTable, string fileName, string sheetName, string titleName)
        {
            //创建 Excel 文件   
            HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
            //创建 Excel Sheet   
            HSSFSheet hssfSheet = hssfWorkbook.CreateSheet(sheetName);
            //调色板实例
            Color LevelThreeColor = Color.FromArgb(197, 217, 241);

            hssfSheet.DefaultColumnWidth = 20;
            hssfSheet.DefaultRowHeight = 10;

            //下拉列表
            CellRangeAddressList regions = new CellRangeAddressList(0, 65535, 12, 12);
            DVConstraint constraint = DVConstraint.CreateExplicitListConstraint(new string[] { "未启动", "整改中", "已完成" });
            HSSFDataValidation dataValidate = new HSSFDataValidation(regions, constraint);
            hssfSheet.AddValidationData(dataValidate);

            #region 合并单元格 

            //合并单元格 , 开始行 , 结束行 ,开始列 ,结束列
            List<CellRangeAddress> cellRange = new List<CellRangeAddress>();
            List<CellRangeAddress> cellRange2 = new List<CellRangeAddress>();
            cellRange.Add(new CellRangeAddress(0, 0, 0, 13));
            cellRange.Add(new CellRangeAddress(1, 1, 0, 8));
            cellRange.Add(new CellRangeAddress(1, 1, 9, 13));

            cellRange2.Add(new CellRangeAddress(2, 3, 0, 0));
            cellRange2.Add(new CellRangeAddress(2, 3, 1, 1));
            cellRange2.Add(new CellRangeAddress(2, 3, 2, 2));
            cellRange2.Add(new CellRangeAddress(2, 3, 3, 3));
            cellRange2.Add(new CellRangeAddress(2, 3, 4, 4));
            cellRange2.Add(new CellRangeAddress(2, 3, 5, 5));
            cellRange2.Add(new CellRangeAddress(2, 3, 6, 6));
            cellRange2.Add(new CellRangeAddress(2, 3, 7, 7));
            cellRange2.Add(new CellRangeAddress(2, 2, 8, 10));
            cellRange2.Add(new CellRangeAddress(2, 3, 11, 11));
            cellRange2.Add(new CellRangeAddress(2, 3, 12, 12));
            cellRange2.Add(new CellRangeAddress(2, 3, 13, 13));

            cellRange.Add(new CellRangeAddress(2, 3, 0, 0));
            cellRange.Add(new CellRangeAddress(2, 3, 1, 1));
            cellRange.Add(new CellRangeAddress(2, 3, 2, 2));
            cellRange.Add(new CellRangeAddress(2, 3, 3, 3));
            cellRange.Add(new CellRangeAddress(2, 3, 4, 4));
            cellRange.Add(new CellRangeAddress(2, 3, 5, 5));
            cellRange.Add(new CellRangeAddress(2, 3, 6, 6));
            cellRange.Add(new CellRangeAddress(2, 3, 7, 7));
            cellRange.Add(new CellRangeAddress(2, 2, 8, 10));
            cellRange.Add(new CellRangeAddress(2, 3, 11, 11));
            cellRange.Add(new CellRangeAddress(2, 3, 12, 12));
            cellRange.Add(new CellRangeAddress(2, 3, 13, 13));
            foreach (CellRangeAddress cell in cellRange)
            {
                hssfSheet.AddMergedRegion(cell);
            }

            #endregion

            #region 南京分行信息科技工作检查问题整改跟踪信息表

            //创建标题列头 xx分行信息科技工作检查问题整改跟踪信息表												
            HSSFRow head_1_HSSFRow = hssfSheet.CreateRow(0);
            head_1_HSSFRow.Height = 200 * 5;
            head_1_HSSFRow.CreateCell(0).SetCellValue(titleName);

            //创建样式 Style Header
            HSSFCellStyle hssfCellStyle = hssfWorkbook.CreateCellStyle();
            //创建字体 Font  Header
            HSSFFont hssfFontHead = (HSSFFont)hssfWorkbook.CreateFont();
            //字体
            hssfFontHead.FontName = "宋体";
            hssfFontHead.FontHeightInPoints = 16;
            hssfFontHead.Color = HSSFColor.BLACK.index;
            hssfCellStyle.SetFont(hssfFontHead);

            head_1_HSSFRow.GetCell(0).CellStyle = hssfCellStyle;
            #endregion

            #region 检查基本信息 、整改落实情况跟踪

            //创建标题列头 检查基本信息、	整改落实情况跟踪						
            HSSFRow head_2_HSSFRow = hssfSheet.CreateRow(1);
            head_2_HSSFRow.HeightInPoints = 20;
            head_2_HSSFRow.CreateCell(0).SetCellValue("检查基本信息");
            head_2_HSSFRow.CreateCell(9).SetCellValue("整改落实情况跟踪");
            //样式
            HSSFCellStyle hssf_2_CellStyle = hssfWorkbook.CreateCellStyle();
            HSSFFont hssf_2_FontHead = (HSSFFont)hssfWorkbook.CreateFont();
            //字体
            hssfFontHead.FontName = "宋体";
            hssfFontHead.FontHeightInPoints = 9;
            hssfFontHead.Color = HSSFColor.BLACK.index;
            hssf_2_CellStyle.SetFont(hssf_2_FontHead);

            head_2_HSSFRow.GetCell(0).CellStyle = hssf_2_CellStyle;
            head_2_HSSFRow.GetCell(9).CellStyle = hssf_2_CellStyle;

            #endregion

            #region 表列头 、 列名
            //样式
            HSSFCellStyle hssf_3_CellStyle = hssfWorkbook.CreateCellStyle();
            HSSFFont hssf_3_FontHead = (HSSFFont)hssfWorkbook.CreateFont();
            hssf_3_CellStyle.FillPattern = CellFillPattern.SOLID_FOREGROUND;
            hssf_3_CellStyle.FillBackgroundColor = GetXLColour(hssfWorkbook, LevelThreeColor);
            hssf_3_CellStyle.BorderBottom = CellBorderType.THIN;
            hssf_3_CellStyle.BorderLeft = CellBorderType.THIN;
            hssf_3_CellStyle.BorderRight = CellBorderType.THIN;
            hssf_3_CellStyle.BorderTop = CellBorderType.THIN;

            //字体
            hssf_3_FontHead.FontName = "宋体";
            hssf_3_FontHead.FontHeightInPoints = 9;
            hssf_3_FontHead.Color = HSSFColor.BLACK.index;
            hssf_3_CellStyle.SetFont(hssf_3_FontHead);

            HSSFRow content_1_HSSFRow = hssfSheet.CreateRow(2);
            content_1_HSSFRow.CreateCell(0).SetCellValue("序号");
            content_1_HSSFRow.CreateCell(1).SetCellValue("分行名称");
            content_1_HSSFRow.CreateCell(2).SetCellValue("检查开始时间");
            content_1_HSSFRow.CreateCell(3).SetCellValue("检查结束时间");
            content_1_HSSFRow.CreateCell(4).SetCellValue("检查项分类");
            content_1_HSSFRow.CreateCell(5).SetCellValue("问题性质");
            content_1_HSSFRow.CreateCell(6).SetCellValue("问题描述");
            content_1_HSSFRow.CreateCell(7).SetCellValue("检查意见");
            content_1_HSSFRow.CreateCell(8).SetCellValue("整改措施计划");
            content_1_HSSFRow.CreateCell(11).SetCellValue("推进情况");
            content_1_HSSFRow.CreateCell(12).SetCellValue("整改状态");
            content_1_HSSFRow.CreateCell(13).SetCellValue("最终完成时间");

            HSSFRow content_2_HSSFRow = hssfSheet.CreateRow(3);
            content_2_HSSFRow.CreateCell(8).SetCellValue("整改措施");
            content_2_HSSFRow.CreateCell(9).SetCellValue("整改完成标志");
            content_2_HSSFRow.CreateCell(10).SetCellValue("计划完成时间");

            content_2_HSSFRow.GetCell(8).CellStyle = hssf_3_CellStyle;
            content_2_HSSFRow.GetCell(9).CellStyle = hssf_3_CellStyle;
            content_2_HSSFRow.GetCell(10).CellStyle = hssf_3_CellStyle;
            #endregion

            #region 边框、背景颜色设置 

            //创建样式 Style Bord
            HSSFCellStyle hssfCellStyleBord = hssfWorkbook.CreateCellStyle();
            //创建字体 Font  Bord
            hssfCellStyleBord.Alignment = CellHorizontalAlignment.CENTER;
            hssfCellStyleBord.VerticalAlignment = CellVerticalAlignment.CENTER;
            hssfCellStyleBord.BorderBottom = CellBorderType.THIN;
            hssfCellStyleBord.BorderLeft = CellBorderType.THIN;
            hssfCellStyleBord.BorderRight = CellBorderType.THIN;
            hssfCellStyleBord.BorderTop = CellBorderType.THIN;
            HSSFFont hssfFontBord = (HSSFFont)hssfWorkbook.CreateFont();
            hssfFontBord.FontName = "宋体";
            hssfFontBord.FontHeightInPoints = 12;
            hssfFontBord.Color = HSSFColor.BLACK.index;
            hssfCellStyleBord.SetFont(hssfFontBord);

            //边框
            foreach (var cell in cellRange)
            {
                for (int i = cell.FirstRow; i <= cell.LastRow; i++)
                {
                    HSSFRow row = HSSFCellUtil.GetRow(i, hssfSheet);
                    for (int j = cell.FirstColumn; j <= cell.LastColumn; j++)
                    {
                        HSSFCell singleCell = HSSFCellUtil.GetCell(row, (short)j);
                        singleCell.CellStyle = hssfCellStyleBord;
                    }
                }
            }

            //创建样式 Style Bord
            HSSFCellStyle hssfCellStyleBord_2 = hssfWorkbook.CreateCellStyle();
            hssfCellStyleBord_2.Alignment = CellHorizontalAlignment.CENTER;
            hssfCellStyleBord_2.VerticalAlignment = CellVerticalAlignment.CENTER;
            hssfCellStyleBord_2.BorderBottom = CellBorderType.THIN;
            hssfCellStyleBord_2.BorderLeft = CellBorderType.THIN;
            hssfCellStyleBord_2.BorderRight = CellBorderType.THIN;
            hssfCellStyleBord_2.BorderTop = CellBorderType.THIN;
            hssfCellStyleBord_2.FillPattern = CellFillPattern.SOLID_FOREGROUND;
            hssfCellStyleBord_2.FillBackgroundColor = GetXLColour(hssfWorkbook, LevelThreeColor);

            hssfCellStyleBord_2.SetFont(hssfFontBord);

            foreach (var cell in cellRange2)
            {
                for (int i = cell.FirstRow; i <= cell.LastRow; i++)
                {
                    HSSFRow row = HSSFCellUtil.GetRow(i, hssfSheet);
                    for (int j = cell.FirstColumn; j <= cell.LastColumn; j++)
                    {
                        HSSFCell singleCell = HSSFCellUtil.GetCell(row, (short)j);
                        singleCell.CellStyle = hssfCellStyleBord_2;
                    }
                }
            }

            #endregion

            #region 数据内容

            try
            {
                #region 内容样式

                HSSFCellStyle hssf_i_CellStyle = hssfWorkbook.CreateCellStyle();
                HSSFFont hssf_i_FontHead = (HSSFFont)hssfWorkbook.CreateFont();
                hssf_i_CellStyle.Alignment = CellHorizontalAlignment.CENTER;
                hssf_i_CellStyle.VerticalAlignment = CellVerticalAlignment.CENTER;
                hssf_i_CellStyle.BorderBottom = CellBorderType.THIN;
                hssf_i_CellStyle.BorderLeft = CellBorderType.THIN;
                hssf_i_CellStyle.BorderRight = CellBorderType.THIN;
                hssf_i_CellStyle.BorderTop = CellBorderType.THIN;

                //字体
                hssf_i_FontHead.FontName = "宋体";
                hssf_i_FontHead.FontHeightInPoints = 12;
                hssf_i_FontHead.Color = HSSFColor.BLACK.index;
                hssf_i_CellStyle.SetFont(hssf_i_FontHead);
                hssf_i_CellStyle.IsLocked = false;
                #endregion
                if (dataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        HSSFRow dataHSSFRow = hssfSheet.CreateRow(i + 4);
                        dataHSSFRow.HeightInPoints = 20;
                        dataHSSFRow.CreateCell(0).SetCellValue(dataTable.Rows[i][0].ToString());
                        dataHSSFRow.CreateCell(1).SetCellValue(dataTable.Rows[i][1].ToString());
                        dataHSSFRow.CreateCell(2).SetCellValue(dataTable.Rows[i][2].ToString());
                        dataHSSFRow.CreateCell(3).SetCellValue(dataTable.Rows[i][3].ToString());
                        dataHSSFRow.CreateCell(4).SetCellValue(dataTable.Rows[i][4].ToString());
                        dataHSSFRow.CreateCell(5).SetCellValue(dataTable.Rows[i][5].ToString());
                        dataHSSFRow.CreateCell(6).SetCellValue(dataTable.Rows[i][6].ToString());
                        dataHSSFRow.CreateCell(7).SetCellValue(dataTable.Rows[i][7].ToString());

                        dataHSSFRow.CreateCell(8).SetCellValue(string.Empty);
                        dataHSSFRow.CreateCell(9).SetCellValue(string.Empty);
                        dataHSSFRow.CreateCell(10).SetCellValue(string.Empty);
                        dataHSSFRow.CreateCell(11).SetCellValue(string.Empty);
                        dataHSSFRow.CreateCell(12).SetCellValue(string.Empty);
                        dataHSSFRow.CreateCell(13).SetCellValue(string.Empty);
                        for (int j = 0; j < 14; j++)
                        {
                            dataHSSFRow.GetCell(j).CellStyle = hssf_i_CellStyle;
                        }
                    }
                }
                hssfSheet.ProtectSheet("21");
            }
            catch (Exception ex)
            {

                throw new Exception("导出 Excel 错误 , " + ex.Message);
            }

            #endregion

            //保存文件
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            {
                hssfWorkbook.Write(fileStream);
            }
        }

        /// <summary>
        /// 背景颜色
        /// </summary>
        /// <param name="workbook">HSSFWorkbook</param>
        /// <param name="SystemColour">Color</param>
        /// <returns>short</returns>
        private static short GetXLColour(HSSFWorkbook workbook, System.Drawing.Color SystemColour)
        {
            short s = 0;
            HSSFPalette XlPalette = workbook.GetCustomPalette();
            HSSFColor XlColour = XlPalette.FindColor(SystemColour.R, SystemColour.G, SystemColour.B);
            if (XlColour == null)
            {
                if (NPOI.HSSF.Record.PaletteRecord.STANDARD_PALETTE_SIZE < 255)
                {
                    if (NPOI.HSSF.Record.PaletteRecord.STANDARD_PALETTE_SIZE < 64)
                    {
                        NPOI.HSSF.Record.PaletteRecord.STANDARD_PALETTE_SIZE = 64;
                        NPOI.HSSF.Record.PaletteRecord.STANDARD_PALETTE_SIZE += 1;
                        XlColour = XlPalette.AddColor(SystemColour.R, SystemColour.G, SystemColour.B);
                    }
                    else
                    {
                        XlColour = XlPalette.FindSimilarColor(SystemColour.R, SystemColour.G, SystemColour.B);
                    }

                    s = XlColour.GetIndex();
                }

            }
            else
                s = XlColour.GetIndex();

            return s;
        }

        #endregion

        #region 解析Excle，返回DataTable

        /// <summary>
        /// Excel，解析，通过message判断是否有错
        /// </summary>
        /// <param name="path">excel路径</param>
        /// <param name="columns">列名</param>
        /// <param name="message">错误消息</param>
        /// <returns>DataTable</returns>
        public static DataTable LoadExcel(string path, string columns, out string message)
        {
            message = string.Empty;
            string selectSQL = @"select " + columns + " from {0}";
            string connString = string.Empty;
            if (path.EndsWith(".xls"))
            {
                connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'";
            }
            else
            {
                connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
            }
            string tableName = GetExcelFirstTableName(connString, out message);
            if (message != string.Empty) { return null; }
            selectSQL = string.Format(selectSQL, "[" + tableName + "]");
            OleDbConnection oleConn = new OleDbConnection(connString);
            OleDbCommand oleCmd = new OleDbCommand(selectSQL, oleConn);
            OleDbDataAdapter oleAdapter = new OleDbDataAdapter();
            oleAdapter.SelectCommand = oleCmd;
            DataTable dataTable = new DataTable();
            try
            {
                oleConn.Open();
                oleAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                message = "解析错误 : " + System.Environment.NewLine + ex.Message;
            }
            finally { oleConn.Close(); }
            return dataTable;
        }

        /// <summary>
        /// Excel 读取第一个工作单名称
        /// </summary>
        /// <param name="oleConn">连接字符串</param>
        /// <returns>string</returns>
        private static string GetExcelFirstTableName(string oleConn, out string message)
        {
            message = string.Empty;
            string tableName = string.Empty;
            OleDbConnection oledbConn = new OleDbConnection(oleConn);
            try
            {
                oledbConn.Open();
                DataTable dt = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                tableName = dt.Rows[0][2].ToString().Trim();
            }
            catch (Exception ex)
            {
                message = "读取第一个工作单名称 : " + System.Environment.NewLine + ex.Message;
            }
            return tableName;
        }

        #endregion

        /// <summary>
        /// NPOI Excel转成DataTable
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="sheetName">sheet名称</param>
        /// <param name="isFirstRowColumn">第一行是否为列名</param>
        /// <param name="Message">错误</param>
        /// <returns>DataTable</returns>
        public static DataTable ExcelToDataTable(string fileName, string sheetName, bool isFirstRowColumn, out string Message)
        {
            HSSFWorkbook workbook = null;
            HSSFSheet sheet = null;
            FileStream fs = null;
            DataTable data = new DataTable();
            Message = string.Empty;
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new HSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    HSSFRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            HSSFCell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else { startRow = sheet.FirstRowNum; }

                    //最后一列
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        HSSFRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Message = "解析Excel错误:" + ex.Message;
                return new DataTable();
            }
        }

    }

}

