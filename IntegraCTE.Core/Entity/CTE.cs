using IntegraCTE.Core.DTO;
using System.Globalization;
using System.Xml;

namespace IntegraCTE.Core.Entity
{

    public class Linha
    {
        public string ItemNumber { get; internal set; }
        public int LineNumber { get; internal set; }
        public decimal PurchasePrice { get; internal set; }
        public int PurchasePriceQuantity { get; internal set; }
        public string CFOPCode { get; internal set; }
        public string dataAreaId { get; internal set; }
    }
    public class CTE
    {
        private CultureInfo globalCulture { get; set; } = new CultureInfo("en-US");

        public Guid Id { get; set; }
        public string XML { get; set; }
        public List<Nota> Notas { get; set; }
        public Guid TransportadoraID { get; private set; }
        public Transportadora Transportadora { get; private set; }
        public Destinatario Destinatario { get; private set; }

        public DateTime DataHoraCriacao { get; private set; }

        public string Site { get; private set; }

        public string CNPJRemetente { get; private set; }

        public string CNPJEntidadeLegal { get; private set; }

        public string ValorCte { get; private set; }

        public string ModeloCte { get; private set; }

        public string SerieCte { get; private set; }

        public string NumeroCte { get; private set; }

        public string ChaveAcessoCte { get; private set; }

        public string Justificativa { get; private set; }

        public DateTime DataEmissao { get; private set; }

        public string TomadorServico { get; private set; }

        public string NotaFiscal { get; private set; }
        public string ChaveNotaFiscal { get; private set; }

        public string CFOP { get; private set; }

        public string UFEnv { get; private set; }

        public string UFEmitente { get; private set; }

        public string UFRemetente { get; private set; }

        public Linha Linha { get; private set; }

        public void PreencherLinha(string itemId, string dataAreaId)
        {
            const string cfop_MesmoEstado = "1.353";
            const string cfop_OutroEstado = "2.353";

            Linha = new()
            {
                ItemNumber = itemId,
                LineNumber = 1,
                PurchasePrice = decimal.Parse(this.ValorCte, globalCulture),
                dataAreaId = dataAreaId.ToLower(),
                PurchasePriceQuantity = 1,
                CFOPCode = UFEmitente == UFRemetente ? cfop_MesmoEstado : cfop_OutroEstado
            };
        }

        public void ProcessarXML(string dataAreaID)
        {
            Notas = new List<Nota>();
            MontarCTePorXml(XML, dataAreaID);
        }

        public void AdicionarDadosNotas(List<NotaDTO> dadnosNotas)
        {
            var notas = dadnosNotas.Select(s => new Nota() { ChaveNotaFical = s.ChaveNotaFical, NumeroNotaFical = s.NumeroNotaFical, SerieNotaFical = s.SerieNotaFical });
            Notas.AddRange(notas);
            Site = dadnosNotas.FirstOrDefault()?.Estabelecimento;
        }

        public void AdicionarDadosTransportadora(TransportadoraDTO transportadoraDTO)
        {
            if (transportadoraDTO is null || transportadoraDTO.Id == Guid.Empty || string.IsNullOrEmpty(transportadoraDTO.Cnpj) || string.IsNullOrEmpty(transportadoraDTO.Nome)) return;
            var transportadora = new Transportadora(transportadoraDTO.Id, transportadoraDTO.Cnpj, transportadoraDTO.Nome);
            this.Transportadora = transportadora;
            TransportadoraID = transportadoraDTO.Id;
        }

        private void MontarCTePorXml(string xml, string dataArea)
        {
            var destinatario = new Destinatario();
            Transportadora transportadora;
            if (!xml.Contains("cteProc"))
            {
                return;
            }


            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xml);

                XmlNodeList dadosemitente = xmlDocument.GetElementsByTagName("emit");
                XmlNodeList dadosdestinatario = xmlDocument.GetElementsByTagName("dest");
                XmlNodeList dadosremetente = xmlDocument.GetElementsByTagName("rem");
                XmlNodeList dadosvalores = xmlDocument.GetElementsByTagName("vPrest");
                XmlNodeList dadosmodeloxml = xmlDocument.GetElementsByTagName("ide");
                XmlNodeList dadosInfCte = xmlDocument.GetElementsByTagName("infCte");
                XmlNodeList dadostomador = xmlDocument.GetElementsByTagName("toma3");
                XmlNodeList dadostomador4 = xmlDocument.GetElementsByTagName("toma4");
                XmlNodeList dadosinfDoc = xmlDocument.GetElementsByTagName("infDoc");

                foreach (XmlElement emit in dadosemitente)
                {
                    var cnpjTransportadora = emit.GetElementsByTagName("CNPJ")[0].InnerText.Trim();
                    transportadora = new Transportadora(cnpjTransportadora);
                    Transportadora = transportadora;
                    foreach (var endEmit in emit.GetElementsByTagName("enderEmit"))
                    {
                        UFEmitente = emit.GetElementsByTagName("UF")[0].InnerText.Trim();
                    }
                }

                try
                {
                    foreach (XmlElement dest in dadosdestinatario)
                        destinatario.CNPJCPF = dest.GetElementsByTagName("CNPJ")[0].InnerText.Trim();
                }
                catch
                {
                    foreach (XmlElement dest in dadosdestinatario)
                        destinatario.CNPJCPF = dest.GetElementsByTagName("CPF")[0].InnerText.Trim();
                }

