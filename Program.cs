using System;
using System.Data.SqlClient;

namespace tp_5
{
    class Program
    {
        const string connectionString = @"Server=.\SQLEXPRESS;Database=GestionLivres;Trusted_Connection=True;";

        static void Main(string[] args)
        {
            int choix;

            do
            {
                Console.WriteLine();
                Console.WriteLine("===== Gestion des livres =====");
                Console.WriteLine("1. Afficher la liste des livres");
                Console.WriteLine("2. Ajouter un livre");
                Console.WriteLine("3. Supprimer un livre par Id");
                Console.WriteLine("4. Rechercher un livre par Titre");
                Console.WriteLine("5. Compter les livres");
                Console.WriteLine("6. Afficher le max des nombres de pages");
                Console.WriteLine("7. Modifier le titre et le nombre de pages d'un livre");
                Console.WriteLine("8. Trier et afficher les livres par nombre de pages");
                Console.WriteLine("0. Quitter");
                Console.Write("Votre choix : ");

                if (!int.TryParse(Console.ReadLine(), out choix))
                {
                    choix = -1;
                }

                Console.WriteLine();

                switch (choix)
                {
                    case 1: AfficherLivres(); break;
                    case 2: AjouterLivre(); break;
                    case 3: SupprimerLivre(); break;
                    case 4: RechercherParTitre(); break;
                    case 5: CompterLivres(); break;
                    case 6: AfficherMaxPages(); break;
                    case 7: ModifierLivre(); break;
                    case 8: TrierLivres(); break;
                    case 0: Console.WriteLine("Au revoir."); break;
                    default: Console.WriteLine("Choix invalide."); break;
                }

            } while (choix != 0);
        }

