//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using HD.Framework.Helper;

namespace HD.Framework.Utils
{
    /// <summary>
    /// 文件操作类
    /// </summary>
    public class FileUtils
    {
        #region "文 件 夹"
        /// <summary>
        /// 判断文件夹是否存在
        /// </summary>
        /// <param name="Path">文件夹绝对路径</param>
        /// <returns></returns>
        public static bool FolderExists(string Path)
        {
            return Directory.Exists(Path);
        }
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="Path">文件夹绝对路径</param>
        public static void CreateFolder(string Path)
        {
            if (Path.Length == 0) return;
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
        }
        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="Path"></param>
        public static void DeleteFolder(string Path)
        {
            if (Path.Length == 0) return;
            if (Directory.Exists(Path))
            {
                Directory.Delete(Path, true);
            }
        }
        /// <summary>
        /// 递归拷贝文件内容
        /// </summary>
        /// <param name="FormPath">原始文件夹</param>
        /// <param name="ToPath">目标文件夹</param>
        public static void CopyFolder(string FormPath, string ToPath)
        {
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加之
                if (ToPath[ToPath.Length - 1] != Path.DirectorySeparatorChar)
                    ToPath += Path.DirectorySeparatorChar;
                // 判断目标目录是否存在如果不存在则新建之
                if (!Directory.Exists(ToPath)) Directory.CreateDirectory(ToPath);
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                // 如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
                // string[] fileList = Directory.GetFiles(srcPath);
                string[] fileList = Directory.GetFileSystemEntries(FormPath);
                // 遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                    if (Directory.Exists(file))
                        CopyFolder(file, ToPath + Path.GetFileName(file));
                    // 否则直接Copy文件
                    else
                        File.Copy(file, ToPath + Path.GetFileName(file), true);
                }
            }
            catch(Exception ex)
            {
                string LogContent = string.Format("复制文件出错，原始文件夹{0}到{1}，错误详细：{2}", FormPath, ToPath, ex.ToString());
                LogHelper.WriteLog(LogContent);
            }
        }
        #endregion
        #region "文件操作"
        /// <summary>
        /// 获得文件编码
        /// </summary>
        /// <param name="FileName">文件目录</param>
        /// <returns></returns>
        public static Encoding GetEncoding(string FileName)
        {
            Encoding Encode = Encoding.Default;
            using (FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    Byte[] buffer = br.ReadBytes(2);
                    if (buffer[0] >= 0xEF)
                    {
                        if (buffer[0] == 0xEF && buffer[1] == 0xBB)
                        {
                            Encode = Encoding.UTF8;
                        }
                        else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
                        {
                            Encode = Encoding.BigEndianUnicode;
                        }
                        else if (buffer[0] == 0xFF && buffer[1] == 0xFE)
                        {
                            Encode = Encoding.Unicode;
                        }
                    }
                }
            }
            return Encode;
        }
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static bool FileExists(string FileName)
        {
            return File.Exists(FileName);
        }
        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="FileName">文件绝对路径</param>
        /// <returns></returns>
        public static string ReadFile(string FileName)
        {
            try
            {
                using (StreamReader sr = new StreamReader(FileName, GetEncoding(FileName)))
                {
                    string str = sr.ReadToEnd();
                    sr.Close();
                    return str;
                }               
            }
            catch (Exception ex)
            {
                string LogContent = string.Format("找不到相关文件{0}，错误详细：{1}", FileName, ex.ToString());
                LogHelper.WriteLog(LogContent);
                return "";
            }
        }
        /// <summary>
        /// 保存文件内容
        /// </summary>
        /// <param name="FileName">文件绝对路径</param>
        /// <returns></returns>
        public static void SaveFile(string Str, string FileName)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(FileName, false, GetEncoding(FileName)))
                {
                    sw.Write(Str);
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                string LogContent = string.Format("保存文件失败，文件路径：{0}，文件内容:{1}，错误详细：{2}", FileName, Str, ex.ToString());
                LogHelper.WriteLog(LogContent);
            }
        }
        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="FormPath">原始文件</param>
        /// <param name="ToPath">模板文件</param>
        public static void CopyFile(string FormPath, string ToPath)
        {
            if (FileExists(FormPath))
            {
                SaveFile(ReadFile(FormPath), ToPath);
            }
            else
            {
                LogHelper.WriteLog(string.Format("复制文件出错,源文件{0}不存在。", FormPath));
            }
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="File"></param>
        public static void DeleteFile(string File)
        {
            if (File.Length == 0) return;
            if (System.IO.File.Exists(File))
            {
                System.IO.File.Delete(File);
            }
        }
        #endregion
        #region "目录列表"
        /// <summary>
        /// 获取图片列表
        /// </summary>
        /// <param name="FolderName">图片文件夹</param>
        /// <returns></returns>
        public static DataTable GetAllIcons(string FolderName)
        {
            DataTable Dt = new DataTable();
            Dt.Columns.Add(new DataColumn("FileName", typeof(String)));
            Dt.Columns.Add(new DataColumn("FileSize", typeof(Int32)));
            Dt.Columns.Add(new DataColumn("FileCreateTime", typeof(DateTime)));

            DirectoryInfo Dir = new DirectoryInfo(FolderName);
            FileInfo[] Files = Dir.GetFiles("*.png");
            DateSorter.QuickSort(Files, 0, Files.Length - 1);//按时间排序
            foreach (FileInfo f in Files)
            {
                DataRow Row = Dt.NewRow();
                Row[0] = f.ToString();
                Row[1] = f.Length;
                Row[2] = f.LastAccessTime.ToString("yyyy-MM-dd HH:mm:ss");
                Dt.Rows.Add(Row);
            }
            return Dt;
        }
        #endregion
        #region "获取指定格式的文件列表"
        /// <summary>
        /// 获取指定格式的文件列表
        /// </summary>
        /// <param name="FilePath">绝对路径</param>
        /// <param name="FileType">文件格式，例如：*.aspx</param>
        /// <returns></returns>
        public static DataTable GetAllFolder(string FilePath, string FileType)
        {
            DataTable Dt = new DataTable();
            Dt.Columns.Add(new DataColumn("FileName", typeof(String)));
            Dt.Columns.Add(new DataColumn("FileSize", typeof(Int32)));
            Dt.Columns.Add(new DataColumn("FileCreateTime", typeof(DateTime)));
            Dt = GetAllFolder(FilePath, Dt, FilePath, FileType);
            return Dt;
        }
        /// <summary>
        /// 获取指定格式的文件列表
        /// </summary>
        /// <param name="dirPath">当前路径</param>
        /// <param name="Dt"></param>
        /// <param name="ReplacePath">替换路径</param>
        /// <param name="FileType">文件格式</param>
        /// <returns></returns>
        private static DataTable GetAllFolder(string dirPath, DataTable Dt, string ReplacePath, string FileType)
        {
            DirectoryInfo Dir = new DirectoryInfo(dirPath);
            foreach (FileInfo f in Dir.GetFiles(FileType))
            {
                string FileName = Dir + f.ToString();
                //FileName = BasePage.InstallDir + FileName.Replace(HttpContext.Current.Server.MapPath("~/"), "").Replace("\\", "/");
                //Edit By Apollo/2010-07-02
                //FileName = FileName.Replace(HttpContext.Current.Server.MapPath("~/"), "").Replace("\\", "/");
                DataRow Row = Dt.NewRow();
                Row[0] = FileName;
                Row[1] = f.Length;
                Row[2] = f.LastAccessTime.ToString("yyyy-MM-dd HH:mm:ss");
                Dt.Rows.Add(Row);
            }
            foreach (DirectoryInfo d in Dir.GetDirectories())
            {
                Dt = GetAllFolder(Dir + d.ToString() + "\\", Dt, ReplacePath, FileType);
            }
            return Dt;
        }
        #endregion
    }
}