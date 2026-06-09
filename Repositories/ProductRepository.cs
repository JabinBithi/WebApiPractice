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
                    ID,
                    ROLL_NO AS RollNo,
                    GSM,
                    WIDTH,
                    COLOR_FASTNESS,
                    PILLING_TEST,
                    TWIST
                FROM PRODUCT_INFO";

            using var cmd = new OracleCommand(sql, conn);

            using var reader = await cmd.ExecuteReaderAsync();

            // Cache ordinals and handle possible DBNull values to avoid InvalidCastException.
            int idOrd = reader.GetOrdinal("ID");
            int rollNoOrd = reader.GetOrdinal("RollNo");
            int gsmOrd = reader.GetOrdinal("GSM");
            int widthOrd = reader.GetOrdinal("WIDTH");
            int colorOrd = reader.GetOrdinal("COLOR_FASTNESS");
            int pillingOrd = reader.GetOrdinal("PILLING_TEST");
            int twistOrd = reader.GetOrdinal("TWIST");

            while (await reader.ReadAsync())
            {
                products.Add(new ProductInfo
                {
                    Id = reader.IsDBNull(idOrd) ? 0 : reader.GetInt32(idOrd),
                    RollNo = reader.IsDBNull(rollNoOrd) ? "" : reader.GetString(rollNoOrd),
                    GSM = reader.IsDBNull(gsmOrd) ? "" : reader.GetString(gsmOrd),
                    Width = reader.IsDBNull(widthOrd) ? "" : reader.GetString(widthOrd),
                    ColorFastness = reader.IsDBNull(colorOrd) ? "" : reader.GetString(colorOrd),
                    PillingTest = reader.IsDBNull(pillingOrd) ? "" : reader.GetString(pillingOrd),
                    Twist = reader.IsDBNull(twistOrd) ? "" : reader.GetString(twistOrd)
                });
            }

            return products;
        }

        public async Task<ProductInfo?> GetProductByRollNoAsync(int id)
        {
            using var conn = _db.CreateConnection();

            await conn.OpenAsync();

            string sql = @"
                SELECT
                    ID,
                    ROLL_NO,
                    GSM,
                    WIDTH,
                    COLOR_FASTNESS,
                    PILLING_TEST,
                    TWIST
                FROM PRODUCT_INFO
                WHERE id = :P_ID";

            using var cmd = new OracleCommand(sql, conn);

            cmd.Parameters.Add(
                new OracleParameter("P_ID", id));

            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new ProductInfo
                {
                    Id = Convert.ToInt32(reader["ID"]),
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
        (   ID,
            ROLL_NO,
            GSM,
            WIDTH,
            COLOR_FASTNESS,
            PILLING_TEST,
            TWIST
        )
        VALUES
        (   PRODUCT_INFO_SEQ.NEXTVAL,
            :ROLL_NO,
            :GSM,
            :WIDTH,
            :COLOR_FASTNESS,
            :PILLING_TEST,
            :TWIST
        )";

            using var cmd = new Oracle.ManagedDataAccess.Client.OracleCommand(sql, conn);

            cmd.Parameters.Add(new OracleParameter("ROLL_NO", product.RollNo));
            cmd.Parameters.Add(new OracleParameter("GSM", product.GSM));
            cmd.Parameters.Add(new OracleParameter("WIDTH", product.Width));
            cmd.Parameters.Add(new OracleParameter("COLOR_FASTNESS", product.ColorFastness));
            cmd.Parameters.Add(new OracleParameter("PILLING_TEST", product.PillingTest));
            cmd.Parameters.Add(new OracleParameter("TWIST", product.Twist));

            return await cmd.ExecuteNonQueryAsync();
        }



        public async Task<bool> updateProductByIdNoAsync(ProductInfo product)
        {

            using var conn = _db.CreateConnection();

            await conn.OpenAsync();

            string sql = @"
                    UPDATE PRODUCT_INFO
                    SET
                        ROLL_NO = :RollNo,
                        GSM = :GSM,
                        WIDTH = :Width,
                        COLOR_FASTNESS = :ColorFastness,
                        PILLING_TEST = :PillingTest,
                        TWIST = :Twist
                    WHERE ID = :Id";

            using var cmd = new OracleCommand(sql, conn);

            cmd.Parameters.Add(new OracleParameter("RollNo", product.RollNo));
            cmd.Parameters.Add(new OracleParameter("GSM", product.GSM));
            cmd.Parameters.Add(new OracleParameter("Width", product.Width));
            cmd.Parameters.Add(new OracleParameter("ColorFastness", product.ColorFastness));
            cmd.Parameters.Add(new OracleParameter("PillingTest", product.PillingTest));
            cmd.Parameters.Add(new OracleParameter("Twist", product.Twist));
            cmd.Parameters.Add(new OracleParameter("Id", product.Id));

            var rowsAffected = await cmd.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            using var conn = _db.CreateConnection();
            await conn.OpenAsync();

            string sql = "DELETE FROM PRODUCT_INFO WHERE ID = :Id";

            using var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("Id", id);

            var rowsAffected = await cmd.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }
    }
}