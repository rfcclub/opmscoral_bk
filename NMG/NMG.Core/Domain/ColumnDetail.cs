using System;

namespace NMG.Core.Domain
{
    [Serializable]
    public class ColumnDetail
    {
        public ColumnDetail(string columnName, string dataType, int dataLength, int dataPrecision, int dataScale, bool isNullable)
        {
            DataLength = dataLength;
            DataPrecision = dataPrecision;
            DataScale = dataScale;
            ColumnName = columnName;
            DataType = dataType;
            IsNullable = isNullable;
            MappedType = new DataTypeMapper().MapFromDBType(DataType, DataLength, DataPrecision, DataScale).Name;
        }

        public bool IsNullable { get; private set; }

        public int DataLength { get; private set; }
        public int DataPrecision { get; private set; }
        public int DataScale { get; private set; }

        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public string MappedType { get; set; }
        public bool IsPrimaryKey { get; set; }
    }
}