# Cadastro de Curriculos 📑

🏆 Esse Projeto foi desenvolvido para a disciplina de "Linguagem de Programação 1" no 5° semestre, juntamente com a Marcelly Marsura na Faculdade Engenheiro Salvador Arena. Tivemos a oportunidade de desenvolver um site de **Cadastro de Currículos** (carinhosamente apelidado de *LinkedIn 2*), no qual aplicamos os conceitos aprendidos na faculdade e explorar ferramentas avançadas para tornar o projeto mais robusto e eficiente. 💻

![print1](https://user-images.githubusercontent.com/105802950/230798756-47bd0970-1ed5-43d7-a039-e1db97ac563d.png)

💡 Foi um projeto desafiador que nos permitiu adentrar ainda mais no mundo do desenvolvimento Web. Durante o processo de construção do site, aplicamos o framework ASP.NET com a estrutura MVC em páginas Razor, valendo-se de conexão com um banco de dados SQL usando System.Data.SqlClient e fazendo uso do framework front-end Bootstrap para a primorar o projeto. 

![image](https://user-images.githubusercontent.com/105802950/230798809-66de6d63-1c0d-4dba-bac1-8e9980018a84.png)

> 🌐 A estrutura MVC permitiu dividir o projeto em suas respectivas três partes: o Model, a View e a Controller. Com essa abordagem, separamos as responsabilidades de cada componente do projeto, facilitando a sua manutenção e aprimoramento.

🎲 Desenvolvemos também um relacionamento intertabular no banco de dados entre os currículos cadastrados, as áreas a eles atribuídas e as experiências, formações e idiomas cadastrados. Para isso, desenvolvemos a camada DAO (Data Access Object), cujo objetivo é segregar a lógica de negócios da lógica para persistência de dados.

🖨 Isso agregado à linguagem Razor, criando páginas HTML dinâmicas, e ao Bootstrap, que agrega valor visual e funcional à página, programamos a totalidade do CRUD (Create, Read, Update e Delete), além de atributos adicionais, como a opção de pesquisa no menu principal, tratamento personalizado de erros e a opção de impressão do currículo formatado.

🎉 Em resumo, o desenvolvimento desse site foi uma oportunidade incrível para aplicarmos os conhecimentos aprendidos na faculdade e aprender novas técnicas e ferramentas para o desenvolvimento web. Gostaríamos de agradecer a oportunidade ao professor EDUARDO Rosalém Marcelino! Estamos ansiosos pelos próximos passos!

## Executando o projeto ⚙

Para executar esse projeto, inicialmente execute o arquivo *'CriacaoBase.sql'* para criação da DataBase CurriculoDB, que armazenará os dados.

Em seguida, configure no arquivo *'DAO > ConexaoDB.cs'* a forma de acesso ao servidor do banco, informando o nome do Data Source e forma de login. A seguir alguns exemplos para acesso via usuário ou valendo-se de Windows Authentication: 

- Acesso por usuário usando LOCALHOST como Data Source

```string strCon = "Data Source=LOCALHOST; Database=CurriculoDB; user id=sa; password=123456";```

- Acesso por Windows Authentication em um servidor ficitício *DESKTOP-48F838C\SQLEXPRESS*.

```string strCon = @"Data Source=DESKTOP-48F838C\SQLEXPRESS; Database=CurriculoDB; Integrated Security=True";```


Por fim execute o projeto!


## Prints do projeto

> Tela de Cadastro: 
![print2](https://user-images.githubusercontent.com/105802950/230799218-b51d9992-0b5e-4851-80a4-70717c84bbbe.png)

> Relacionamento tabular:
![print4](https://user-images.githubusercontent.com/105802950/230799239-e59b15ba-674e-440a-9fc3-7b2ee348cd9d.png)

> Exemplo de Impressão:
![print5](https://user-images.githubusercontent.com/105802950/230799248-aa3e89f5-8718-45d8-a97a-4a49d630286c.png)


