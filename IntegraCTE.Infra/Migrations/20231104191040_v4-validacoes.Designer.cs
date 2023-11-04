﻿// <auto-generated />
using System;
using IntegraCTE.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IntegraCTE.Infra.Migrations
{
    [DbContext(typeof(IntegraCTEContext))]
    [Migration("20231104191040_v4-validacoes")]
    partial class v4validacoes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("IntegraCTE.Core.Model.ArquivoModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataArquivo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataIntegracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Empresa")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<bool>("Integrado")
                        .HasColumnType("bit");

                    b.Property<bool>("Processado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("XML")
                        .IsRequired()
                        .HasColumnType("VARCHAR(MAX)");

                    b.HasKey("Id");

                    b.ToTable("Arquivos", (string)null);
                });

            modelBuilder.Entity("IntegraCTE.Core.Model.CTEModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CFOP")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("CFOPCode")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("CNPJEntidadeLegal")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("CNPJRemetente")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("ChaveAcessoCte")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("ChaveNotaFiscal")
                        .IsRequired()
                        .HasColumnType("VARCHAR(MAX)");

                    b.Property<DateTime>("DataArquivo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataEmissao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataHoraCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataIntegracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("DestinatarioBairro")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("DestinatarioCEP")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("DestinatarioCNPJCPF")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("DestinatarioCodigoMunicipio")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("DestinatarioCodigoPais")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("DestinatarioLogradouro")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("DestinatarioMunicipio")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("DestinatarioNome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("DestinatarioNro")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("DestinatarioPais")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("DestinatarioUF")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<bool>("Integrado")
                        .HasColumnType("bit");

                    b.Property<string>("ItemNumber")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("Justificativa")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<int>("LineNumber")
                        .HasColumnType("int");

                    b.Property<string>("ModeloCte")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("NotaFiscal")
                        .IsRequired()
                        .HasColumnType("VARCHAR(MAX)");

                    b.Property<string>("Notas")
                        .IsRequired()
                        .HasColumnType("VARCHAR(MAX)");

                    b.Property<string>("NumeroCte")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("OrdemCompra")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<decimal>("PurchasePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PurchasePriceQuantity")
                        .HasColumnType("int");

                    b.Property<string>("SerieCte")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("Site")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("TomadorServico")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<Guid>("TransportadoraID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UFEmitente")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("UFEnv")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("UFRemetente")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("ValorCte")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("dataAreaId")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.HasKey("Id");

                    b.HasIndex("TransportadoraID");

                    b.ToTable("CTEs", (string)null);
                });

            modelBuilder.Entity("IntegraCTE.Core.Model.TransportadoraModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CalendarioPagamento")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("CodigoExterno")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("EspecificacaoMetodoPagamento")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("MetodoPagamento")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Transportadoras", (string)null);
                });

            modelBuilder.Entity("IntegraCTE.Core.Model.ValidacaoModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdArquivo")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Mensagem")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("TipoMensagem")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.HasKey("Id");

                    b.HasIndex("IdArquivo");

                    b.ToTable("Validacao", (string)null);
                });

            modelBuilder.Entity("IntegraCTE.Core.Model.CTEModel", b =>
                {
                    b.HasOne("IntegraCTE.Core.Model.TransportadoraModel", "Transportadora")
                        .WithMany("CTEs")
                        .HasForeignKey("TransportadoraID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Transportadora");
                });

            modelBuilder.Entity("IntegraCTE.Core.Model.ValidacaoModel", b =>
                {
                    b.HasOne("IntegraCTE.Core.Model.ArquivoModel", "Arquivo")
                        .WithMany("Validacoes")
                        .HasForeignKey("IdArquivo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Arquivo");
                });

            modelBuilder.Entity("IntegraCTE.Core.Model.ArquivoModel", b =>
                {
                    b.Navigation("Validacoes");
                });

            modelBuilder.Entity("IntegraCTE.Core.Model.TransportadoraModel", b =>
                {
                    b.Navigation("CTEs");
                });
#pragma warning restore 612, 618
        }
    }
}
