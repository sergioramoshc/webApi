Repositório on-line: 

---

Como executar a API

* Ambiente:

1. Acessar o Visual Studio Installer para então realizar a seguinte modificação da sua instalação:

Instalar a carga de desenvolvimento web e ASP.NET (ASP.NET and web development).

2. Preparar a base de dados:

Neste repositório, pasta "Utils", haverá um script que criará a estrutura de dados no SQL Server, basta providenciar a base e apontar a criação.

* Solução:

1. Clicar com o direito sobre a solução > Limpar solução (Clean Solution);

2. Clique com o direito sobre a solução > Construir solução (Build Solution);

Isso deve instalar todas as dependências necessárias.

3. Edite a string de conexão com o banco de dados no arquivo "appsettings.json";

Após o procedimento poderá executar a API via IIS Express (ao topo do ambiente do Visual Studio).