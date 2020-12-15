using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler;
using Irony.Parsing;

namespace CMD
{
	class Program
	{
		static int Main(string[] args)
		{
			// <exe> <bo2-gsc-source> <path/in/gsc/strtable>

			if (args.Length < 2) return 1;

			Console.WriteLine(args[0]);

			GSCGrammar grammar = new GSCGrammar();
			Parser parser = new Parser(grammar);
			ScriptCompiler compiler = new ScriptCompiler(parser.Parse(File.ReadAllText(args[0])), args[0], "maps/mp/" + args[1]);

			if (!compiler.Init()) return 1;

			byte[] compiledScript = compiler.Compiled;

			Directory.CreateDirectory("gsc/");

			FileStream outfile = File.Create("gsc/" + args[0]);

			outfile.Write(compiledScript, 0, compiledScript.Length);

			return 0;
		}
	}
}
