
using CsvHelper.Configuration;
using CsvHelper;
using System.Diagnostics;
using System.Globalization;
using CsvHelper.Configuration.Attributes;
using System.Data.SqlClient;
//using Lab1.Models;

namespace Lab1
{
    public class Program
    {
        static string connectionString = @"Server=LOCALHOST\LOCALDATABASE;User ID=ADMIN;Password=cisco123;Encrypt=False;TrustServerCertificate=True;Database=lab1";
        static List<Kody> imported_kody = new List<Kody>();

        static async Task Main(string[] args)
        {

            Stopwatch stopwatch = new Stopwatch();
            //Lab1Context _context = new Lab1Context();



            stopwatch.Start();
            await importCSV();
            stopwatch.Stop();
            Console.WriteLine($"The import was performed for {stopwatch.Elapsed}");
            stopwatch.Restart();

            //const int samples = 10;
            //Stopwatch meanRecordStopwatch = new Stopwatch();
            //TimeSpan recordTimeSpan = new TimeSpan();
            //TimeSpan sampleTimeSpan = new TimeSpan();

            //for (int i = 1; i <= samples; i++)
            //{
            //    await ClearTable();

            //    stopwatch.Start();
            //    foreach (var record in imported_kody)
            //    {
            //        meanRecordStopwatch.Start();
            //        await SaveOneRecord(record);
            //        meanRecordStopwatch.Stop();

            //        recordTimeSpan += meanRecordStopwatch.Elapsed;
            //        meanRecordStopwatch.Restart();
            //    }
            //    stopwatch.Stop();
            //    sampleTimeSpan += stopwatch.Elapsed;
            //    Console.WriteLine($"Average amount of time to complete a cycle {stopwatch.Elapsed}");

            //    stopwatch.Restart();

            //}

            //sampleTimeSpan = sampleTimeSpan.Divide(samples);
            //recordTimeSpan = recordTimeSpan.Divide(imported_kody.Count * samples);
            //Console.WriteLine($"Mean Saving by one Record Time is {sampleTimeSpan}");
            //Console.WriteLine($"Mean Record Saving Time is {recordTimeSpan}\n");


            //await SaveAllCollection(imported_kody);

            //await EFsaveOneRecord(imported_kody, _context);

            //await DappersaveOneRecord(imported_kody, kodyPocztoweRepository);

            //await DapperSaveAll(imported_kody, kodyPocztoweRepository);

            //await BulkCopySaveAll(imported_kody);


        }

        static async Task importCSV()
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ";",
                MemberTypes = MemberTypes.Properties,
                HeaderValidated = null,
                MissingFieldFound = null,
            };

