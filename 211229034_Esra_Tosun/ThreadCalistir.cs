﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _211229034_Esra_Tosun
{
    public class ThreadCalistir
    {
        List<int> sayilarListesi = new List<int>();
        List<int> sayilarListesi1 = new List<int>();
        List<int> sayilarListesi2 = new List<int>();
        List<int> sayilarListesi3 = new List<int>();
        List<int> sayilarListesi4 = new List<int>();

        public static BindingList<int> ciftSayilarList = new BindingList<int>();
        public static BindingList<int> tekSayilarList = new BindingList<int>();
        public static BindingList<int> asalSayilarList = new BindingList<int>();

        private static object kilit1 = new object();
        private static object kilit2 = new object();
        private static object kilit3 = new object();

        private Stopwatch stopwatch1 = new Stopwatch(); // Her bir thread'in süresini ölçmek için bir kronometre
        private Stopwatch stopwatch2 = new Stopwatch();
        private Stopwatch stopwatch3 = new Stopwatch();
        private Stopwatch stopwatch4 = new Stopwatch();

        public void ThreadleriCalistir()
        {
            // List<int> oluştur

            // 1'den 1.000.000'e kadar olan sayıları ekle
            for (int i = 1; i <= 1000000; i++)
            {
                sayilarListesi.Add(i);
            }

            SayilarListesiBol();
            Console.WriteLine(sayilarListesi1.Count);
            Console.WriteLine(sayilarListesi2.Count);
            Console.WriteLine(sayilarListesi3.Count);
            Console.WriteLine(sayilarListesi4.Count);



            Thread thread1 = new Thread(new ThreadStart(Sayi));
            Thread thread2 = new Thread(new ThreadStart(Sayi2));
            Thread thread3 = new Thread(new ThreadStart(Sayi3));
            Thread thread4 = new Thread(new ThreadStart(Sayi4));


            // Thread önceliklerini belirle, istenilen sıraya göre
            thread1.Priority = ThreadPriority.Highest;    // 1. en yüksek öncelik
            thread2.Priority = ThreadPriority.AboveNormal; // 2. üst normal öncelik
            thread3.Priority = ThreadPriority.BelowNormal; // 3. alt normal öncelik
            thread4.Priority = ThreadPriority.Lowest;      // 4. en düşük öncelik



            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();

            stopwatch1.Reset();
            stopwatch2.Reset();
            stopwatch3.Reset();
            stopwatch4.Reset();

            stopwatch1.Start(); // Kronometreyi başlat
            stopwatch2.Start(); // Kronometreyi başlat
            stopwatch3.Start(); // Kronometreyi başlat
            stopwatch4.Start(); // Kronometreyi başlat

            thread1.Join();
            thread2.Join();
            thread3.Join();
            thread4.Join();

            // Thread'lerin işi tamamlandığında güvenli bir şekilde sonlandırın
            SafeAbortThread(thread1);
            SafeAbortThread(thread2);
            SafeAbortThread(thread3);
            SafeAbortThread(thread4);

            Console.WriteLine(ciftSayilarList.Count);
            //ListeYazdir(asalSayilarList);



            GC.Collect();
        }

        static void SafeAbortThread(Thread thread)
        {
            if (thread != null && thread.IsAlive)
            {
                thread.Abort();
                Console.WriteLine($"Thread {thread.ManagedThreadId} güvenli bir şekilde sonlandırıldı.");
            }
        }

        public void SayilarListesiBol()
        {
            for (int i = 0; i < sayilarListesi.Count / 4; i++)
            {
                sayilarListesi1.Add(i);
            }
            for (int i = sayilarListesi.Count / 4; i < sayilarListesi.Count / 4 * 2; i++)
            {
                sayilarListesi2.Add(i);
            }
            for (int i = sayilarListesi.Count / 4 * 2; i < sayilarListesi.Count / 4 * 3; i++)
            {
                sayilarListesi3.Add(i);
            }
            for (int i = sayilarListesi.Count / 4 * 3; i < sayilarListesi.Count / 4 * 4; i++)
            {
                sayilarListesi4.Add(i);
            }
        }

        public void Sayi()
        {
            int kontrol;
            for (int i = 0; i < sayilarListesi1.Count; i++)
            {
                kontrol = 0;
                if (sayilarListesi1[i] % 2 == 0)
                {
                    lock (kilit1)
                    {
                        ciftSayilarList.Add(sayilarListesi1[i]);
                    }
                }
                else if (sayilarListesi1[i] % 2 != 0)
                {
                    lock (kilit2)
                    {
                        tekSayilarList.Add(sayilarListesi1[i]);
                    }
                }
                for (int k = 2; k <= sayilarListesi1[i] / 2; k++)
                {
                    if (sayilarListesi1[i] % k == 0)
                    {
                        kontrol++;
                        break;
                    }
                }
                if (kontrol == 0 & sayilarListesi1[i] != 0 & sayilarListesi1[i] != 1)
                {
                    lock (kilit3)
                    {
                        asalSayilarList.Add(sayilarListesi1[i]);
                    }
                }
                //Console.WriteLine("Thread 1 => " + i);
            }
            stopwatch1.Stop(); // Kronometreyi durdur
            TimeSpan elapsed = stopwatch1.Elapsed; // Geçen süreyi al

            // Elapsed.TotalSeconds, geçen süreyi saniye cinsinden verir
            //UpdateTextBox(threadName + " için geçen süre: " + elapsed.TotalSeconds.ToString("F2") + " saniye");
            // Elapsed.TotalSeconds, geçen süreyi saniye cinsinden verir
            Console.WriteLine($"Thread1 için geçen süre: {elapsed.TotalSeconds.ToString("F2")} saniye");
        }

        public void Sayi2()
        {
            int kontrol;
            for (int i = 0; i < sayilarListesi2.Count; i++)
            {
                kontrol = 0;
                if (sayilarListesi2[i] % 2 == 0)
                {
                    lock (kilit1)
                    {
                        ciftSayilarList.Add(sayilarListesi2[i]);
                    }
                }
                else if (sayilarListesi2[i] % 2 != 0)
                {
                    lock (kilit2)
                    {
                        tekSayilarList.Add(sayilarListesi2[i]);
                    }
                }
                for (int k = 2; k <= sayilarListesi2[i] / 2; k++)
                {
                    if (sayilarListesi2[i] % k == 0)
                    {
                        kontrol++;
                        break;
                    }
                }
                if (kontrol == 0 & sayilarListesi2[i] != 0 & sayilarListesi2[i] != 1)
                {
                    lock (kilit3)
                    {
                        asalSayilarList.Add(sayilarListesi2[i]);
                    }
                }
                //Console.WriteLine("Thread 2 => " + i);
            }
            stopwatch2.Stop(); // Kronometreyi durdur
            TimeSpan elapsed = stopwatch2.Elapsed; // Geçen süreyi al

            // Elapsed.TotalSeconds, geçen süreyi saniye cinsinden verir
            //UpdateTextBox(threadName + " için geçen süre: " + elapsed.TotalSeconds.ToString("F2") + " saniye");
            Console.WriteLine($"Thread2 için geçen süre: {elapsed.TotalSeconds.ToString("F2")} saniye");
        }

        public void Sayi3()
        {
            int kontrol;
            for (int i = 0; i < sayilarListesi3.Count; i++)
            {
                kontrol = 0;
                if (sayilarListesi3[i] % 2 == 0)
                {
                    lock (kilit1)
                    {
                        ciftSayilarList.Add(sayilarListesi3[i]);
                    }
                }
                else if (sayilarListesi3[i] % 2 != 0)
                {
                    lock (kilit2)
                    {
                        tekSayilarList.Add(sayilarListesi3[i]);
                    }
                }
                for (int k = 2; k <= sayilarListesi3[i] / 2; k++)
                {
                    if (sayilarListesi3[i] % k == 0)
                    {
                        kontrol++;
                        break;
                    }
                }
                if (kontrol == 0 & sayilarListesi3[i] != 0 & sayilarListesi3[i] != 1)
                {
                    lock (kilit3)
                    {
                        asalSayilarList.Add(sayilarListesi3[i]);
                    }
                }
                //Console.WriteLine("Thread 3 => " + i);
            }
            stopwatch3.Stop(); // Kronometreyi durdur
            TimeSpan elapsed = stopwatch3.Elapsed; // Geçen süreyi al

            // Elapsed.TotalSeconds, geçen süreyi saniye cinsinden verir
            //UpdateTextBox(threadName + " için geçen süre: " + elapsed.TotalSeconds.ToString("F2") + " saniye");
            Console.WriteLine($"Thread3 için geçen süre: {elapsed.TotalSeconds.ToString("F2")} saniye");
        }

        public void Sayi4()
        {
            int kontrol;
            for (int i = 0; i < sayilarListesi4.Count; i++)
            {
                kontrol = 0;
                if (sayilarListesi4[i] % 2 == 0)
                {
                    lock (kilit1)
                    {
                        ciftSayilarList.Add(sayilarListesi4[i]);
                    }
                }
                else if (sayilarListesi1[i] % 2 != 0)
                {
                    lock (kilit2)
                    {
                        tekSayilarList.Add(sayilarListesi4[i]);
                    }
                }
                for (int k = 2; k <= sayilarListesi4[i] / 2; k++)
                {
                    if (sayilarListesi4[i] % k == 0)
                    {
                        kontrol++;
                        break;
                    }
                }
                if (kontrol == 0 & sayilarListesi4[i] != 0 & sayilarListesi4[i] != 1)
                {
                    lock (kilit3)
                    {
                        asalSayilarList.Add(sayilarListesi4[i]);
                    }
                }
                //Console.WriteLine("Thread 4 => " + i);
            }
            stopwatch4.Stop(); // Kronometreyi durdur
            TimeSpan elapsed = stopwatch4.Elapsed; // Geçen süreyi al

            // Elapsed.TotalSeconds, geçen süreyi saniye cinsinden verir
            //UpdateTextBox(threadName + " için geçen süre: " + elapsed.TotalSeconds.ToString("F2") + " saniye");
            Console.WriteLine($"Thread4 için geçen süre: {elapsed.TotalSeconds.ToString("F2")} saniye");
        }

        public void ListeYazdir(List<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }
        }


    }
}
