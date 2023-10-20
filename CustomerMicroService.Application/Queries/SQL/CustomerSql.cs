namespace CustomerMicroService.Application.Queries.SQL
{
    public static class CustomerSql
    {
        public static string GetById
        {
            get
            {
                return @"SELECT
                             c.CustomerId,
                             c.FirstName,
                             c.LastName,
                             c.Email,
                             c.Phone,
                             c.Cpf,
                             a.AddressId,
                             a.Street,
                             a.Number,
                             a.Complement,
                             a.Neighborhood,
                             a.City,
                             a.State,
                             a.ZipCode
                         FROM Customer c
                         JOIN Address a ON c.AddressId = a.AddressId
                         WHERE c.CustomerId = @customerId";
            }
        }

        public static string GetByFilter
        {
            get
            {
                return @"SELECT
                             c.CustomerId,
                             c.FirstName,
                             c.LastName,
                             c.Email,
                             c.Phone,
                             c.Cpf,
                             a.AddressId,
                             a.Street,
                             a.Number,
                             a.Complement,
                             a.Neighborhood,
                             a.City,
                             a.State,
                             a.ZipCode
                         FROM Customer c
                         JOIN Address a ON c.AddressId = a.AddressId
                        ORDER BY c.CustomerId
                        OFFSET @offset ROWS
                        FETCH NEXT @pageSize ROWS ONLY;";
            }
        }
    }
}
