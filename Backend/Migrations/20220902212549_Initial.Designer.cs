﻿// <auto-generated />
using System;
using Backend.AspPlugins;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.Migrations
{
    [DbContext(typeof(AtriaContext))]
<<<<<<<< HEAD:Backend/Migrations/20220903145837_Initial.Designer.cs
    [Migration("20220903145837_Initial")]
========
    [Migration("20220902212549_Initial")]
>>>>>>>> main:Backend/Migrations/20220902212549_Initial.Designer.cs
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Models.Answer", b =>
                {
                    b.Property<long>("WseId")
                        .HasColumnType("bigint")
                        .HasColumnName("wse_id");

                    b.Property<long>("QuestionId")
                        .HasColumnType("bigint")
                        .HasColumnName("question_id");

                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTimeOffset>("CreationTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_time");

                    b.Property<long>("CreatorId")
                        .HasColumnType("bigint")
                        .HasColumnName("creator_id");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("text");

                    b.HasKey("WseId", "QuestionId", "Id")
                        .HasName("pk_answers");

                    b.HasIndex("CreatorId")
                        .HasDatabaseName("ix_answers_creator_id");

                    b.ToTable("answers", (string)null);
                });

            modelBuilder.Entity("Models.Collaborator", b =>
                {
                    b.Property<long>("WseId")
                        .HasColumnType("bigint")
                        .HasColumnName("wse_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.Property<int>("Rights")
                        .HasColumnType("integer")
                        .HasColumnName("rights");

                    b.HasKey("WseId", "UserId")
                        .HasName("pk_collaborator");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_collaborator_user_id");

                    b.ToTable("collaborator", (string)null);
                });

            modelBuilder.Entity("Models.Question", b =>
                {
                    b.Property<long>("WseId")
                        .HasColumnType("bigint")
                        .HasColumnName("wse_id");

                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTimeOffset>("CreationTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_time");

                    b.Property<long>("CreatorId")
                        .HasColumnType("bigint")
                        .HasColumnName("creator_id");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("text");

                    b.HasKey("WseId", "Id")
                        .HasName("pk_questions");

                    b.HasIndex("CreatorId")
                        .HasDatabaseName("ix_questions_creator_id");

                    b.ToTable("questions", (string)null);
                });

            modelBuilder.Entity("Models.ResetToken", b =>
                {
                    b.Property<byte[]>("Token")
                        .HasColumnType("bytea")
                        .HasColumnName("token");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Token")
                        .HasName("pk_reset_tokens");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_reset_tokens_user_id");

                    b.ToTable("reset_tokens", (string)null);
                });

            modelBuilder.Entity("Models.Review", b =>
                {
                    b.Property<long>("WseId")
                        .HasColumnType("bigint")
                        .HasColumnName("wse_id");

                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTimeOffset>("CreationTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_time");

                    b.Property<long>("CreatorId")
                        .HasColumnType("bigint")
                        .HasColumnName("creator_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("StarCount")
                        .HasColumnType("integer")
                        .HasColumnName("star_count");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("WseId", "Id")
                        .HasName("pk_reviews");

                    b.HasIndex("CreatorId")
                        .HasDatabaseName("ix_reviews_creator_id");

                    b.ToTable("reviews", (string)null);
                });

            modelBuilder.Entity("Models.Session", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("text")
                        .HasColumnName("token");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ip");

                    b.Property<string>("UserAgent")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_agent");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Token")
                        .HasName("pk_sessions");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_sessions_user_id");

                    b.ToTable("sessions", (string)null);
                });

            modelBuilder.Entity("Models.Tag", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTimeOffset>("CreationTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_time");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.HasKey("Name")
                        .HasName("pk_tags");

                    b.ToTable("tags", (string)null);
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Biography")
                        .HasColumnType("text")
                        .HasColumnName("biography");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstNames")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_names");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("password_hash");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("password_salt");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("text")
                        .HasColumnName("profile_picture");

                    b.Property<int>("Rights")
                        .HasColumnType("integer")
                        .HasColumnName("rights");

                    b.Property<string>("SignUpIp")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("sign_up_ip");

                    b.Property<string>("Title")
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_users_email");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Models.WebserviceEntry", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("ChangeLog")
                        .HasColumnType("text")
                        .HasColumnName("change_log");

                    b.Property<long>("ContactPersonId")
                        .HasColumnType("bigint")
                        .HasColumnName("contact_person_id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Documentation")
                        .HasColumnType("text")
                        .HasColumnName("documentation");

                    b.Property<string>("DocumentationLink")
                        .HasColumnType("text")
                        .HasColumnName("documentation_link");

                    b.Property<string>("FullDescription")
                        .HasColumnType("text")
                        .HasColumnName("full_description");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("link");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("short_description");

                    b.Property<long>("ViewCount")
                        .HasColumnType("bigint")
                        .HasColumnName("view_count");

                    b.HasKey("Id")
                        .HasName("pk_webservice_entries");

                    b.HasIndex("ContactPersonId")
                        .HasDatabaseName("ix_webservice_entries_contact_person_id");

                    b.ToTable("webservice_entries", (string)null);
                });

            modelBuilder.Entity("Models.WseDraft", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("ChangeLog")
                        .HasColumnType("text")
                        .HasColumnName("change_log");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<long>("CreatorId")
                        .HasColumnType("bigint")
                        .HasColumnName("creator_id");

                    b.Property<string>("Documentation")
                        .HasColumnType("text")
                        .HasColumnName("documentation");

                    b.Property<string>("DocumentationLink")
                        .HasColumnType("text")
                        .HasColumnName("documentation_link");

                    b.Property<string>("FullDescription")
                        .HasColumnType("text")
                        .HasColumnName("full_description");

                    b.Property<string>("Link")
                        .HasColumnType("text")
                        .HasColumnName("link");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("text")
                        .HasColumnName("short_description");

                    b.HasKey("Id")
                        .HasName("pk_wse_draft");

                    b.HasIndex("CreatorId")
                        .HasDatabaseName("ix_wse_draft_creator_id");

                    b.ToTable("wse_draft", (string)null);
                });

            modelBuilder.Entity("TagWebserviceEntry", b =>
                {
                    b.Property<string>("TagsName")
                        .HasColumnType("text")
                        .HasColumnName("tags_name");

                    b.Property<long>("WebserviceEntriesId")
                        .HasColumnType("bigint")
                        .HasColumnName("webservice_entries_id");

                    b.HasKey("TagsName", "WebserviceEntriesId")
                        .HasName("pk_tag_webservice_entry");

                    b.HasIndex("WebserviceEntriesId")
                        .HasDatabaseName("ix_tag_webservice_entry_webservice_entries_id");

                    b.ToTable("tag_webservice_entry", (string)null);
                });

            modelBuilder.Entity("Models.Answer", b =>
                {
                    b.HasOne("Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_answers_users_creator_id");

                    b.HasOne("Models.WebserviceEntry", "Wse")
                        .WithMany()
                        .HasForeignKey("WseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_answers_webservice_entries_wse_id");

                    b.HasOne("Models.Question", "Question")
                        .WithMany()
                        .HasForeignKey("WseId", "QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_answers_questions_wse_id_question_id");

                    b.Navigation("Creator");

                    b.Navigation("Question");

                    b.Navigation("Wse");
                });

            modelBuilder.Entity("Models.Collaborator", b =>
                {
                    b.HasOne("Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_collaborator_users_user_id");

                    b.HasOne("Models.WebserviceEntry", "Wse")
                        .WithMany("Collaborators")
                        .HasForeignKey("WseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_collaborator_webservice_entries_wse_id");

                    b.Navigation("User");

                    b.Navigation("Wse");
                });

            modelBuilder.Entity("Models.Question", b =>
                {
                    b.HasOne("Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_questions_users_creator_id");

                    b.HasOne("Models.WebserviceEntry", "Wse")
                        .WithMany("Questions")
                        .HasForeignKey("WseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_questions_webservice_entries_wse_id");

                    b.Navigation("Creator");

                    b.Navigation("Wse");
                });

            modelBuilder.Entity("Models.ResetToken", b =>
                {
                    b.HasOne("Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_reset_tokens_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Models.Review", b =>
                {
                    b.HasOne("Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_reviews_users_creator_id");

                    b.HasOne("Models.WebserviceEntry", "Wse")
                        .WithMany("Reviews")
                        .HasForeignKey("WseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_reviews_webservice_entries_wse_id");

                    b.Navigation("Creator");

                    b.Navigation("Wse");
                });

            modelBuilder.Entity("Models.Session", b =>
                {
                    b.HasOne("Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_sessions_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Models.WebserviceEntry", b =>
                {
                    b.HasOne("Models.User", "ContactPerson")
                        .WithMany("Bookmarks")
                        .HasForeignKey("ContactPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_webservice_entries_users_contact_person_id");

                    b.Navigation("ContactPerson");
                });

            modelBuilder.Entity("Models.WseDraft", b =>
                {
                    b.HasOne("Models.User", "Creator")
                        .WithMany("WseDrafts")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_wse_draft_users_creator_id");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("TagWebserviceEntry", b =>
                {
                    b.HasOne("Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tag_webservice_entry_tags_tags_name");

                    b.HasOne("Models.WebserviceEntry", null)
                        .WithMany()
                        .HasForeignKey("WebserviceEntriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tag_webservice_entry_webservice_entries_webservice_entries_");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Navigation("Bookmarks");

                    b.Navigation("WseDrafts");
                });

            modelBuilder.Entity("Models.WebserviceEntry", b =>
                {
                    b.Navigation("Collaborators");

                    b.Navigation("Questions");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
