using Livraria.Domain.Commands.Inputs;
using Livraria.Domain.Handlers;
using Livraria.Domain.Interfaces.Repositories;
using Livraria.Domain.Query;
using Livraria.Infra.Interfaces.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Livraria.Api.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroRepository _repository;
        private readonly LivroHandler _handler;

        public LivroController(ILivroRepository repository, LivroHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        [HttpGet]
        [Route("v1/livros")]
        public List<LivroQueryResult> ListarLivros()
        {
            return _repository.Listar();
        }

        [HttpGet]
        [Route("v1/livros/{id}")]
        public LivroQueryResult ObterLivro(long id)
        {
            return _repository.Obter(id);
        }

        [HttpPost]
        [Route("v1/livros")]
        public ICommandResult InserirLivro([FromBody] AdicionarLivroCommand command)
        {
            var result = _handler.Handle(command);
            return result;
        }

        [HttpPut]
        [Route("v1/livros/{id}")]
        public ICommandResult AlterarLivro(long id, [FromBody] AtualizarLivroCommand command)
        {
            command.Id = id;
            var result = _handler.Handle(command);
            return result;
        }

        [HttpDelete]
        [Route("v1/livros/{id}")]
        public ICommandResult ExcluirLivro(long id)
        {
            var command = new ApagarLivroCommand() { Id = id };
            var result = _handler.Handle(command);
            return result;
        }
    }
}
