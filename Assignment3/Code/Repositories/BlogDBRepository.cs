using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Assignment3.Code.DataModels;

namespace Assignment3.Code.Repositories
{
    public class BlogDBRepository : IDataEntityRepository<BlogPost>
    {
        private readonly string _connectionString;
        public BlogDBRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DB_BlogPosts"];
        }
        public BlogPost Get(int id)
        {
            BlogPost blogPost = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT ID, Author, Title, Content, Timestamp FROM BlogPosts WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            blogPost = new BlogPost
                            {
                                ID = reader.GetInt32(0),
                                Author = reader.GetString(1),
                                Title = reader.GetString(2),
                                Content = reader.GetString(3),
                                Timestamp = reader.GetDateTime(4)
                            };
                        }
                    }
                }
            }

            return blogPost;
        }

        public void Save(BlogPost entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query;
                if (entity.ID == 0)
                {
                    query = "INSERT INTO BlogPosts (Author, Title, Content, Timestamp) VALUES (@Author, @Title, @Content, @Timestamp); SELECT SCOPE_IDENTITY();";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Author", entity.Author);
                        command.Parameters.AddWithValue("@Title", entity.Title);
                        command.Parameters.AddWithValue("@Content", entity.Content);
                        command.Parameters.AddWithValue("@Timestamp", entity.Timestamp);
                        entity.ID = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                else
                {
                    query = "UPDATE BlogPosts SET Author = @Author, Title = @Title, Content = @Content, Timestamp = @Timestamp WHERE ID = @ID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", entity.ID);
                        command.Parameters.AddWithValue("@Author", entity.Author);
                        command.Parameters.AddWithValue("@Title", entity.Title);
                        command.Parameters.AddWithValue("@Content", entity.Content);
                        command.Parameters.AddWithValue("@Timestamp", entity.Timestamp);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
        public List<BlogPost> GetList()
        {
            List<BlogPost> blogPosts = new List<BlogPost>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT ID, Author, Title, Content, Timestamp FROM BlogPosts";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BlogPost blogPost = new BlogPost
                            {
                                ID = reader.GetInt32(0),
                                Author = reader.GetString(1),
                                Title = reader.GetString(2),
                                Content = reader.GetString(3),
                                Timestamp = reader.GetDateTime(4)
                            };

                            blogPosts.Add(blogPost);
                        }
                    }
                }
            }

            return blogPosts;
        }
    }
}