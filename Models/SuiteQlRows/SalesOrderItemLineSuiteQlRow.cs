using System.Text.Json.Serialization;

namespace FishUsaWebApp.Models.SuiteQlRows;

public sealed class SalesOrderItemLineSuiteQlRow
{
    [JsonPropertyName("item")]
    public string? Item { get; set; }

    [JsonPropertyName("item_display_name")]
    public string? ItemDisplayName { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("quantity")]
    public string? Quantity { get; set; }

    [JsonPropertyName("committed")]
    public string? Committed { get; set; }

    [JsonPropertyName("fulfilled")]
    public string? Fulfilled { get; set; }

    [JsonPropertyName("invoiced")]
    public string? Invoiced { get; set; }

    [JsonPropertyName("back_ordered")]
    public string? BackOrdered { get; set; }

    [JsonPropertyName("closed")]
    public string? Closed { get; set; }

    [JsonPropertyName("rate")]
    public string? Rate { get; set; }

    [JsonPropertyName("amount")]
    public string? Amount { get; set; }

    [JsonPropertyName("line_level_total_discount")]
    public string? LineLevelTotalDiscount { get; set; }

    [JsonPropertyName("promotion_1")]
    public string? Promotion1 { get; set; }

    [JsonPropertyName("create_po")]
    public string? CreatePo { get; set; }

    [JsonPropertyName("drop_ship_eligible")]
    public string? DropShipEligible { get; set; }

    [JsonPropertyName("price_level")]
    public string? PriceLevel { get; set; }

    [JsonPropertyName("units")]
    public string? Units { get; set; }

    [JsonPropertyName("gift_certificate_amount")]
    public string? GiftCertificateAmount { get; set; }

    [JsonPropertyName("gift_certificate")]
    public string? GiftCertificate { get; set; }

    [JsonPropertyName("inventory_detail")]
    public string? InventoryDetail { get; set; }

    [JsonPropertyName("tax_code")]
    public string? TaxCode { get; set; }

    [JsonPropertyName("tax_rate")]
    public string? TaxRate { get; set; }

    [JsonPropertyName("commit")]
    public string? Commit { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("po_rate")]
    public string? PoRate { get; set; }

    [JsonPropertyName("free_gift_promotion")]
    public string? FreeGiftPromotion { get; set; }

    [JsonPropertyName("cost_estimate_type")]
    public string? CostEstimateType { get; set; }

    [JsonPropertyName("est_extended_cost")]
    public string? EstExtendedCost { get; set; }

    [JsonPropertyName("excise_eligible")]
    public string? ExciseEligible { get; set; }

    [JsonPropertyName("item_upc")]
    public string? ItemUpc { get; set; }

    [JsonPropertyName("oversize_item")]
    public string? OversizeItem { get; set; }

    [JsonPropertyName("item_upc_item_fulfillment")]
    public string? ItemUpcItemFulfillment { get; set; }

    [JsonPropertyName("url_component")]
    public string? UrlComponent { get; set; }

    [JsonPropertyName("vat_code")]
    public string? VatCode { get; set; }

    [JsonPropertyName("etail_order_line_id")]
    public string? ETailOrderLineId { get; set; }

    [JsonPropertyName("etail_order_item_type_id")]
    public string? ETailOrderItemTypeId { get; set; }

    [JsonPropertyName("etail_order_line_tax")]
    public string? ETailOrderLineTax { get; set; }

    [JsonPropertyName("promotion_2")]
    public string? Promotion2 { get; set; }

    [JsonPropertyName("promotion_3")]
    public string? Promotion3 { get; set; }

    [JsonPropertyName("gift_card_number")]
    public string? GiftCardNumber { get; set; }

    [JsonPropertyName("payment_method")]
    public string? PaymentMethod { get; set; }

    [JsonPropertyName("bigcommerce_handling_fee")]
    public string? BigCommerceHandlingFee { get; set; }

    [JsonPropertyName("udf1")]
    public string? Udf1 { get; set; }

    [JsonPropertyName("tax_amount_2")]
    public string? TaxAmount2 { get; set; }

    [JsonPropertyName("gift_card_to")]
    public string? GiftCardTo { get; set; }

    [JsonPropertyName("gift_card_from")]
    public string? GiftCardFrom { get; set; }

    [JsonPropertyName("edi_ns_source_purchase_order_line_id")]
    public string? EdiNsSourcePurchaseOrderLineId { get; set; }

    [JsonPropertyName("edi_ns_acknowledgment_type_code_line")]
    public string? EdiNsAcknowledgmentTypeCodeLine { get; set; }

    [JsonPropertyName("image_url")]
    public string? ImageUrl { get; set; }

    [JsonPropertyName("edi_buyer_part_number")]
    public string? EdiBuyerPartNumber { get; set; }

    [JsonPropertyName("edi_item_description")]
    public string? EdiItemDescription { get; set; }

    [JsonPropertyName("edi_customer_price")]
    public string? EdiCustomerPrice { get; set; }

    [JsonPropertyName("serial_number")]
    public string? SerialNumber { get; set; }

    [JsonPropertyName("pick_up_2")]
    public string? PickUp2 { get; set; }

    [JsonPropertyName("ship_from_vat_bin")]
    public string? ShipFromVatBin { get; set; }

    [JsonPropertyName("ship_to_vat_bin")]
    public string? ShipToVatBin { get; set; }

    [JsonPropertyName("bottle_tax_rate")]
    public string? BottleTaxRate { get; set; }

    [JsonPropertyName("bottle_tax")]
    public string? BottleTax { get; set; }

    [JsonPropertyName("recycling_fees_rate")]
    public string? RecyclingFeesRate { get; set; }

    [JsonPropertyName("recycling_fees")]
    public string? RecyclingFees { get; set; }

    [JsonPropertyName("select_tax_type")]
    public string? SelectTaxType { get; set; }

    [JsonPropertyName("edit_tax_type")]
    public string? EditTaxType { get; set; }
}