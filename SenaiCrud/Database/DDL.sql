-- Criação do banco de dados (opcional)
CREATE DATABASE IF NOT EXISTS GerenciadorTarefas;
USE GerenciadorTarefas;

-- Criação da tabela Tarefas
CREATE TABLE IF NOT EXISTS Tarefas (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Titulo VARCHAR(255) NOT NULL,
    Descricao TEXT,
    DataCriacao DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    DataConclusao DATETIME,
    Concluida BOOLEAN NOT NULL DEFAULT FALSE
);
