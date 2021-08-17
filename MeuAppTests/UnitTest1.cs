using System;
using Xunit;
using FluentValidation.TestHelper;

namespace MeuAppTests
{
    public class UnitTest1
    {
        [Fact]
        public void NomeDiretorAprentarErroSeForNullo()
        {
            var validator = new DiretorInputPostDTOValidator();
            var dto = new DiretorInputPostDTO { Nome = null };
            var result = validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(diretor => diretor.Nome);
        }

        [Fact]
        public void NomeDiretorAprentarErroSeForVazio()
        {
            var validator = new DiretorInputPostDTOValidator();
            var dto = new DiretorInputPostDTO { Nome = "" };
            var result = validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(diretor => diretor.Nome);
        }

        [Fact]
        public void NomeDiretorAprentarErroSeForGrande()
        {
            var validator = new DiretorInputPostDTOValidator();
            var dto = new DiretorInputPostDTO { Nome = "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" };
            var result = validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(diretor => diretor.Nome);
        }
    }
}
