using ClosedXML.Excel;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

public class ExportService
{
    public async Task<MemoryStream> ExportToCSV<T>(IEnumerable<T> data)
    {
        var memoryStream = new MemoryStream();
        using var writer = new StreamWriter(memoryStream, leaveOpen: true);
        using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ",",
        });

        await csv.WriteRecordsAsync(data);
        await writer.FlushAsync();
        memoryStream.Position = 0;
        return memoryStream;
    }

    public MemoryStream ExportToExcelWithClosedXML<T>(IEnumerable<T> data)
    {
        var memoryStream = new MemoryStream();
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Hospitals");

        // Get properties of type T
        var properties = typeof(T).GetProperties();

        // Write headers with custom formatting
        var headerRow = worksheet.Row(1);
        headerRow.Style.Font.Bold = true;
        headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;

        for (int i = 0; i < properties.Length; i++)
        {
            var cell = worksheet.Cell(1, i + 1);
            cell.Value = properties[i].Name;
            cell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        }

        int row = 2;
        foreach (var item in data)
        {
            for (int i = 0; i < properties.Length; i++)
            {
                var cell = worksheet.Cell(row, i + 1);
                var value = properties[i].GetValue(item);

                if (value is bool boolValue)
                {
                    cell.Value = boolValue ? "Yes" : "No";
                }
                else
                {
                    cell.Value = value?.ToString() ?? "";
                }

                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            }
            row++;
        }

        worksheet.Columns().AdjustToContents();

        workbook.SaveAs(memoryStream);
        memoryStream.Position = 0;
        return memoryStream;
    }
}