            using (var reader = new StreamReader("kody.csv"))
            using (var csv = new CsvReader(reader, csvConfig))
            {
                imported_kody = csv.GetRecords<Kody>().ToList();
                Console.WriteLine($"Records Imported: {imported_kody.Count}");
            }
        }



        //ADO.NET
        static async Task SaveOneRecord(Kody kody)
        {
            string sqlExpression = "INSERT INTO Kody_Pocztowe (Kod_pocztowy, Adres, Miejscowosc, Wojewodztwo, Powiat) Values (@Kod_pocztowy, @Adres, @Miejscowosc, @Wojewodztwo, @Powiat)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                SqlParameter kod_pocztowyParameter = new SqlParameter("@Kod_pocztowy", kody.kod_pocztowy);
                SqlParameter adresParameter = new SqlParameter("@Adres", kody.adres);
                SqlParameter miejscowoscParameter = new SqlParameter("@Miejscowosc", kody.miejscowosc);
                SqlParameter wojewodztwoParameter = new SqlParameter("@Wojewodztwo", kody.wojewodztwo);
                SqlParameter powiatParameter = new SqlParameter("@Powiat", kody.powiat);

                command.Parameters.Add(kod_pocztowyParameter);
                command.Parameters.Add(adresParameter);
                command.Parameters.Add(miejscowoscParameter);
                command.Parameters.Add(wojewodztwoParameter);
                command.Parameters.Add(powiatParameter);

                await command.ExecuteNonQueryAsync();
            }

        }

        //SaveAllCollection
        //static async Task SaveAllCollection(List<Kody> kody)
        //{
        //    const int samples = 10;
        //    Stopwatch stopwatch = new Stopwatch();

        //    Stopwatch meanRecordStopwatch = new Stopwatch();
        //    TimeSpan recordTimeSpan = new TimeSpan();
        //    TimeSpan sampleTimeSpan = new TimeSpan();

        //    string sqlExpression = "INSERT INTO Kody_Pocztowe (Kod_pocztowy, Adres, Miejscowosc, Wojewodztwo, Powiat) Values (@Kod_pocztowy, @Adres, @Miejscowosc, @Wojewodztwo, @Powiat)";

        //    for (int i = 1; i <= samples; i++)
        //    {
        //        await ClearTable();
        //        stopwatch.Start();
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            await connection.OpenAsync();

        //            foreach (var kod in kody)
        //            {
        //                meanRecordStopwatch.Start();
        //                SqlCommand command = new SqlCommand(sqlExpression, connection);

        //                SqlParameter kod_pocztowyParameter = new SqlParameter("@Kod_pocztowy", kod.kod_pocztowy);
        //                SqlParameter adresParameter = new SqlParameter("@Adres", kod.adres);
        //                SqlParameter miejscowoscParameter = new SqlParameter("@Miejscowosc", kod.miejscowosc);
        //                SqlParameter wojewodztwoParameter = new SqlParameter("@Wojewodztwo", kod.wojewodztwo);
        //                SqlParameter powiatParameter = new SqlParameter("@Powiat", kod.powiat);

        //                command.Parameters.Add(kod_pocztowyParameter);
        //                command.Parameters.Add(adresParameter);
        //                command.Parameters.Add(miejscowoscParameter);
        //                command.Parameters.Add(wojewodztwoParameter);
        //                command.Parameters.Add(powiatParameter);

        //                await command.ExecuteNonQueryAsync();

        //                meanRecordStopwatch.Stop();
        //                recordTimeSpan += meanRecordStopwatch.Elapsed;
        //                meanRecordStopwatch.Restart();
        //            }
        //        }
        //        stopwatch.Stop();
        //        sampleTimeSpan += stopwatch.Elapsed;
        //        Console.WriteLine($"Mean Saving by one Record Time is {stopwatch.Elapsed}");

        //        stopwatch.Restart();
        //    }

        //    sampleTimeSpan = sampleTimeSpan.Divide(samples);
        //    recordTimeSpan = recordTimeSpan.Divide(imported_kody.Count * samples);
        //    Console.WriteLine($"Mean Saving by one Record Time is {sampleTimeSpan}");
        //    Console.WriteLine($"Mean Record Saving Time is {recordTimeSpan}\n");
        //}

        //EFsaveOneRecord
        //static async Task EFsaveOneRecord(List<Kody> kody, Lab1Context context)
        //{
        //    const int samples = 10;
        //    Stopwatch sampleStopwatch = new Stopwatch();
        //    Stopwatch recordStopwatch = new Stopwatch();
        //    TimeSpan recordTimeSpan = new TimeSpan();
        //    TimeSpan sampleTimeSpan = new TimeSpan();

        //    for (int i = 1; i <= samples; i++)
        //    {
        //        await ClearTable();

        //        sampleStopwatch.Start();
        //        for (int j = 0; j < kody.Count; j++)
        //        {
        //            recordStopwatch.Start();
        //            var data = new KodyPocztowe
        //            {
        //                Adres = kody[j].adres,
        //                Miejscowosc = kody[j].miejscowosc,
        //                Powiat = kody[j].powiat,
        //                KodPocztowy = kody[j].kod_pocztowy,
        //                Wojewodztwo = kody[j].wojewodztwo
        //            };

        //            context.KodyPocztowes.Add(data);

        //            recordStopwatch.Stop();
        //            recordTimeSpan += recordStopwatch.Elapsed;
        //            recordStopwatch.Restart();
        //        }
        //        context.SaveChanges();
        //        sampleStopwatch.Stop();
        //        sampleTimeSpan += sampleStopwatch.Elapsed;
        //        sampleStopwatch.Restart();
        //    }

        //    sampleTimeSpan = sampleTimeSpan.Divide(samples);
        //    recordTimeSpan = recordTimeSpan.Divide(imported_kody.Count * samples);
        //    Console.WriteLine($"Mean Saving by one Record Time is {sampleTimeSpan}");
        //    Console.WriteLine($"Mean Record Saving Time is {recordTimeSpan}\n");
        //}



        public class Kody
        {
            [Name("KOD POCZTOWY")]
            [Index(0)]
            public string kod_pocztowy { get; set; } = "";
            [Name("ADRES")]
            [Index(1)]
            public string adres { get; set; } = "";
            [Name("MIEJSCOWOŚĆ")]
            [Index(2)]
            public string miejscowosc { get; set; } = "";
            [Name("WOJEWÓDZTWO")]
            [Index(3)]
            public string wojewodztwo { get; set; } = "";
            [Name("POWIAT")]
            [Index(4)]
            public string powiat { get; set; } = "";
        }
       
        static async Task ClearTable()
        {
            string sqlExpression = "DELETE FROM Kody_pocztowe";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                await command.ExecuteNonQueryAsync();
            }
        }


    }


}