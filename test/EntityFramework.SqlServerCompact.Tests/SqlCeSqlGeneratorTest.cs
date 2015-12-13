using System;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Storage.Internal;
using Xunit;

namespace Microsoft.Data.Entity.Tests
{
    public class SqlCeSqlGeneratorTest
    {
        [Fact]
        public void BatchSeparator_returns_seperator()
        {
            Assert.Equal("GO" + Environment.NewLine + Environment.NewLine, CreateSqlGenerator().BatchTerminator);
        }

        [Fact]
        public void GenerateLiteral_returns_ByteArray_literal()
        {
            var literal = CreateSqlGenerator().GenerateLiteral(new byte[] { 0xDA, 0x7A });
            Assert.Equal("0xDA7A", literal);
        }

        [Fact]
        public void GenerateLiteral_returns_bool_literal_when_true()
        {
            var literal = CreateSqlGenerator().GenerateLiteral(true);
            Assert.Equal("1", literal);
        }

        [Fact]
        public void GenerateLiteral_returns_bool_literal_when_false()
        {
            var literal = CreateSqlGenerator().GenerateLiteral(false);
            Assert.Equal("0", literal);
        }

        [Fact]
        public void GenerateLiteral_returns_DateTime_literal()
        {
            var value = new DateTime(2015, 3, 12, 13, 36, 37, 371);
            var literal = CreateSqlGenerator().GenerateLiteral(value);
            Assert.Equal("'2015-03-12 13:36:37.371'", literal);
        }

        [Fact]
        public virtual void GenerateLiteral_returns_Guid_literal()
        {
            var value = new Guid("c6f43a9e-91e1-45ef-a320-832ea23b7292");
            var literal = CreateSqlGenerator().GenerateLiteral(value);
            Assert.Equal("'c6f43a9e-91e1-45ef-a320-832ea23b7292'", literal);
        }

        protected ISqlGenerationHelper CreateSqlGenerator()
            => new SqlCeSqlGenerationHelper();
    }
}
