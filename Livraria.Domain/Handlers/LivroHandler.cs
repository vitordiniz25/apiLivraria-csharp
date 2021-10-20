using Flunt.Notifications;
using Livraria.Domain.Commands.Inputs;
using Livraria.Domain.Commands.Outputs;
using Livraria.Domain.Entidades;
using Livraria.Domain.Interfaces.Repositories;
using Livraria.Infra.Interfaces.Commands;
using System;

namespace Livraria.Domain.Handlers
{
    public class LivroHandler : ICommandHandler<AdicionarLivroCommand>,
                                ICommandHandler<AtualizarLivroCommand>, 
                                ICommandHandler<ApagarLivroCommand>
    {
        private readonly ILivroRepository _repository;

        public LivroHandler(ILivroRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(AdicionarLivroCommand command)
        {
            try
            {
                if (!command.ValidarCommand())
                    return new LivroCommandResult(false, "Por favor corrija as inconsistencias abaixo", command.Notifications);
                long id = 0;
                string nome = command.Nome;
                string autor = command.Autor;
                int edicao = command.Edicao;
                string isbn = command.Isbn;
                string imagem = command.Imagem;

                Livro livro = new Livro(id, nome, autor, edicao, isbn, imagem);

                id = _repository.Inserir(livro);

                var retorno = new LivroCommandResult(true, "Livro adicionando com sucesso!", new
                {
                    Id = id,
                    Nome = livro.Nome,
                    Autor = livro.Autor,
                    Edicao = livro.Edicao,
                    Isbn = livro.Isbn,
                    Imagem = livro.Imagem
                });

                return retorno;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public ICommandResult Handle(AtualizarLivroCommand command)
        {
            try
            {
                if (!command.ValidarCommand())
                    return new LivroCommandResult(false, "Por favor corrija as inconsistencias abaixo", command.Notifications);

                if (!_repository.CheckId(command.Id))
                {
                    return new LivroCommandResult(false, "Id", new Notification("Id", "Id invalido. Este id nao esta cadastrado"));
                }

                long id = command.Id;
                string nome = command.Nome;
                string autor = command.Autor;
                int edicao = command.Edicao;
                string isbn = command.Isbn;
                string imagem = command.Imagem;

                Livro livro = new Livro(id, nome, autor, edicao, isbn, imagem);

                _repository.Atualizar(livro);

                var retorno = new LivroCommandResult(true, "Livro atualizado com sucesso!", new
                {
                    Id = id,
                    Nome = livro.Nome,
                    Autor = livro.Autor,
                    Edicao = livro.Edicao,
                    Isbn = livro.Isbn,
                    Imagem = livro.Imagem
                });

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICommandResult Handle(ApagarLivroCommand command)
        {
            try
            {
                if (!command.ValidarCommand())
                    return new LivroCommandResult(false, "Por favor corrija as inconsistencias abaixo", command.Notifications);

                if (!_repository.CheckId(command.Id))
                {
                    return new LivroCommandResult(false, "Id", new Notification("Id", "Id invalido. Este id nao esta cadastrado"));
                }


                _repository.Excluir(command.Id);

                var retorno = new LivroCommandResult(true, "Livro apagado com sucesso!", new
                {
                    Id = command.Id
                });

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
