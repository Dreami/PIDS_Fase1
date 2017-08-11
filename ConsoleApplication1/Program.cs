using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string allfiles = @"C:\Users\maple\Documents\9° Semester\CS13309_Archivos_HTML\Files";
            DirectoryInfo d = new DirectoryInfo(allfiles);
            FileInfo[] Files = d.GetFiles("*.html");

            string output_path1 = @"C:\Users\maple\Documents\9° Semester\CS13309_Archivos_HTML\a1_matricula.txt";
            string output_path2 = @"C:\Users\maple\Documents\9° Semester\CS13309_Archivos_HTML\a2_matricula.txt";
            string output_path3 = @"C:\Users\maple\Documents\9° Semester\CS13309_Archivos_HTML\a3_matricula.txt";

            string output = "";
            // ------------PARTE 1
            var watch = System.Diagnostics.Stopwatch.StartNew();
            
            foreach (FileInfo file in Files)
            {
                var watchEach = System.Diagnostics.Stopwatch.StartNew();
                output = file.Name + "\t" + watchEach.Elapsed.TotalMilliseconds.ToString() + " ms";
                Console.WriteLine(output);
                watchEach.Stop();
                output_print(output_path1, output);
            }
            output = "Tiempo total en abrir los archivos: " + watch.Elapsed.TotalSeconds.ToString() + " s";
            Console.WriteLine(output);
            output_print(output_path1, output);
            watch.Stop();
            output = "Tiempo de ejecución: " + watch.Elapsed.TotalSeconds.ToString() + " s";
            Console.WriteLine(output);
            output_print(output_path1, output);

            Console.ReadLine();

            // ------------PARTE 2
            
            watch.Restart();
            string outputNoTags = "";
            foreach (FileInfo file in Files)
            {
                var watchEach = System.Diagnostics.Stopwatch.StartNew();
                string htmlContent = File.ReadAllText(file.FullName);
                htmlContent = Regex.Replace(htmlContent, "<.*?>", String.Empty);
                outputNoTags = allfiles + "\\" + file.Name;
                outputNoTags = outputNoTags.Replace(".html", ".txt");
                output_print(outputNoTags, htmlContent);
                watchEach.Stop();
                output = htmlContent + "\n Todos los tags quitados en : " + watchEach.Elapsed.TotalSeconds.ToString() +
                    " s \n--------------------------";
                Console.WriteLine(output);
                output_print(output_path2, output);
            }
            output = "Tiempo total en eliminar las etiquetas HTML: " + watch.Elapsed.TotalSeconds.ToString() + " s";
            Console.WriteLine(output);
            output_print(output_path2, output);
            watch.Stop();
            output = "Tiempo de ejecución: " + watch.Elapsed.TotalSeconds.ToString() + " s";
            Console.WriteLine(output);
            output_print(output_path2, output);

            Console.ReadLine();

            // ------------PARTE 3
            
            Files = d.GetFiles("*.txt");
            string pattern = "([A-Za-z?]&(.?)(acute;|tilde;)+[^\\s\\.\\,\\?=#?]+)";

            List<string> specialWords = new List<string>();
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);

            watch.Restart();
            foreach (FileInfo file in Files)
            {
                var watchEach = System.Diagnostics.Stopwatch.StartNew();
                string txtContent = File.ReadAllText(file.FullName);
                txtContent.Trim();
                Match m = r.Match(txtContent);
                for (int i = 0; i < m.Length; i++)
                    specialWords.Add(m.Groups[i].ToString());
                foreach (string word in specialWords)
                {
                    Console.WriteLine(word);
                    output_print(output_path3, word);
                }
                
                watchEach.Stop();
                output = "\nTiempo en encontrar palabras con caracteres especiales: " + watchEach.Elapsed.TotalSeconds.ToString() +
                    " s \n--------------------------";
                Console.WriteLine(output);
                output_print(output_path3, output);
            }
            output = "Tiempo en crear nuevo archivo: " + watch.Elapsed.TotalSeconds.ToString() + " s";
            Console.WriteLine(output);
            output_print(output_path3, output);
            watch.Stop();
            output = "Tiempo de ejecución: " + watch.Elapsed.TotalSeconds.ToString() + " s";
            Console.WriteLine(output);
            output_print(output_path3, output);
            Console.Read();
        }

        public static void output_print(String output_path, String output)
        {
            try
            {
                if (!File.Exists(output_path))
                {
                    using (var stream = File.Create(output_path));
                    TextWriter tw = new StreamWriter(output_path, false);
                    tw.WriteLine(output);
                    tw.Close();
                }
                else if (File.Exists(output_path))
                {
                    using (var tw = new StreamWriter(output_path, true))
                    {
                        tw.WriteLine(output);
                        tw.Close();
                    }
                }
            }
            catch (DirectoryNotFoundException directoryExc)
            {
                Console.WriteLine(directoryExc.StackTrace);
            }
            catch (IOException ioExc) {
                Console.WriteLine(ioExc.StackTrace);
            }
            catch (UnauthorizedAccessException unauthorizedExc)
            {
                Console.WriteLine(unauthorizedExc.StackTrace);
            }
        }
    }
}
