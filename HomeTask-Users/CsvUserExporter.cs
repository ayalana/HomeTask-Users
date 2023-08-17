using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTask_Users
{
    internal class CsvUserExporter : IUserExporter
    {
        public string ExportData<T>(T data)
        {
            // Create a StringWriter to write CSV data
            using (var writer = new StringWriter())
            {
                // Create a CsvWriter and configure the settings
                using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    // Check if the data is already an IEnumerable
                    if (data is IEnumerable enumerableData)
                    {
                        // Write records to the CSV
                        csvWriter.WriteRecords(enumerableData);
                    }
                    else
                    {
                        throw new ArgumentException("The data object must be an IEnumerable or IEnumerable<T>.");
                    }
                }

                // Get the CSV string from StringWriter
                return writer.ToString();
            }

        }
    }
}
