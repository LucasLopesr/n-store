using Bogus;
using Bogus.DataSets;
using NStore.Core.DomainObjects;
using System;
using Xunit;

namespace NStore.Core.Tests.DomainObjects
{
	/// <summary>
	/// Padr�o de nomenclatura - MetodoEmTeste_EstadoEmTeste_ComportamentoExperado
	/// </summary>
	public class CpfTest
	{
		[Fact(DisplayName = "CPF V�lido")]
		[Trait("Categoria", "Testes de CPF")]
		public void Validar_CpfValido_DeveRetornarVerdadeiro()
		{
			Assert.True(Cpf.Validar("385.235.940-60"));
		}

		[Fact(DisplayName = "CPF Inv�lido")]
		[Trait("Categoria", "Testes de CPF")]
		public void Validar_CpfInvalido_DeveGerarException()
		{
			//Arrange
			//Act
			//Assert
			var ex = Assert.Throws<DomainException>(
				() => new Cpf("12332154544"));

			
		}
	}
}
