using InsuranceWebApp.Helper;
using InsuranceWebApp.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace InsuranceWebApp.Data
{
    public class HospitalDocument : IDocument
    {
        private readonly List<HospitalViewModel> _hospitalViewModels;
        public HospitalDocument(List<HospitalViewModel> hospitalViewModels)
        {
            _hospitalViewModels = hospitalViewModels;
        }
        private static IContainer DefaultCellStyle(IContainer container,string backgroundColor,Color borderColor)
        {
            return container
                .Border(1)
                .BorderColor(borderColor)
                .Background(backgroundColor)
                .PaddingVertical(5)
                .PaddingHorizontal(10)
                .AlignCenter()
                .AlignMiddle();
        }

        public void Compose(IDocumentContainer container)
        {
            int index = 1;
            container
                .Page(page =>
                {
                    page.Margin(20);
                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(40);
                            columns.RelativeColumn(5);
                            columns.RelativeColumn(4);
                            columns.RelativeColumn(3);
                            columns.ConstantColumn(25);
                            columns.ConstantColumn(25);
                            columns.ConstantColumn(25);
                            columns.ConstantColumn(25);
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(2);
                           
                        });
                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("#");
                            header.Cell().Element(CellStyle).Text("CSYT");
                            header.Cell().Element(CellStyle).Text("Địa chỉ");
                            header.Cell().Element(CellStyle).Text("Điện thoại");
                            header.Cell().Element(CellStyle).Text("BV Công");
                            header.Cell().Element(CellStyle).Text("Nội khoa");
                            header.Cell().Element(CellStyle).Text("Ngoại khoa");
                            header.Cell().Element(CellStyle).Text("Nha sĩ");
                            header.Cell().Element(CellStyle).Text("TG Tiếp nhận");
                            header.Cell().Element(CellStyle).Text("Bảo hiểm");
                            static IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten3,Colors.Grey.Lighten1);
                        });

                        foreach(var hospitalViewModel in _hospitalViewModels)
                        {
                            table.Cell().Element(CellStyle).Text(index.ToString()).FontSize(10);
                            table.Cell().Element(CellStyle).Text(hospitalViewModel.HospitalName ?? "N/A");
                            table.Cell().Element(CellStyle).Text(hospitalViewModel.HospitalAddress ?? "N/A");
                            table.Cell().Element(CellStyle).Text(hospitalViewModel.PhoneNumber ?? "N/A");
                            table.Cell().Element(CellStyle).Text(hospitalViewModel.IsPublicHospital.ConvertBooltoString("X", " "));
                            table.Cell().Element(CellStyle).Text(hospitalViewModel.InPatient.ConvertBooltoString("X", " "));
                            table.Cell().Element(CellStyle).Text(hospitalViewModel.OutPatient.ConvertBooltoString("X", " "));
                            table.Cell().Element(CellStyle).Text(hospitalViewModel.Dental.ConvertBooltoString("X", " "));
                            table.Cell().Element(CellStyle).Text(hospitalViewModel.BillingTime ?? "N/A");
                            table.Cell().Element(CellStyle).Text(hospitalViewModel.Note ?? "N/A");
                            index++;
                            static IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White,Colors.Grey.Lighten1).ShowOnce();
                        }


                    });
                });
        }
    }
}

