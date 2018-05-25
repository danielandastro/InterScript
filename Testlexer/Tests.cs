using System;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using LexerLib;

namespace Testlexer
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Assigner()
        {
            Assert.True(true);
            Console.WriteLine(LexerLib.Intersharp.Lexer("string X = 3"));

        }
        
        [Test]
        public void Retriver(){
            Console.WriteLine(LexerLib.Intersharp.Lexer("retrive string x"));
    }
    }
}