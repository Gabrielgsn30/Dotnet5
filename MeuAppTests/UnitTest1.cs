using System;
using Xunit;
using FluentValidation.TestHelper;

namespace MeuAppTests
{
    public class UnitTest1
    {
        [Fact]
        public void NomeDiretorAprentarErroSeForVazio()
        {
            var validator = new DiretorInputPostDTOValidator();
            var dto = new DiretorInputPostDTO { Nome = null };
            var result = validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(diretor => diretor.Nome);
        }
    }
}
