using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace VfpClient
{
    public static partial class VfpMapping
    {
		private static readonly IDictionary<VfpType, string> _vfpTypeToVfpTypeName = 
			new Dictionary<VfpType, string> {
				{ VfpType.Character, "Character" },
				{ VfpType.Currency, "Currency" },
				{ VfpType.Date, "Date" },
				{ VfpType.DateTime, "DateTime" },
				{ VfpType.Double, "Double" },
				{ VfpType.Float, "Float" },
				{ VfpType.Integer, "Integer" },
				{ VfpType.Logical, "Logical" },
				{ VfpType.Numeric, "Numeric" },
				{ VfpType.Varbinary, "Varbinary" },
				{ VfpType.Blob, "Blob" },
				{ VfpType.Varchar, "Varchar" },
				{ VfpType.Memo, "Memo" },
				{ VfpType.Variant, "Variant" },
				{ VfpType.General, "General" },
				{ VfpType.AutoIncInteger, "Integer (AutoInc)" },
				{ VfpType.BinaryCharacter, "Character (binary)" },
				{ VfpType.BinaryMemo, "Memo (binary)" },
				{ VfpType.BinaryVarchar, "Varchar (binary)" },
			};

		private static readonly IDictionary<VfpType, Type> _vfpTypeToClrType = 
			new Dictionary<VfpType, Type> {
				{ VfpType.Character, typeof(System.String) },
				{ VfpType.Currency, typeof(System.Decimal) },
				{ VfpType.Date, typeof(System.DateTime) },
				{ VfpType.DateTime, typeof(System.DateTime) },
				{ VfpType.Double, typeof(System.Double) },
				{ VfpType.Float, typeof(System.Decimal) },
				{ VfpType.Integer, typeof(System.Int32) },
				{ VfpType.Logical, typeof(System.Boolean) },
				{ VfpType.Numeric, typeof(System.Decimal) },
				{ VfpType.Varbinary, typeof(System.Byte[]) },
				{ VfpType.Blob, typeof(System.Byte[]) },
				{ VfpType.Varchar, typeof(System.String) },
				{ VfpType.Memo, typeof(System.String) },
				{ VfpType.Variant, typeof(System.Object) },
				{ VfpType.General, typeof(System.Byte[]) },
				{ VfpType.AutoIncInteger, typeof(System.Int32) },
				{ VfpType.BinaryCharacter, typeof(System.String) },
				{ VfpType.BinaryMemo, typeof(System.String) },
				{ VfpType.BinaryVarchar, typeof(System.String) },
			};

		private static readonly IDictionary<VfpType, DbType> _vfpTypeToDbType = 
			new Dictionary<VfpType, DbType> {
				{ VfpType.Character, DbType.StringFixedLength },
				{ VfpType.Currency, DbType.Currency },
				{ VfpType.Date, DbType.Date },
				{ VfpType.DateTime, DbType.DateTime },
				{ VfpType.Double, DbType.Double },
				{ VfpType.Float, DbType.Single },
				{ VfpType.Integer, DbType.Int32 },
				{ VfpType.Logical, DbType.Boolean },
				{ VfpType.Numeric, DbType.Decimal },
				{ VfpType.Varbinary, DbType.Binary },
				{ VfpType.Blob, DbType.Binary },
				{ VfpType.Varchar, DbType.String },
				{ VfpType.Memo, DbType.String },
				{ VfpType.Variant, DbType.Object },
				{ VfpType.General, DbType.Object },
				{ VfpType.AutoIncInteger, DbType.Int32 },
				{ VfpType.BinaryCharacter, DbType.StringFixedLength },
				{ VfpType.BinaryMemo, DbType.String },
				{ VfpType.BinaryVarchar, DbType.String },
			};

		private static readonly IDictionary<VfpType, OleDbType> _vfpTypeToOleDbType = 
			new Dictionary<VfpType, OleDbType> {
				{ VfpType.Character, OleDbType.Char },
				{ VfpType.Currency, OleDbType.Currency },
				{ VfpType.Date, OleDbType.Date },
				{ VfpType.DateTime, OleDbType.DBTimeStamp },
				{ VfpType.Double, OleDbType.Double },
				{ VfpType.Float, OleDbType.Single },
				{ VfpType.Integer, OleDbType.Integer },
				{ VfpType.Logical, OleDbType.Boolean },
				{ VfpType.Numeric, OleDbType.Numeric },
				{ VfpType.Varbinary, OleDbType.VarBinary },
				{ VfpType.Blob, OleDbType.LongVarBinary },
				{ VfpType.Varchar, OleDbType.VarChar },
				{ VfpType.Memo, OleDbType.LongVarChar },
				{ VfpType.Variant, OleDbType.Variant },
				{ VfpType.General, OleDbType.LongVarBinary },
				{ VfpType.AutoIncInteger, OleDbType.Integer },
				{ VfpType.BinaryCharacter, OleDbType.Char },
				{ VfpType.BinaryMemo, OleDbType.LongVarChar },
				{ VfpType.BinaryVarchar, OleDbType.VarChar },
			};

		private static readonly IDictionary<DbType, VfpType> _dbTypeToVfpType = 
			new Dictionary<DbType, VfpType> {
				{ DbType.StringFixedLength, VfpType.Character },
				{ DbType.AnsiStringFixedLength, VfpType.Character },
				{ DbType.Currency, VfpType.Currency },
				{ DbType.Date, VfpType.Date },
				{ DbType.Time, VfpType.Date },
				{ DbType.DateTime, VfpType.DateTime },
				{ DbType.Double, VfpType.Double },
				{ DbType.Single, VfpType.Float },
				{ DbType.Int32, VfpType.Integer },
				{ DbType.Byte, VfpType.Integer },
				{ DbType.Int16, VfpType.Integer },
				{ DbType.SByte, VfpType.Integer },
				{ DbType.UInt16, VfpType.Integer },
				{ DbType.Boolean, VfpType.Logical },
				{ DbType.Decimal, VfpType.Numeric },
				{ DbType.Int64, VfpType.Numeric },
				{ DbType.UInt32, VfpType.Numeric },
				{ DbType.UInt64, VfpType.Numeric },
				{ DbType.VarNumeric, VfpType.Numeric },
				{ DbType.Binary, VfpType.Varbinary },
				{ DbType.String, VfpType.Varchar },
				{ DbType.AnsiString, VfpType.Varchar },
				{ DbType.Guid, VfpType.Varchar },
				{ DbType.Xml, VfpType.Varchar },
				{ DbType.Object, VfpType.Variant },
			};

		private static readonly IDictionary<OleDbType, VfpType> _oleDbTypeToVfpType = 
			new Dictionary<OleDbType, VfpType> {
				{ OleDbType.Char, VfpType.Character },
				{ OleDbType.WChar, VfpType.Character },
				{ OleDbType.Currency, VfpType.Currency },
				{ OleDbType.Date, VfpType.Date },
				{ OleDbType.DBDate, VfpType.Date },
				{ OleDbType.DBTimeStamp, VfpType.DateTime },
				{ OleDbType.Double, VfpType.Double },
				{ OleDbType.Single, VfpType.Float },
				{ OleDbType.Integer, VfpType.Integer },
				{ OleDbType.TinyInt, VfpType.Integer },
				{ OleDbType.SmallInt, VfpType.Integer },
				{ OleDbType.UnsignedTinyInt, VfpType.Integer },
				{ OleDbType.UnsignedSmallInt, VfpType.Integer },
				{ OleDbType.Boolean, VfpType.Logical },
				{ OleDbType.Numeric, VfpType.Numeric },
				{ OleDbType.UnsignedInt, VfpType.Numeric },
				{ OleDbType.BigInt, VfpType.Numeric },
				{ OleDbType.UnsignedBigInt, VfpType.Numeric },
				{ OleDbType.VarNumeric, VfpType.Numeric },
				{ OleDbType.Decimal, VfpType.Numeric },
				{ OleDbType.VarBinary, VfpType.Varbinary },
				{ OleDbType.LongVarBinary, VfpType.Blob },
				{ OleDbType.VarChar, VfpType.Varchar },
				{ OleDbType.VarWChar, VfpType.Varchar },
				{ OleDbType.Guid, VfpType.Varchar },
				{ OleDbType.LongVarChar, VfpType.Memo },
				{ OleDbType.Variant, VfpType.Variant },
				{ OleDbType.Binary, VfpType.General },
			};

		private static readonly IDictionary<string, string> _vfpAbbrevToVfpTypeName = 
			new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) {
				{ "C", "Character" },
				{ "Y", "Currency" },
				{ "D", "Date" },
				{ "T", "DateTime" },
				{ "B", "Double" },
				{ "F", "Float" },
				{ "I", "Integer" },
				{ "L", "Logical" },
				{ "N", "Numeric" },
				{ "Q", "Varbinary" },
				{ "W", "Blob" },
				{ "V", "Varchar" },
				{ "M", "Memo" },
				{ "G", "General" },
			};

		private static readonly IDictionary<VfpType, string> _vfpTypeToFieldType = 
			new Dictionary<VfpType, string> {
				{ VfpType.Character, "C({0})" },
				{ VfpType.Currency, "Y" },
				{ VfpType.Date, "D" },
				{ VfpType.DateTime, "T" },
				{ VfpType.Double, "B({0})" },
				{ VfpType.Float, "F({0}, {1})" },
				{ VfpType.Integer, "I" },
				{ VfpType.Logical, "L" },
				{ VfpType.Numeric, "N({0}, {1})" },
				{ VfpType.Varbinary, "Q({0})" },
				{ VfpType.Blob, "W" },
				{ VfpType.Varchar, "V({0})" },
				{ VfpType.Memo, "M" },
				{ VfpType.General, "G" },
				{ VfpType.AutoIncInteger, "I AUTOINC" },
				{ VfpType.BinaryCharacter, "C({0}) NOCPTRANS" },
				{ VfpType.BinaryMemo, "M NOCPTRANS" },
				{ VfpType.BinaryVarchar, "V({0}) NOCPTRANS" },
			};

		private static readonly IDictionary<string, VfpType> _vfpAbbrevToVfpType = 
			new Dictionary<string, VfpType>(StringComparer.InvariantCultureIgnoreCase) {
				{ "C", VfpType.Character },
				{ "Y", VfpType.Currency },
				{ "D", VfpType.Date },
				{ "T", VfpType.DateTime },
				{ "B", VfpType.Double },
				{ "F", VfpType.Float },
				{ "I", VfpType.Integer },
				{ "L", VfpType.Logical },
				{ "N", VfpType.Numeric },
				{ "Q", VfpType.Varbinary },
				{ "W", VfpType.Blob },
				{ "V", VfpType.Varchar },
				{ "M", VfpType.Memo },
				{ "G", VfpType.General },
			};

		private static readonly IDictionary<string, VfpType> _vfpTypeNameToVfpType = 
			new Dictionary<string, VfpType>(StringComparer.InvariantCultureIgnoreCase) {
				{ "Character", VfpType.Character },
				{ "Currency", VfpType.Currency },
				{ "Date", VfpType.Date },
				{ "DateTime", VfpType.DateTime },
				{ "Double", VfpType.Double },
				{ "Float", VfpType.Float },
				{ "Integer", VfpType.Integer },
				{ "Logical", VfpType.Logical },
				{ "Numeric", VfpType.Numeric },
				{ "Varbinary", VfpType.Varbinary },
				{ "Blob", VfpType.Blob },
				{ "Varchar", VfpType.Varchar },
				{ "Memo", VfpType.Memo },
				{ "Variant", VfpType.Variant },
				{ "General", VfpType.General },
				{ "Integer (AutoInc)", VfpType.AutoIncInteger },
				{ "Character (binary)", VfpType.BinaryCharacter },
				{ "Memo (binary)", VfpType.BinaryMemo },
				{ "Varchar (binary)", VfpType.BinaryVarchar },
			};

		private static readonly IDictionary<string, string> _dbTypeNameToVfpTypeName = 
			new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) {
				{ "DBTYPE_CHAR", "Character" },
				{ "DBTYPE_CY", "Currency" },
				{ "DBTYPE_DBDATE", "Date" },
				{ "DBTYPE_DBTIMESTAMP", "DateTime" },
				{ "DBTYPE_R8", "Double" },
				{ "DBTYPE_I4", "Integer" },
				{ "DBTYPE_BOOL", "Logical" },
				{ "DBTYPE_NUMERIC", "Numeric" },
				{ "DBTYPE_VARBINARY", "Varchar" },
				{ "DBTYPE_LONGVARCHAR", "Memo" },
				{ "DBTYPE_BINARY", "Character (binary)" },
				{ "DBTYPE_LONGVARBINARY", "Memo (binary)" },
			};
	}
}