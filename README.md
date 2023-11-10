# Desafio back-end para desenvolvedores iniciantes

# O Desafio

Precisamos de uma API que sirva o back-end de uma aplicação de aluguel de carros. Essa API precisa ser capaz de:

- Cadastrar um carro/cliente
- Editar um carro/cliente
- Deletar um carro/cliente
- Listar os carros/clientes
- Obter informações de um carro/cliente por ID

A entidade `carro` deve ter os seguintes campos:

- id -> Identificador do carro.
- marca -> Marca do carro.
- modelo -> Modelo do carro.
- dataCadastro -> Data em que o carro foi cadastrado.

A entidade `cliente` deve ter os seguintes campos:

- id -> Identificador do cliente.
- nome -> Nome do cliente (máx. de 50 caracteres)
- cpf -> CPF do cliente (deve ser único e não guardar pontos e traços).
- carro -> Carro alugado pelo cliente.
- dataNascimento -> Data de nascimento do cliente (formatada no padrão br: dd/MM/aaaa).
- created_at -> Data de criação do cliente.
- updated_at -> Data em que houve alteração no cliente.