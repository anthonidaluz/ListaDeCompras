<img width="1080" height="600" alt="VsDebugConsole_D5rGXbzRIF" src="https://github.com/user-attachments/assets/d4145d3e-dd8b-4a7d-92f4-59211c33b464" />

## Lista de Compras 

Um sistema de gerenciamento de listas de compras desenvolvido em **C# (.NET)** para terminal (**Console Application**). O projeto aplica conceitos de **Programação Orientada a Objetos (POO)**, **Herança**, **Polimorfismo** e o padrão arquitetural de **Repository** para operações de CRUD em memória.

---

## Funcionalidades

O sistema é dividido em quatro módulos principais, cada um com suas próprias regras de negócio e validações.

### Módulo de Categorias

Responsável pelo gerenciamento das categorias dos produtos.

#### Funcionalidades

- Cadastro de categorias
- Edição de categorias
- Exclusão de categorias
- Visualização de categorias
- Atribuição de cores padrão:
  - Branco
  - Vermelho
  - Verde
  - Azul


### Módulo de Produtos

Responsável pelo catálogo de produtos disponíveis para as listas de compras.

#### Funcionalidades

- Cadastro de produtos
- Associação de produtos a uma categoria
- Definição do preço estimado
- Definição da unidade de medida

#### Unidades de Medida

- Quilograma
- Unidade
- Litro
- Caixa

#### Regras de Negócio

- O nome deve possuir entre **2 e 100 caracteres**.
- Não permite cadastrar produtos com o mesmo nome dentro da mesma categoria.

---

### Módulo de Listas de Compras

Responsável pelo gerenciamento das listas de compras.

#### Funcionalidades

- Cadastro de listas
- Edição de listas
- Exclusão de listas
- Visualização de listas
- Registro automático da data de criação
- Controle de status:
  - Aberta
  - Concluída
- Cálculo automático:
  - Total de itens
  - Valor estimado da lista

#### Regras de Negócio

- Não permite excluir listas que possuam itens cadastrados.

---

### Módulo de Itens da Lista

Responsável pela associação entre listas e produtos.

#### Funcionalidades

- Adicionar produtos à lista
- Definir quantidade do produto
- Calcular automaticamente o valor total do item

#### Regras de Negócio

- A quantidade deve ser um número positivo.
- Não permite adicionar o mesmo produto mais de uma vez na mesma lista.

---

## Arquitetura do Projeto

O projeto foi desenvolvido com foco em reutilização de código, organização e boas práticas de desenvolvimento.

### EntidadeBase

Classe abstrata responsável por:

- Fornecer o identificador (`Id`)
- Tornar obrigatório o método `Atualizar()`

---

### RepositorioBase

Classe responsável pelas operações genéricas de CRUD:

- Criar
- Ler
- Atualizar
- Excluir

Manipula coleções de `EntidadeBase` em memória.

---

### TelaBase

Responsável pelo fluxo padrão das telas do sistema:

- Cadastrar
- Editar
- Excluir
- Visualizar

Utiliza **Polimorfismo** para permitir que cada módulo implemente sua própria lógica.

---

## Como Executar o Projeto

### Pré-requisitos

- .NET 8 SDK ou superior
- Visual Studio 2022 ou Visual Studio Code

---

### Clonar o repositório

```bash
git clone https://github.com/anthonidaluz/ListaDeCompras.git
```

---

### Acessar o diretório do projeto

```bash
cd ListaDeCompras
```

---

### Executar a aplicação

```bash
dotnet run
```

---

## Contribuindo

Contribuições são bem-vindas.

1. Faça um **Fork** do projeto.
2. Crie uma nova branch:

```bash
git checkout -b feature/minha-melhoria
```

3. Realize suas alterações e faça os commits.
4. Envie a branch para o GitHub.
5. Abra um **Pull Request**.

---

## Tecnologias Utilizadas

- C#
- .NET 8
- Programação Orientada a Objetos (POO)
- Herança
- Polimorfismo
- Repository Pattern
- Clean Code
- Console Application

---

## Autor

Desenvolvido por **Anthoni da Luz** como parte dos estudos na **Academia do Programador**, com foco no aprimoramento em C#, Orientação a Objetos, boas práticas de desenvolvimento e arquitetura de software.
