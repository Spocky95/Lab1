
using CsvHelper.Configuration;
using CsvHelper;
using System.Diagnostics;
using System.Globalization;
using CsvHelper.Configuration.Attributes;
using System.Data.SqlClient;

namespace Lab1
{
    public class Program
    {
        static string connectionString = @"Data Source=LOCALHOST\LOCALDATABASE;Initial Catalog=lab1;User ID=ADMIN;Password=cisco123;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        static List<Kody> imported_kody = new List<Kody>();

        static async Task Main(string[] args)
        {
            await importCSV();
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




    }
}

//Data Source=LOCALHOST\LOCALDATABASE;Initial Catalog=lab1;User ID=ADMIN;Password=cisco123;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False