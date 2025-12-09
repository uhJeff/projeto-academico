Create Database dbAcademico;
use dbAcademico;

Create table Alunos(
	Prontuario char(9) Primary Key,
	Nome varchar(50),
	Cpf char(12),
	Rg char(12),
	Email varchar(50)
);