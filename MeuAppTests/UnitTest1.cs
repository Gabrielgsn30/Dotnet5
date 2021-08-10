using System;
using Xunit;

namespace MeuAppTests
{
    public class UnitTest1
    {
        [Fact]
        public void CriaUmDiretor()
        {
            var diretor = new Diretor("Teste");
            Assert.Equal("Teste",diretor.Nome);
        }
    }
}
