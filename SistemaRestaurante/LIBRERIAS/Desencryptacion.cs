﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SistemaRestaurante.LIBRERIAS
{
    class Desencryptacion
    {

        static private AES aes = new AES();
        static public string CnString;
        static string dbcnString;
        static public string appPwdUnique = "Strix.Owl2021.SistemaRestaurante";

        public static object checkServer()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("ConnectionString.xml");
            XmlElement root = doc.DocumentElement;
            dbcnString = root.Attributes[0].Value;
            CnString = (aes.Decrypt(dbcnString, appPwdUnique, int.Parse("256")));
            return CnString;
        }

    }
}
