using System;
using System.IO;
using System.Diagnostics;

namespace VideoPart
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Introduce Path of the your Video -> ");
            var pathVideo = Console.ReadLine();

            Console.WriteLine("Introduce first point (format 00:00:00) -> ");
            var firstPoint = Console.ReadLine();

            Console.WriteLine("Introduce second point (format 00:00:00) -> ");
            var secondPoint = Console.ReadLine();

            PerformingVideo(pathVideo, firstPoint, secondPoint);
        }

        private static void PerformingVideo(string path, string firstPoint, string secondPoint)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine(currentDirectory.ToString());
            Process newVideo = new Process();
            newVideo.StartInfo.FileName = "ffmpeg";
            newVideo.StartInfo.Arguments = "-i " + path + " -ss " + firstPoint + " -t " + secondPoint + " -c copy " + currentDirectory + "\\OutPutVideo.mp4";
            newVideo.StartInfo.RedirectStandardError = true;
            newVideo.StartInfo.UseShellExecute = false;
            newVideo.Start();
        }
    }
}
