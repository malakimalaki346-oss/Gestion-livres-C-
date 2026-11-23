# Gestion des livres – TP ADO.NET

Application console en **C#** qui permet de gérer une table `Livres` dans une base **SQL Server** grâce à **ADO.NET**.

Travaux pratiques du module **DotNet** – EMSI (Mr. IDMANSOUR).

## Fonctionnalités

Le programme affiche un menu et demande toutes les valeurs à l'utilisateur (aucune valeur fixe) :

| N° | Opération |
|----|-----------|
| 1 | Afficher la liste des livres |
| 2 | Ajouter un livre |
| 3 | Supprimer un livre par son Id |
| 4 | Rechercher et afficher un livre par son titre |
| 5 | Compter le nombre de livres |
| 6 | Afficher le maximum des nombres de pages |
| 7 | Modifier le titre et le nombre de pages d'un livre |
| 8 | Trier et afficher les livres par nombre de pages |
| 0 | Quitter |

## Technologies

- C# (application console)
- ADO.NET (`System.Data.SqlClient`)
- SQL Server / SQL Server Express
- Visual Studio

## Structure du projet

```
tp-5/
├── Program.cs       # Code de l'application (menu + 8 opérations)
├── script_bdd.sql   # Création de la base GestionLivres et de la table Livres
├── tp-5.csproj
└── tp-5.slnx
```

## Base de données

Base : `GestionLivres` – Table : `Livres`

| Colonne | Type | Remarque |
|---------|------|----------|
| Id | INT | Clé primaire, auto-incrémentée |
| Titre | NVARCHAR(100) | Obligatoire |
| Auteur | NVARCHAR(100) | Obligatoire |
| NombrePages | INT | Obligatoire |

## Installation et lancement

1. **Cloner le dépôt**
   ```bash
   git clone https://github.com/malakimalaki346-oss/Gestion-livres-C-.git
   ```
2. **Créer la base de données** : ouvrir `script_bdd.sql` dans SQL Server Management Studio et l'exécuter. Il crée la base, la table et quelques livres de test.
3. **Adapter la chaîne de connexion** dans `Program.cs` si besoin :
   ```csharp
   static string connectionString = "Server=.\\SQLEXPRESS;Database=GestionLivres;Trusted_Connection=True;";
   ```
   Remplacer `.\SQLEXPRESS` par le nom de votre serveur SQL.
4. **Installer le package NuGet** `System.Data.SqlClient` (nécessaire en .NET 6 ou plus) :
   ```bash
   dotnet add package System.Data.SqlClient
   ```
5. **Lancer le programme** depuis Visual Studio (touche `F5`) ou avec :
   ```bash
   dotnet run
   ```

## Concepts ADO.NET utilisés

- `SqlConnection` : connexion à la base de données
- `SqlCommand` : exécution des requêtes SQL
- `SqlDataReader` (`ExecuteReader`) : lecture des résultats d'un `SELECT`
- `ExecuteNonQuery` : `INSERT`, `UPDATE`, `DELETE`
- `ExecuteScalar` : valeur unique (`COUNT`, `MAX`)
- Requêtes **paramétrées** (`@Titre`, `@Pages`, `@Id`) pour éviter l'injection SQL
- Gestion des erreurs avec `try / catch`

## Auteur

**Malak Nait Haddou** – 4IIR, EMSI