        static void AfficherLivres()
        {
            try
            {
                using (SqlConnection connexion = new SqlConnection(connectionString))
                {
                    connexion.Open();
                    string requete = "SELECT Id, Titre, Auteur, NombrePages FROM Livres";

                    using (SqlCommand commande = new SqlCommand(requete, connexion))
                    using (SqlDataReader lecteur = commande.ExecuteReader())
                    {
                        while (lecteur.Read())
                        {
                            Console.WriteLine("Id : {0} | Titre : {1} | Auteur : {2} | Nombre de pages : {3}",
                                lecteur["Id"], lecteur["Titre"], lecteur["Auteur"], lecteur["NombrePages"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }

        static void AjouterLivre()
        {
            Console.Write("Titre : ");
            string titre = Console.ReadLine();

            Console.Write("Auteur : ");
            string auteur = Console.ReadLine();

            Console.Write("Nombre de pages : ");
            int nombrePages;
            if (!int.TryParse(Console.ReadLine(), out nombrePages))
            {
                Console.WriteLine("Nombre de pages invalide.");
                return;
            }

            try
            {
                using (SqlConnection connexion = new SqlConnection(connectionString))
                {
                    connexion.Open();
                    string requete = "INSERT INTO Livres (Titre, Auteur, NombrePages) VALUES (@titre, @auteur, @nombrePages)";

                    using (SqlCommand commande = new SqlCommand(requete, connexion))
                    {
                        commande.Parameters.AddWithValue("@titre", titre);
                        commande.Parameters.AddWithValue("@auteur", auteur);
                        commande.Parameters.AddWithValue("@nombrePages", nombrePages);
                        commande.ExecuteNonQuery();
                        Console.WriteLine("Livre ajouté.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }

        static void SupprimerLivre()
        {
            Console.Write("Id du livre à supprimer : ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Id invalide.");
                return;
            }

            try
            {
                using (SqlConnection connexion = new SqlConnection(connectionString))
                {
                    connexion.Open();
                    string requete = "DELETE FROM Livres WHERE Id = @id";

                    using (SqlCommand commande = new SqlCommand(requete, connexion))
                    {
                        commande.Parameters.AddWithValue("@id", id);
                        int lignes = commande.ExecuteNonQuery();

                        if (lignes > 0)
                            Console.WriteLine("Livre supprimé.");
                        else
                            Console.WriteLine("Aucun livre avec cet Id.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }

        static void RechercherParTitre()
        {
            Console.Write("Titre à rechercher : ");
            string titre = Console.ReadLine();

            try
            {
                using (SqlConnection connexion = new SqlConnection(connectionString))
                {
                    connexion.Open();
                    string requete = "SELECT Id, Titre, Auteur, NombrePages FROM Livres WHERE Titre = @titre";

                    using (SqlCommand commande = new SqlCommand(requete, connexion))
                    {
                        commande.Parameters.AddWithValue("@titre", titre);

                        using (SqlDataReader lecteur = commande.ExecuteReader())
                        {
                            bool trouve = false;

                            while (lecteur.Read())
                            {
                                Console.WriteLine("Id : {0} | Titre : {1} | Auteur : {2} | Nombre de pages : {3}",
                                    lecteur["Id"], lecteur["Titre"], lecteur["Auteur"], lecteur["NombrePages"]);
                                trouve = true;
                            }

                            if (!trouve)
                                Console.WriteLine("Aucun livre trouvé.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }

        static void CompterLivres()
        {
            try
            {
                using (SqlConnection connexion = new SqlConnection(connectionString))
                {
                    connexion.Open();
                    string requete = "SELECT COUNT(*) FROM Livres";

                    using (SqlCommand commande = new SqlCommand(requete, connexion))
                    {
                        Console.WriteLine("Nombre de livres : " + commande.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }

        static void AfficherMaxPages()
        {
            try
            {
                using (SqlConnection connexion = new SqlConnection(connectionString))
                {
                    connexion.Open();
                    string requete = "SELECT MAX(NombrePages) FROM Livres";

                    using (SqlCommand commande = new SqlCommand(requete, connexion))
                    {
                        object resultat = commande.ExecuteScalar();

                        if (resultat == DBNull.Value)
                            Console.WriteLine("Aucun livre.");
                        else
                            Console.WriteLine("Max des nombres de pages : " + resultat);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }

        static void ModifierLivre()
        {
            Console.Write("Id du livre à modifier : ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Id invalide.");
                return;
            }

            Console.Write("Nouveau titre : ");
            string titre = Console.ReadLine();

            Console.Write("Nouveau nombre de pages : ");
            int nombrePages;
            if (!int.TryParse(Console.ReadLine(), out nombrePages))
            {
                Console.WriteLine("Nombre de pages invalide.");
                return;
            }

            try
            {
                using (SqlConnection connexion = new SqlConnection(connectionString))
                {
                    connexion.Open();
                    string requete = "UPDATE Livres SET Titre = @titre, NombrePages = @nombrePages WHERE Id = @id";

                    using (SqlCommand commande = new SqlCommand(requete, connexion))
                    {
                        commande.Parameters.AddWithValue("@titre", titre);
                        commande.Parameters.AddWithValue("@nombrePages", nombrePages);
                        commande.Parameters.AddWithValue("@id", id);
                        int lignes = commande.ExecuteNonQuery();

                        if (lignes > 0)
                            Console.WriteLine("Livre modifié.");
                        else
                            Console.WriteLine("Aucun livre avec cet Id.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }

        static void TrierLivres()
        {
            try
            {
                using (SqlConnection connexion = new SqlConnection(connectionString))
                {
                    connexion.Open();
                    string requete = "SELECT Id, Titre, Auteur, NombrePages FROM Livres ORDER BY NombrePages";

                    using (SqlCommand commande = new SqlCommand(requete, connexion))
                    using (SqlDataReader lecteur = commande.ExecuteReader())
                    {
                        while (lecteur.Read())
                        {
                            Console.WriteLine("Id : {0} | Titre : {1} | Auteur : {2} | Nombre de pages : {3}",
                                lecteur["Id"], lecteur["Titre"], lecteur["Auteur"], lecteur["NombrePages"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }
    }
}
