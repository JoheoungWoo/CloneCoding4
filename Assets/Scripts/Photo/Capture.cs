using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Text.RegularExpressions;

public class Capture : MonoBehaviour
{
    public IEnumerator Rendering(Action<bool> action)
    {
        yield return new WaitForEndOfFrame();
        Time.timeScale = 0.0f;

        string path = Application.persistentDataPath;

        DirectoryInfo directory = new DirectoryInfo(path);

        int pngCount;

        try
        {
            FileInfo[] files = directory.GetFiles();

            var numbers = files
                        .Select(file =>
                        {
                            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.Name);
                            var match = Regex.Match(fileNameWithoutExtension, @"\d+$"); // ���� �̸��� �� �κп��� ���ڸ� ã��
                            return match.Success ? (int?)int.Parse(match.Value) : null;
                        })
                        .Where(num => num.HasValue)  // null �� ����
                        .Select(num => num.Value)    // int?�� int�� ��ȯ
                        .OrderByDescending(num => num) // ������������ ����
                        .ToList();


            pngCount = numbers[0] + 1;
        } 
        catch(Exception e)
        {
            pngCount = 1;
            Debug.Log(e);
        }


        byte[] imgBytes;
        path += $"/picture{pngCount}.png";

        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
        texture.Apply();

        imgBytes = texture.EncodeToPNG();
        File.WriteAllBytes(path, imgBytes);
        Debug.Log($"{path} has been saved");
        Time.timeScale = 1.0f;

        action?.Invoke(true);
        yield return new WaitForEndOfFrame();
    }
}

