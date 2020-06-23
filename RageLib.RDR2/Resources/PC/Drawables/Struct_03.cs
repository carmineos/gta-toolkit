﻿using RageLib.Resources.Common;
using System;
using System.Collections.Generic;

namespace RageLib.Resources.RDR2.PC.Drawables
{
	// ShaderGroup
	// VFT = 0x0000000140912C88
	public class Struct_03 : DatBase64
	{
		public override long Length => 0x40;

		// structure data
		public ulong TextureDictionaryPointer;
		public ResourcePointerList64<Struct_20> Unknown_10h;
		public ulong Unknown_20h;           // 0x0000000000000000
		public ulong Unknown_28h;           // 0x0000000000000000
		public ulong Unknown_30h;           // 0x0000000000000000
		public ulong Unknown_38h;           // 0x0000000000000000

		// reference data
		public PgDictionary64<Struct_16> TextureDictionary;

		public override void Read(ResourceDataReader reader, params object[] parameters)
		{
			base.Read(reader, parameters);

			// read structure data
			this.TextureDictionaryPointer = reader.ReadUInt64();
			this.Unknown_10h = reader.ReadBlock<ResourcePointerList64<Struct_20>>();
			this.Unknown_20h = reader.ReadUInt64();
			this.Unknown_28h = reader.ReadUInt64();
			this.Unknown_30h = reader.ReadUInt64();
			this.Unknown_38h = reader.ReadUInt64();

			// read reference data
			this.TextureDictionary = reader.ReadBlockAt<PgDictionary64<Struct_16>>(this.TextureDictionaryPointer);
		}

		public override void Write(ResourceDataWriter writer, params object[] parameters)
		{
			base.Write(writer, parameters);

			// update structure data
			this.TextureDictionaryPointer = (ulong)(this.TextureDictionary != null ? this.TextureDictionary.Position : 0);

			// write structure data
			writer.Write(this.TextureDictionaryPointer);
			writer.WriteBlock(this.Unknown_10h);
			writer.Write(this.Unknown_20h);
			writer.Write(this.Unknown_28h);
			writer.Write(this.Unknown_30h);
			writer.Write(this.Unknown_38h);
		}

		public override IResourceBlock[] GetReferences()
		{
			var list = new List<IResourceBlock>(base.GetReferences());
			if (TextureDictionary != null) list.Add(TextureDictionary);
			return list.ToArray();
		}

		public override Tuple<long, IResourceBlock>[] GetParts()
		{
			return new Tuple<long, IResourceBlock>[] {
				new Tuple<long, IResourceBlock>(0x10, Unknown_10h)
			};
		}
	}
}