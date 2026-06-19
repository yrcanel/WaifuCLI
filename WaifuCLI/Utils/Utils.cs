using System;
using System.Collections.Generic;
using System.Text;
using WaifuCLI.Core.Models;

namespace WaifuCLI.Utils
{
    static class Utils
    {
        public static async Task PrintTagsAsync(Tag[] tags)
        {
            foreach (Tag tag in tags)
            {
                Console.WriteLine(tag.ToString());
            }
        }
        public static async Task StartSpinner(string message, CancellationToken token)
        {
            string[] frames = { "/", "-", "\\", "|" };
            int counter = 0;
            while (!token.IsCancellationRequested)
            {

                Console.Write($"\r{frames[counter % frames.Length]} {message}");
                counter++;
                try
                {
                    await Task.Delay(75, token);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }

        }
    }
}
