using Oracle.ManagedDataAccess.Client;
using WebApplication1.Database;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly OracleDbContext _db;

        public ProductRepository(OracleDbContext db)
        {
            _db = db;
        }

        public async Task<List<ProductInfo>> GetProductsAsync()
        {
            var products = new List<ProductInfo>();

            using var conn = _db.CreateConnection();

            await conn.OpenAsync();

            string sql = @"
                SELECT
                    ROLL_NO AS RollNo,
                    GSM,
                    WIDTH,
                    COLOR_FASTNESS,
                    PILLING_TEST,
                    TWIST
                FROM PRODUCT_INFO";

            using var cmd = new OracleCommand(sql, conn);

            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                products.Add(new ProductInfo
                {
                    RollNo = reader["RollNo"]?.ToString() ?? "",
                    GSM = reader["GSM"]?.ToString() ?? "",
                    Width = reader["WIDTH"]?.ToString() ?? "",
                    ColorFastness = reader["COLOR_FASTNESS"]?.ToString() ?? "",
                    PillingTest = reader["PILLING_TEST"]?.ToString() ?? "",
                    Twist = reader["TWIST"]?.ToString() ?? ""

                });
            }

            return products;
        }

        public async Task<ProductInfo?> GetProductByRollNoAsync(string rollNo)
        {
            using var conn = _db.CreateConnection();

            await conn.OpenAsync();

            string sql = @"
                SELECT
                    ROLL_NO,
                    GSM,
                    WIDTH,
                    COLOR_FASTNESS,
                    PILLING_TEST,
                    TWIST
                FROM PRODUCT_INFO
                WHERE ROLL_NO = :P_ROLL_NO";

            using var cmd = new OracleCommand(sql, conn);

            cmd.Parameters.Add(
                new OracleParameter("P_ROLL_NO", rollNo));

            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new ProductInfo
                {
                    RollNo = reader["ROLL_NO"]?.ToString() ?? "",
                    GSM = reader["GSM"]?.ToString() ?? "",
                    Width = reader["WIDTH"]?.ToString() ?? "",
                    ColorFastness = reader["COLOR_FASTNESS"]?.ToString() ?? "",
                    PillingTest = reader["PILLING_TEST"]?.ToString() ?? "",
                    Twist = reader["TWIST"]?.ToString() ?? ""
                };
            }

            return null;
        }


        public async Task<int> AddProductAsync(ProductInfo product)
        {
            using var conn = _db.CreateConnection();

            await conn.OpenAsync();

            string sql = @"
        INSERT INTO PRODUCT_INFO
        (
            ROLL_NO,
            GSM,
            WIDTH,
            COLOR_FASTNESS,
            PILLING_TEST,
            TWIST
        )
        VALUES
        (
            :ROLL_NO,
            :GSM,
            :WIDTH,
            :COLOR_FASTNESS,
            :PILLING_TEST,
            :TWIST
        )";

            using var cmd = new Oracle.ManagedDataAccess.Client.OracleCommand(sql, conn);

            cmd.Parameters.Add("ROLL_NO", product.RollNo);
            cmd.Parameters.Add("GSM", product.GSM);
            cmd.Parameters.Add("WIDTH", product.Width);
            cmd.Parameters.Add("COLOR_FASTNESS", product.ColorFastness);
            cmd.Parameters.Add("PILLING_TEST", product.PillingTest);
            cmd.Parameters.Add("TWIST", product.Twist);

            return await cmd.ExecuteNonQueryAsync();
        }

    }
}