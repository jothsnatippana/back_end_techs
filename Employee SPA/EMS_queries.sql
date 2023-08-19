CREATE DATABASE EmployeeManagementSystem;

CREATE SCHEMA Foundation;

CREATE TABLE Foundation.Roles (
	Id INT identity(1, 1) PRIMARY KEY
	,Name VARCHAR(100) NOT NULL
	);

CREATE TABLE Foundation.Teams (
	Id INT identity(1, 1) PRIMARY KEY
	,Name VARCHAR(100) NOT NULL
	);

CREATE TABLE Foundation.Employees (
	Id INT identity(1, 1) PRIMARY KEY
	,Name VARCHAR(100) NOT NULL
	,Gender VARCHAR(100) NOT NULL
	,DateOfJoining DATE NOT NULL
	,Roleid INT NOT NULL
	,CurrentTeamId INT 
	,FOREIGN KEY (Roleid) REFERENCES Foundation.Roles(Id)
	,FOREIGN KEY (CurrentTeamId) REFERENCES Foundation.Teams(Id)
	);

CREATE TABLE Foundation.Employees_Teams (
	EmployeeId INT NOT NULL
	,TeamId INT NOT NULL
	,FOREIGN KEY (EmployeeId) REFERENCES Foundation.Employees(Id)
	,FOREIGN KEY (TeamId) REFERENCES Foundation.Teams(Id)
	)