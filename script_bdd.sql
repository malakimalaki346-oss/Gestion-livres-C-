CREATE DATABASE GestionLivres;
GO

USE GestionLivres;
GO

CREATE TABLE Livres
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Titre NVARCHAR(100) NOT NULL,
    Auteur NVARCHAR(100) NOT NULL,
    NombrePages INT NOT NULL
);
GO

INSERT INTO Livres (Titre, Auteur, NombrePages) VALUES
(N'Le Petit Prince', N'Antoine de Saint-Exupéry', 96),
(N'Les Misérables', N'Victor Hugo', 1232),
(N'Le Comte de Monte-Cristo', N'Alexandre Dumas', 1498),
(N'La Peste', N'Albert Camus', 308),
(N'L''Étranger', N'Albert Camus', 185);
GO
