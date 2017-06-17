using System;
using System.IO;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;

public static class FileWriter
{
    #region Read

    public static string Read(string fileName)
    {
        string path = Path.Combine(Application.dataPath, fileName);
        FileInfo fileInfo = new FileInfo(path);

        Debug.Log(File.ReadAllText(path, Encoding.UTF8));
        if (fileInfo == null || fileInfo.Exists == false)
            return null;
        return File.ReadAllText(path, Encoding.UTF8);
    }

    #endregion

    #region Write

    private static void Write(string fileName, string text)
    {
        string path = Path.Combine(Application.dataPath, fileName);
        File.WriteAllText(path, text);
        Debug.Log("Succ");
    }

    public static void Write(string fileName, object obj)
    {
        string serialized = JsonConvert.SerializeObject(obj);
        Write(fileName, serialized);
        Debug.Log(serialized);
    }
    #endregion

    /// <summary>
    /// Удалить файл если такой имеется
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static bool Remove(string fileName)
    {
        if (FileExists(fileName))
        {
            try
            {
                File.Delete(GetFilePath(fileName));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Полный путь файла
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="checkFileExists">проверить есть ли такой файл</param>
    /// <returns></returns>
    public static string GetFilePath(string fileName, bool checkFileExists = false)
    {
        var path = String.Format("{0}/{1}", Application.persistentDataPath, fileName);

        if (checkFileExists)
        {
            return FileExists(fileName) ? path : String.Empty;
        }
        else
        {
            return path;
        }

    }

    /// <summary>
    /// Существует ли такой файл
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static bool FileExists(string fileName)
    {
        var path = String.Format("{0}/{1}", Application.persistentDataPath, fileName);
        FileInfo fileInfo = new FileInfo(path);
        return (fileInfo != null && fileInfo.Exists);
    }

}
