﻿using Alura.ListaLeitura.Modelos;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.WebAPI.WebApp.Formatters
{
    public class LivroCsvFormatters : TextOutputFormatter
    {
        public LivroCsvFormatters()
        {
            var textCsvMediaType = MediaTypeHeaderValue.Parse("text/csv");
            var appCsvMediaType = MediaTypeHeaderValue.Parse("application/csv");
            SupportedMediaTypes.Add(textCsvMediaType);
            SupportedMediaTypes.Add(appCsvMediaType);
            SupportedEncodings.Add(Encoding.UTF8);
        }

        protected override bool CanWriteType(Type type)
        {
            return type == typeof(LivroApi);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var livroCSV = "";

            if (context.Object is LivroApi)
            {
                var livro = context.Object as LivroApi;
                livroCSV = $"{livro.Titulo};{livro.Subtitulo},{livro.Autor};{livro.Lista}";
            }
            

            using (var escritor = context.WriterFactory(context.HttpContext.Response.Body, selectedEncoding))
            {
                return escritor.WriteAsync(livroCSV);
            }//escritor.Close()

            
        }
    }
}
