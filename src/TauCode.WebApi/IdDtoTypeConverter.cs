using System;
using System.ComponentModel;
using System.Globalization;

namespace TauCode.WebApi
{
    internal class IdDtoTypeConverter : TypeConverter
    {
        private readonly Type _targetType;

        public IdDtoTypeConverter(Type type)
        {
            _targetType = type;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string) || sourceType == typeof(Guid))
            {
                return true;
            }
            else
            {
                return base.CanConvertFrom(context, sourceType);
            }
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string s)
            {
                if (Guid.TryParse(s, out var guid))
                {
                    value = guid;
                }
            }

            if (value is Guid)
            {
                var ctor = _targetType.GetConstructor(new[] { typeof(Guid) });
                if (ctor != null)
                {
                    var identity = ctor.Invoke(new object[] { value });
                    return identity;
                }
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
