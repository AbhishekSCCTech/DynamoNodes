using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FinalDataFilterNodePOC
{
    public static class FilterCsvData
    {
        /// <summary>
        /// Processes CSV data, filters specific operations, and aggregates data.
        /// </summary>
        /// <param name="csvFilePath">Path to the input CSV file.</param>
        /// <returns>A list of processed data with aggregated results.</returns>
        public static List<string> FilterData(string csvFilePath)
        {
            if (string.IsNullOrWhiteSpace(csvFilePath) || !File.Exists(csvFilePath))
            {
                throw new ArgumentException("Invalid file path or file does not exist.");
            }

            // Read CSV file and convert it to a list of rows (List<List<string>>)
            var csvData = File.ReadAllLines(csvFilePath)
                              .Select(line => line.Split(',').ToList())
                              .ToList();

            if (csvData == null || csvData.Count < 2)
            {
                throw new ArgumentException("CSV data must contain a header row and at least one data row.");
            }

            // Column indices based on the CSV file format
            int operationIndex = 0; // First column: Operation
            int timeIndex = 1;      // Second column: Time
            int eventsIndex = 2;    // Third column: Events
            int exchangeNameIndex = 3; // Fourth column: ExchangeName
            int modelNameIndex = 4; // Fifth column: ModelName

            // Skip the header row and parse data into a list of ExchangeData objects
            var data = csvData.Skip(1) // Skip the header row
                .Select(row => new ExchangeData
                {
                    Operation = row[operationIndex],
                    Time = int.TryParse(row[timeIndex], out var time) ? time : 0,
                    Events = int.TryParse(row[eventsIndex], out var events) ? events : 0,
                    ExchangeName = row[exchangeNameIndex],
                    ModelName = row[modelNameIndex],
                })
                .ToList();

            // Filter data for the required operations
            var filteredData = data.Where(d => d.Operation == "UpdateExchangeAsync" ||
                                               d.Operation == "UpdateExchangeAsync:GenerateViewableAsync");

            // LINQ query to group by ModelName and ExchangeName and sum the Time for the filtered data
            var result = from entry in filteredData
                         group entry by new { entry.ModelName, entry.ExchangeName } into grouped
                         select new ExchangeData
                         {
                             ModelName = grouped.Key.ModelName,
                             ExchangeName = grouped.Key.ExchangeName,
                             Time = grouped.Sum(g => g.Time),
                         };

            // Convert the result into a list of strings for Dynamo output
            return result.Select(r => r.ToString()).ToList();
        }
    }

    /// <summary>
    /// Represents a single row of data in the CSV file.
    /// </summary>
    public class ExchangeData
    {
        public string Operation { get; set; }
        public int Time { get; set; }
        public int Events { get; set; }
        public string ExchangeName { get; set; }
        public string ModelName { get; set; }
        public int ExecutionTime { get; set; }

        public override string ToString()
        {
            return $"ModelName: {ModelName}, ExchangeName: {ExchangeName}, Time: {Time}";
        }
    }
}
