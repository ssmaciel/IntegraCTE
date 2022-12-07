using System.Xml;

namespace IntegraCTE.Core.Entity
{
    public class CTE
    {
        public Guid Id { get; set; }
        public string XML { get; set; }
        public List<Nota> Notas { get; set; }

        public DateTime DataHoraCriacao { get; private set; }

        public string CNPJTransportadora { get; private set; }

        public string CNPJCPFDestinatario { get; private set; }

        public string CNPJRemetente { get; private set; }

        public string CNPJEntidadeLegal { get; private set; }

        public string ValorCte { get; private set; }

        public string ModeloCte { get; private set; }

        public string SerieCte { get; private set; }

        public string NumeroCte { get; private set; }

        public string ChaveAcessoCte { get; private set; }

        public string Justificativa { get; private set; }

        public DateTime DataEmissao { get; private set; }

        public string Xml { get; private set; }

        public string TomadorServico { get; private set; }

        public string NotaFiscal { get; private set; }

        public string CFOP { get; private set; }

        public string UFEnv { get; private set; }

        public string UFEmitente { get; private set; }

        public string UFRemetente { get; private set; }

        #region Endereço Destinatário
        public string DestinatarioNome { get; private set; }
        public string DestinatarioLogradouro { get; private set; }
        public string DestinatarioNro { get; private set; }
        public string DestinatarioBairro { get; private set; }
        public string DestinatarioCodigoMunicipio { get; private set; }
        public string DestinatarioMunicipio { get; private set; }
        public string DestinatarioCEP { get; private set; }
        public string DestinatarioUF { get; private set; }
        public string DestinatarioCodigoPais { get; private set; }
        public string DestinatarioPais { get; private set; }
        #endregion

        public void ProcessarXML()
        {
            MontarCTePorXml(XML, "CNX");
        }

        private void MontarCTePorXml(string xml, string dataArea)
        {
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
                    CNPJTransportadora = emit.GetElementsByTagName("CNPJ")[0].InnerText.Trim();

                    foreach (var endEmit in emit.GetElementsByTagName("enderEmit"))
                    {
                        UFEmitente = emit.GetElementsByTagName("UF")[0].InnerText.Trim();
                    }
                }

                try
                {
                    foreach (XmlElement dest in dadosdestinatario)
                        CNPJCPFDestinatario = dest.GetElementsByTagName("CNPJ")[0].InnerText.Trim();
                }
                catch
                {
                    foreach (XmlElement dest in dadosdestinatario)
                        CNPJCPFDestinatario = dest.GetElementsByTagName("CPF")[0].InnerText.Trim();
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
                            CNPJCPFDestinatario = tomador4.GetElementsByTagName("CNPJ")[0].InnerText.Trim();
                        }
                        catch (Exception ex)
                        {
                            if (dataArea?.ToUpper() == "CNX")
                            {
                                CNPJCPFDestinatario = tomador4.GetElementsByTagName("CPF")[0].InnerText.Trim();
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
                    CNPJEntidadeLegal = CNPJTransportadora;
                else if (TomadorServico == "3")
                    CNPJEntidadeLegal = CNPJCPFDestinatario;
                else if (TomadorServico == "4")
                    CNPJEntidadeLegal = CNPJCPFDestinatario;

                foreach (XmlElement infDoc in dadosinfDoc)
                {
                    var chavesAcessoNfe = infDoc.GetElementsByTagName("chave");
                    if (chavesAcessoNfe != null)
                    {
                        foreach (XmlElement chaveAcesso in chavesAcessoNfe)
                        {
                            NotaFiscal += $"{chaveAcesso.InnerText.Trim()}{(chavesAcessoNfe.Count == 1 ? string.Empty : " - ")}";
                        }
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
                foreach (XmlElement destinatario in dadosdestinatario)
                {
                    DestinatarioNome = destinatario.GetElementsByTagName("xNome")[0].InnerText.Trim();

                    foreach (XmlElement endDestinatario in destinatario.GetElementsByTagName("enderDest"))
                    {
                        DestinatarioLogradouro = endDestinatario.GetElementsByTagName("xLgr")[0]?.InnerText.Trim();
                        DestinatarioNro = endDestinatario.GetElementsByTagName("nro")[0]?.InnerText.Trim();
                        DestinatarioBairro = endDestinatario.GetElementsByTagName("xBairro")[0]?.InnerText.Trim();
                        DestinatarioCodigoMunicipio = endDestinatario.GetElementsByTagName("cMun")[0]?.InnerText.Trim();
                        DestinatarioMunicipio = endDestinatario.GetElementsByTagName("xMun")[0]?.InnerText.Trim();
                        DestinatarioCEP = endDestinatario.GetElementsByTagName("CEP")[0]?.InnerText.Trim();
                        DestinatarioUF = endDestinatario.GetElementsByTagName("UF")[0]?.InnerText.Trim();
                        DestinatarioCodigoPais = endDestinatario.GetElementsByTagName("cPais")[0]?.InnerText.Trim();
                        DestinatarioPais = endDestinatario.GetElementsByTagName("xPais")[0]?.InnerText.Trim();
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
            }
        }
    }
}