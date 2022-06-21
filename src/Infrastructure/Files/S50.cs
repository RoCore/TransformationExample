using System.Runtime.InteropServices;
using static Infrastructure.S50LayoutDefinition;

namespace Infrastructure.Files;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Size = 397)]
public class S50 : IStructLayout
{
    [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = FormatIdSize)]
    public string? FormatId { get; set; }

    [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = ExternalIdSize)]
    public string? Id { get; set; }

    [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = Number2Size)]
    public string? Number2 { get; set; }

    [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = Number3Size)]
    public string? Number3 { get; set; }

    [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = LastNameSize)]
    public string? LastName { get; set; }

    [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = FirstNameSize)]
    public string? FirstName { get; set; }

    [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = StreetSize)]
    public string? Street { get; set; }

    [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = CountrySize)]
    public string? Country { get; set; }

    [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = ZipSize)]
    public string? Zip { get; set; }

    [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = CitySize)]
    public string? City { get; set; }

    [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = Number4Size)]
    public string? Number4 { get; set; }

    [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = Number5Size)]
    public string? Number5 { get; set; }

    [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = OrderDateSize)]
    public string? OrderDate { get; set; }

    [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = Number6Size)]
    public string? Number6 { get; set; }

    [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = Number7Size)]
    public string? Number7 { get; set; }

    [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = ShipmentPriceSize)]
    public string? ShipmentPrice { get; set; }

    [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = ShipmentPriceCurrencySize)]
    public string? ShipmentPriceCurrency { get; set; }

    [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = ShipmentTaxSize)]
    public string? ShipmentTax { get; set; }

    [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = ShipmentTaxCurrencySize)]
    public string? ShipmentTaxCurrency { get; set; }

    [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = UndefinedSpaceSize)]
    public string? UndefinedSpace { get; set; }

    [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = Number8Size)]
    public string? Number8 { get; set; }
}