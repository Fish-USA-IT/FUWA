using FishUsaWebApp.Models.ViewModels.CommonModels;

namespace FishUsaWebApp.SuiteQlQueries
{
    public static class CustomerQueries
    {
        public static string SearchCustomers(
            List<SearchFilterRowViewModel> activeFilters,
            int page,
            int pageSize)
        {
            var offset = Math.Max(0, (page - 1) * pageSize);
            var whereFilters = BuildCustomerWhereFilters(activeFilters);

            return $@"
                SELECT
                    id,
                    entityid,
                    companyname,
                    firstname,
                    lastname,
                    email,
                    phone,
                    isinactive
                FROM customer
                WHERE isinactive = 'F'
                    {whereFilters}
                ORDER BY id DESC
                OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";
        }

        private static string BuildCustomerWhereFilters(
            List<SearchFilterRowViewModel> activeFilters)
        {
            var clauses = new List<string>();

            foreach (var filter in activeFilters)
            {
                var safeValue = EscapeSuiteQl(filter.Value?.Trim() ?? string.Empty);

                if (string.IsNullOrWhiteSpace(safeValue))
                {
                    continue;
                }

                var clause = filter.Filter switch
                {
                    "customer_id" =>
                        $"TO_CHAR(id) = '{safeValue}'",

                    "entity_id" =>
                        $"LOWER(entityid) LIKE LOWER('%{safeValue}%')",

                    "email" =>
                        $"LOWER(email) LIKE LOWER('%{safeValue}%')",

                    "phone" =>
                        $"LOWER(phone) LIKE LOWER('%{safeValue}%')",

                    _ => null
                };

                if (!string.IsNullOrWhiteSpace(clause))
                {
                    clauses.Add(clause);
                }
            }

            return clauses.Count == 0
                ? string.Empty
                : "AND " + string.Join(" AND ", clauses);
        }

        private static string EscapeSuiteQl(string value)
        {
            return value.Replace("'", "''");
        }
    }

    public static class SalesOrderQueries
    {
        public static string SearchSalesOrders(
            List<SearchFilterRowViewModel> activeFilters,
            int page,
            int pageSize)
        {
            var offset = Math.Max(0, (page - 1) * pageSize);
            var whereFilters = BuildSalesOrderWhereFilters(activeFilters);

            return $@"
                SELECT
                    t.id,
                    t.tranid,
                    t.trandate,
                    t.entity AS entity_id,
                    BUILTIN.DF(t.entity) AS customer_name,
                    BUILTIN.DF(t.status) AS status,
                    t.total
                FROM transaction t
                LEFT JOIN customer c
                    ON c.id = t.entity
                WHERE t.type = 'SalesOrd'
                    {whereFilters}
                ORDER BY t.id DESC
                OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";
        }

        public static string GetSalesOrderById(string salesOrderId)
        {
            return $@"
                SELECT
                    t.id,
                    t.tranid,
                    t.trandate,
                    t.entity AS entity_id,
                    BUILTIN.DF(t.entity) AS customer_name,
                    BUILTIN.DF(t.status) AS status,
                    t.total
                FROM transaction t
                WHERE t.type = 'SalesOrd'
                    AND t.id = {salesOrderId}";
        }

        public static string GetSalesOrderItemLinesBySalesOrderId(string salesOrderId)
        {
            return $@"
                SELECT
                    BUILTIN.DF(tl.item) AS item,
                    i.displayname AS item_display_name,
                    tl.memo AS description,
                    tl.quantity AS quantity,
                    tl.quantitycommitted AS committed,
                    tl.quantityshiprecv AS fulfilled,
                    tl.quantitybilled AS invoiced,
                    tl.quantitybackordered AS back_ordered,
                    tl.isclosed AS closed,
                    tl.rate AS rate,
                    tl.netamount AS amount,
                    tl.custcol_line_level_total_discount AS line_level_total_discount,
                    BUILTIN.DF(tl.price) AS price_level,
                    BUILTIN.DF(tl.units) AS units,
                    BUILTIN.DF(tl.taxcode) AS tax_code,
                    tl.taxrate1 AS tax_rate,
                    BUILTIN.DF(tl.commitinventory) AS commit,
                    BUILTIN.DF(t.currency) AS currency,
                    tl.porate AS po_rate,
                    BUILTIN.DF(tl.costestimatetype) AS cost_estimate_type,
                    tl.costestimate AS est_extended_cost,
                    i.upccode AS item_upc,
                    i.urlcomponent AS url_component,
                    tl.taxamount AS tax_amount_2
                FROM transaction t
                INNER JOIN transactionline tl
                    ON tl.transaction = t.id
                LEFT JOIN item i
                    ON i.id = tl.item
                WHERE t.id = {salesOrderId}
                    AND t.type = 'SalesOrd'
                    AND tl.item IS NOT NULL
                ORDER BY tl.id";
        }

        private static string BuildSalesOrderWhereFilters(
            List<SearchFilterRowViewModel> activeFilters)
        {
            var clauses = new List<string>();

            foreach (var filter in activeFilters)
            {
                var safeValue = EscapeSuiteQl(filter.Value?.Trim() ?? string.Empty);

                if (string.IsNullOrWhiteSpace(safeValue))
                {
                    continue;
                }

                var clause = filter.Filter switch
                {
                    "so_number" =>
                        $"LOWER(t.tranid) LIKE LOWER('%{safeValue}%')",

                    "bigcommerce_number" =>
                        $"LOWER(t.custbody_bigcommerce_order_number) LIKE LOWER('%{safeValue}%')",

                    "customer_email" =>
                        $"LOWER(c.email) LIKE LOWER('%{safeValue}%')",

                    "customer_name" =>
                        $@"(
                            LOWER(c.companyname) LIKE LOWER('%{safeValue}%')
                            OR LOWER(c.firstname) LIKE LOWER('%{safeValue}%')
                            OR LOWER(c.lastname) LIKE LOWER('%{safeValue}%')
                            OR LOWER(c.entityid) LIKE LOWER('%{safeValue}%')
                        )",

                    "customer_phone" =>
                        $@"(
                            LOWER(c.phone) LIKE LOWER('%{safeValue}%')
                            OR LOWER(c.altphone) LIKE LOWER('%{safeValue}%')
                            OR LOWER(c.mobilephone) LIKE LOWER('%{safeValue}%')
                        )",

                    _ => null
                };

                if (!string.IsNullOrWhiteSpace(clause))
                {
                    clauses.Add(clause);
                }
            }

            return clauses.Count == 0
                ? string.Empty
                : "AND " + string.Join(" AND ", clauses);
        }

        private static string EscapeSuiteQl(string value)
        {
            return value.Replace("'", "''");
        }
    }

    public static class EmployeeQueries
    {
        public static string GetEmployeeById(string employeeId)
        {
            return $@"
            SELECT 
                id,
                entityid,
                firstname,
                lastname,
                email
            FROM employee
            WHERE id = {employeeId}";
        }
    }
}