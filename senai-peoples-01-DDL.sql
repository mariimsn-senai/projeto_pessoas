CREATE DATABASE T_Peoples;

USE T_Peoples;

CREATE TABLE Funcionarios(
	IdFuncionarios INT PRIMARY KEY IDENTITY
	,Nome VARCHAR (225) NOT NULL 
	,Sobrenome VARCHAR (255) NOT NULL
	,DataNascimento Date
);