                foreach (XmlElement reme in dadosremetente)
                {
                    try
                    {
                        CNPJRemetente = reme.GetElementsByTagName("CNPJ")[0].InnerText.Trim();
                    }
                    catch (Exception ex)
                    {
                        if (dataArea?.ToUpper() == "CNX")
                        {
                            CNPJRemetente = reme.GetElementsByTagName("CPF")[0].InnerText.Trim();
                        }
                        else
                        {
                            throw ex;
                        }
                    }
                    foreach (XmlElement endReme in reme.GetElementsByTagName("enderReme"))
                    {
                        UFRemetente = endReme.GetElementsByTagName("UF") != null ? endReme.GetElementsByTagName("UF")[0].InnerText.Trim() : "";
                    }
                }

                foreach (XmlElement tomador in dadostomador)
                    TomadorServico = tomador.GetElementsByTagName("toma")[0].InnerText.Trim();

                if (string.IsNullOrEmpty(TomadorServico))
                {
                    foreach (XmlElement tomador4 in dadostomador4)
                    {
                        TomadorServico = tomador4.GetElementsByTagName("toma")[0].InnerText.Trim();
                        try
                        {
                            destinatario.CNPJCPF = tomador4.GetElementsByTagName("CNPJ")[0].InnerText.Trim();
                        }
                        catch (Exception ex)
                        {
                            if (dataArea?.ToUpper() == "CNX")
                            {
                                destinatario.CNPJCPF = tomador4.GetElementsByTagName("CPF")[0].InnerText.Trim();
                            }
                            else
                            {
                                throw ex;
                            }
                        }
                    }
                }

                foreach (XmlElement valor in dadosvalores)
                    ValorCte = valor.GetElementsByTagName("vRec")[0].InnerText.Trim();

                foreach (XmlElement modelo in dadosmodeloxml)
                {
                    ModeloCte = modelo.GetElementsByTagName("mod")[0].InnerText.Trim();
                    DataEmissao = DateTime.Parse(modelo.GetElementsByTagName("dhEmi")[0].InnerText.Trim());
                    SerieCte = modelo.GetElementsByTagName("serie")[0].InnerText.Trim();
                    NumeroCte = modelo.GetElementsByTagName("nCT")[0].InnerText.Trim();
                    CFOP = modelo.GetElementsByTagName("CFOP")[0].InnerText.Trim();
                }

                //Tomador do Serviço (0 - Remetente; 1 - Expedidor; 2 - Recebedor; 3- Destinatário,  4 - Outros)
                if (TomadorServico == "0")
                    CNPJEntidadeLegal = CNPJRemetente;
                else if (TomadorServico == "1")
                    CNPJEntidadeLegal = this.Transportadora.Cnpj;
                else if (TomadorServico == "3")
                    CNPJEntidadeLegal = destinatario.CNPJCPF;
                else if (TomadorServico == "4")
                    CNPJEntidadeLegal = destinatario.CNPJCPF;

                foreach (XmlElement infDoc in dadosinfDoc)
                {
                    var chavesAcessoNfe = infDoc.GetElementsByTagName("chave");
                    if (chavesAcessoNfe != null)
                    {
                        foreach (XmlElement chaveAcesso in chavesAcessoNfe)
                        {
                            NotaFiscal += $"{chaveAcesso.InnerText.Trim()}{(chavesAcessoNfe.Count == 1 ? string.Empty : " - ")}";
                            ChaveNotaFiscal += $"{chaveAcesso.InnerText.Trim()}{(chavesAcessoNfe.Count == 1 ? string.Empty : ",")}";
                        }
                        var notas = ChaveNotaFiscal.Split(",").Select(s => new Nota() { ChaveNotaFical = s });
                        Notas.AddRange(notas);
                    }
                }

                ChaveAcessoCte = dadosInfCte[0].OuterXml.Substring(dadosInfCte[0].OuterXml.IndexOf("Id=\"CTe", 0) + 7, 44);
                Justificativa = $"CTe: {NumeroCte} Valor: {ValorCte.ToString()}";

                foreach (XmlElement item in dadosInfCte)
                {
                    foreach (var endEmit in item.GetElementsByTagName("ide"))
                    {
                        UFEnv = item.GetElementsByTagName("UFEnv")[0].InnerText.Trim();
                    }
                }

                #region Endereço Destinatário
                foreach (XmlElement destinatarioXML in dadosdestinatario)
                {
                    destinatario.Nome = destinatarioXML.GetElementsByTagName("xNome")[0].InnerText.Trim();

                    foreach (XmlElement endDestinatario in destinatarioXML.GetElementsByTagName("enderDest"))
                    {
                        destinatario.Logradouro = endDestinatario.GetElementsByTagName("xLgr")[0]?.InnerText.Trim();
                        destinatario.Nro = endDestinatario.GetElementsByTagName("nro")[0]?.InnerText.Trim();
                        destinatario.Bairro = endDestinatario.GetElementsByTagName("xBairro")[0]?.InnerText.Trim();
                        destinatario.CodigoMunicipio = endDestinatario.GetElementsByTagName("cMun")[0]?.InnerText.Trim();
                        destinatario.Municipio = endDestinatario.GetElementsByTagName("xMun")[0]?.InnerText.Trim();
                        destinatario.CEP = endDestinatario.GetElementsByTagName("CEP")[0]?.InnerText.Trim();
                        destinatario.UF = endDestinatario.GetElementsByTagName("UF")[0]?.InnerText.Trim();
                        destinatario.CodigoPais = endDestinatario.GetElementsByTagName("cPais")[0]?.InnerText.Trim();
                        destinatario.Pais = endDestinatario.GetElementsByTagName("xPais")[0]?.InnerText.Trim();
                    }
                }
                Destinatario = destinatario;
                #endregion
            }
            catch (Exception ex)
            {
            }
        }
    }
}