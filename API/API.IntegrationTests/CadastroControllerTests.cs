using Domain.Dto;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace API.IntegrationTests
{
    public class CadastroControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory _factory;
        public CadastroControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Create_Should_ReturnCreated_When_CommandIsValid()
        {
            var command = new PessoaDto
            {
                Nome = "Cliente de Teste Integrado",
                Cpf = "37984237093",
                DataNascimento = new DateTime(1995, 5, 15),
                Email = "teste.valido@email.com",
                Sexo = 'M',
            };

            var response = await _client.PostAsJsonAsync("/api/cadastro/pessoas", command);

            if (response.StatusCode != HttpStatusCode.Created)
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                Assert.Fail($"A API retornou um erro inesperado: {response.StatusCode}. Conteúdo da resposta: {errorContent}");
            }

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            response.Headers.Location.Should().NotBeNull();
        }

        [Fact]
        public async Task Create_Should_ReturnBadRequest_When_NameIsEmpty()
        {

            var command = new PessoaDto
            {
                Nome = "A",
                Cpf = "28330791054",
                DataNascimento = new System.DateTime(1995, 5, 15)
            };


            var response = await _client.PostAsJsonAsync("/api/cadastro/pessoas", command);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var validationErrors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

            validationErrors.Errors.Should().ContainKey("Nome");
            validationErrors.Errors["Nome"].Should().Contain("O nome deve ter entre 2 e 50 caracteres.");
        }

        [Fact]
        public async Task GetById_Should_ReturnOk_When_PessoaExists()
        {
            var createCommand = new PessoaDto
            {
                Nome = "Pessoa a Ser Buscada",
                Cpf = "58467471085",
                DataNascimento = new DateTime(1988, 8, 8),
                Sexo = 'M'
            };

            var createResponse = await _client.PostAsJsonAsync("/api/cadastro/pessoas", createCommand);

            if (!createResponse.IsSuccessStatusCode)
            {
                var errorContent = await createResponse.Content.ReadAsStringAsync();
                Assert.Fail($"A requisição POST para criar a pessoa falhou com status {createResponse.StatusCode}. Erro: {errorContent}");
            }

            var locationUri = createResponse.Headers.Location;
            var createdId = locationUri.Segments.Last();

  
            var getResponse = await _client.GetAsync($"/api/cadastro/pessoas/{createdId}");

           
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var pessoaResult = await getResponse.Content.ReadFromJsonAsync<PessoaView>();
            pessoaResult.Should().NotBeNull();
            pessoaResult.Id.ToString().Should().Be(createdId);
            pessoaResult.Nome.Should().Be(createCommand.Nome.ToUpper());
        }

        [Fact]
        public async Task Update_Should_ReturnNoContent_When_DataIsValid()
        {
            var createCommand = new PessoaDto
            {
                Nome = "Pessoa Original",
                Cpf = "09754788006",
                DataNascimento = new DateTime(1992, 2, 2),
                Sexo = 'F'
            };
            var createResponse = await _client.PostAsJsonAsync("/api/cadastro/pessoas", createCommand);

            if (!createResponse.IsSuccessStatusCode)
            {
                var errorContent = await createResponse.Content.ReadAsStringAsync();
                Assert.Fail($"A requisição POST (para criar o dado de teste) falhou com status {createResponse.StatusCode}. Erro: {errorContent}");
            }
            
            var locationUri = createResponse.Headers.Location;
            var createdId = locationUri.AbsolutePath.Split('/').Last();


            var updateCommand = new PessoaDto
            {
                Id = int.Parse(createdId),
                Cpf = createCommand.Cpf,
                Nome = "Pessoa Atualizada",
                DataNascimento = createCommand.DataNascimento,
            };


            var updateResponse = await _client.PutAsJsonAsync($"/api/cadastro/pessoas/{createdId}", updateCommand);

            if (updateResponse.StatusCode != HttpStatusCode.NoContent)
            {
                var errorContent = await updateResponse.Content.ReadAsStringAsync();
                Assert.Fail($"A requisição PUT (para atualizar) falhou com status {updateResponse.StatusCode}. Erro: {errorContent}");
            }

            updateResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var getResponse = await _client.GetAsync($"/api/cadastro/pessoas/{createdId}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var updatedPessoa = await getResponse.Content.ReadFromJsonAsync<PessoaView>();
            updatedPessoa.Nome.Should().Be(updateCommand.Nome.ToUpper());
        }

        [Fact]
        public async Task Delete_Should_ReturnNoContent_And_RemovePessoa()
        {
            var createCommand = new PessoaDto
            {
                Nome = "Pessoa a Ser Deletada",
                Cpf = "09754788006",
                DataNascimento = new DateTime(2001, 1, 1),
                Sexo = 'F',
            };

            var createResponse = await _client.PostAsJsonAsync("/api/cadastro/pessoas", createCommand);
            if (!createResponse.IsSuccessStatusCode)
            {
                var errorContent = await createResponse.Content.ReadAsStringAsync();
                Assert.Fail($"A requisição POST (para criar o dado de teste) falhou com status {createResponse.StatusCode}. Erro: {errorContent}");
            }
            createResponse.EnsureSuccessStatusCode();
            var createdId = createResponse.Headers.Location.AbsolutePath.Split('/').Last();

            var deleteResponse = await _client.DeleteAsync($"/api/cadastro/pessoas/{createdId}");

            if (!deleteResponse.IsSuccessStatusCode)
            {
                var errorContent = await createResponse.Content.ReadAsStringAsync();
                Assert.Fail($"A requisição POST (para criar o dado de teste) falhou com status {deleteResponse.StatusCode}. Erro: {errorContent}");
            }

            deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var getResponse = await _client.GetAsync($"/api/cadastro/pessoas/{createdId}");

            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
